using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthoritySignClient.Models
{
    public class AuthModel : Base.ModelBase
    {
        private Utils.ConfigSet.Config _config = Utils.ConfigSet.Config.GetInstance();

        public string Login
        {
            get {
                return _config.DataBaseUser;
            }

            set {
                _config.DataBaseUser = value;
                OnPropertyChanged("Login");
            }
        }

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
    }
}
