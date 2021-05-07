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

            oracleModel.AddNewUser("Cosminelul", "1234");

        }
    }
}
