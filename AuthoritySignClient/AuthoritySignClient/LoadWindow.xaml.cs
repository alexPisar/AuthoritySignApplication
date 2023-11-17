using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AuthoritySignClient
{
    /// <summary>
    /// Логика взаимодействия для LoadWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window
    {
        private string _loadSuccessResourceName => "Resources.OK.png";

        public LoadWindow()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (Owner != null)
                Owner.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Owner != null)
                Owner.IsEnabled = false;
        }

        public void SetSuccessfulLoad(string text)
        {
            label1.Content = text ?? "Загрузка завершена успешно";
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var res = Utils.ResourcesUtil.GetEmbeddedResourceAsBytes(assembly, _loadSuccessResourceName);
            loadImage.Source = Utils.ByteImageUtil.ConvertBytesToBitmap(res, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
