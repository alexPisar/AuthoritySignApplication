using System;
using System.Linq;
using System.Collections.Generic;
using AuthoritySignClient.DataBase;
using AuthoritySignClient.DataBase.DataBaseObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthoritySignClientTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DataBaseConnectTest()
        {
            using (var dataBaseWrapper = new DataBaseFactory().Create())
            {
                var customer = dataBaseWrapper.Select<RefCustomer>(s => s.Id == 14195061).FirstOrDefault();
            }
        }
    }
}
