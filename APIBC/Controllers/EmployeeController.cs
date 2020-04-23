using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIBC.Models;
using System.Data;
using System.Web;
using System.IO;

namespace APIBC.Controllers
{
    [Authorize] //Ini pake token bearer ya syg
    public class EmployeeController : ApiController
    {
        private string connStr = ConstApps.ConnectionPostgre;
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;

        [HttpGet]
        public HttpResponseMessage GetListEmployee()
        {
            try
            {
                connRealize();
                conn.Open();
                var getEmployee = f_GetDataEmployee();
                return Request.CreateResponse(HttpStatusCode.OK, getEmployee);
            }
            catch (Exception ex)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage CreateEmployee([FromBody]EmployeeData datEmployee)
        {
            try
            {
                connRealize();
                conn.Open();
                bool checkInsert = f_CreateEmployee(datEmployee);
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
        public HttpResponseMessage UploadImage()
        {
            try
            {
                string imageName = null;
                var httpRequest = HttpContext.Current.Request;
                // Upload image
                var postedFile = httpRequest.Files["Image"];
                imageName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                imageName += "-" + DateTime.Now.ToString("yyyyMMdd-hhmmss") + Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
                postedFile.SaveAs(filePath);

                return Request.CreateResponse(HttpStatusCode.OK, imageName);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage RemoveImage(string Filename)
        {
            try
            {
                var filePath = HttpContext.Current.Server.MapPath("~/Image/" + Filename);
                File.Delete(filePath);
                return Request.CreateResponse(HttpStatusCode.OK, "Success Delete Image");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateEmployee([FromBody]EmployeeData datEmployee)
        {
            try
            {
                connRealize();
                conn.Open();
                bool checkInsert = f_UpdateEmployee(datEmployee);
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
        [HttpDelete]
        public HttpResponseMessage DeleteEmployee(string Id)
        {
            try
            {
                connRealize();
                conn.Open();
                bool checkInsert = f_DeleteEmployee(Id);
                if (checkInsert)
                    return Request.CreateResponse(HttpStatusCode.OK, "Success full delete data.");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, "Fail delete data. Check parameters again.");
            }
            catch (Exception ex)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetSingleEmployee(string Id)
        {
            try
            {
                connRealize();
                conn.Open();
                var getEmployee = f_GetSingleDataEmployee(Id);
                return Request.CreateResponse(HttpStatusCode.OK, getEmployee);
            }
            catch (Exception ex)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }

        private void connRealize()
        {
            conn = new NpgsqlConnection(connStr);
        }


        /// Private Function/ Method Employee
        /// Just in case you need some code, write here.
        private List<EmployeeData> f_GetDataEmployee()
        {
            List<EmployeeData> lst = new List<EmployeeData>();
            DataTable dt = new DataTable();
            string sqlQuery = @"select * from tblEmployee_select() order by employee_id";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            dt.Load(cmd.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                EmployeeData EmployeeDat = new EmployeeData();
                EmployeeDat.EmployeeID = dr["employee_id"].ToString();
                EmployeeDat.EmployeeName = dr["employee_name"].ToString();
                EmployeeDat.Gender = char.Parse(dr["gender"].ToString());
                EmployeeDat.BirthDate = DateTime.Parse(dr["birth_date"].ToString());
                EmployeeDat.Email = dr["email"].ToString();
                EmployeeDat.ContactPhone = dr["contact_phone"].ToString();
                EmployeeDat.Address = dr["address"].ToString();
                EmployeeDat.Position = dr["position_id"].ToString();
                EmployeeDat.Division = dr["division_id"].ToString();
                EmployeeDat.SupervisorID = dr["supervisor_id"].ToString();
                EmployeeDat.Picture = dr["image_path"].ToString();
                lst.Add(EmployeeDat);
            }

            return lst;
        }
        private EmployeeData f_GetSingleDataEmployee(string szEmployeeId)
        {
            EmployeeData EmployeeDat = new EmployeeData();
            string sqlQuery = @"select * from tblEmployee_select() where employee_id = '" + szEmployeeId + "'";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                EmployeeDat.EmployeeID = reader["employee_id"].ToString();
                EmployeeDat.EmployeeName = reader["employee_name"].ToString();
                EmployeeDat.Gender = char.Parse(reader["gender"].ToString());
                EmployeeDat.BirthDate = DateTime.Parse(reader["birth_date"].ToString());
                EmployeeDat.Email = reader["email"].ToString();
                EmployeeDat.ContactPhone = reader["contact_phone"].ToString();
                EmployeeDat.Address = reader["address"].ToString();
                EmployeeDat.Position = reader["position_id"].ToString();
                EmployeeDat.Division = reader["division_id"].ToString();
                EmployeeDat.SupervisorID = reader["supervisor_id"].ToString();
                EmployeeDat.Picture = reader["image_path"].ToString();
            }
            return EmployeeDat;
        }
        private bool f_CreateEmployee(EmployeeData Employee)
        {
            int result;
            string sqlQuery = @"select * from tblemployee_insert(
                               :_employee_id,
                               :_employee_name,
                               :_gender,
                               :_birth_date,
                               :_email,
                               :_contact_phone,
                               :_address,
                               :_position_id,
                               :_division_id,
                               :_supervisor_id,
                               :_image_path)";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("_employee_id", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.EmployeeID);
            cmd.Parameters.AddWithValue("_employee_name", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.EmployeeName);
            cmd.Parameters.AddWithValue("_gender", NpgsqlTypes.NpgsqlDbType.Char, Employee.Gender);
            cmd.Parameters.AddWithValue("_birth_date", NpgsqlTypes.NpgsqlDbType.Date, Employee.BirthDate);
            cmd.Parameters.AddWithValue("_email", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.Email);
            cmd.Parameters.AddWithValue("_contact_phone", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.ContactPhone);
            cmd.Parameters.AddWithValue("_address", NpgsqlTypes.NpgsqlDbType.Text, Employee.Address);
            cmd.Parameters.AddWithValue("_position_id", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.Position);
            cmd.Parameters.AddWithValue("_division_id", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.Division);
            cmd.Parameters.AddWithValue("_supervisor_id", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.SupervisorID);
            cmd.Parameters.AddWithValue("_image_path", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.Picture);
            

            result = (int)cmd.ExecuteScalar();
            conn.Close();
            if (result == 1)
                return true;
            else
                return false;
        }
        private bool f_UpdateEmployee(EmployeeData Employee)
        {
            int result;
            string sqlQuery = @"select * from tblemployee_update(
                               :_employee_id,
                               :_employee_name,
                               :_gender,
                               :_birth_date,
                               :_email,
                               :_contact_phone,
                               :_address,
                               :_position_id,
                               :_division_id,
                               :_supervisor_id,
                               :_image_path)";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("_employee_id", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.EmployeeID);
            cmd.Parameters.AddWithValue("_employee_name", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.EmployeeName);
            cmd.Parameters.AddWithValue("_gender", NpgsqlTypes.NpgsqlDbType.Char, Employee.Gender);
            cmd.Parameters.AddWithValue("_birth_date", NpgsqlTypes.NpgsqlDbType.Date, Employee.BirthDate);
            cmd.Parameters.AddWithValue("_email", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.Email);
            cmd.Parameters.AddWithValue("_contact_phone", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.ContactPhone);
            cmd.Parameters.AddWithValue("_address", NpgsqlTypes.NpgsqlDbType.Text, Employee.Address);
            cmd.Parameters.AddWithValue("_position_id", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.Position);
            cmd.Parameters.AddWithValue("_division_id", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.Division);
            cmd.Parameters.AddWithValue("_supervisor_id", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.SupervisorID);
            cmd.Parameters.AddWithValue("_image_path", NpgsqlTypes.NpgsqlDbType.Varchar, Employee.Picture);
            result = (int)cmd.ExecuteScalar();
            conn.Close();
            if (result == 1)
                return true;
            else
                return false;
        }
        private bool f_DeleteEmployee(string Id)
        {
            int result;
            string sqlQuery = @"select * from tblemployee_delete(:_employee_id)";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("_employee_id", Id);
            result = (int)cmd.ExecuteScalar();
            conn.Close();
            if (result == 1)
                return true;
            else
                return false;
        }

    }
}
