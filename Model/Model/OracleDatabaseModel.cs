using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DatabaseConnection;
using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;

namespace Model
{
    public class OracleDatabaseModel : IModel
    {
        IDatabaseConnection databaseConnection;
        public OracleDatabaseModel()
        {
            databaseConnection = new OracleDatabaseConnection();
        }

        public bool CheckIfUserExists(string username)
        {
            /*            if (Regex.IsMatch(username, "0"))
                            throw new Exception("Wrong username format");*/

            databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT COUNT(*) FROM users WHERE user_name = '{username}'";
                using (OracleCommand query = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read() && oracleDataReader.GetInt32(0) == 1)
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                databaseConnection.CloseConnection();
            }

            return false;
        }

        public void AddNewUser(string username, string password)
        {

            if (Regex.IsMatch(username, "0"))
                throw new Exception("Wrong username format");

            if (Regex.IsMatch(password, "0"))
                throw new Exception("Wrong password format");

            databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Users VALUES(12, '{username}', '{password}', 'F', (SELECT CURRENT_DATE FROM dual))";

                using (OracleCommand oracleCommand = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    // oracleCommand.Parameters.Add("username", username);
                    // oracleCommand.Parameters.Add("password", password);
                    oracleCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                databaseConnection.CloseConnection();
            }
        }

        public void ChangeNickname(string fromUsername, string toUsername)
        {
            throw new NotImplementedException();
        }

        public void CreateConversation(string username1, string username2)
        {
            throw new NotImplementedException();
        }

        public void RegisterFriendRequest(string username1, string username2)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(string username, string firstname, string lastname, string email, string birthdate)
        {
            throw new NotImplementedException();
        }

        public void StoreMessage(string senderUsername, string receiverUsername, string format, byte[] message_data, string sentDate)
        {
            throw new NotImplementedException();
        }
    }
}
