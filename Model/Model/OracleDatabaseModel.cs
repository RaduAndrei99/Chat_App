using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DatabaseConnection;
using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;
using Model.Commons;

namespace Model
{
    public class OracleDatabaseModel : IModel
    {
        IDatabaseConnection databaseConnection;
        public OracleDatabaseModel()
        {
            databaseConnection = new OracleDatabaseConnection();
        }

        #region USER TABLE PERSISTENCY

        public void AddNewUser(string username, string password)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username}.");

            if (GetUsernameId(username) != -1)
                throw new Exception($"Username {username} already exists.");

            databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Users(user_name, user_password, is_active) VALUES('{username}', '{password}', 'F')";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, databaseConnection.Connection))
                {
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

        private int GetUsernameId(string username)
        {
            databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT user_id FROM Users WHERE user_name = '{username}'";
                using (OracleCommand query = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read())
                            return oracleDataReader.GetInt32(0);
                    }
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

            return -1;
        }

        #endregion

        #region USER_INFORMATIONS TABLE PERSISTENCY

        public void RegisterUser(string username, string firstname, string lastname, string email, DateTime birthdate)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username}.");

            if (!Regex.IsMatch(email, Constraints.EmailRegex))
                throw new Exception($"Wrong email format for {email}.");

            int usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new Exception($"Username {username} do not exists.");

            if (CheckIfUserRegistrationExists(usernameId))
                throw new Exception($"Username {username} registration already exists.");

            databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO User_informations VALUES('{firstname}', '{lastname}', '{email}', TO_DATE('{birthdate.ToString("dd/MM/yyyy")}', 'DD/MM/YYYY'), {usernameId})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    oracleCommand.ExecuteNonQuery();
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

        }

        private bool CheckIfUserRegistrationExists(int usernameId)
        {
            databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT COUNT(*) FROM User_informations WHERE (Users_user_id = {usernameId})";
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

        #endregion

        #region APPLICAITON SETTINGS

        public void AddApplicationSettings(string username)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username}.");

            int usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new Exception($"Username {username} do not exists.");

            databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Application_settings(Users_user_id) VALUES({usernameId})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    oracleCommand.ExecuteNonQuery();
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
        }

        #endregion

        #region CONVERSATION TABLE PERSISTENCY

        public void CreateConversation(string username1, string username2)
        {
            if (!Regex.IsMatch(username1, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username1}.");

            if (!Regex.IsMatch(username2, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username2}.");

            int usernameId1 = GetUsernameId(username1);
            if (usernameId1 == -1)
                throw new Exception($"Username {username1} do not exists.");

            int usernameId2 = GetUsernameId(username2);
            if (usernameId2 == -1)
                throw new Exception($"Username {username2} do not exists.");

            if (GetConversationId(usernameId1, usernameId2) != -1)
                throw new Exception($"Conversation already exists.");

            databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Conversations(Users_user_id, Users_user_id2) VALUES({usernameId1}, {usernameId2})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    oracleCommand.ExecuteNonQuery();
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
        }

        private int GetConversationId(int usernameId1, int usernameId2)
        {
            databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT COUNT(*) FROM Friend_relationships WHERE (Users_user_id = {usernameId1} and Users_user_id2 = {usernameId2}) or (Users_user_id = {usernameId2} and Users_user_id2 = {usernameId1})";
                using (OracleCommand query = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read())
                            return oracleDataReader.GetInt32(0);
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

            return -1;
        }

        #endregion

        #region FRIEND RELATIONSHIP TABLE PERSISTENCY

        public void RegisterFriendRequest(string fromUsername, string toUsername)
        {
            if (!Regex.IsMatch(fromUsername, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {fromUsername}.");

            if (!Regex.IsMatch(toUsername, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {toUsername}.");

            int fromUsernameId = GetUsernameId(fromUsername);
            if (fromUsernameId == -1)
                throw new Exception($"Username {fromUsername} do not exists.");

            int toUsernameId = GetUsernameId(toUsername);
            if (toUsernameId == -1)
                throw new Exception($"Username {toUsername} do not exists.");

            if (GetFriendRelationshipId(fromUsernameId, toUsernameId) != -1)
                throw new Exception($"Friend relationship already exists.");

            databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Friend_relationships(Users_user_id, users_user_id2, status) VALUES({fromUsernameId}, {toUsernameId})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    oracleCommand.ExecuteNonQuery();
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
        }

        private int GetFriendRelationshipId(int usernameId1, int usernameId2)
        {
            databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT relationship_id FROM Friend_relationships WHERE (Users_user_id = {usernameId1} and Users_user_id2 = {usernameId2}) or (Users_user_id = {usernameId2} and Users_user_id2 = {usernameId1})";
                using (OracleCommand query = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read())
                            return oracleDataReader.GetInt32(0);
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

            return -1;
        }

        private int GetOrderInFriendRelationship(int usernameId, int relationshipId)
        {
            databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT COUNT(*) FROM Friend_relationships WHERE (Users_user_id = {usernameId} and relationship_id = {relationshipId})";
                using (OracleCommand query = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read() && oracleDataReader.GetInt32(0) == 1)
                            return 1;
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

            return 2;
        }

        #endregion

        #region RELATIONSHIP SETTINGS TABLE PERSISTENCY

        public void AddRelationshipSettings(string username1, string username2)
        {
            if (!Regex.IsMatch(username1, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username1}.");

            if (!Regex.IsMatch(username2, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username2}.");

            int usernameId1 = GetUsernameId(username1);
            if (usernameId1 == -1)
                throw new Exception($"Username {username1} do not exists.");

            int usernameId2 = GetUsernameId(username2);
            if (usernameId2 == -1)
                throw new Exception($"Username {username2} do not exists.");

            int friendRelationshipId = GetFriendRelationshipId(usernameId1, usernameId2);
            if (GetFriendRelationshipId(usernameId1, usernameId2) == -1)
                throw new Exception($"Friend relationship do not exists.");

            databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Relationship_settings(Friend_relationships_id) VALUES({friendRelationshipId})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    oracleCommand.ExecuteNonQuery();
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
        }

        public void ChangeNickname(string fromUsername, string toUsername, string nickname)
        {
            if (!Regex.IsMatch(fromUsername, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {fromUsername}.");

            if (!Regex.IsMatch(toUsername, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {toUsername}.");

            int fromUsernameId = GetUsernameId(fromUsername);
            if (fromUsernameId == -1)
                throw new Exception($"Username {fromUsername} do not exists.");

            int toUsernameId = GetUsernameId(toUsername);
            if (toUsernameId == -1)
                throw new Exception($"Username {toUsername} do not exists.");

            int relationshipId = GetFriendRelationshipId(fromUsernameId, toUsernameId);
            if (relationshipId == -1)
                throw new Exception($"Friend relationship do not exists.");

            int userPosition = GetOrderInFriendRelationship(toUsernameId, relationshipId);

            databaseConnection.Connect();
            try
            {
                string cmdString = String.Empty;
                if (userPosition == 1)
                {
                    cmdString = $"UPDATE Relationship_settings SET nickname_user_1 = '{nickname}' WHERE Friend_relationships_id = {relationshipId}";
                }
                else if (userPosition == 2)
                {
                    cmdString = $"UPDATE Relationship_settings SET nickname_user_2 = '{nickname}' WHERE Friend_relationships_id = {relationshipId}";
                }

                using (OracleCommand oracleCommand = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    oracleCommand.ExecuteNonQuery();
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
        }


        #endregion

        #region MESSAGES TABLE PERSISTENCY

        public void StoreMessage(string senderUsername, string receiverUsername, string format, byte[] message_data, DateTime sentDate)
        {
            if (!Regex.IsMatch(senderUsername, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {senderUsername}.");

            if (!Regex.IsMatch(receiverUsername, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {receiverUsername}.");

            if (!Regex.IsMatch(receiverUsername, Constraints.FormatRegex))
                throw new Exception("Wrong format for the message/attachment.");

            int senderUsernameId = GetUsernameId(senderUsername);
            if (senderUsernameId == -1)
                throw new Exception($"Username {senderUsername} do not exists.");

            int receiverUsernameId = GetUsernameId(receiverUsername);
            if (receiverUsernameId == -1)
                throw new Exception($"Username {receiverUsername} do not exists.");

            int conversationId = GetConversationId(senderUsernameId, receiverUsernameId);
            if (conversationId == -1)
                throw new Exception($"Conversation between {senderUsername} and {receiverUsername} do not exists.");

            databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Messages(format, message_data, seen, sent_at, seen_at, Users_user_id, Conversations_conversation_id) VALUES('txt', @message_data, 'F', TO_DATE({sentDate.ToString("dd/MM/yyyy HH:mm:ss")}, 'DD/MM/YYYY HH24:MI:SS')', 'DD/MM/YYYY'), NULL, {senderUsername}, {conversationId})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, databaseConnection.Connection))
                {
                    using(OracleParameter oracle = new OracleParameter("message_data", message_data))
                    { 
                        oracleCommand.ExecuteNonQuery();
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
        }

        #endregion
    }
}