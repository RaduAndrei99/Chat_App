using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
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

            List<MessageDTO> messages = null;
            long lastMessageId = -1;

            oracleModel.GetLastNMessagesFromConversation("CCC29", "Cosmin", -1, 5, out messages, out lastMessageId);
        }
    }
}
