using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Exceptions.AlreadyExistsExceptions;
using Model.Exceptions.DoNotExistsExceptions;
using Model.Exceptions.WrongFormatExceptions;
using System;

namespace ModelUnitsTest
{
    [TestClass]
    public class ModelUsersUnitsTest
    {
        private IModel _oracleModel = null;
        private Random _random = null;

        private string _username1 = String.Empty;
        private string _username2 = String.Empty;
        private const string _password = "1234";

        [TestInitialize]
        public void ModelUnitTestInit()
        {
            _oracleModel = new OracleDatabaseModel(Commons.UserId, Commons.Password, Commons.Hostname, Commons.Port, Commons.Sid, Commons.Pooling);
            _random = new Random(DateTime.Now.Millisecond);

            _username1 = $"TestUser{_random.Next(1000, 10000)}";
            _username2 = $"TestUser{_random.Next(1000, 10000)}";
        }


        [TestMethod]
        public void AddNewUser()
        {
            _oracleModel.AddNewUser(_username1, _password);
        }


        [TestMethod]
        [ExpectedException(typeof(WrongUsernameFormatException))]
        public void WrongUserFormatTest()
        {
            _oracleModel.AddNewUser("Cosmin Cosmin", _password);
        }


        [TestMethod]
        [ExpectedException(typeof(WrongPasswordFormatException))]
        public void WrongPasswordFormatTest()
        {
            const string password = "134 67 \n9";
            _oracleModel.AddNewUser(_username1, password);
        }

        [TestMethod]
        [ExpectedException(typeof(UserAlreadyExistsException))]
        public void AddTheSameUserTest()
        {
            const string password = "134679";
            _oracleModel.AddNewUser(_username1, password);
            _oracleModel.AddNewUser(_username1, password);
        }


        [TestCleanup]
        public void ModelUnitTestCleanUp()
        {
            try
            {
                _oracleModel.DeleteUser(_username1);
                _oracleModel.DeleteUser(_username2);
            }
            catch(UserDoNotExistsException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            _oracleModel = null;
            _random = null;
        }
    }
}
