using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DataBaseKernel
{
    public class DBList
    {
        public static string MSSQL_Conn = ConfigurationManager.ConnectionStrings["AWS_MSSQL"].ConnectionString;
    }
}