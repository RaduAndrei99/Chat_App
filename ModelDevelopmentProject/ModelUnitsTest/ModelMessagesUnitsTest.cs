using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUnitsTest
{
    [TestClass]
    public class ModelMessagesUnitsTest
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
            _password = "1234";

            _oracleModel.AddNewUser(_username1, _password);
            _oracleModel.AddNewUser(_username2, _password);

            _oracleModel.CreateConversation(_username1, _username2);
        }

        [TestMethod]
        public void StoreMessageTest()
        {
            const string Message = "Hello World!";
            const string MessageFormat = "txt";

            _oracleModel.StoreMessage(_username1, _username2, MessageFormat, Encoding.ASCII.GetBytes(Message), DateTime.UtcNow);

            List<MessageDTO> readMessages;
            long lastMessageId;

            _oracleModel.GetLastNMessagesFromConversation(_username1, _username2, -1, 1, out readMessages, out lastMessageId);
            Assert.IsTrue(Message.Equals(Encoding.ASCII.GetString(readMessages[0].MessageData)));
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
