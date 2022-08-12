using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Tdin20.tdinCode
{
    public class SayDataBase
    {
 //public
        #region properties
        protected string[] sname = null;
        protected Dictionary<string, object> data;
        protected Dictionary<string, Type> dataType;
        #endregion

        public SayDataBase() : base()
        {
            data = new Dictionary<string, object>();
            dataType = new Dictionary<string, Type>();
            createDataSize();
        }
        protected virtual void createDataSize()
        {

        }
        protected void setValue(string name, string value)
        {
            Type t = dataType[name];
            if (t == typeof(int))
                data[name] = convertText.ToInt(value);
            else if (t == typeof(float))
                data[name] = convertText.ToFloat(value);
            else if (t == typeof(double))
                data[name] = convertText.ToDouble(value);
            else if (t == typeof(string))
                data[name] = value;
            else if (t == typeof(DateTime))
                data[name] = convertText.ToDateTime(value);
        }
        public virtual object getStringItem(string name)
        {
            return data[name];
        }

        public virtual void readConfig(string fileConfigName)
        {
            // neu tep chua co thi
            if (false == System.IO.File.Exists(fileConfigName))
            {
                System.IO.File.Create(fileConfigName);
                return;
            }
            string[] lines = System.IO.File.ReadAllLines(fileConfigName);
            for (int i = 0; i < sname.Count(); i++)
            {
                if (i < lines.Count())
                    setValue(sname[i], lines[i]);
            }
        }
        public virtual void writeConfig(string fileConfigName)
        {
            int n = data.Count;
            string[] scontent = new string[n];

            for (int i = 0; i < n; i++)
            {
                scontent[i] = data[sname[i]].ToString();
            }
            System.IO.File.WriteAllLines(fileConfigName, scontent);

        }
    }
}
