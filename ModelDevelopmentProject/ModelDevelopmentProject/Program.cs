using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DatabaseConnection;
using Oracle.ManagedDataAccess.Client;

namespace ModelDevelopmentProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseConnection database = new OracleDatabaseConnection();
            IModel oracleModel = new OracleDatabaseModel();

            bool result = oracleModel.CheckUserCredentials("CCC29", "1234");
            Console.WriteLine(result);
        }
    }
}
