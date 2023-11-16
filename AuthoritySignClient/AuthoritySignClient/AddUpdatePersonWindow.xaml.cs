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

            if (string.IsNullOrEmpty(model.Surname))
            {
                model.Error("Не указана фамилия");
                return;
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                model.Error("Не указано имя");
                return;
            }

            if (string.IsNullOrEmpty(model.Position))
            {
                model.Error("Не указана должность");
                return;
            }

            if (string.IsNullOrEmpty(model.Inn))
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

        private void EmchdLoadButton_Click(object sender, RoutedEventArgs e)
        {
            var model = DataContext as Models.AddUpdatePersonModel;
            if (model.SelectedCustomer == null)
            {
                model.Error("Не выбрана организация");
                return;
            }

            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "XML Files|*.xml";

            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    var fileInfo = new System.IO.FileInfo(openFileDialog.FileName);

                    if (!(fileInfo?.Name?.StartsWith("ON_EMCHD") ?? false))
                        throw new Exception("Выбран файл, не соответствующий формату по названию для машиночитаемой доверенности.");

                    var xml = new System.Xml.XmlDocument();
                    xml.Load(openFileDialog.FileName);
                    var report = Utils.Xml.DeserializeString<EMCHD_1.Доверенность>(xml.OuterXml);

                    if (report == null)
                        throw new Exception("Не удалось загрузить отчёт.");

                    var powerOfAttorney = report?.Документ?.Item as EMCHD_1.ДоверенностьДокументДовер;

                    if (powerOfAttorney == null)
                        throw new Exception("Не удалось загрузить доверенность.");

                    if (string.IsNullOrEmpty(powerOfAttorney?.СвДов?.НомДовер))
                        throw new Exception("Не указан номер доверенности.");

                    var principal = powerOfAttorney?.СвДоверит?.FirstOrDefault();

                    if (principal == null)
                        throw new Exception("Не удалось загрузить данные доверителя.");

                    if (model.SelectedCustomer.Inn != principal?.Доверит?.РосОргДовер?.СвРосОрг?.ИННЮЛ)
                        throw new Exception("ИНН юр. лица в доверенности не указан, либо не соответствует выбранной организации.");

                    var authorisedRepresentative = powerOfAttorney?.СвУпПред?.FirstOrDefault();

                    if (authorisedRepresentative == null)
                        throw new Exception("Не указаны сведения об уполномоченном представителе.");

                    if (string.IsNullOrEmpty(authorisedRepresentative?.Пред?.СведФизЛ?.ИННФЛ))
                        throw new Exception("Не указан ИНН физ. лица.");

                    if (string.IsNullOrEmpty(authorisedRepresentative?.Пред?.СведФизЛ?.СведФЛ?.ФИО?.Фамилия))
                        throw new Exception("Не указана фамилия физ. лица.");

                    var lowStrSurname =
                        authorisedRepresentative.Пред.СведФизЛ.СведФЛ.ФИО.Фамилия.Length == 1 ? string.Empty :
                        authorisedRepresentative.Пред.СведФизЛ.СведФЛ.ФИО.Фамилия.Substring(1).ToLower();

                    if (string.IsNullOrEmpty(authorisedRepresentative?.Пред?.СведФизЛ?.СведФЛ?.ФИО?.Имя))
                        throw new Exception("Не указана имя физ. лица.");

                    var lowStrName =
                        authorisedRepresentative.Пред.СведФизЛ.СведФЛ.ФИО.Имя.Length == 1 ? string.Empty :
                        authorisedRepresentative.Пред.СведФизЛ.СведФЛ.ФИО.Имя.Substring(1).ToLower();

                    if (string.IsNullOrEmpty(authorisedRepresentative?.Пред?.СведФизЛ?.СведФЛ?.ФИО?.Отчество))
                        throw new Exception("Не указана отчество физ. лица.");

                    var lowStrPatronymicSurname =
                        authorisedRepresentative.Пред.СведФизЛ.СведФЛ.ФИО.Отчество.Length == 1 ? string.Empty :
                        authorisedRepresentative.Пред.СведФизЛ.СведФЛ.ФИО.Отчество.Substring(1).ToLower();

                    model.Inn = authorisedRepresentative.Пред.СведФизЛ.ИННФЛ;
                    model.Surname = authorisedRepresentative.Пред.СведФизЛ.СведФЛ.ФИО.Фамилия.First() + lowStrSurname;
                    model.Name = authorisedRepresentative.Пред.СведФизЛ.СведФЛ.ФИО.Имя.First() + lowStrName;
                    model.PatronymicSurname = authorisedRepresentative.Пред.СведФизЛ.СведФЛ.ФИО.Отчество.First() + lowStrPatronymicSurname;
                    model.EmchdId = powerOfAttorney.СвДов.НомДовер;
                    model.EmchdBeginDate = powerOfAttorney.СвДов?.ДатаВыдДовер;
                    model.EmchdEndDate = powerOfAttorney.СвДов?.СрокДейст;
                    emchdLoadTetxEdit.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                model.Error(ex);
                model.Log("EmchdLoadButton: Ошибка.");
            }
        }
    }
}
