using Farmex.Repositories;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Farmex.Utilities
{
    public class DbConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            // Aquí configuras la cadena de conexión a tu base de datos
            //string connectionString = "Data Source=DESKTOP-KOMT1IV\\SQLEXPRESS;Initial Catalog=Farmex;Integrated Security=True";
           // string connectionString = "Server=DESKTOP-KOMT1IV\\SQLEXPRESS;Database=Farmex;Integrated Security=True;";
            string connectionString = "Data Source = DESKTOP - KOMT1IV\\SQLEXPRESS; Initial Catalog = Farmex; Persist Security Info = True; Integrated Security = True; Application Name = Farmex;";

           



            return new SqlConnection(connectionString);
        }
    }
}
