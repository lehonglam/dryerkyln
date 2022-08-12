using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data;
using System.Configuration;

namespace Tdin20.tdinCode
{
    public class AccessDatabase
    {
        //private string strConectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //private string strConectString = config.connectionstring;//.ConnectionStrings["DefaultConnection"].ConnectionString;
        public SqlConnection getConect()
        {
            //config c = new config();
            string strConectString = "";
            return new SqlConnection(strConectString);
        }
        public DataTable getTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable(); ;
            try
            {
                //SqlConnection con = getConect();
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(dt);
                // Giai phong doi tuong
                ad.Dispose();
                //con.Close();
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, "HttpCall in AccessDatabase.cs");
                return null;
            }
            return dt;
        }
        // Hàm trả về datatable
        public DataTable getTable(string sql)
        {
            DataTable dt=new DataTable();;
            try
            {
                SqlConnection con = getConect();
                SqlDataAdapter ad = new SqlDataAdapter(sql, con);
                ad.Fill(dt);
                // Giai phong doi tuong
                ad.Dispose();
                con.Close();
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, "HttpCall in AccessDatabase.cs"); 
            }
            return dt;
        }
        //Hàm thực hiện execcuNonQuery
        public void ExeCuteNoQeury(string sql)
        {
            try
            {
                SqlConnection con = getConect();
                con.Open();
                SqlCommand com = new SqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
                com.Dispose();
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, "HttpCall in AccessDatabase.cs"); 
            }
        }

        // thực thi exec scalar
        public string ExecScalar(string sql)
        {
            try
            {

                SqlConnection con = getConect();
                con.Open();
                SqlCommand com = new SqlCommand(sql, con);
                string str = com.ExecuteScalar().ToString();
                con.Close();
                return str;
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, "HttpCall in AccessDatabase.cs");
            }
            return null;
        }
        //
        public SqlDataReader ExecuteReader(string sql)
        {

            SqlConnection con = getConect();
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataReader reader = com.ExecuteReader();
                return reader;
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, "HttpCall in AccessDatabase.cs");
            }
            return null;

        }
    }
}