using System;
using System.Collections.Generic;
using Model;
using Model.DatabaseConnection;
using Model.DataTransferObjects;
using Model.Exceptions;
using Model.Exceptions.AlreadyExistsExceptions;
using Model.Exceptions.DoNotExistsExceptions;

namespace ModelDevelopmentProject
{
    class Program
    {
        private const string UserId = "stud_nume";
        private const string Password = "stud_parola";
        private const string Hostname = "localhost";
        private const string Port = "1521";
        private const string Sid = "xe";
        private const bool Pooling = true;

        static void Main(string[] args)
        {
            IModel oracleModel = new OracleDatabaseModel(UserId, Password, Hostname, Port, Sid, Pooling);

            try
            {
                oracleModel.AddNewUser("CCC29", "12346");
            }
            catch(UserAlreadyExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
            

        }
    }
}
