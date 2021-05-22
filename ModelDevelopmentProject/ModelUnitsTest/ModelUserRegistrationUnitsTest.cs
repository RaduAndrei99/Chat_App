using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Exceptions.AlreadyExistsExceptions;
using Model.Exceptions.WrongFormatExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUnitsTest
{
    [TestClass]
    public class ModelUserRegistrationUnitsTest
    {
        private IModel _oracleModel;
        private Random _random;

        private string _username1;
        private string _username2;
        private string _password;


        [TestInitialize]
        public void ModelUserRegistrationInitTest()
        {
            _oracleModel = new OracleDatabaseModel(Commons.UserId, Commons.Password, Commons.Hostname, Commons.Port, Commons.Sid, Commons.Pooling);
            _random = new Random(DateTime.Now.Millisecond);

            _username1 = $"TestUser{_random.Next(1000, 10000)}";
            _username2 = $"TestUser{_random.Next(1000, 10000)}";
            _password = "1234";

            _oracleModel.AddNewUser(_username1, _password);
            _oracleModel.AddNewUser(_username2, _password);
        }

        [TestMethod]
        public void RegisterUserTest()
        {
            const string FirstName = "Cosmin-Constantin";
            const string LastName = "Cojocaru";
            const string Email = "constantin-cosmin.cojocaru@student.tuiasi.ro";
            DateTime birthDate = new DateTime(1999, 4, 29);

            _oracleModel.RegisterUser(_username1, FirstName, LastName, Email, birthDate);
        }

        [TestMethod]
        [ExpectedException(typeof(UserRegistrationAlreadyExistsException))]
        public void RegisterTheSameUserTest()
        {
            const string FirstName = "Cosmin-Constantin";
            const string LastName = "Cojocaru";
            const string Email = "constantin-cosmin.cojocaru@student.tuiasi.ro";
            DateTime birthDate = new DateTime(1999, 4, 29);

            _oracleModel.RegisterUser(_username1, FirstName, LastName, Email, birthDate);
            _oracleModel.RegisterUser(_username1, FirstName, LastName, Email, birthDate);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongEmailFormatException))]
        public void WrongEmailFormatTest()
        {
            const string FirstName = "Cosmin-Constantin";
            const string LastName = "Cojocaru";
            const string Email = "constantin-cosmin.cojocaru|student.tuiasi.ro";
            DateTime birthDate = new DateTime(1999, 4, 29);

            _oracleModel.RegisterUser(_username1, FirstName, LastName, Email, birthDate);
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
