using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
namespace Tdin20.tdinCode
{
    public class itemDataBase : abb_data
    {
        //public
        #region properties
        protected bool checkid = true;
        public int id
        {
            get { return convertText.ToInt(getStringItem(IndexIDName)); }
        }
        public int idtacgia
        {
            get{ return convertText.ToInt(getStringItem("idtacgia")); }
            set{ setDataItem(value, "idtacgia"); }
        }
        public DateTime ngaytao
        {
            get { return convertText.ToDateTime(getStringItem("ngaytao")); }
            set { setDataItem(value, "ngaytao"); }
        }
        // Protected
        protected DataRow dataRow;
        protected DataTable dataTable=null;
        protected string[] names = null;
        protected Type[] type = null;
        protected string[] namesbase = new string[] { "idtacgia","ngaytao" };
        protected Type[] typebase = new Type[] { typeof(int), typeof(DateTime) };
        string stest="";
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public itemDataBase() : base()
        {
            createDataSize();
            initDataAll();
        }
        protected virtual void createDataSize()
        {

        }
        protected void initDataAll()
        {
            insertcmd = inserSql();
            updatecmd = updateSql();
            initDataRow();
            initDefaultData();
        }
        protected virtual void initDefaultData() { }
        /// <summary>
        /// initdatarow: Hàm tạo danh sách dữ liệu, datarow
        /// </summary>
        protected void initDataRow()
        {
            if (names == null) return;
            dataTable = new DataTable();
            for (int i = 0; i < names.Count(); i++)
            {
                stest += names[i] + ",";
                 dataTable.Columns.Add(names[i]);
            }
            foreach (string s in namesbase)
            {
                stest += s + ",";
                dataTable.Columns.Add(s);
            }
            dataRow = dataTable.NewRow();
        }
        /// updateSql: Hàm tạo câu lệnh UPDATE to UPDATE
        /// vd: Update sanpham set name=@name,ngaysinh=@ngaisinh
        /// </summary>
        /// <returns></returns>
        protected string updateSql()
        {
            if (names == null) return "";
            string strupdatecmd = @"UPDATE " + tablename + " SET ";
            string s = "";
            
            for (int i = 0; i < names.Count(); i++)
            {
                if (i < names.Count() - 1) s = names[i] + "=@" + names[i] + ",";
                else s = names[i] + "=@" + names[i];
                strupdatecmd += s;

            }
            if (strupdatecmd.Length != 0 && namesbase.Count()>0)
                strupdatecmd += ",";
            for (int i = 0; i < namesbase.Count(); i++)
            {
                if (i < namesbase.Count() - 1) s = namesbase[i] + "=@" + namesbase[i] + ",";
                else s = namesbase[i] + "=@" + namesbase[i];
                strupdatecmd += s;

            }
            strupdatecmd += " ";
            return strupdatecmd;
        }
        /*
         * updateSql: Hàm tạo câu lệnh insert to insert
         */
        protected string inserSql()
        {
            if (names == null) return "";
            string strupdatecmd = @"insert into  ";
            string value = " values( ";
            string tbname = tablename+"( ";

            string sv = "",st="" ;
            
            for (int i = 0; i < names.Count(); i++)
            {
                if (i < names.Count() - 1)
                {
                    st = names[i] + ",";
                    sv = "@" + names[i] + ",";
                }
                else
                {
                    st = names[i];
                    sv = "@" + names[i];
                }
                value+=sv;
                tbname+=st;
                //strupdatecmd += s;
            }
            if (value.Length != 0 && namesbase.Count() > 0)
                value += ",";
            if (tbname.Length != 0 && namesbase.Count() > 0)
                tbname += ",";
            for (int i = 0; i < namesbase.Count(); i++)
            {
                if (i < namesbase.Count() - 1)
                {
                    st = namesbase[i] + ",";
                    sv = "@" + namesbase[i] + ",";
                }
                else
                {
                    st = namesbase[i];
                    sv = "@" + namesbase[i];
                }
                value += sv;
                tbname += st;
                //strupdatecmd += s;
            }
            value+=")";
            tbname+= ")";
            strupdatecmd += tbname;
            strupdatecmd += value;
            return strupdatecmd;
        }
        protected virtual string createSql() { return ""; }
        SqlDbType convertSqlType(Type t)
        {
            SqlDbType s = SqlDbType.NVarChar;

            if (t == typeof(DateTime))
            {
                s = SqlDbType.DateTime;
            }
            else if (t == typeof(int))
            {
                s = SqlDbType.Int;
            }
            else if (t == typeof(float))
            {
                s = SqlDbType.Float;

            }
            else if (t == typeof(double))
            {
                s = SqlDbType.Float;
            }
            return s;
        }
        protected override void AddParameter(ref SqlCommand cmd)
        {
            string s = "";
            //string value = "";
            for (int i = 0; i < names.Count(); i++)
            {
                s = names[i];
                string tenbien="@"+s;
                SqlDbType st = convertSqlType(type[i]);
                cmd.Parameters.Add(tenbien, st);
                cmd.Parameters[tenbien].Value = dataRow[s];
            }
            for (int i = 0; i < namesbase.Count(); i++)
            {
                s = namesbase[i];
                string tenbien = "@" + s;
                SqlDbType st = convertSqlType(typebase[i]);
                cmd.Parameters.Add(tenbien, st);
                cmd.Parameters[tenbien].Value = dataRow[s];
            }
        }
        //Them Moi Du Lieu vao Database
        public override bool addnew()
        {
            return base.addnew(); 
        }
        // Xoa item tu database
        public override int Xoa(int ID)
        {

            return base.Xoa(tablename, IndexIDName, ID);
        }

        // Sua Du Lieu
        public override int UpdateToDatabase(int IDin)
        {
            return base.UpdateToDatabase(IDin);
        }
        /*
         * GetData: lấy datatable tất cả các trường, của một mục theo id
         */
        public override int  GetData(int IDin)
        {
            try
            {
                AccessDatabase ac = new AccessDatabase();
                string sql = "select * from " + tablename + " where " + IndexIDName + "=" + IDin;
                DataTable dt = ac.getTable(sql);
                if (dt.Rows.Count > 0)
                {
                    dataRow = dt.Rows[0];
                }
                else return 0;
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e,"Lỗi dọc data base");
                return 0;
            }
            return 1;
        }
        public bool    setDataItem(object value, string name)
        {
            if(value!=null)
                dataRow[name] = value;
            return true;
        }
        public virtual string getStringItem(string name)
        {
            if (dataRow[name] != null)
            {
                return dataRow[name].ToString();
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDin"></param>
        /// <param name="idNhom"></param>
        /// <param name="tableName"></param>
        /// <param name="fieldParentName"></param>
        /// <param name="fieldIndexName"></param>
        /// <returns></returns>
        private int thaynhom(int IDin, int idNhom, string fieldParentName, string tableName, string fieldIndexName)
        {
            AccessDatabase ac = new AccessDatabase();
            SqlConnection con = ac.getConect();
            try
            {
                con.Open();
                // Tao mot addapter
                SqlCommand cmd;
                string update = "UPDATE " + tableName + " SET " + fieldParentName + "=@idNhom where " + fieldIndexName + "=@IDin";

                cmd = new SqlCommand(update, con);
                AddParameter(ref cmd);
                cmd.Parameters.AddWithValue("@idNhom", idNhom);
                cmd.Parameters.AddWithValue("@IDin", IDin);
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
        protected int thaynhom(int IDin, int idNhom, string fieldParentName)
        {
            return thaynhom(IDin, idNhom, fieldParentName, tablename, IndexIDName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDinIn"> value id</param>
        /// <param name="indexNameIn">id name</param>
        /// <param name="tablename">table name</param>
        /// <param name="parentName">parentFile parent</param>
        /// <returns></returns>
        private int GetParent(int IDinIn, string parentName, string indexNameIn, string tablename)
        {
            AccessDatabase ac = new AccessDatabase();
            string sql = "select " + parentName + " from " + tablename + " where " + indexNameIn + "=@IDinIn";// +IDin;
            SqlCommand cmd = new SqlCommand(sql, ac.getConect());
            cmd.Parameters.AddWithValue("@IDinIn", IDinIn);
            DataTable dt = ac.getTable(cmd);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                if (row[parentName] != null)
                {
                    string str = row[parentName].ToString();
                    if (str.Length != 0)
                    {
                        int parent = convertText.ToInt(str);
                        return parent;
                    }
                }

            }
            return -1;
        }

        protected int GetParent(int IDin,string parentName)
        {
            return GetParent(IDin,parentName, IndexIDName, tablename);
        }
        // ghi ra tep
        public virtual void readConfig(string fileConfigName)
        {
            // neu tep chua co thi
            if (false == System.IO.File.Exists(fileConfigName))
            {
                System.IO.File.Create(fileConfigName);
                return;
            }
            string[] lines = System.IO.File.ReadAllLines(fileConfigName);
            for (int i = 0; i < names.Count(); i++)
            {
                if (i < lines.Count())
                    setDataItem(lines[i],names[i]);
            }
        }
        public virtual void writeConfig(string fileConfigName)
        {
            int n = names.Count();
            string[] scontent = new string[n];

            for (int i = 0; i < n; i++)
            {
                scontent[i] = getStringItem(names[i]);
            }
            System.IO.File.WriteAllLines(fileConfigName, scontent);

        }
    }
}
