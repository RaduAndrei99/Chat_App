using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Model.DatabaseConnection;
using Model.DataTransferObjects;
using Model.Exceptions;
using Model.Exceptions.AlreadyExistsExceptions;
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

            /*oracleModel.AddNewUser("GGGGGGGGGGGGGGGGG", "123456798");
            oracleModel.CreateConversation("CosminCCC29", "GGGGGGGGGGGGGGGGG");*/



/*            oracleModel.StoreMessage("CosminCCC29", "BRABRABRA", "txt", Encoding.ASCII.GetBytes("1"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "GGGGGGGGGGGGGGGGG", "txt", Encoding.ASCII.GetBytes("2"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "BRABRABRA", "txt", Encoding.ASCII.GetBytes("3"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "GGGGGGGGGGGGGGGGG", "txt", Encoding.ASCII.GetBytes("4"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "BRABRABRA", "txt", Encoding.ASCII.GetBytes("5"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "GGGGGGGGGGGGGGGGG", "txt", Encoding.ASCII.GetBytes("6"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "BRABRABRA", "txt", Encoding.ASCII.GetBytes("7"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "GGGGGGGGGGGGGGGGG", "txt", Encoding.ASCII.GetBytes("8"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "BRABRABRA", "txt", Encoding.ASCII.GetBytes("9"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "GGGGGGGGGGGGGGGGG", "txt", Encoding.ASCII.GetBytes("10"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "BRABRABRA", "txt", Encoding.ASCII.GetBytes("11"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "GGGGGGGGGGGGGGGGG", "txt", Encoding.ASCII.GetBytes("12"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "BRABRABRA", "txt", Encoding.ASCII.GetBytes("13"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "GGGGGGGGGGGGGGGGG", "txt", Encoding.ASCII.GetBytes("14"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "BRABRABRA", "txt", Encoding.ASCII.GetBytes("15"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "GGGGGGGGGGGGGGGGG", "txt", Encoding.ASCII.GetBytes("16"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "BRABRABRA", "txt", Encoding.ASCII.GetBytes("17"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "GGGGGGGGGGGGGGGGG", "txt", Encoding.ASCII.GetBytes("18"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "BRABRABRA", "txt", Encoding.ASCII.GetBytes("19"), DateTime.UtcNow);
            oracleModel.StoreMessage("CosminCCC29", "GGGGGGGGGGGGGGGGG", "txt", Encoding.ASCII.GetBytes("20"), DateTime.UtcNow);*/

            List<MessageDTO> list;
            long lastId = -1;
            
            oracleModel.GetLastNMessagesFromConversation("CosminCCC29", "GGGGGGGGGGGGGGGGG", -1, 6, out list, out lastId);
            for(int i = 0; i < list.Count; ++i)
            {
                Console.WriteLine(Encoding.ASCII.GetString(list[i].MessageData));
            }
            Console.WriteLine("LastId = " + lastId.ToString());

            while (lastId != -1)
            {
                oracleModel.GetLastNMessagesFromConversation("CosminCCC29", "GGGGGGGGGGGGGGGGG", lastId, 6, out list, out lastId);
                for (int i = 0; i < list.Count; ++i)
                {
                    Console.WriteLine(Encoding.ASCII.GetString(list[i].MessageData));
                    Console.WriteLine("LastId = " + lastId.ToString());
                }
            }
        }

    }
}
