using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data;

namespace Tdin20.tdinCode
{
    public class abb_data
    {
        protected int id { get; set; }
        protected string tablename { get; set; }
        protected string IndexIDName { get; set; }
        // sql command
        protected string addnewcmd { get; set; }
        protected string insertcmd { get; set; }
        public string    updatecmd { get; set; }
        protected string selectcmd { get; set; }
        protected string deletecmd{ get; set; }

        public abb_data()
        {
            tablename = "";
            IndexIDName = "ID";
            id = 0;
        }
        public int Id() { return id; }
        public int MaxID()
        {
            AccessDatabase ac = new AccessDatabase();
            int kq = 0;
            string sql = "Select Max(" + IndexIDName + ") from " + tablename;
            try
            {
                kq = int.Parse(ac.ExecScalar(sql));
            }
            catch
            {
                kq = 0;
            }
            return kq;
        }
        protected virtual void AddParameter(ref SqlCommand cmd)
        {
        }
        public virtual bool addnew()
        {
            AccessDatabase ac = new AccessDatabase();
            SqlConnection con = ac.getConect();
            try
            {
                con.Open();
                // Tao mot addapter
                SqlCommand cmd = new SqlCommand(insertcmd, con);
                AddParameter(ref cmd);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // Log the exception.
                ExceptionUtility.LogException(e, "abb_data: add new");
                return false;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                id = MaxID();
            }
            return true;
        }
        public virtual int InsertToDatabase()
        {
            id = MaxID() + 1;
            AccessDatabase ac = new AccessDatabase();
            SqlConnection con = ac.getConect();
            try
            {
                con.Open();
                // Tao mot addapter
                SqlCommand cmd = new SqlCommand(insertcmd, con);
                AddParameter(ref cmd);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // Log the exception.
                ExceptionUtility.LogException(e, "abb_data:InsertToDatabase()");
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
        public virtual int UpdateToDatabase(int IDin)
            // update theo update comnad dat o ngoai doi tuong
        {
            AccessDatabase ac = new AccessDatabase();
            SqlConnection con = ac.getConect();
            try
            {
                con.Open();
                // Tao mot addapter
                SqlCommand cmd;
                string update = updatecmd + " where " + IndexIDName + "=" + IDin;
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
        public virtual int UpdateToDatabase(string update)
        // update theo update comnad dat o ngoai doi tuong
        {
            AccessDatabase ac = new AccessDatabase();
            SqlConnection con = ac.getConect();
            try
            {
                con.Open();
                // Tao mot addapter
                SqlCommand cmd;
                cmd = new SqlCommand(update, con);
                AddParameter(ref cmd);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // Log the exception.
                ExceptionUtility.LogException(e, "abb_data:UpdateToDatabase()");
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
        public virtual int GetData(int ID)
        {
            return 1;
        }
        public bool getDataItem(ref string value, string name, DataRow row)
        {
            if (row[name] != null)
            {
                value = row[name].ToString();
                return true;
            }
            return false;
        }
        public bool getDataItem(ref double value, string name, DataRow row)
        {
            if (row[name] != null)
            {
                string str = row[name].ToString();
                if (str.Length != 0)
                {
                    value = double.Parse(str);
                    return true;
                }
            }
            return false;
        }
        public bool getDataItem(ref int value, string name, DataRow row)
        {
            if (row[name] != null)
            {
                string str = row[name].ToString();
                if (str.Length != 0)
                {
                    value = int.Parse(str);
                    return true;
                }
            }
            return false;
        }
        public bool getDataItem(ref DateTime value, string name, DataRow row)
        {
            if (row[name] != null)
            {
                string str = row[name].ToString();
                if (str.Length != 0)
                {
                    value = DateTime.Parse(str);
                    return true;
                }
            }
            return false;
        }
        public virtual DataTable getTable(string sql)
        {
            AccessDatabase ac = new AccessDatabase();
            return ac.getTable(sql);
        }
        public int Xoa(string tablenameIn, string indexnameIn, int ID)
        {
            AccessDatabase ac = new AccessDatabase();
            SqlConnection con = ac.getConect();
            try
            {
                con.Open();
                // Tao mot addapter
                string sql = "delete from " + tablenameIn + " WHERE " + indexnameIn + "= @" + indexnameIn;
                SqlCommand cmd;
                cmd = new SqlCommand(sql, con);
                string strindex = "@" + indexnameIn;
                cmd.Parameters.AddWithValue(strindex, ID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // Log the exception.
                ExceptionUtility.LogException(e, "abb_data:Xoa(string tablenameIn, string indexnameIn, int ID)");
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
        public virtual int Xoa(int ID)
        {
            return Xoa(tablename, IndexIDName, ID);
        }
        public virtual bool KiemTraDaCoChua(string sql)
        {
            AccessDatabase ac = new AccessDatabase();
            DataTable dt= ac.getTable(sql);
            if(dt!=null)
            {
                return dt.Rows.Count>0;
            }
            return false;
        }
   }
}