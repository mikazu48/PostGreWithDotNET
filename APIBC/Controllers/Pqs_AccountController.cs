using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIBC;
using APIBC.Models;
using Newtonsoft.Json;
using Npgsql;
using System.Data;
using System.Security.Cryptography;

namespace APIBC.Controllers
{
    public class Pqs_AccountController : ApiController
    {
        private string connStr = "Server=localhost;Port=5432;User Id=postgres;Password=12345;Database=budgetsystem;";
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;

        [HttpGet]
        // For basic authentication
        public HttpResponseMessage Login([FromUri]string username, [FromUri]string password)
        {
            try
            {
                bool getLogin = LoginSet.UsernamePassword(username, password);
                if (!getLogin)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid username / password!");
                }

                var AccountUser = new AccountUser()
                {
                    Username = username,
                    Password = password,
                };
                var jsonString = JsonConvert.SerializeObject(AccountUser);
                var token = StringCrypter.Encrypt(jsonString, password);
                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage CreateAccount([FromBody]AccountUser datLogin)
        {
            try
            {
                connRealize();
                conn.Open();
                bool checkInsert = f_CreateLogin(datLogin);
                if (checkInsert)
                    return Request.CreateResponse(HttpStatusCode.OK, "Success full insert data.");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "Fail insert data. Check parameters again.");
            }
            catch (Exception ex)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateAccount([FromBody]AccountUser datLogin)
        {
            try
            {
                connRealize();
                conn.Open();
                bool checkInsert = f_UpdateLogin(datLogin);
                if (checkInsert)
                    return Request.CreateResponse(HttpStatusCode.OK, "Success full update data.");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "Fail update data. Check parameters again.");
            }
            catch (Exception ex)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetSingleAccount(string Id)
        {
            try
            {
                connRealize();
                conn.Open();
                var getAccount = f_GetSingleDataAccount(Id);
                if(getAccount.EmployeeID == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Account / User not found!");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, getAccount);
            }
            catch (Exception ex)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }

        /// Private Function/ Method Area
        /// Just in case you need some code, write here.
        /// 
        private bool f_CreateLogin(AccountUser lgn)
        {
            int result;
            string sqlQuery = @"select * from tblaccount_insert(:_Username,:_Password,:_employee_id)";
            cmd = new NpgsqlCommand(sqlQuery, conn);

            lgn.Password = LoginSet.ComputeSha256Hash(lgn.Password);

            cmd.Parameters.AddWithValue("_Username", lgn.Username.ToLower());
            cmd.Parameters.AddWithValue("_Password", lgn.Password);
            cmd.Parameters.AddWithValue("_employee_id", lgn.EmployeeID);
            result = (int)cmd.ExecuteScalar();
            conn.Close();
            if (result == 1)
                return true;
            else
                return false;
        }
        private bool f_UpdateLogin(AccountUser datLogin)
        {
            int result;
            string sqlQuery = @"select * from tblAccount_update(
                               :_username,
                               :_password,
                               :_employee_id)";
            cmd = new NpgsqlCommand(sqlQuery, conn);

            datLogin.Password = LoginSet.ComputeSha256Hash(datLogin.Password);
            cmd.Parameters.AddWithValue("_username", NpgsqlTypes.NpgsqlDbType.Varchar, datLogin.Username);
            cmd.Parameters.AddWithValue("_password", NpgsqlTypes.NpgsqlDbType.Varchar, datLogin.Password);
            cmd.Parameters.AddWithValue("_employee_id", NpgsqlTypes.NpgsqlDbType.Varchar, datLogin.EmployeeID);
            result = (int)cmd.ExecuteScalar();
            conn.Close();
            if (result == 1)
                return true;
            else
                return false;
        }

        private AccountUser f_GetSingleDataAccount(string username)
        {
            AccountUser AccountDat = new AccountUser();
            string sqlQuery = @"select * from tblAccount_select() where username = '" + username + "'";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                AccountDat.Username = reader["username"].ToString();
                AccountDat.EmployeeID = reader["employee_id"].ToString();
            }
            return AccountDat;
        }
        private void connRealize()
        {
            conn = new NpgsqlConnection(connStr);
        }
    }
}
