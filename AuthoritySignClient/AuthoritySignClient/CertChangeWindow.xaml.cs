using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AuthoritySignClient.Models.Base;

namespace AuthoritySignClient
{
    /// <summary>
    /// Логика взаимодействия для CertChangeWindow.xaml
    /// </summary>
    public partial class CertChangeWindow : Window
    {
        private IEnumerable<string> _inns;
        private Utils.CertUtil _certUtil;

        public CertChangeWindow(IEnumerable<string> inns)
        {
            InitializeComponent();
            _inns = inns;

            var client = new System.Net.WebClient();

            if (Utils.ConfigSet.Config.GetInstance().ProxyEnabled)
            {
                var webProxy = new System.Net.WebProxy();

                webProxy.Address = new Uri("http://" + Utils.ConfigSet.Config.GetInstance().ProxyAddress);
                webProxy.Credentials = new System.Net.NetworkCredential(Utils.ConfigSet.Config.GetInstance().ProxyUserName,
                    Utils.ConfigSet.Config.GetInstance().ProxyUserPassword);

                client.Proxy = webProxy;
            }
            _certUtil = new Utils.CertUtil(client);
            DataContext = new ListViewModel<X509Certificate2>();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as ListViewModel<X509Certificate2>)?.SelectedItem == null)
            {
                (DataContext as ListViewModel<X509Certificate2>)?.Error("Не выбран сертификат");
                return;
            }

            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "XML Files|*.xml";

            try
            {
                if(openFileDialog.ShowDialog() == true)
                {
                    var contentBytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                    var signature = _certUtil.Sign((DataContext as ListViewModel<X509Certificate2>).SelectedItem, contentBytes, true);
                    System.IO.File.WriteAllBytes($"{openFileDialog.FileName}.sig", signature);

                    var loadWindow = new LoadWindow();
                    loadWindow.Owner = this;
                    loadWindow.SetSuccessfulLoad("Файл SIG успешно сохранён.");
                    loadWindow.Show();
                }
            }
            catch(Exception ex)
            {
                (DataContext as ListViewModel<X509Certificate2>).Error(ex);
                (DataContext as ListViewModel<X509Certificate2>).Log("SignButton: Ошибка.");
            }
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var personalCertificates = _certUtil.GetAllGostPersonalCertificates();
            var certs = personalCertificates.Where(c => _inns.Any(i => i == _certUtil.GetOrgInnFromCertificate(c)) && _certUtil.IsCertificateValid(c) && c.NotAfter > DateTime.Now).OrderByDescending(c => c.NotBefore);

            (DataContext as ListViewModel<X509Certificate2>).ItemsList = new System.Collections.ObjectModel.ObservableCollection<X509Certificate2>(certs);
            (DataContext as ListViewModel<X509Certificate2>).SelectedItem = null;

            (DataContext as ListViewModel<X509Certificate2>).OnPropertyChanged("SelectedItem");
            (DataContext as ListViewModel<X509Certificate2>).OnPropertyChanged("ItemsList");
        }

    }
}
