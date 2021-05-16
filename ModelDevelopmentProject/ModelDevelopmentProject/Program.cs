using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Commons;
using Model.DatabaseConnection;
using Model.DataTransferObjects;
using Oracle.ManagedDataAccess.Client;

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


            // oracleModel.StoreMessage("CCC29", "BRA", "txt", new byte[] { 0x20, 0x22 }, DateTime.UtcNow);
            // oracleModel.GetLastNMessagesFromConversation("CCC29", "BRA", -1, 5, out messageDTOs, out lastId);

            
            TimeFormat date = oracleModel.GetTimeFormat("CCC29");

            Console.WriteLine("asd");
        }
    }
}
