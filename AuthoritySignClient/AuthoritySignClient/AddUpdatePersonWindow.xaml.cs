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
    /// Логика взаимодействия для AddUpdatePersonWindow.xaml
    /// </summary>
    public partial class AddUpdatePersonWindow : Window
    {
        public AddUpdatePersonWindow(Models.AddUpdatePersonModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as Models.AddUpdatePersonModel;

            if(model.SelectedCustomer == null)
            {
                model.Error("Не выбрана организация");
                return;
            }

            if (model.IsCreate && model.AuthorizedPersons.Any(a => a.Customer.Id == model.SelectedCustomer.Id))
            {
                model.Error("Для данной организации уже были добавлены данные подписанта");
                return;
            }

            if (model.Surname == null)
            {
                model.Error("Не указана фамилия");
                return;
            }

            if (model.Name == null)
            {
                model.Error("Не указано имя");
                return;
            }

            if (model.Position == null)
            {
                model.Error("Не указана должность");
                return;
            }

            if (model.Inn == null)
            {
                model.Error("Не указан ИНН подписанта");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
