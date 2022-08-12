using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tdin20.tdinCode
{
    // lớp dư liệu sấy
    public class dryerLibLocal : SayDataBase
    {
        #region DuLieuDauVao
        public string Ten
        {
            get { return data["Ten"].ToString(); }
            set { data["Ten"] = value; }
        }
        public int NhomGo
        {
            get { return convertText.ToInt(data["NhomGo"].ToString()); }
            set { data["NhomGo"] = value; }
        }
        public double DoDayGoSay
        {
            get { return convertText.ToDouble(data["DoDayGoSay"].ToString()); }
            set { data["DoDayGoSay"] = value; }
        }
        public double DoAmDauVao
        {
            get { return convertText.ToDouble(data["DoAmDauVao"].ToString()); }
            set { data["DoAmDauVao"] = value; }
        }
        public double DoAmDauRa
        {
            get { return convertText.ToDouble(data["DoAmDauRa"].ToString()); }
            set { data["DoAmDauRa"] = value; }
        }
        public int CheDoSay
        {
            get { return convertText.ToInt(data["CheDoSay"].ToString()); }
            set { data["CheDoSay"] = value; }
        }
        public DateTime NgayKhoiTao
        {
            get { return convertText.ToDateTime(data["NgayKhoiTao"].ToString()); }
            set { data["NgayKhoiTao"] = value; }
        }
        public DateTime NgayKetThuc
        {
            get { return convertText.ToDateTime(data["NgayKetThuc"].ToString()); }
            set { data["NgayKetThuc"] = value; }
        }
        public string TacGia
        {
            get { return (data["TacGia"].ToString()); }
            set { data["TacGia"] = value; }
        }
        public string ghichu
        {
            get { return (data["ghichu"].ToString()); }
            set { data["ghichu"] = value; }
        }
        public string fileName
        {
            get { return (data["fileName"].ToString()); }
            set { data["fileName"] = value; }
        }
        #endregion

        #region DuLieuDo
        // Du lieu do tu mach
        public List<Point> listNhietDo=new List<Point>();
        public List<Point> listDoAmMoiTruong = new List<Point>();
        public List<Point> listDoAmGo = new List<Point>();
        #endregion

        #region DuLieuTinhToan
        // Du lieu tinh toan
        // neu go quy, go co dau, co nhua, nen say che do mem
        // thanh ke day lo
        public string thanhkeday = "10cmx10cmx độ dài hầm sấy";
        public string khoangcachcacthanhkeday = "0.5m có hướng song song với chiều gió thổi của quạt";
        //thanh ke cac lop go say
        // thanh ke go co chieu day bang 70% lop go say. Lam thanh ke 2cmx3cmx0.5m
        // khi day go >3 thi dat chieu 2cm nam, khi < 3 thi dat chieu 3cm nam
        public string thanhkego = "20x30x500";
        public double dodaythanhke = 30;
        public string huongDanWord { get; set; }
        public string ChuanBiGoSay { get; set; }
        public string ChuanBinoihoi { get; set; }
        public string chuanbihamsay { get; set; }
        public string chuanbithanhke { get; set; }
        public string chedoquat = @"Quạt chạy trong suất quá trình,
1h đảo chiều quạt 1 lần,
Khi kết thúc sấy, hé mở cửa, cho quạt chạy 8 tiếng";
        public string ketthucsay = @"Tắt lò,
hé mở cửa, cho quạt chạy 8 tiếng";
        public string phunam = @"Phun ẩm khi luộc,
trong giai đoạn ủ, thì phun ẩm cách nhau 30 phút, mỗi lần 15 giây";
        public string theodoidoamgo = @"theo dõi vào giai đoạn cuối, cách nhau 5 tiếng 1 lần";
        // thong so tinh toan
        //60 – 40         40 – 30         30 – 20           20 – 15         15 – 12           12 -  9
        // Các mảng dữ liệu chứa thông tin gồm các giai đoạn = ( Luộc, gd1,gd2,gd3,dg4,gd5, ủ)

        public int luoc = 0, gd1 = 1, gd2 = 2, gd3 = 3, gd4 = 4, gd5 = 5, gdu = 6;
        // độ ẩm
        public double[] DoAmGd = new double[7];
        // Nhiệt độ
        public double[] NhietDoGd = new double[7];
        //Thời gian sấy
        public double[] TgGd = new double[7];
        // độ mở ven, %
        public double[] DoMoVenGd = new double[7];
        // độ mở ven, %
        public double[] DoAmMoiTruongGd = new double[7];
        #endregion

        public dryerLibLocal()
            : base()
        {
            Ten = "Chế độ sấy gỗ nhóm 2, dày 25mm";
            NhomGo = 2;//  {get;set;}       // nhom=1,2,3,4,5
            DoDayGoSay = 25;//  { get; set; }
            DoAmDauVao = 50;//  { get; set; }
            DoAmDauRa = 12;//   { get; set; }
            CheDoSay = 5;//    { get; set; }// =1. say cung, =2, say trung binh, =3 say mem
            NgayKhoiTao = DateTime.Today;
            TacGia = "Lê Hồng Lam";// { get; set; }
            ghichu = "Ghi chú về chế độ sấy:";
        }
        public void addPointDoAmMoiTruong(int x, int y)
        {
            listDoAmMoiTruong.Add(new Point(x,y));
        }
        public void addPointDoAmGo(int x, int y)
        {
            listDoAmGo.Add(new Point(x, y));
        }
        public void addPointNhietDo(int x, int y)
        {
            listNhietDo.Add(new Point(x, y));
        }

        string fileDoamGo()
        {
            return ("dago_" + fileName + ".dag");
        }
        string fileDoamMoitruong()
        {
            return ("damt_" + fileName+".dam");
        }
        string fileDoamNhietDo()
        {
            return ("nhietdo_" + fileName+".nhd");
        }

        public void writeFileDoAmMoiTruong()
        {
            string filedoam = fileDoamMoitruong();
            writeListPointToFile(filedoam,listDoAmMoiTruong);
        }
        public void writeFileDoAmGo()
        {
            writeListPointToFile(fileDoamGo(), listDoAmGo);
        }
        public void writeFileNhietDo()
        {
            string filedoam = fileDoamNhietDo();
            writeListPointToFile(filedoam, listNhietDo);
        }

        public void readFileDoAmMoiTruong()
        {
            listDoAmMoiTruong.Clear();
            string filedoam = fileDoamMoitruong();
            readListPointToFile(filedoam, listDoAmMoiTruong);
        }
        public void readFileDoAmGo()
        {
            listDoAmGo.Clear();
            readListPointToFile(fileDoamGo(), listDoAmGo);
        }
        public void readFileNhietDo()
        {
            listNhietDo.Clear();
            string filedoam = fileDoamNhietDo();
            readListPointToFile(filedoam, listNhietDo);
        }

        public void readListPointToFile(string fileConfigName, List<Point> listPoint)
        {
            // neu tep chua co thi
            if (false == System.IO.File.Exists(fileConfigName))
            {
                System.IO.File.Create(fileConfigName);
                return;
            }
            string[] lines = System.IO.File.ReadAllLines(fileConfigName);

            foreach( string s in lines)
            {
                string[] sp=s.Split(':');
                if(sp.Count()<2) break;
                string x=sp[0]; string y = sp[1];
                listPoint.Add(new Point(convertText.ToInt(x),convertText.ToInt(y)));
            }

        }
        public void writeListPointToFile(string fileConfigName, List<Point> listPoint)
        {
            // neu tep chua co thi
            /*if (false == System.IO.File.Exists(fileConfigName))
            {
                System.IO.File.Create(fileConfigName);
                return;
            }
             * */
            List<string> ls = new List<string>();
            foreach(Point p in listPoint)
            {
                string s=p.X.ToString()+':'+p.Y.ToString();
                ls.Add(s);

            }
            System.IO.File.WriteAllLines(fileConfigName, ls.ToArray());
        }

        protected override void createDataSize()
        {
            sname=new string[11];
            int i = 0;

            data.Add("Ten", "Chế độ sấy gỗ nhóm 2, dày 25mm");
            dataType.Add("Ten", typeof(string));
            sname[i] = "Ten"; i++;

            data.Add("NhomGo", 2);
            dataType.Add("NhomGo", typeof(int));
            sname[i] = "NhomGo"; i++;

            data.Add("DoDayGoSay", 25);
            dataType.Add("DoDayGoSay", typeof(double));
            sname[i] = "DoDayGoSay"; i++;

            data.Add("DoAmDauVao", 60);
            dataType.Add("DoAmDauVao", typeof(double));
            sname[i] = "DoAmDauVao"; i++;

            data.Add("DoAmDauRa", 12);
            dataType.Add("DoAmDauRa", typeof(double));
            sname[i] = "DoAmDauRa"; i++;

            data.Add("CheDoSay", 7);
            dataType.Add("CheDoSay", typeof(int));
            sname[i] = "CheDoSay"; i++;

            data.Add("NgayKhoiTao", DateTime.Now);
            dataType.Add("NgayKhoiTao", typeof(DateTime));
            sname[i] = "NgayKhoiTao"; i++;

            data.Add("NgayKetThuc", DateTime.Now);
            dataType.Add("NgayKetThuc", typeof(DateTime));
            sname[i] = "NgayKetThuc"; i++;

            data.Add("TacGia", "Tác giả");
            dataType.Add("TacGia", typeof(string));
            sname[i] = "TacGia"; i++;

            data.Add("ghichu", "ghi chú");
            dataType.Add("ghichu", typeof(string));
            sname[i] = "ghichu"; i++;

            data.Add("fileName", "say-" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString() + ".con");
            dataType.Add("fileName", typeof(string));
            sname[i] = "fileName"; i++;
        }
        public void read()
        {
            readConfig(fileName);
            readFileDoAmGo();
            readFileDoAmMoiTruong();
            readFileNhietDo();
        }
        public void write()
        {
            writeConfig(fileName);
            writeFileDoAmGo();
            writeFileDoAmMoiTruong();
            writeFileNhietDo();
        }
        public string getThongTin()
        {
            string s = "THÔNG TIN SẤY " + System.Environment.NewLine;
            s += "Tệp         :" + fileName + System.Environment.NewLine;
            s += "Tên         :" + Ten + System.Environment.NewLine;
            s += "Loại Gỗ Nhóm:" + NhomGo.ToString() + System.Environment.NewLine;
            s += "Độ dày      :" + DoDayGoSay + System.Environment.NewLine;
            s += "Độ ẩm vào   :" + DoAmDauVao + System.Environment.NewLine;
            s += "tác giả     :" + TacGia + System.Environment.NewLine;
            return s;
        }
    }
}
