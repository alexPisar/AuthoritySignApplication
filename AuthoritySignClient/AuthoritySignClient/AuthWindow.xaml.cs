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
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
            DataContext = new Models.AuthModel();
            passwordBoxEdit.Text = new Utils.PasswordUtil().GetPassword() ?? passwordBoxEdit.Text;
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            new Utils.PasswordUtil().GenerateParametersForPassword();
            new Utils.PasswordUtil().SetDataBasePassword(passwordBoxEdit.Text);
            Utils.ConfigSet.Config.GetInstance().Save(Utils.ConfigSet.Config.GetInstance(), Utils.ConfigSet.Config.ConfFileName);

            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
