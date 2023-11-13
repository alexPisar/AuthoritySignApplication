using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AuthoritySignClient.Utils;
using AuthoritySignClient.DataBase.DataBaseObjects;

namespace AuthoritySignClient.Models
{
    public class MainViewModel : Base.ListViewModel<View.AuthoritySignDocumentsView>
    {
        private DataBase.IDataBase _dataBaseContext;

        public override RelayCommand RefreshCommand => new RelayCommand(o => Refresh());
        public override RelayCommand CreateNewCommand => new RelayCommand(o => CreateNew());
        public override RelayCommand EditCommand => new RelayCommand(o => Edit());
        public override RelayCommand DeleteCommand => new RelayCommand(o => Delete());

        public List<Utils.ConfigSet.Server> Servers => Utils.ConfigSet.ServersConfig.GetInstance()?.Servers;
        public Utils.ConfigSet.Server SelectedServer
        {
            get {
                return Utils.ConfigSet.ServersConfig.GetInstance().SelectedServer;
            }

            set {
                Utils.ConfigSet.ServersConfig.GetInstance().SelectedServer = value;
                OnPropertyChanged("SelectedServer");
            }
        }

        public override void Refresh()
        {
            try
            {
                _dataBaseContext = new DataBase.DataBaseFactory().Create();

                var items = from a in _dataBaseContext.SelectAll<RefAuthoritySignDocuments>()
                            join customer in _dataBaseContext.SelectAll<RefCustomer>()
                            on a.IdCustomer equals customer.Id
                            select new View.AuthoritySignDocumentsView
                            {
                                Customer = customer,
                                AuthoritySignDocuments = a
                            };

                ItemsList = new ObservableCollection<View.AuthoritySignDocumentsView>(items);
                SelectedItem = null;

                OnPropertyChanged("SelectedItem");
                OnPropertyChanged("ItemsList");
            }
            catch(Exception ex)
            {
                Error(ex);
                Log("Refresh: произошла ошибка обновления.");
            }
        }

        public override void Delete()
        {
            if(SelectedItem == null)
            {
                _log.UIShowError("Не выбран уполномоченный представитель.");
                return;
            }

            if (DevExpress.Xpf.Core.DXMessageBox.Show(
                    "Вы действительно хотите удалить уполномоченное лицо из списка?", "Удаление", 
                    System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != System.Windows.MessageBoxResult.Yes)
                return;

            try
            {
                _dataBaseContext.Delete(SelectedItem.AuthoritySignDocuments);
                _dataBaseContext.Commit();
                Refresh();
            }
            catch(Exception ex)
            {
                _dataBaseContext.Rollback();
                Error(ex);
                Log("Delete: произошла ошибка удаления.");
            }
        }
    }
}
