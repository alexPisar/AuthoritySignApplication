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
            try
            {
                new Utils.PasswordUtil().GenerateParametersForPassword();
                new Utils.PasswordUtil().SetDataBasePassword(passwordBoxEdit.Text);

                var dataBaseContext = new DataBase.DataBaseFactory().Create();
                dataBaseContext.OpenConnection();

                Utils.ConfigSet.Config.GetInstance().Save(Utils.ConfigSet.Config.GetInstance(), Utils.ConfigSet.Config.ConfFileName);

                var mainWindow = new MainWindow();
                var mainModel = new Models.MainViewModel();
                mainWindow.DataContext = mainModel;

                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                (DataContext as Models.Base.ModelBase)?.Error(ex);
                (DataContext as Models.Base.ModelBase)?.Log("EnterButton: вход выполнен с ошибкой.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
