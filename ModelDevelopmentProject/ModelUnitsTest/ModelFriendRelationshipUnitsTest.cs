using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Exceptions.AlreadyExistsExceptions;
using Model.Exceptions.DoNotExistsExceptions;
using Model.Exceptions.WrongFormatExceptions;

namespace ModelUnitsTest
{
    [TestClass]
    public class ModelFriendRelationshipUnitsTest
    {
        private IModel _oracleModel = null;
        private Random _random = null;

        private string _username1 = String.Empty;
        private string _username2 = String.Empty;
        private const string _password = "1234";

        [TestInitialize]
        public void ModelFriendRelationshipUnitsTestInit()
        {
            _oracleModel = new OracleDatabaseModel(Commons.UserId, Commons.Password, Commons.Hostname, Commons.Port, Commons.Sid, Commons.Pooling);
            _random = new Random(DateTime.Now.Millisecond);

            _username1 = $"TestUser{_random.Next(1000, 10000)}";
            _username2 = $"TestUser{_random.Next(1000, 10000)}";

            _oracleModel.AddNewUser(_username1, _password);
            _oracleModel.AddNewUser(_username2, _password);
        }


        [TestMethod]
        public void SendFriendRequestTest()
        {
            _oracleModel.RegisterFriendRequest(_username1, _username2);
        }


        [TestMethod]
        [ExpectedException(typeof(FriendRelationshipAlreadyExistsException))]
        public void SendTheSameFriendRequestTest()
        {
            _oracleModel.RegisterFriendRequest(_username1, _username2);
            _oracleModel.RegisterFriendRequest(_username1, _username2);
        }


        [TestMethod]
        public void DeleteFriendRelationshipTest()
        {
            _oracleModel.RegisterFriendRequest(_username1, _username2);
            _oracleModel.DeleteFriendRelationship(_username1, _username2);
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
