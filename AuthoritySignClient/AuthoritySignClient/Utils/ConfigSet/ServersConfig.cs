using System.Collections.Generic;
using AuthoritySignClient.Utils.Configuration;
using System.Linq;

namespace AuthoritySignClient.Utils.ConfigSet
{
    public class ServersConfig :  Configuration<List<Server>>
    {
        public const string ConfFileName = "servers";
        private static volatile ServersConfig _instance;
        private static readonly object syncRoot = new object();

        private ServersConfig()
        {
            Servers = Load(ConfFileName);
            SelectedServer = Servers.FirstOrDefault(s => s.IP == Config.GetInstance().DefaultDataBaseIpAddress);
        }

        public static ServersConfig GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new ServersConfig();
                    }
                }
            }

            return _instance;
        }

        public List<Server> Servers { get; set; }
        public Server SelectedServer { get; set; }

        public string GetConnectionString()
        {
            if (SelectedServer == null)
                throw new System.Exception("Не выбран сервер");

            return $"User Id={Config.GetInstance().DataBaseUser};Password={new PasswordUtil().GetPassword()};Data Source={SelectedServer.IP}/{SelectedServer.SID}";
        }

        public string GetConnectionStringByServer(Server server)
        {
            if (SelectedServer == null)
                throw new System.Exception("Не инициализирован экземпляр сервера");

            return $"User Id={Config.GetInstance().DataBaseUser};Password={new PasswordUtil().GetPassword()};Data Source={server.IP}/{server.SID}";
        }

        public List<string> GetAllConnectionStrings()
        {
            var connStrings = new List<string>();

            foreach (var s in Servers)
            {
                connStrings.Add(GetConnectionStringByServer(s));
            }

            return connStrings;
        }
    }
}
