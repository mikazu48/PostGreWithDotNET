using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIBC.Models;

namespace APIBC.Controllers
{
    [Authorize] //Ini pake token bearer ya syg
    public class AreaController : ApiController
    {
        private string connStr = ConstApps.ConnectionPostgre;
        private NpgsqlConnection conn;
        private NpgsqlCommand cmd;

        //[MyAuthentication] ini kalo kita pake nya basic authentication cuma naro tokennya
        [HttpGet]
        public HttpResponseMessage GetListArea()
        {
            try
            {
                connRealize();
                conn.Open();
                var getArea = f_GetDataArea();
                return Request.CreateResponse(HttpStatusCode.OK, getArea);
            }
            catch (Exception ex)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetSingleArea(string Id)
        {
            try
            {
                connRealize();
                conn.Open();
                var getArea = f_GetSingleDataArea(Id);
                return Request.CreateResponse(HttpStatusCode.OK, getArea);
            }
            catch (Exception ex)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There error :" + ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage CreateArea([FromBody]AreaData datArea)
        {
            try
            {
                connRealize();
                conn.Open();
                bool checkInsert = f_CreateArea(datArea);
                if(checkInsert)
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

        [HttpPut]
        public HttpResponseMessage UpdateArea([FromBody]AreaData datArea)
        {
            try
            {
                connRealize();
                conn.Open();
                bool checkInsert = f_UpdateArea(datArea);
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
        public HttpResponseMessage DeleteArea(string Id)
        {
            try
            {
                connRealize();
                conn.Open();
                bool checkInsert = f_DeleteArea(Id);
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
        /// Private Function/ Method Area
        /// Just in case you need some code, write here.
        private List<AreaData> f_GetDataArea()
        {
            List<AreaData> lst = new List<AreaData>();
            DataTable dt = new DataTable();
            string sqlQuery = @"select * from tblArea_select() order by area_id";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            dt.Load(cmd.ExecuteReader());
            foreach (DataRow dr in dt.Rows)
            {
                AreaData area = new AreaData();
                area.AreaID = dr["area_id"].ToString();
                area.AreaName = dr["area_name"].ToString();
                area.Regional = dr["regional"].ToString();
                area.District = dr["District"].ToString();
                lst.Add(area);
            }

            return lst;
        }
        private AreaData f_GetSingleDataArea(string szAreaId)
        {
            AreaData areaDat = new AreaData();
            string sqlQuery = @"select * from tblArea_select() where area_id = '" + szAreaId + "'";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                areaDat.AreaID = reader["area_id"].ToString();
                areaDat.AreaName = reader["area_name"].ToString();
                areaDat.Regional = reader["regional"].ToString();
                areaDat.District = reader["District"].ToString();
            }
            return areaDat;
        }
        private bool f_CreateArea(AreaData area)
        {
            int result;
            string sqlQuery = @"select * from tblArea_insert(:_area_id,:_area_name,:_regional,:_district)";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("_area_id", area.AreaID);
            cmd.Parameters.AddWithValue("_area_name", area.AreaName);
            cmd.Parameters.AddWithValue("_regional", area.Regional);
            cmd.Parameters.AddWithValue("_district", area.District);
            result = (int)cmd.ExecuteScalar();
            conn.Close();
            if (result == 1)
                return true;
            else
                return false;
        }
        private bool f_UpdateArea(AreaData area)
        {
            int result;
            string sqlQuery = @"select * from tblArea_update(:_area_id,:_area_name,:_regional,:_district)";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("_area_id", area.AreaID);
            cmd.Parameters.AddWithValue("_area_name", area.AreaName);
            cmd.Parameters.AddWithValue("_regional", area.Regional);
            cmd.Parameters.AddWithValue("_district", area.District);
            result = (int)cmd.ExecuteScalar();
            conn.Close();
            if (result == 1)
                return true;
            else
                return false;
        }
        private bool f_DeleteArea(string Id)
        {
            int result;
            string sqlQuery = @"select * from tblArea_delete(:_area_id)";
            cmd = new NpgsqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("_area_id", Id);
            result = (int)cmd.ExecuteScalar();
            conn.Close();
            if (result == 1)
                return true;
            else
                return false;
        }


        private void connRealize()
        {
            conn = new NpgsqlConnection(connStr);
        }
    }


}
