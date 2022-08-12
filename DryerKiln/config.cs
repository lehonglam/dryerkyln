using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tdin20.tdinCode
{
    public class config : SayDataBase
    {
        #region properties
        public string user
        {
            get { return data["user"].ToString(); }
            set { data["user"] = value; }
        }
        public string pass
        {
            get { return data["pass"].ToString(); }
            set { data["pass"] = value; }
        }
        public string datasource
        {
            get { return data["datasource"].ToString(); }
            set { data["datasource"] = value; }
        }
        public string catalog
        {
            get { return data["catalog"].ToString(); }
            set { data["catalog"] = value; }
        }
        public string host
        {
            get { return data["host"].ToString(); }
            set { data["host"] = value; }
        }
        public string hostUser
        {
            get { return data["hostUser"].ToString(); }
            set { data["hostUser"] = value; }
        }
        public string hostPass
        {
            get 
            {
                string s=data["hostPass"].ToString();
                return s;
            }
            set 
            { 
                string s = value;
                data["hostPass"] = s;
            }
        }
        public string fileConfigName
        {
            get { return data["fileConfigName"].ToString(); }
            set { data["fileConfigName"] = value; }
        }
        public string com
        {
            get { return data["com"].ToString(); }
            set { data["com"] = value; }
        }
        public int thoigiandocCom
        {
            get { return convertText.ToInt(data["thoigiandocCom"].ToString()); }
            set { data["thoigiandocCom"] = value; }
        }
        public int thoigiandaochieuquat
        {
            get { return convertText.ToInt(data["thoigiandaochieuquat"].ToString()); }
            set { data["thoigiandaochieuquat"] = value; }
        }
        public int thoigiannghiChoBatQuat
        {
            get { return convertText.ToInt(data["thoigiannghiChoBatQuat"].ToString()); }
            set { data["thoigiannghiChoBatQuat"] = value; }
        }
        public int thoigianchayquatsautatlo
        {
            get { return convertText.ToInt(data["thoigianchayquatsautatlo"].ToString()); }
            set { data["thoigianchayquatsautatlo"] = value; }
        }
        public int thoigianChodungPhunAm
        {
            get { return convertText.ToInt(data["thoigianChodungPhunAm"].ToString()); }
            set { data["thoigianChodungPhunAm"] = value; }
        }
        public int thoigianChayQuat
        {
            get { return convertText.ToInt(data["thoigianChayQuat"].ToString()); }
            set { data["thoigianChayQuat"] = value; }
        }
        public config():base()
        {
        }
        #endregion
        protected override void createDataSize()
        {
            mahoastringtheoCrypto mahoa = new mahoastringtheoCrypto();
            sname=new string[15];
            int i = 0;
            data.Add("user", "lehonglam");
            dataType.Add("user",typeof(string));
            sname[i] = "user"; i++;

            data.Add("pass","honglamhong123"); 
            dataType.Add("pass",typeof(string));
            sname[i] = "pass"; i++;

            data.Add("datasource","112.213.89.17"); dataType.Add("datasource",typeof(string));
            sname[i] = "datasource"; i++;

            data.Add("catalog","tdin20"); dataType.Add("catalog",typeof(string));
            sname[i]="catalog"; i++;

            data.Add("host","www.td-in.com"); dataType.Add("host",typeof(string));
            sname[i]="host"; i++;

            data.Add("hostUser","td_in"); dataType.Add("hostUser",typeof(string));
            sname[i]="hostUser"; i++;

            data.Add("hostPass","honglamhong123"); dataType.Add("hostPass",typeof(string));
            sname[i]="hostPass"; i++;

            data.Add("fileConfigName","tdinDryerKiln.con"); dataType.Add("fileConfigName",typeof(string));
            sname[i]="fileConfigName"; i++;

            data.Add("com","COM3"); dataType.Add("com",typeof(string));
            sname[i]="com"; i++;

            data.Add("thoigiandocCom",5 * 1000); dataType.Add("thoigiandocCom",typeof(int));
            sname[i]="thoigiandocCom"; i++;

            data.Add("thoigiandaochieuquat",10 * 1000); dataType.Add("thoigiandaochieuquat",typeof(int));
            sname[i]="thoigiandaochieuquat"; i++;

            data.Add("thoigiannghiChoBatQuat",2 * 1000); dataType.Add("thoigiannghiChoBatQuat",typeof(int));
            sname[i]="thoigiannghiChoBatQuat"; i++;

            data.Add("thoigianchayquatsautatlo",6 * 1000); dataType.Add("thoigianchayquatsautatlo",typeof(int));
            sname[i]="thoigianchayquatsautatlo"; i++; 
            
            data.Add("thoigianChodungPhunAm",30 * 1000); dataType.Add("thoigianChodungPhunAm",typeof(int));
            sname[i]="thoigianChodungPhunAm"; i++; 
            
            data.Add("thoigianChayQuat",15 * 1000); dataType.Add("thoigianChayQuat",typeof(int));
            sname[i] = "thoigianChayQuat"; i++;
        }
        public string connectionString()
        {
            //public string connectionstring = "Data Source=112.213.89.17;Initial Catalog=tdin20;User ID=lehonglam;Password=honglamhong123";
            string connectionstring = "Data Source=" + datasource + ";";
            connectionstring += "Initial Catalog=" + catalog + ";";
            connectionstring += "User ID=" + user + ";";
            connectionstring += "Password=" + pass + ";";
            return connectionstring;
        }
        public void readConfig()
        {
            readConfig(fileConfigName);
            mahoastringtheoCrypto mahoa = new mahoastringtheoCrypto();
            data["pass"] = mahoa.DecryptQueryString(data["pass"].ToString());
            data["hostPass"] = mahoa.DecryptQueryString(data["hostPass"].ToString());
            data["hostUser"] = mahoa.DecryptQueryString(data["hostUser"].ToString());
            data["user"] = mahoa.DecryptQueryString(data["user"].ToString());
            data["datasource"] = mahoa.DecryptQueryString(data["datasource"].ToString());
            data["catalog"] = mahoa.DecryptQueryString(data["catalog"].ToString());
        }
        public void writeConfig()
        {
            Dictionary<string, object> dataCopy = data;
            mahoastringtheoCrypto mahoa=new mahoastringtheoCrypto();
            dataCopy["pass"]=mahoa.EncryptQueryString(data["pass"].ToString());
            dataCopy["hostPass"] = mahoa.EncryptQueryString(data["hostPass"].ToString());
            dataCopy["hostUser"] = mahoa.EncryptQueryString(data["hostUser"].ToString());
            dataCopy["user"] = mahoa.EncryptQueryString(data["user"].ToString());
            dataCopy["datasource"] = mahoa.EncryptQueryString(data["datasource"].ToString());
            dataCopy["catalog"] = mahoa.EncryptQueryString(data["catalog"].ToString());

            int n = data.Count;
            string[] scontent = new string[n]; 
            for (int i = 0; i < n; i++)
            {
                scontent[i] = dataCopy[sname[i]].ToString();
            }
            System.IO.File.WriteAllLines(fileConfigName, scontent);

        }
    }
}
