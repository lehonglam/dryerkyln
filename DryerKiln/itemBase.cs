using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Tdin20.tdinCode
{
    public class itemBase : itemDataBase
    {
        public itemBase()
            : base()
        {}
        public int thaynhom(int IDin, int idNhom,string nameofIdNhom)
        {
            // string update = "UPDATE "+tablename+" SET  NhomSanphamID=" + idNhom + " where "+IndexIDName+"=" + IDin;
            AccessDatabase ac = new AccessDatabase();
            SqlConnection con = ac.getConect();
            try
            {
                con.Open();
                // Tao mot addapter
                SqlCommand cmd;
                string update = "UPDATE "+tablename+" SET  "+nameofIdNhom+"=" + idNhom + " where "+IndexIDName+"=" + IDin;
                cmd = new SqlCommand(update, con);
                AddParameter(ref cmd);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // Log the exception.
                ExceptionUtility.LogException(e, "abb_data:UpdateToDatabase()");
                return 0;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return 1;
        }
        public int getnhom(int IDin, string nameofIdNhom)
        {//string sql = "select NhomSanphamID from vtVattu where ProductID =@IDin";// +IDin;
            try
            {
                AccessDatabase ac = new AccessDatabase();
                string sql = "select "+nameofIdNhom+" from "+tablename+" where "+IndexIDName+" =@IDin";// +IDin;
                SqlCommand cmd = new SqlCommand(sql, ac.getConect());
                cmd.Parameters.AddWithValue("@IDin", IDin);
                DataTable dt = ac.getTable(cmd);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return convertText.ToInt(row[nameofIdNhom].ToString());
                }
            }
            catch (Exception e)
            {
                // Log the exception.
                ExceptionUtility.LogException(e, "abb_data:UpdateToDatabase()");
                return 0;
            }
            return 1;
        }
    }
}
