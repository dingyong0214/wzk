using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WZK.Entity;
using WZK.Business;
using WZK.Common;


namespace WZK.Unit
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void TestLogin()
        {
            //生成token
            var authorize = "ee7b5c9e-aabb - 4727 - 8366 - e7bafdc74322";
            string key1 = authorize.Split('-')[2];
            string key2 = authorize.Split('-')[3];
            var token = Encrypt.EncryptDES(Encrypt.MD5("515151"), key1 + key2);

        }
    }
}
