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
        private IModel _oracleModel;
        private Random _random;

        private string _username;
        private string _password;

        [TestInitialize]
        public void ModelUnitTestInit()
        {
            _oracleModel = new OracleDatabaseModel(Commons.UserId, Commons.Password, Commons.Hostname, Commons.Port, Commons.Sid, Commons.Pooling);
            _random = new Random(DateTime.Now.Millisecond);

            _username = $"TestUser{_random.Next(1000, 10000)}";
            _password = "123456789";
        }

        [TestMethod]
        public void AddNewUser()
        {
            _oracleModel.AddNewUser(_username, _password);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongUsernameFormatException))]
        public void WrongUserFormatTest()
        {
            _oracleModel.AddNewUser("Cosmin Cosmin", _password);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongHashedPasswordFormatException))]
        public void WrongPasswordFormatTest()
        {
            const string password = "134 67 \n9";
            _oracleModel.AddNewUser(_username, password);
        }

        [TestMethod]
        [ExpectedException(typeof(UserAlreadyExistsException))]
        public void AddTheSameUserTest()
        {
            _oracleModel.AddNewUser(_username, _password);
            _oracleModel.AddNewUser(_username, _password);
        }


        [TestCleanup]
        public void ModelUnitTestCleanUp()
        {
            try
            {
                _oracleModel.DeleteUser(_username);
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
