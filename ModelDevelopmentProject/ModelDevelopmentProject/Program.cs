using System;
using System.Collections.Generic;
using Model;
using Model.DatabaseConnection;
using Model.DataTransferObjects;

namespace ModelDevelopmentProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseConnection database = new OracleDatabaseConnection();
            IModel oracleModel = new OracleDatabaseModel();

            List<MessageDTO> messageDTOs = null;
            long lastId = -1;

            List<String> list = oracleModel.GetReceivedPendingRequest("CCC29");

            Console.WriteLine("asd");



        }
    }
}
