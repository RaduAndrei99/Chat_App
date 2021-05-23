using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Exceptions.AlreadyExistsExceptions;
using Model.Exceptions.DoNotExistsExceptions;
using Model.Exceptions.WrongFormatExceptions;
using System;

namespace ModelUnitsTest
{
    [TestClass]
    public class ModelConversationsUnitsTest
    {
        private IModel _oracleModel;
        private Random _random;

        private string _username1;
        private string _username2;
        private string _password;

        [TestInitialize]
        public void ModelConversationUnitTestInit()
        {
            _oracleModel = new OracleDatabaseModel(Commons.UserId, Commons.Password, Commons.Hostname, Commons.Port, Commons.Sid, Commons.Pooling);
            _random = new Random(DateTime.Now.Millisecond);

            _username1 = $"TestUser{_random.Next(1000, 10000)}";
            _username2 = $"TestUser{_random.Next(1000, 10000)}";
            _password = "123456789";

            _oracleModel.AddNewUser(_username1, _password);
            _oracleModel.AddNewUser(_username2, _password);
        }

        [TestMethod]
        public void CreateConversation()
        {
            _oracleModel.CreateConversation(_username1, _username2);
        }

        [TestMethod]
        [ExpectedException(typeof(ConversationAlreadyExistsException))]
        public void AddTheSameConversationTest()
        {
            _oracleModel.CreateConversation(_username1, _username2);
            _oracleModel.CreateConversation(_username1, _username2);
        }

        [TestMethod]
        public void DeleteConversationTest()
        {
            _oracleModel.CreateConversation(_username1, _username2);
            _oracleModel.DeleteConversation(_username1, _username2);
        }

        [TestCleanup]
        public void ModelUnitTestCleanUp()
        {
            _oracleModel.DeleteUser(_username1);
            _oracleModel.DeleteUser(_username2);

            _oracleModel = null;
            _random = null;
        }
    }
}
