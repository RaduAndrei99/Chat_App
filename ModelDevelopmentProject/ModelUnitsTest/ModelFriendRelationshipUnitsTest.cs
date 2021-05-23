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
        private IModel _oracleModel;
        private Random _random;

        private string _username1;
        private string _username2;
        private string _username3;
        private string _username4;
        private string _password;

        [TestInitialize]
        public void ModelFriendRelationshipUnitsTestInit()
        {
            _oracleModel = new OracleDatabaseModel(Commons.UserId, Commons.Password, Commons.Hostname, Commons.Port, Commons.Sid, Commons.Pooling);
            _random = new Random(DateTime.Now.Millisecond);

            _username1 = $"TestUser{_random.Next(1000, 10000)}";
            _username2 = $"TestUser{_random.Next(1000, 10000)}";
            _username3 = $"TestUser{_random.Next(1000, 10000)}";
            _username4 = $"TestUser{_random.Next(1000, 10000)}";
            _password = "1234";

                _oracleModel.AddNewUser(_username1, _password);
                _oracleModel.AddNewUser(_username2, _password);
                _oracleModel.AddNewUser(_username3, _password);
                _oracleModel.AddNewUser(_username4, _password);
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

        [TestMethod]
        public void AcceptFriendshipStatus()
        {
            _oracleModel.RegisterFriendRequest(_username1, _username2);
            _oracleModel.AcceptFriendRequest(_username1, _username2);
            List<string> friendList = _oracleModel.GetFriendList(_username1);

            Assert.IsTrue(friendList[0].Equals(_username2));
        }

        [TestMethod]
        public void GetEmptyFriendList()
        {
            _oracleModel.RegisterFriendRequest(_username1, _username2);
            _oracleModel.AcceptFriendRequest(_username1, _username2);
            
            List<string> friendList = _oracleModel.GetFriendList(_username3);
            Assert.AreEqual(friendList.Count, 0);
        }

        [TestMethod]
        public void GetFriendshipRequests()
        {
            _oracleModel.RegisterFriendRequest(_username1, _username2);
            List<string> requestsList = _oracleModel.GetReceivedPendingRequest(_username2);

            Assert.IsTrue(requestsList[0].Equals(_username1));
        }

        [TestCleanup]
        public void ModelUnitTestCleanUp()
        {
            _oracleModel.DeleteUser(_username1);
            _oracleModel.DeleteUser(_username2);
            _oracleModel.DeleteUser(_username3);
            _oracleModel.DeleteUser(_username4);

            _oracleModel = null;
            _random = null;
        }
    }
}
