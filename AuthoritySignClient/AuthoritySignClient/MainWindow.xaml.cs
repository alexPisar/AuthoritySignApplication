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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Ribbon;

namespace AuthoritySignClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXRibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void serverBarEditItem_EditValueChanged(object sender, RoutedEventArgs e)
        {
            if(IsLoaded)
                (DataContext as Models.Base.ListViewModel<Models.View.AuthoritySignDocumentsView>)?.Refresh();
        }

        private void TableView_RowDoubleClick(object sender, DevExpress.Xpf.Grid.RowDoubleClickEventArgs e)
        {
            (DataContext as Models.Base.ListViewModel<Models.View.AuthoritySignDocumentsView>)?.Edit();
        }
    }
}
