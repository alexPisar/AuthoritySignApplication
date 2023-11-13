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
    }
}
