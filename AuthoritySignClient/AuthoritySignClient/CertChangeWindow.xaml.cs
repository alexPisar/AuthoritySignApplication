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

        public CertChangeWindow(IEnumerable<string> inns)
        {
            InitializeComponent();
            DataContext = new ListViewModel<X509Certificate2>();
            _inns = inns;
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
            (DataContext as ListViewModel<X509Certificate2>).SelectedItem = null;
            (DataContext as ListViewModel<X509Certificate2>).OnPropertyChanged("SelectedItem");
            (DataContext as ListViewModel<X509Certificate2>).OnPropertyChanged("ItemsList");
        }
    }
}
