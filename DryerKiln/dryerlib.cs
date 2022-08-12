using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Tdin20.tdinCode
{
    public class dryerlib 
    {
        
        /*
            Ten            ,
            DoDayGoSay        ,
            DoAmDauVao        ,
            DoAmDauRa         ,
            CheDoSay         ,
            NhomGo          ,
            NgayKhoiTao    ,
            TacGia         ,
            TgLuoc            ,
            TgGd1             ,
            TgGd2             ,
            TgGd3             ,
            TgGd4             ,
            TgGd5             ,
            TgGdU             ,
            NhietDoLuoc       ,
            NhietDoGd1        ,
            NhietDoGd2        ,
            NhietDoGd3        ,
            NhietDoGd4        ,
            NhietDoGd5        ,
            NhietDoGdU        ,
            DoMoVenGdLuoc     ,
            DoMoVenGd1        ,
            DoMoVenGd2        ,
            DoMoVenGd3        ,
            DoMoVenGd4        ,
            DoMoVenGd5        ,
            DoMoVenGdU        ,
            DoAmGdLuoc     ,
            DoAmGd1        ,
            DoAmGd2        ,
            DoAmGd3        ,
            DoAmGd4        ,
            DoAmGd5        ,
            DoAmGdU        , 
         * */
        // du lieu chinh
        #region pro
        public string   Ten="Chế độ sấy gỗ nhóm 2, dày 25mm";
        public int      NhomGo=2;//  {get;set;}       // nhom=1,2,3,4,5
        public double   DoDayGoSay=25;//  { get; set; }
        public double   DoAmDauVao=50;//  { get; set; }
        public double   DoAmDauRa=12;//   { get; set; }
        public int      CheDoSay = 5;//    { get; set; }// =1. say cung, =2, say trung binh, =3 say mem
        public DateTime NgayKhoiTao = DateTime.Today;
        public string   TacGia = "Lê Hồng Lam";// { get; set; }
        public string   ghichu = "Ghi chú về chế độ sấy:";

        public int luoc = 0, gd1 = 1, gd2 = 2, gd3 = 3, gd4 = 4, gd5 = 5, gdu = 6;
        // độ ẩm
        public double[] DoAmGd = new double[7];
        // Nhiệt độ
        public double[] NhietDoGd = new double[7];
        //Thời gian sấy
        public double[] TgGd = new double[7];
        // độ mở ven, %
        public double[] DoAmMoiTruongGd = new double[7];
        #endregion

        public dryerlib()
            : base()
       {
            // khởi tạo
            NhomGo = 2;
            DoDayGoSay = 3;// 3cm
            DoAmDauVao =50;
            DoAmDauRa =12;
            CheDoSay = 5;
            NgayKhoiTao = DateTime.Today;
            //
        }
// TINH Toán
        public void tinhToan()
        {
            /*
             *Ví dụ : Sấy gỗ có độ ẩm ban đầu (Wđ) = 60%; Wc =12% ta có:
`Wđ – Wc %      60 – 40         40 – 30         30 – 20           20 – 15         15 – 12           12 -  9
60  –  12 %
Σ thời gian                             										   
sấy = 1              0,25               0,18            0,25                0,18                0,14               [ 0,18 ]
Ở giai đoạn 12 9% - thời gian đưa độ ẩm từ 15 - 12 xuống 12 - 9 cần tỷ lệ thời gian 0,18. 
( Ngoài tổng thời gian sấy = 1 ). Với mục đích khi “ủ” gỗ trở lại độ ẩm 12% là vừa. 
Theo tỉ lệ trên, giả sử ta sấy 1 loại gỗ có chế độ là 5 thì thời gian sấy mỗi giai đoạn như sau :
5×24h = 120h , ta có thời gian tương ứng các giai đoạn trên như sau:
120 = 30+22+30+22+16+ [22] 
             */
            DoAmGd[luoc] = DoAmDauVao;

            DoAmGd[gd1] = DoAmDauVao*40/60;
            DoAmGd[gd2] = DoAmDauVao * 30 / 60;
            DoAmGd[gd3] = DoAmDauVao * 20 / 60;
            DoAmGd[gd4] = DoAmDauVao * 15 / 60;
            DoAmGd[gd5] = DoAmDauRa;
            DoAmGd[gdu] = DoAmDauRa - 3;
            // thoi gian
            double thoigiantong = CheDoSay * 24;//h
            TgGd[luoc] = DoDayGoSay/2; // = số h * số mm độ dày gỗ /2
            TgGd[gd1] = thoigiantong*0.25;
            TgGd[gd2] = thoigiantong * 0.18;
            TgGd[gd3] = thoigiantong * 0.25;
            TgGd[gd4] = thoigiantong * 0.18;
            TgGd[gd5] = thoigiantong * 0.14;
            TgGd[gdu] = thoigiantong * 0.18; 

            NhietDoGd[luoc]=70;// thông thường dùng nhiệt độ từ 70 đến 90 độ C hoặc lấy nhiệt độ cao nhất có thể được
            //45                 49                  53                  57                  61                  65
            NhietDoGd[gd1]=45;
            NhietDoGd[gd2]=49;
            NhietDoGd[gd3]=53;
            NhietDoGd[gd4]=57;
            NhietDoGd[gd5]=61;
            NhietDoGd[gdu]=65;
            
            DoAmMoiTruongGd[luoc]=88;
            DoAmMoiTruongGd[gd1] = 69;
            DoAmMoiTruongGd[gd2] = 55;
            DoAmMoiTruongGd[gd3] = 44;
            DoAmMoiTruongGd[gd4] = 34;
            DoAmMoiTruongGd[gd5] = 27;


        }
// 
        public bool addnew()
        {
            return true;
        }
        public void GetData(int id)
        {
        }
        public void UpdateToDatabase(int id)
        {
        }


        void ghiDouble7(double[] value, List<string> scontent)
        {
            foreach (double x in value)
            {
                scontent.Add(convertText.ToString(x));
            }
        }
        public int ghivaoTep(string fileConfigName)
        {
            List<string> scontent = new List<string>();
            scontent.Add(Ten);
            scontent.Add(convertText.ToString(NhomGo));
            scontent.Add(convertText.ToString(DoDayGoSay));//  { get; set; }
            scontent.Add(convertText.ToString(DoAmDauVao));//  { get; set; }
            scontent.Add(convertText.ToString(DoAmDauRa));//   { get; set; }
            scontent.Add(convertText.ToString(CheDoSay));//    { get; set; }// =1. say cung, =2, say trung binh, =3 say mem
            scontent.Add(convertText.ToString(NgayKhoiTao));
            scontent.Add(convertText.ToString(TacGia));// { get; set; }
            scontent.Add(ghichu);

            ghiDouble7(DoAmGd,scontent);
            ghiDouble7(NhietDoGd,scontent);
            ghiDouble7(TgGd,scontent);
            ghiDouble7(DoAmMoiTruongGd,scontent);
            System.IO.File.AppendAllLines(fileConfigName, scontent);
            return 1;
        }
        public int docTep(string fileConfigName)
        {
            List<string> scontent = new List<string>();
            scontent.Add(Ten);
            scontent.Add(convertText.ToString(NhomGo));
            scontent.Add(convertText.ToString(DoDayGoSay));//  { get; set; }
            scontent.Add(convertText.ToString(DoAmDauVao));//  { get; set; }
            scontent.Add(convertText.ToString(DoAmDauRa));//   { get; set; }
            scontent.Add(convertText.ToString(CheDoSay));//    { get; set; }// =1. say cung, =2, say trung binh, =3 say mem
            scontent.Add(convertText.ToString(NgayKhoiTao));
            scontent.Add(convertText.ToString(TacGia));// { get; set; }
            scontent.Add(ghichu);

            ghiDouble7(DoAmGd, scontent);
            ghiDouble7(NhietDoGd, scontent);
            ghiDouble7(TgGd, scontent);
            ghiDouble7(DoAmMoiTruongGd, scontent);
            string[] str= System.IO.File.ReadAllLines(fileConfigName);
            if (str == null) return 0;
            if (str.Count() > 37) return 0;

            Ten = str[0];


            return 1;
        }


    }
}
