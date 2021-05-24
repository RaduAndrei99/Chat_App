/****************************************************************************
 *                                                                          *
 *  Autor:  Cojocaru Constantin-Cosmin                                      *
 *  Grupa:  1309A                                                           *
 *  Fisier: OracleDatabaseModel.cs                                          *
 *                                                                          *
 *  Descriere: Clasa cu unit tests pentru ApplicationSettings               *
 *                                                                          *
 ****************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Model.Commons;
using System;

namespace ModelUnitsTest
{
    [TestClass]
    public class ModelApplicationSettingsUnitsTest
    {
        private IModel _oracleModel;
        private Random _random;

        private string _username;
        private string _password;

        [TestInitialize]
        public void ModelFriendRelationshipUnitsTestInit()
        {
            _oracleModel = new OracleDatabaseModel(Commons.UserId, Commons.Password, Commons.Hostname, Commons.Port, Commons.Sid, Commons.Pooling);
            _random = new Random(DateTime.Now.Millisecond);

            _username = $"TestUser{_random.Next(1000, 10000)}";
            _password = "123456789";

            _oracleModel.AddNewUser(_username, _password);
        }

        [TestMethod]
        public void AddApplicationSettingsTest()
        {
            _oracleModel.AddApplicationSettings(_username);
        }

        [TestMethod]
        public void GetWrontDateformatTest()
        {
            const string DateFormatTest = "dd/MMMM/yy";
            Assert.IsNull(DateFormat.GetDateFormat(DateFormatTest));
        }

        [TestMethod]
        public void GetWrontTimeformatTest()
        {
            const string TimeFormatTest = "HH/mm";
            Assert.IsNull(TimeFormat.GetTimeFormat(TimeFormatTest));
        }

        [TestMethod]
        public void ChangeDateFormatTest()
        {
            _oracleModel.AddApplicationSettings(_username);
            _oracleModel.SetDateFormat(_username, DateFormat.DayMonthNameYear);

            string expectedDateFormat = _oracleModel.GetDateFormat(_username).ToString();
            Assert.IsTrue(expectedDateFormat.Equals(DateFormat.DayMonthNameYear.ToString()));
        }

        [TestMethod]
        public void ChangeTimeFormatTest()
        {
            _oracleModel.AddApplicationSettings(_username);
            _oracleModel.SetTimeFormat(_username, TimeFormat.Hours24TimeFormat);

            string expectedTimeFormat = _oracleModel.GetTimeFormat(_username).ToString();
            Assert.IsTrue(expectedTimeFormat.Equals(TimeFormat.Hours24TimeFormat.ToString()));
        }

        [TestCleanup]
        public void ModelUnitTestCleanUp()
        {
            _oracleModel.DeleteUser(_username);

            _oracleModel = null;
            _random = null;
        }
    }
}
