using APIBC.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APIBC
{
    public class LoginSet
    {
        public const string Password = "12345";
        public static bool UsernamePassword(string name,string password)
        {
            AccountUser lgn = new AccountUser
            {
                Username = name,
                Password = ComputeSha256Hash(password)
            };
            return f_GetUserCheck(lgn) ;
        }

        //public async Task<AccountUser> FindUser(string userName, string password)
        //{
        //    await f_GetUserCheck();
        //}

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static bool f_GetUserCheck(AccountUser lgn)
        {
            string connStr = ConstApps.ConnectionPostgre;
            NpgsqlConnection conn = new NpgsqlConnection();
            NpgsqlCommand cmd;
            int result = 0;
            conn = new NpgsqlConnection(connStr);
            conn.Open();
            string sqlQuery = @"select * from tblaccount_checkaccount(:_Username,:_Password)";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("_Username", lgn.Username.ToLower());
            cmd.Parameters.AddWithValue("_Password", lgn.Password.ToLower());
            //result = cmd.ExecuteReader()
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = int.Parse(reader["countData"].ToString());
            }
            conn.Close();
            if (result == 1)
                return true;
            else
                return false;
        }

    }
}