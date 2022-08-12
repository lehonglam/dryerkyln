using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Tdin20.tdinCode
{
    class docCom
    {
        

        string codeMovanphunAm = "A", codeDongvanphunAm = "0";
        string codeMovanNhiet = "B", codeDongvanNhiet = "1";
        string codeQuatXuoi = "C", codeDongQuatXuoi = "2";
        string codeQuatNguoc = "D", codeDongQuatNGuoc = "3";
        private SerialPort port =null;
        public docCom()
        {
            port = new SerialPort(QuyTrinhSayGo.configTdin.com, 9600, Parity.None, 8, StopBits.One);
        }
        public int Open()
        {
            if (port.IsOpen)
            {
                return 0;
            }
            try
            {
                port.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening port: {0}", ex.Message);
                return 0;
            } 
            return 1;
        }
        public int Close()
        {
            port.Close();
            return 1;

        }
        public int readPort()
        {
            int result = 1;
            return result;
        }
        public string docString()
        {
            if (Open() == 0) return "Không mở được cổng";
            string str = port.ReadLine();
            Close();
            return str;
        }
        public int docNhietDoDoAm(ref float nhietDo, ref float doam)
        {
            // doc du lieu tu cong com
            if (Open() == 0) return 0;
            string str=port.ReadLine();
            // kiểm tra str hợp lệ mới thực hiện
            //str = "tem??r?hsdfgsdhg;";
            //
            int x=str.LastIndexOf("temp: ");
            if (x < 0) return 0;
            x=str.LastIndexOf("humi: ");
            if ( x< 0) return 0;

            //temp: 29.1C humi: 89.0%
            string nhietdoStr = str.Substring(str.LastIndexOf("temp: ")+6,4);
            string doamStr = str.Substring(str.LastIndexOf("humi: ")+6, 4);
            nhietDo = convertText.ToFloat(nhietdoStr);
            doam = convertText.ToFloat(doamStr);

            Close();

            return 1;
        }
        public int docDoAmGo(ref float doamGo)
        {
            // doc du lieu tu cong com
            if (Open() == 0) return 0;
            return Close();
        }
        public int sentCode(string code)
        {
            // doc du lieu tu cong com
            if (Open() == 0) return 0;

            port.Write(code);

            //
            port.Close();
            return 1;
        }

        public int movanPhunAm()
        {
            // doc du lieu tu cong com
            return sentCode(codeMovanphunAm);
        }
        public int DongvanPhunAm()
        {
            return sentCode(codeDongvanphunAm);
        }
        public int movanNhiet()
        {
            return sentCode(codeMovanNhiet);
        }
        public int DongvanNhiet()
        {
            return sentCode(codeDongvanNhiet);
        }
        public int ChayXuoiQuat()
        {
            // dong ca quat xuoi va nguoc
            return sentCode(codeQuatXuoi);
        }
        public int ChayNguocQuat()
        {
            return sentCode(codeQuatNguoc);
        }
        public int tatQuat()
        {
            if(0==sentCode(codeDongQuatXuoi)) return 0;
            return sentCode(codeDongQuatNGuoc);
        }
    }
}
