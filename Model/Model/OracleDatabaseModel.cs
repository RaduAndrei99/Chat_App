/****************************************************************************
 *                                                                          *
 *  Autor:  Cojocaru Constantin-Cosmin                                      *
 *  Grupa:  1309A                                                           *
 *  Fisier: OracleDatabaseModel.cs                                          *
 *                                                                          *
 *  Descriere: Modelul concret pentru o baza de date Oracle                 *
 *              Stocheaza si interogheaza datele stocate in baza de date.   *
 *                                                                          *
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Model.DatabaseConnection;
using Oracle.ManagedDataAccess.Client;
using System.Text.RegularExpressions;
using Model.Commons;
using Model.DataTransferObjects;
using Model.Exceptions.DoNotExistsExceptions;
using Model.Exceptions.WrongFormatExceptions;
using Model.Exceptions.AlreadyExistsExceptions;

namespace Model
{
    /// <summary>
    /// Modelul aplicatiei pentru o baza de date Oracle
    /// </summary>
    public class OracleDatabaseModel : IModel
    {

        /// <summary>
        /// Referinta generica la un obiect ce incapsuleaza conexiunea la baza de date
        /// </summary>
        private IDatabaseConnection _databaseConnection;

        /// <summary>
        /// Constructorul clasei. Acesta initializeaza datele conexiunii la baza de date oracle
        /// </summary>
        /// <param name="databaseUsername">Numele utilizatorului bazei de date</param>
        /// <param name="databaseUserPassword">Numele utilizatorului bazei de date</param>
        /// <param name="hostname">Adresa IP al host-ului</param>
        /// <param name="port">Port-ul pe care ruleaza baza de date</param>
        /// <param name="sid">Sid-ul bazei de date</param>
        /// <param name="pooling">Setarea care indica daca baza de date va face pooling la conexiuni sau nu</param>
        /// <returns></returns>
        public OracleDatabaseModel(in string databaseUsername, in string databaseUserPassword, in string hostname, in string port, in string sid, in bool pooling = true)
        {
            _databaseConnection = new OracleDatabaseConnection(databaseUsername, databaseUserPassword, hostname, port, sid, pooling);
        }

        #region USERS TABLE PERSISTENCY

        /// <summary>
        /// Adauga un nou utilizator in baza de date.
        /// </summary>
        /// <param name="username">Numele de utilizator al persoanei</param>
        /// <param name="hashedPassword">Parola utilizatorului</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca formatul parolei utilizatorului este gresit. See <see cref="Commons.Constraints.PasswordHashedRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul exista deja in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void AddNewUser(string username, string hashedPassword)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            if (!Regex.IsMatch(hashedPassword, Constraints.PasswordHashedRegex))
                throw new WrongHashedPasswordFormatException(hashedPassword);

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Users(user_name, user_password) VALUES('{username}', '{hashedPassword}')";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    oracleCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-00001"))
                {
                    throw new UserAlreadyExistsException(username);
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

        /// <summary>
        /// Modifica numele unui utilizatorului.
        /// </summary>
        /// <param name="currentUsername">Numele de utilizator actual al persoanei</param>
        /// <param name="newUsername">Noul nume al utilizatorului</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca formatul parolei utilizatorului este gresit. See <see cref="Commons.Constraints.PasswordHashedRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void ChangeUsername(string currentUsername, string newUsername)
        {
            if (!Regex.IsMatch(currentUsername, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(currentUsername);

            if (!Regex.IsMatch(newUsername, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(newUsername);

            long usernameId = GetUsernameId(currentUsername);
            if (usernameId == -1)
                throw new UserDoNotExistsException(currentUsername);

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"UPDATE Users SET user_name = '{newUsername}' WHERE user_id = {usernameId}";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    oracleCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ORA-00001"))
                {
                    throw new UserAlreadyExistsException(newUsername);
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

        /// <summary>
        /// Modifica parola unui utilizatorului.
        /// </summary>
        /// <param name="username">Numele de utilizator al persoanei</param>
        /// <param name="newPassword">Noul nume al utilizatorului</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca formatul parolei utilizatorului este gresit. See <see cref="Commons.Constraints.PasswordHashedRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date.</para>
        /// </exception>
        /// <returns></returns>
        public void ChangeUserPassword(string username, string newPassword)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            if (!Regex.IsMatch(newPassword, Constraints.PasswordHashedRegex))
                throw new WrongHashedPasswordFormatException(newPassword);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"UPDATE Users SET user_password = '{newPassword}' WHERE user_id = {usernameId}";
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

        /// <summary>
        /// Sterge un utilizator din baza de date.
        /// </summary>
        /// <param name="username">Numele actual de utilizator al persoanei</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca formatul parolei utilizatorului este gresit. See <see cref="Commons.Constraints.PasswordHashedRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void DeleteUser(string username)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

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

        /// <summary>
        /// Returneaza id-ul unui utilizator.
        /// </summary>
        /// <param name="username">Numele de utilizator al persoanei</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// </exception>
        /// <returns>Id-ul utilizatorului in caz de succes sau -1 in cazul in care utilizatorul nu exista in baza de date</returns>
        private long GetUsernameId(string username)
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

        /// <summary>
        /// Returneaza numele de utilizator al persoanei.
        /// </summary>
        /// <param name="usernameId">Id-ul utilizatorului</param>
        /// <returns>Numele utilizatorului in caz de succes sau null in caz de eroare</returns>
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

            return null;
        }

        #endregion

        #region USER INFORMATIONS TABLE PERSISTENCY

        /// <summary>
        /// Inregistreaza datele personale ale unui user in baza de date.
        /// </summary>
        /// <param name="username">Numele de utilizator la persoanei</param>
        /// <param name="firstname">Prenumele de familie al utilizatorului</param>
        /// <param name="lastname">Numele de familie al utilizatorului</param>
        /// <param name="email">Adresa de email al utilizatorului</param>
        /// <param name="birthdate">Data de nastere al utilizatorului</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca formatul adresei de email este gresit. See <see cref="Commons.Constraints.EmailRegex"/></para>
        /// <para>Arunca exceptie daca formatul numelui de familie sau al prenumelui este gresit. See <see cref="Commons.Constraints.PersonalNameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void RegisterUser(string username, string firstname, string lastname, string email, DateTime birthdate)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            if (!Regex.IsMatch(email, Constraints.EmailRegex))
                throw new WrongEmailFormatException(email);

            if (!Regex.IsMatch(firstname, Constraints.PersonalNameRegex))
                throw new WrongFirstNameFormatException(firstname);

            if (!Regex.IsMatch(lastname, Constraints.PersonalNameRegex))
                throw new WrongLastNameFormatException(lastname);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

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
                    throw new UserRegistrationAlreadyExistsException(username);
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

        #region APPLICAITON SETTINGS PERSISTENCY

        /// <summary>
        /// Adauga sectiunea cu setarile implicite ale aplicatiei pentru un utilizator.
        /// </summary>
        /// <param name="username">Numele de utilizator la persoanei</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void AddApplicationSettings(string username)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

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
                if (ex.Message.Contains("ORA-00001") && ex.Message.Contains("APPLICATION_SETTINGS_PK"))
                {
                    throw new ApplicationSettingsAlreadyExistsException(username);
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

        /// <summary>
        /// Seteaza formatul datei in baza de date.
        /// </summary>
        /// <param name="username">Numele de utilizator la persoanei</param>
        /// <param name="dateFormat">Formatul datei</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void SetDateFormat(string username, DateFormat dateFormat)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"UPDATE Application_settings SET date_format = '{dateFormat}' WHERE Users_user_id = {usernameId}";
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

        /// <summary>
        /// Seteaza formatul orei in baza de date.
        /// </summary>
        /// <param name="username">Numele de utilizator la persoanei</param>
        /// <param name="timeFormat">Formatul orei</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void SetTimeFormat(string username, TimeFormat timeFormat)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"UPDATE Application_settings SET hour_format = '{timeFormat}' WHERE Users_user_id = {usernameId}";
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

        #region CONVERSATION TABLE PERSISTENCY

        /// <summary>
        /// Creeaza o conversatie intre 2 utilizatori in baza de date.
        /// </summary>
        /// <param name="username1">Numele de utilizator al primei persoane</param>
        /// <param name="username2">Numele de utilizator celei de a doua persoane</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// <para>Arunca exceptie daca conversatia exista deja in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void CreateConversation(string username1, string username2)
        {
            if (!Regex.IsMatch(username1, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username1);

            if (!Regex.IsMatch(username2, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username2);

            long usernameId1 = GetUsernameId(username1);
            if (usernameId1 == -1)
                throw new UserDoNotExistsException(username1);

            long usernameId2 = GetUsernameId(username2);
            if (usernameId2 == -1)
                throw new UserDoNotExistsException(username2);

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
                    throw new ConversationAlreadyExistsException(username1, username2);
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

        /// <summary>
        /// Sterge o conversatie intre 2 utilizatori in baza de date.
        /// </summary>
        /// <param name="username1">Numele de utilizator al primei persoane</param>
        /// <param name="username2">Numele de utilizator celei de a doua persoane</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// <para>Arunca exceptie daca conversatia exista deja in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void DeleteConversation(string username1, string username2)
        {
            if (!Regex.IsMatch(username1, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username1);

            if (!Regex.IsMatch(username2, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username2);

            long usernameId1 = GetUsernameId(username1);
            if (usernameId1 == -1)
                throw new UserDoNotExistsException(username1);

            long usernameId2 = GetUsernameId(username2);
            if (usernameId2 == -1)
                throw new UserDoNotExistsException(username2);

            long conversationId = GetConversationId(usernameId1, usernameId2);
            if (conversationId == -1)
                throw new ConversationDoNotExistsException(username1, username2);

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

        /// <summary>
        /// Returneaza id-ul unei conversatii intre 2 utilizatori.
        /// </summary>
        /// <param name="usernameId1">Id-ul primului utilizator</param>
        /// <param name="usernameId2">Id-ul celui de al doilea utilizator</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca unul din utilizatori nu exista in baza de date</para>
        /// </exception>
        /// <returns>Id-ul conversatiei in caz de succes sau -1 in cazul in care nu exista conversatia in baza de date</returns>
        private long GetConversationId(long usernameId1, long usernameId2)
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

        /// <summary>
        /// Inregistrarea o cerere de prietenie in baza de date
        /// </summary>
        /// <param name="fromUsername">Numele utilizatorului care trimite cererea</param>
        /// <param name="toUsername">Numele utilizatorului care primeste cererea</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca unul din utilizatori nu exista in baza de date</para>
        /// <para>Arunca exceptie daca unul din utilizatori nu exista in baza de date</para>
        /// <para>Arunca exceptie daca relatia de prietenie exista deja in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void RegisterFriendRequest(string fromUsername, string toUsername)
        {
            if (!Regex.IsMatch(fromUsername, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException($"Wrong username format for {fromUsername}.");

            if (!Regex.IsMatch(toUsername, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException($"Wrong username format for {toUsername}.");

            long fromUsernameId = GetUsernameId(fromUsername);
            if (fromUsernameId == -1)
                throw new UserDoNotExistsException($"Username {fromUsername} do not exists.");

            long toUsernameId = GetUsernameId(toUsername);
            if (toUsernameId == -1)
                throw new UserDoNotExistsException($"Username {toUsername} do not exists.");

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
                    throw new FriendRelationshipAlreadyExistsException(fromUsername, toUsername);
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

        /// <summary>
        /// Modifica statusul cererii de prietenie dintre doi utilizatori
        /// </summary>
        /// <param name="username1">Numele unui utilizator din cerere</param>
        /// <param name="username2">Numele celulalt utilizator din cerere</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca unul din utilizatori nu exista in baza de date</para>
        /// <para>Arunca exceptie daca nu exista nu exista nici o cerere de prietenie inregistrata in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void AcceptFriendRequest(string username1, string username2)
        {
            if (!Regex.IsMatch(username1, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username1);

            if (!Regex.IsMatch(username2, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username2);

            long username1Id = GetUsernameId(username1);
            if (username1Id == -1)
                throw new UserDoNotExistsException(username1);

            long username2Id = GetUsernameId(username2);
            if (username2Id == -1)
                throw new UserDoNotExistsException(username2);

            long relationshipId = GetFriendRelationshipId(username1Id, username2Id);
            if (relationshipId == -1)
                throw new FriendRelationshipDoNotExistsException(username1, username2);

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

        /// <summary>
        /// Sterge o cerere de prietenie dintre doi utilizatori
        /// </summary>
        /// <param name="username1">Numele unui utilizator din cerere</param>
        /// <param name="username2">Numele celulalt utilizator din cerere</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca unul din utilizatori nu exista in baza de date</para>
        /// <para>Arunca exceptie daca nu exista nu exista nici o cerere de prietenie inregistrata in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void DeleteFriendRelationship(string username1, string username2)
        {
            if (!Regex.IsMatch(username1, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username1);

            if (!Regex.IsMatch(username2, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username2);

            long username1Id = GetUsernameId(username1);
            if (username1Id == -1)
                throw new UserDoNotExistsException(username1);

            long username2Id = GetUsernameId(username2);
            if (username2Id == -1)
                throw new UserDoNotExistsException(username2);

            long relationshipId = GetFriendRelationshipId(username1Id, username2Id);
            if (relationshipId == -1)
                throw new FriendRelationshipDoNotExistsException(username1, username2);

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

        /// <summary>
        /// Returneaza id-ul cererii de prietenie dintre doi utilizatori
        /// </summary>
        /// <param name="username1">Numele unui utilizator din cerere</param>
        /// <param name="username2">Numele celulalt utilizator din cerere</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca unul din utilizatori nu exista in baza de date</para>
        /// </exception>
        /// <returns>Id-ul conversatiei in caz de succes sau -1 in cazul in care nu exista relatia in baza dedate</returns>
        private long GetFriendRelationshipId(long usernameId1, long usernameId2)
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

        /// <summary>
        /// Returneaza un numar de ordinde al utilizatorului din cererea de prietenie
        /// </summary>
        /// <param name="usernameId">Id-ul utilizatorului</param>
        /// <param name="relationshipId">Id-ul relatiei de rietenie</param>
        /// <returns>1 daca utilizatorul este cel care a initial cererea, 2 daca este utilizatorul care a primit cererea, -1 daca nu exista seciunea de setari a relatiei de prietenie</returns>
        private int GetOrderInFriendRelationship(long usernameId, long relationshipId)
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

        /// <summary>
        /// Adauga sectiunea cu setarile relatiei de prietenie
        /// </summary>
        /// <param name="username1">Numele de utilizator al primei persoane</param>
        /// <param name="username2">Numele de utilizator celei de a doua persoane</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// <para>Arunca exceptie daca sectiunea de setari exista deja in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void AddRelationshipSettings(string username1, string username2)
        {
            if (!Regex.IsMatch(username1, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username1);

            if (!Regex.IsMatch(username2, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username2);

            long usernameId1 = GetUsernameId(username1);
            if (usernameId1 == -1)
                throw new UserDoNotExistsException(username1);

            long usernameId2 = GetUsernameId(username2);
            if (usernameId2 == -1)
                throw new UserDoNotExistsException(username2);

            long friendRelationshipId = GetFriendRelationshipId(usernameId1, usernameId2);
            if (GetFriendRelationshipId(usernameId1, usernameId2) == -1)
                throw new FriendRelationshipDoNotExistsException(username1, username2);

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
                    throw new RelationshipSettingsAlreadyExistsException(username1, username2);
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


        /// <summary>
        /// Schimba un porecla unui utilizator dintr-o relatie de prietenie
        /// </summary>
        /// <param name="fromUsername">Numele de utilizator al primei persoane</param>
        /// <param name="toUsername">Numele de utilizator celei de a doua persoane</param>
        /// <param name="nickname">Porecla celui de al doilea utilizator</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// <para>Arunca exceptie daca sectiunea de setari nu exista in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void ChangeNickname(string fromUsername, string toUsername, string nickname)
        {
            if (!Regex.IsMatch(fromUsername, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(fromUsername);

            if (!Regex.IsMatch(toUsername, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(toUsername);

            long fromUsernameId = GetUsernameId(fromUsername);
            if (fromUsernameId == -1)
                throw new UserDoNotExistsException(fromUsername);

            long toUsernameId = GetUsernameId(toUsername);
            if (toUsernameId == -1)
                throw new UserDoNotExistsException(toUsername);

            long relationshipId = GetFriendRelationshipId(fromUsernameId, toUsernameId);
            if (relationshipId == -1)
                throw new FriendRelationshipDoNotExistsException(fromUsername, toUsername);

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
                    if (oracleCommand.ExecuteNonQuery() == 0)
                        throw new RelationshipSettingsDoNotExistsException(fromUsername, toUsername);
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

        /// <summary>
        /// Stocheaza un mesaj in baza de date
        /// </summary>
        /// <param name="senderUsername">Numele de utilizator al persoanei care trimite mesajul</param>
        /// <param name="receiverUsername">Numele de utilizator al persoanei care primeste mesajul</param>
        /// <param name="format">Formatul/Extensia mesajului</param>
        /// <param name="message_data">Corpul mesajului</param>
        /// <param name="sentDate">Data de trimitere a mesajului</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// <para>Arunca exceptie daca conversatia dintre cele doua persoane nu exista in baza de date</para>
        /// </exception>
        /// <returns></returns>
        public void StoreMessage(string senderUsername, string receiverUsername, string format, byte[] message_data, DateTime sentDate)
        {
            if (!Regex.IsMatch(senderUsername, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(senderUsername);

            if (!Regex.IsMatch(receiverUsername, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(receiverUsername);

            if (!Regex.IsMatch(format, Constraints.FormatRegex))
                throw new WrongMessageFormatFormatException(format);

            long senderUsernameId = GetUsernameId(senderUsername);
            if (senderUsernameId == -1)
                throw new UserDoNotExistsException(senderUsername);

            long receiverUsernameId = GetUsernameId(receiverUsername);
            if (receiverUsernameId == -1)
                throw new UserDoNotExistsException(receiverUsername);

            long conversationId = GetConversationId(senderUsernameId, receiverUsernameId);
            if (conversationId == -1)
                throw new ConversationDoNotExistsException(senderUsername, receiverUsername);

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"INSERT INTO Messages(format, message_data, sent_at, Users_user_id, Conversations_conversation_id) VALUES('{format}', :message_data, " +
                    $"TO_DATE('{sentDate.ToString("MM/dd/yyyy HH:mm:ss")}', 'MM/DD/YYYY HH24:MI:SS'), {senderUsernameId}, {conversationId})";
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

        /// <summary>
        /// Verifica credentialele unui utilizator
        /// </summary>
        /// <param name="username">Numele de utilizator al persoanei</param>
        /// <param name="hashedPassword">Parola unui utilizator</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca formatul parolei utilizatorului este gresit. See <see cref="Commons.Constraints.PasswordHashedRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns>True daca datele utilizatorului sunt corecte sau False daca nu</returns>
        public bool CheckUserCredentials(string username, string hashedPassword)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            if (!Regex.IsMatch(hashedPassword, Constraints.PasswordHashedRegex))
                throw new WrongHashedPasswordFormatException(hashedPassword);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT COUNT(*) FROM Users WHERE user_id = {usernameId} and user_password = '{hashedPassword}'";
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

        /// <summary>
        /// Returneaza un numar de mesaje din baza de date
        /// </summary>
        /// <param name="username1">Numele unui utilizator al unei conversatiei</param>
        /// <param name="username2">Numele celuilat utilizator din conversatie</param>
        /// <param name="bellowThisMessageId">Id-ul mesajului sub care se incepe citirea mesajelor din baza de date</param>
        /// <param name="howManyMessages">Numarul de mesaje citite</param>
        /// <param name="messages">Lista de obiecte MessageDTO care contine datele mesajelor. See <see cref="DataTransferObjects.MessageDTO"/></param>
        /// <param name="lastMessageId">Id-ul ultimului mesaj citit din baza de date</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// <para>Arunca exceptie daca conversatie dintre cele 2 persoane nu exista in baza de date</para>
        /// </exception>
        /// <returns>Parametrul messages si parametrul lastMessageId</returns>
        public void GetLastNMessagesFromConversation(string username1, string username2, long bellowThisMessageId, uint howManyMessages, out List<MessageDTO> messages, out long lastMessageId)
        {
            if (!Regex.IsMatch(username1, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username1);

            if (!Regex.IsMatch(username2, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username2);

            long usernameId1 = GetUsernameId(username1);
            if (usernameId1 == -1)
                throw new UserDoNotExistsException(username1);

            long usernameId2 = GetUsernameId(username2);
            if (usernameId2 == -1)
                throw new UserDoNotExistsException(username2);

            long conversationId = GetConversationId(usernameId1, usernameId2);
            if (conversationId == -1)
                throw new ConversationDoNotExistsException(username1, username2);

            lastMessageId = -1;
            messages = new List<MessageDTO>();

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = null;
                if (bellowThisMessageId > -1)
                {
                    cmdString = $"SELECT * FROM Messages WHERE Conversations_conversation_id = {conversationId} AND message_id < {bellowThisMessageId} ORDER BY Message_id DESC FETCH NEXT {howManyMessages} ROWS ONLY;";
                }
                else if (bellowThisMessageId == -1)
                {
                    cmdString = $"SELECT * FROM Messages WHERE Conversations_conversation_id = {conversationId} ORDER BY Message_id DESC FETCH NEXT {howManyMessages} ROWS ONLY";
                }
                else
                    throw new Exception("Invalid message id.");

                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader())
                    { 
                        const int BufferSize = 128;
                        while (oracleDataReader.Read())
                        {
                            List<byte[]> messageData = new List<byte[]>();
                            byte[] dataBuffer = new byte[BufferSize];

                            long readedBytes = -1;
                            long startIndex = 0;

                            lastMessageId = oracleDataReader.GetInt64(0);
                            string format = oracleDataReader.GetString(1);
                            DateTime sentAt = oracleDataReader.GetDateTime(3);
                            long senderId = oracleDataReader.GetInt64(4);

                            readedBytes = oracleDataReader.GetBytes(2, startIndex, dataBuffer, 0, BufferSize);
                            while (readedBytes == BufferSize)
                            {
                                messageData.Add(dataBuffer);

                                startIndex += BufferSize;
                                readedBytes = oracleDataReader.GetBytes(2, startIndex, dataBuffer, 0, BufferSize);
                            }

                            // Iau ultimii bytes
                            int lastIndex = Array.FindLastIndex(dataBuffer, b => b != 0);
                            Array.Resize(ref dataBuffer, lastIndex + 1);

                            messageData.Add(dataBuffer);

                            string senderUsername = GetUsername(senderId);
                            messages.Add(new MessageDTO(format, messageData.SelectMany(it => it).ToArray(), sentAt, senderUsername));
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

        /// <summary>
        /// Returneaza formatul datei aplicatiei unui utilizator
        /// </summary>
        /// <param name="username">Numele de utilizator al unei persoane</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns>Formatul datei incapsulat intr-un obiect</returns>
        public DateFormat GetDateFormat(string username)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT date_format FROM Application_settings WHERE Users_user_id = {usernameId}";
                using (OracleCommand query = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read())
                            return DateFormat.GetDateFormat(oracleDataReader.GetString(0));
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

            return null;
        }

        /// <summary>
        /// Returneaza formatul orei aplicatiei unui utilizator
        /// </summary>
        /// <param name="username">Numele de utilizator al unei persoane</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns>Formatul orei incapsulat intr-un obiect</returns>
        public TimeFormat GetTimeFormat(string username)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT hour_format FROM Application_settings WHERE Users_user_id = {usernameId}";
                using (OracleCommand query = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read())
                            return TimeFormat.GetTimeFormat(oracleDataReader.GetString(0));
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

            return null;
        }

        /// <summary>
        /// Returneaza porecla unui utilizator dintr-o relatie de prietenie
        /// </summary>
        /// <param name="fromUsername">Numele de utilizator al primei persoane</param>
        /// <param name="friendUsername">Numele de utilizator celei de a doua persoane</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// <para>Arunca exceptie daca sectiunea de setari a relatiei nu exista in baza de date</para>
        /// </exception>
        /// <returns>Returneaza porecla unui utilizator dintr-o relatie de prietenie sau null daca utilizatorului nu ii este asignata nici o porecla</returns>
        public string GetNicknameFromFriendRelationship(string fromUsername, string friendUsername)
        {
            if (!Regex.IsMatch(fromUsername, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(fromUsername);

            if (!Regex.IsMatch(friendUsername, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(friendUsername);

            long fromUsernameId = GetUsernameId(fromUsername);
            if (fromUsernameId == -1)
                throw new UserDoNotExistsException(fromUsername);

            long friendUsernameId = GetUsernameId(friendUsername);
            if (friendUsernameId == -1)
                throw new UserDoNotExistsException(friendUsername);

            long relationshipId = GetFriendRelationshipId(fromUsernameId, friendUsernameId);
            if (relationshipId == -1)
                throw new FriendRelationshipDoNotExistsException(fromUsername, friendUsername);

            int userPosition = GetOrderInFriendRelationship(friendUsernameId, relationshipId);

            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT nickname_user_{userPosition} FROM relationship_settings WHERE friend_relationships_id = {relationshipId}";
                using (OracleCommand query = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = query.ExecuteReader())
                    {
                        if (oracleDataReader.Read())
                            return oracleDataReader.IsDBNull(0)? null : oracleDataReader.GetString(0);
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

            return null;
        }

        /// <summary>
        /// Returneaza lista numelor de utilizator a prietenilor unui utilizator
        /// </summary>
        /// <param name="username">Numele de utilizator al unei persoane</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns>Lista cu numele prietenilor</returns>
        public List<string> GetFriendList(string username)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

            List<string> friendList = new List<string>();
            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT user_name FROM Users u, Friend_relationships fr WHERE (fr.users_user_id = u.user_id OR fr.users_user_id2 = u.user_id) AND (fr.users_user_id = {usernameId} OR fr.users_user_id2 = {usernameId}) AND u.user_id != {usernameId} AND fr.status = '{Constraints.FriendshipFriendsStatus}' ORDER BY user_name";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader())
                    {
                        while (oracleDataReader.Read())
                            friendList.Add(oracleDataReader.GetString(0));
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

            return friendList;
        }

        /// <summary>
        /// Returneaza lista numelor de utilizator carora un utilizator le-a trimis o cerere si care inca nu este acceptata
        /// </summary>
        /// <param name="username">Numele de utilizator al unei persoane</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns>Lista numelor de utilizator carora un utilizator le-a trimis o cerere si care inca nu este acceptata</returns>
        public List<string> GetSentPendingRequests(string username)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

            List<string> sentPendingRequests = new List<string>();
            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT user_name FROM Users u, Friend_relationships fr WHERE fr.users_user_id = {usernameId} AND fr.users_user_id2 = u.user_id AND fr.status = '{Constraints.FriendshipPendingStatus}' ORDER BY user_name";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader())
                    {
                        while (oracleDataReader.Read())
                            sentPendingRequests.Add(oracleDataReader.GetString(0));
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

            return sentPendingRequests;
        }

        /// <summary>
        /// Returneaza lista numelor de utilizator de la care un utilizator a primit o cerere de prietenie si care inca nu este acceptata
        /// </summary>
        /// <param name="username">Numele de utilizator al unei persoane</param>
        /// <exception cref="System.Exception">
        /// <para>Arunca exceptie daca formatul numelui utilizatorului este gresit. See <see cref="Commons.Constraints.UsernameRegex"/></para>
        /// <para>Arunca exceptie daca utilizatorul nu exista in baza de date</para>
        /// </exception>
        /// <returns>Lista numelor de utilizator de la care un utilizator a primit o cerere de prietenie si care inca nu este acceptata</returns>
        public List<string> GetReceivedPendingRequest(string username)
        {
            if (!Regex.IsMatch(username, Constraints.UsernameRegex))
                throw new WrongUsernameFormatException(username);

            long usernameId = GetUsernameId(username);
            if (usernameId == -1)
                throw new UserDoNotExistsException(username);

            List<string> waitingRequests = new List<string>();
            uint connectionId = _databaseConnection.Connect();
            try
            {
                string cmdString = $"SELECT user_name FROM Users u, Friend_relationships fr WHERE fr.users_user_id2 = {usernameId} AND fr.users_user_id = u.user_id AND fr.status = '{Constraints.FriendshipPendingStatus}' ORDER BY user_name";
                using (OracleCommand oracleCommand = new OracleCommand(cmdString, _databaseConnection.Connection(connectionId)))
                {
                    using (OracleDataReader oracleDataReader = oracleCommand.ExecuteReader())
                    {
                        while (oracleDataReader.Read())
                            waitingRequests.Add(oracleDataReader.GetString(0));
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

            return waitingRequests;
        }

        #endregion

    }
}