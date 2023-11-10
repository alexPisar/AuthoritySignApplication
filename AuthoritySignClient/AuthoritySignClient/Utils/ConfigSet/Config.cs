using System;
using AuthoritySignClient.Utils.Configuration;

namespace AuthoritySignClient.Utils.ConfigSet
{
    public class Config : Configuration<Config>
    {
        public const string ConfFileName = "config.json";

        public bool IsNeedUpdate { get; set; }
        public bool SaveWindowSettings { get; set; }

        public string DefaultDataBaseIpAddress { get; set; }
        public string DefaultDataBaseSid { get; set; }

        public string DataBaseUser { get; set; }
        public string CipherDataBasePassword { get; set; }

        public bool ProxyEnabled { get; set; }
        public string ProxyAddress { get; set; }
        public string ProxyUserName { get; set; }
        public string ProxyUserPassword { get; set; }

        public int? PositionIndex { get; set; }
        public int? ShiftIndex { get; set; }

        [NonSerialized]
        private static volatile Config _instance;

        [NonSerialized]
        private static readonly object syncRoot = new object();

        private Config() { }
        public static Config GetInstance()
        {
            // тут страндартный дабл-чек с блокировкой
            // для создания инстанса синглтона безопасно для многопоточности
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        // а тут паттерн пошёл по пизде.
                        // зато коротко и красиво
                        _instance = new Config().Load(ConfFileName);
                    }
                }
            }

            return _instance;
        }
    }
}
