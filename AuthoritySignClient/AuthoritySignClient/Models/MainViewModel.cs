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

        public MainViewModel(DataBase.IDataBase dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;
            Refresh();
        }

        public override void Refresh()
        {
            var items = from a in _dataBaseContext.SelectAll<RefAuthoritySignDocuments>()
                        join customer in _dataBaseContext.SelectAll<RefCustomer>()
                        on a.IdCustomer equals customer.Id
                        select new View.AuthoritySignDocumentsView
                        {
                            Customer = customer,
                            AuthoritySignDocuments = a
                        };

            ItemsList = new ObservableCollection<View.AuthoritySignDocumentsView>(items);
        }
    }
}
