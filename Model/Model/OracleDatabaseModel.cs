using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DatabaseConnection;
using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;
using Model.Commons;
using Model.DataTransferObjects;

namespace Model
{
    public class OracleDatabaseModel : IModel
    {
        private IDatabaseConnection _databaseConnection;
        public OracleDatabaseModel()
        {
            _databaseConnection = new OracleDatabaseConnection();
        }

        #region USERS TABLE PERSISTENCY

        public void AddNewUser(string username, string password)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username}.");

            if (!Regex.IsMatch(password, Constraints.PasswordRegex))
                throw new Exception($"Wrong username format for {password}.");

            if (GetUsernameId(username) != -1)
                throw new Exception($"Username {username} already exists.");

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Users(user_name, user_password) VALUES('{username}', '{password}')";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
            }
        }

        public void ChangeActiveStatus(string username, bool isActive)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username}.");

            int usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new Exception($"Username {username} do not exists.");

            string activeStatus = isActive ? Constraints.BooleanTrueStatus : Constraints.BooleanFalseStatus;
            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"UPDATE Users SET is_active = '{activeStatus}' WHERE user_id = {usernameId}";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
            }
        }

        public void DeleteUser(string username)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username}.");

            int usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new Exception($"Username {username} do not exists.");

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"DELETE FROM Users WHERE user_id = {usernameId}";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
            }
        }

        private int GetUsernameId(string username)
        {
            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT user_id FROM Users WHERE user_name = '{username}'";
                using (OracleCommand query = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
            }

            return -1;
        }

        private string GetUsername(long usernameId)
        {
            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT user_name FROM Users WHERE user_id = {usernameId}";
                using (OracleCommand query = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read())
                            return oracleDataReader.GetString(0);
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
                _databaseConnection.CloseConnection(connectionId);
            }

            return "";
        }

        #endregion

        #region USER INFORMATIONS TABLE PERSISTENCY

        public void RegisterUser(string username, string firstname, string lastname, string email, DateTime birthdate)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username}.");

            if (!Regex.IsMatch(email, Constraints.EmailRegex))
                throw new Exception($"Wrong email format for {email}.");

            int usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new Exception($"Username {username} do not exists.");

            /*if (CheckIfUserRegistrationExists(usernameId))
                throw new Exception($"Username {username} registration already exists.");*/

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO User_informations VALUES('{firstname}', '{lastname}', '{email}', TO_DATE('{birthdate.ToString("dd/MM/yyyy")}', 'DD/MM/YYYY'), {usernameId})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    oracleCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-00001"))
                {
                    throw new Exception($"Username {username} registration already exists.");
                }
                else
                {
                    Console.WriteLine("Error: " + ex);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                _databaseConnection.CloseConnection(connectionId);
            }

        }

        private bool CheckIfUserRegistrationExists(int usernameId)
        {
            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT COUNT(*) FROM User_informations WHERE (Users_user_id = {usernameId})";
                using (OracleCommand query = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
            }

            return false;
        }

        #endregion

        #region APPLICAITON SETTINGS PERSISTENCY

        public void AddApplicationSettings(string username)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username}.");

            int usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new Exception($"Username {username} do not exists.");

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Application_settings(Users_user_id) VALUES({usernameId})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    oracleCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("ORA-00001") && ex.Message.Contains("APPLICATION_SETTINGS_PK"))
                {
                    throw new Exception("Application settings entry already exists.");
                }
                else
                {
                    Console.WriteLine("Error: " + ex);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                _databaseConnection.CloseConnection(connectionId);
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

            /*if (GetConversationId(usernameId1, usernameId2) != -1)
                throw new Exception($"Conversation between {username1} and {username2} already exists.");*/

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Conversations(Users_user_id, Users_user_id2) VALUES({usernameId1}, {usernameId2})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    oracleCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-00001"))
                {
                    throw new Exception($"Conversation between {username1} and {username2} already exists.");
                }
                else
                {
                    Console.WriteLine("Error: " + ex);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                _databaseConnection.CloseConnection(connectionId);
            }
        }

        public void DeleteConversation(string username1, string username2)
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

            int conversationId = GetConversationId(usernameId1, usernameId2);
            if (conversationId == -1)
                throw new Exception($"Conversation between {username1} and {username2} do not exists.");

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"DELETE FROM Conversations WHERE conversation_id = {conversationId}";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
            }
        }

        private int GetConversationId(int usernameId1, int usernameId2)
        {
            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT conversation_id FROM Conversations WHERE (Users_user_id = {usernameId1} and Users_user_id2 = {usernameId2}) or (Users_user_id = {usernameId2} and Users_user_id2 = {usernameId1})";
                using (OracleCommand query = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
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

            /*if (GetFriendRelationshipId(fromUsernameId, toUsernameId) != -1)
                throw new Exception($"Friend relationship between {fromUsername} and {toUsername} already exists.");*/

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Friend_relationships(Users_user_id, users_user_id2) VALUES({fromUsernameId}, {toUsernameId})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    oracleCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-00001"))
                {
                    throw new Exception($"Friend relationship between {fromUsername} and {toUsername} already exists.");
                }
                else
                {
                    Console.WriteLine("Error: " + ex);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                _databaseConnection.CloseConnection(connectionId);
            }
        }

        public void AcceptFriendRequest(string username1, string username2)
        {
            if (!Regex.IsMatch(username1, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username1}.");

            if (!Regex.IsMatch(username2, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username2}.");

            int fromUsernameId = GetUsernameId(username1);
            if (fromUsernameId == -1)
                throw new Exception($"Username {username1} do not exists.");

            int toUsernameId = GetUsernameId(username2);
            if (toUsernameId == -1)
                throw new Exception($"Username {username2} do not exists.");

            int relationshipId = GetFriendRelationshipId(fromUsernameId, toUsernameId);
            if (relationshipId == -1)
                throw new Exception($"Friend relationship between {username1} and {username2} do not exists.");

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"UPDATE Friend_relationships SET status = '{Constraints.FriendshipFriendsStatus}' WHERE relationship_id = {relationshipId}";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
            }
        }

        public void DeleteFriendRelationship(string username1, string username2)
        {
            if (!Regex.IsMatch(username1, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username1}.");

            if (!Regex.IsMatch(username2, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username2}.");

            int fromUsernameId = GetUsernameId(username1);
            if (fromUsernameId == -1)
                throw new Exception($"Username {username1} do not exists.");

            int toUsernameId = GetUsernameId(username2);
            if (toUsernameId == -1)
                throw new Exception($"Username {username2} do not exists.");

            int relationshipId = GetFriendRelationshipId(fromUsernameId, toUsernameId);
            if (relationshipId == -1)
                throw new Exception($"Friend relationship between {username1} and {username2} do not exists.");

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"DELETE FROM Friend_relationships WHERE relationship_id = {relationshipId}";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
            }
        }

        private int GetFriendRelationshipId(int usernameId1, int usernameId2)
        {
            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT relationship_id FROM Friend_relationships WHERE (Users_user_id = {usernameId1} and Users_user_id2 = {usernameId2}) or (Users_user_id = {usernameId2} and Users_user_id2 = {usernameId1})";
                using (OracleCommand query = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
            }

            return -1;
        }

        private int GetOrderInFriendRelationship(int usernameId, int relationshipId)
        {
            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdStringPosition1 = $"SELECT COUNT(*) FROM Friend_relationships WHERE (Users_user_id = {usernameId} and relationship_id = {relationshipId})";
                string cmdStringPosition2 = $"SELECT COUNT(*) FROM Friend_relationships WHERE (Users_user_id2 = {usernameId} and relationship_id = {relationshipId})";
                
                using (OracleCommand query = new OracleCommand(cmdStringPosition1, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read() && oracleDataReader.GetInt32(0) == 1)
                            return 1;
                    }
                }

                using (OracleCommand query = new OracleCommand(cmdStringPosition2, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read() && oracleDataReader.GetInt32(0) == 1)
                            return 2;
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
                _databaseConnection.CloseConnection(connectionId);
            }

            return -1;
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
                throw new Exception($"Friend relationship between {username1} and {username2} do not exists.");

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Relationship_settings(Friend_relationships_id) VALUES({friendRelationshipId})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    oracleCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-00001"))
                {
                    throw new Exception($"Relationship settings entry between {username1} and {username2} already exists.");
                }
                else
                {
                    Console.WriteLine("Error: " + ex);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            finally
            {
                _databaseConnection.CloseConnection(connectionId);
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
            nickname = nickname.Replace("\'", "''");

            uint connectionId = _databaseConnection.Connect();
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

                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
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
                _databaseConnection.CloseConnection(connectionId);
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

            if (!Regex.IsMatch(format, Constraints.FormatRegex))
                throw new Exception($"Wrong message/attachment format for {format}.");

            int senderUsernameId = GetUsernameId(senderUsername);
            if (senderUsernameId == -1)
                throw new Exception($"Username {senderUsername} do not exists.");

            int receiverUsernameId = GetUsernameId(receiverUsername);
            if (receiverUsernameId == -1)
                throw new Exception($"Username {receiverUsername} do not exists.");

            int conversationId = GetConversationId(senderUsernameId, receiverUsernameId);
            if (conversationId == -1)
                throw new Exception($"Conversation between {senderUsername} and {receiverUsername} do not exists.");

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Messages(format, message_data, seen, sent_at, seen_at, Users_user_id, Conversations_conversation_id) VALUES('{format}', :message_data, 'F', TO_DATE('{sentDate.ToString("MM/dd/yyyy HH:mm:ss")}', 'MM/DD/YYYY HH24:MI:SS'), NULL, {senderUsernameId}, {conversationId})";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using(OracleParameter param = new OracleParameter("message_data", OracleDbType.Blob))
                    {
                        param.Value = message_data;
                        oracleCommand.Parameters.Add(param);
                        
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
                _databaseConnection.CloseConnection(connectionId);
            }
        }

        #endregion

        #region FUNCTIONALITY METHODS

        public bool CheckUserCredentials(string username, string password)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new Exception($"Wrong username format for {username}.");

            if (!Regex.IsMatch(password, Constraints.PasswordRegex))
                throw new Exception($"Wrong username format for {password}.");

            int usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new Exception($"Username {username} do not exists.");

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT COUNT(*) FROM Users WHERE user_id = {usernameId} and user_password = '{password}'";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader())
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
                _databaseConnection.CloseConnection(connectionId);
            }

            return false;
        }

        public void GetLastNMessagesFromConversation(string username1, string username2, long fromMessageId, uint howManyMessages, out List<MessageDTO> messages, out long lastMessageId)
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

            int conversationId = GetConversationId(usernameId1, usernameId2);
            if (conversationId == -1)
                throw new Exception($"Conversation between {username1} and {username2} do not exists.");

            lastMessageId = -1;
            messages = new List<MessageDTO>();

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = null;
                if (fromMessageId > -1)
                {
                    cmdString = $"SELECT * FROM Messages WHERE Conversations_conversation_id = {conversationId} AND message_id < {fromMessageId} AND ROWNUM <= {howManyMessages} ORDER BY Message_id DESC";
                }
                else if (fromMessageId == -1)
                {
                    cmdString = $"SELECT * FROM Messages WHERE Conversations_conversation_id = {conversationId} AND ROWNUM <= {howManyMessages} ORDER BY Message_id DESC";
                }
                else
                    throw new Exception("Invalid message id.");

                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader())
                    { 
                        const int BufferSize = 1024;
                        while (oracleDataReader.Read())
                        {
                            List<byte[]> messageData = new List<byte[]>();
                            byte[] dataBuffer = new byte[BufferSize];

                            long readedBytes = -1;
                            long startIndex = 0;

                            lastMessageId = oracleDataReader.GetInt64(0);
                            string format = oracleDataReader.GetString(1);
                            bool seen = oracleDataReader.GetString(3) == Constraints.BooleanTrueStatus ? true : false;
                            DateTime sentAt = oracleDataReader.GetDateTime(4);
                            DateTime seenAt = oracleDataReader.IsDBNull(5)? new DateTime() : oracleDataReader.GetDateTime(5);
                            long senderId = oracleDataReader.GetInt64(6);

                            readedBytes = oracleDataReader.GetBytes(2, startIndex, dataBuffer, 0, BufferSize);
                            while (readedBytes == BufferSize)
                            {
                                messageData.Add(dataBuffer);

                                startIndex += BufferSize;
                                readedBytes = oracleDataReader.GetBytes(2, startIndex, dataBuffer, 0, BufferSize);
                            }

                            // Iau ultimii bytes
                            messageData.Add(dataBuffer);

                            string senderUsername = GetUsername(senderId);
                            messages.Add(new MessageDTO(format, messageData, seen, sentAt, seenAt, senderUsername));
                        }

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
                _databaseConnection.CloseConnection(connectionId);
            }
        }

        #endregion


    }
}