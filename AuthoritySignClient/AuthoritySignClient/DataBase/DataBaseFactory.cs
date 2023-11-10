using AuthoritySignClient.DataBase.Implementations;

namespace AuthoritySignClient.DataBase
{
    public class DataBaseFactory
    {
        public IDataBase Create()
        {
            return new OraDataBaseWrapper();
        }
    }
}
