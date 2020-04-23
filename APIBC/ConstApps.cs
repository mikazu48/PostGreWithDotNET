using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIBC
{
    public class ConstApps
    {
        public const string ConnectionPostgre = "Server=localhost;Port=5432;User Id=postgres;Password=12345;Database=budgetsystem;timeout=0;";
   
        //public static void DoingConn(NpgsqlConnection connSend)
        //{
        //    connRealize(connSend);
        //    connSend.Open();
        //}
        //private static void connRealize(NpgsqlConnection connSend)
        //{
        //    connSend = new NpgsqlConnection(ConnectionPostgre);
        //}
    }
}