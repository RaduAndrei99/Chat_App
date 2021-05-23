using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Exceptions.AlreadyExistsExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUnitsTest
{
    [TestClass]
    public class ModelRelationshipSettingsUnitsTest
    {
        private IModel _oracleModel;
        private Random _random;

        private string _username1;
        private string _username2;
        private string _password;

        [TestInitialize]
        public void ModelFriendRelationshipUnitsTestInit()
        {
            _oracleModel = new OracleDatabaseModel(Commons.UserId, Commons.Password, Commons.Hostname, Commons.Port, Commons.Sid, Commons.Pooling);
            _random = new Random(DateTime.Now.Millisecond);

            _username1 = $"TestUser{_random.Next(1000, 10000)}";
            _username2 = $"TestUser{_random.Next(1000, 10000)}";
            _password = "123456789";

            _oracleModel.AddNewUser(_username1, _password);
            _oracleModel.AddNewUser(_username2, _password);

            _oracleModel.RegisterFriendRequest(_username1, _username2);
        }

        [TestMethod]
        public void AddRelationshipSettingsTest()
        {
            _oracleModel.AddRelationshipSettings(_username1, _username2);
        }

        [TestMethod]
        [ExpectedException(typeof(RelationshipSettingsAlreadyExistsException))]
        public void AddTheSameRelationshipSettingsTest()
        {
            _oracleModel.AddRelationshipSettings(_username1, _username2);
            _oracleModel.AddRelationshipSettings(_username1, _username2);
        }

        [TestMethod]
        public void ChangeNicknameTest()
        {
            const string nickname = "Cosmin123";

            _oracleModel.AddRelationshipSettings(_username1, _username2);
            _oracleModel.ChangeNickname(_username1, _username2, nickname);
            
            string resultNickname = _oracleModel.GetNicknameFromFriendRelationship(_username1, _username2);
            Assert.AreEqual(nickname, resultNickname);
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
