using System.Collections.ObjectModel;
using AuthoritySignClient.Utils;

namespace AuthoritySignClient.Models.Base
{
    public abstract class ListViewModel<T> : ModelBase
    {
        public ObservableCollection<T> ItemsList { get; set; }
        public T SelectedItem { get; set; }

        public virtual RelayCommand CreateNewCommand { get; set; }
        public virtual RelayCommand DeleteCommand { get; set; }
        public virtual RelayCommand EditCommand { get; set; }
        public virtual RelayCommand RefreshCommand { get; set; }

        public virtual void CreateNew() { }
        public virtual void Delete() { }
        public virtual void Edit() { }
        public virtual void Refresh() { }
    }
}
