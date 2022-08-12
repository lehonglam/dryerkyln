using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tdin20.tdinCode
{
    class BaoGia: abb_data
    {
        /*
[Id]                INT            IDENTITY (1, 1) NOT NULL,
   [SoBaoGia]          NVARCHAR (50)  NULL,
   [NgayBaoGia]        DATETIME       NULL,
   [HieuLucBaoGia]     INT            NULL,
   [GhiChu]            NVARCHAR (MAX) NULL,
   [KhachHang]         INT            NULL,
   [NhanVienBaoGia]    INT            NULL,
   [HinhThucThanhToan] NVARCHAR (MAX) NULL,
   [HinhThucGiaoHang]  NVARCHAR (MAX) NULL,
   [mota]              NVARCHAR (MAX) NULL,
   [noidung]           NVARCHAR (MAX) NULL,
        */
        public string   SoBaoGia { get; set; }
        public DateTime NgayBaoGia { get; set; }
        public int      HieuLucBaoGia { get; set; }
        public string   GhiChu { get; set; }
        public int      KhachHang { get; set; }
        public int      NhanVienBaoGia { get; set; }
        public string   HinhThucThanhToan { get; set; }
        public string   HinhThucGiaoHang { get; set; }
        public string   mota { get; set; }
        public string   noidung { get; set; }
        public int      giamgia { get; set; }    // giam gia % truoc thue   
        public string   formmau{ get; set; }
        public int      thoihangiaohang { get; set; }    // giam gia % truoc thue   
        public string   diadiemgiaohang { get; set; }
        public string   bcc { get; set; }
        public string   attachfile { get; set; }
        public string   GhiChuKinhDoanh { get; set; }

       public       BaoGia(): base ()
       {

           thoihangiaohang = 10;
           diadiemgiaohang = "công trình bên mua";

                NgayBaoGia = DateTime.Today;
                //SoBaoGia = NgayBaoGia.ToString();.Day.ToString() + NgayBaoGia.Month.ToString() + NgayBaoGia.Year.ToString()+"/TDIN-BG";
                

             HieuLucBaoGia=10;
                GhiChu = @"<h3>GHI CHÚ:</h3>
                    <p>- Báo giá trên có giá trị trong vòng 10 ngày</p>											
                    <p>- Đơn giá đã bao gồm chi phí vận chuyển trong phạm vi Hà Nội	</p>										
                    <p>- Thời hạn bảo hành: 12 tháng kể từ ngày ký biên bản nghiệm thu</p>";
              KhachHang=0;
              NhanVienBaoGia=0;
              HinhThucThanhToan = "";
              HinhThucGiaoHang = "";
              mota          =" ";
              noidung = "Báo giá chung";
              formmau = "";
              bcc = "lhonglam@gmail.com";
              attachfile = "";
              GhiChuKinhDoanh = "";

               

              tablename     = "BaoGia";
              IndexIDName   = "Id";
              SoBaoGia = NgayBaoGia.ToString("ddMMyy") + "/TDIN-BG-" + MaxID().ToString();

              insertcmd     = @"insert into BaoGia values(@SoBaoGia,@NgayBaoGia,@HieuLucBaoGia,@GhiChu,
                                    @KhachHang,@NhanVienBaoGia,@HinhThucThanhToan,@HinhThucGiaoHang,@mota,@noidung,
                                    @giamgia,@formmau,@thoihangiaohang,@diadiemgiaohang,@bcc,@attachfile,@GhiChuKinhDoanh)";
              updatecmd     = @"UPDATE BaoGia SET 
                                    SoBaoGia=@SoBaoGia,
                                    NgayBaoGia=@NgayBaoGia,
                                    HieuLucBaoGia=@HieuLucBaoGia,GhiChu=@GhiChu, KhachHang=@KhachHang,
                                    NhanVienBaoGia=@NhanVienBaoGia,HinhThucThanhToan=@HinhThucThanhToan,
                                    HinhThucGiaoHang=@HinhThucGiaoHang,mota=@mota,noidung=@noidung,
                                    giamgia=@giamgia,formmau=@formmau,
                                    thoihangiaohang=@thoihangiaohang,diadiemgiaohang=@diadiemgiaohang,
                                    bcc=@bcc,attachfile=@attachfile,GhiChuKinhDoanh=@GhiChuKinhDoanh  ";
        }
        protected override void AddParameter(ref SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@SoBaoGia", SoBaoGia);
            cmd.Parameters.AddWithValue("@NgayBaoGia", NgayBaoGia);
            cmd.Parameters.AddWithValue("@HieuLucBaoGia", HieuLucBaoGia);
            cmd.Parameters.AddWithValue("@GhiChu", GhiChu);
            cmd.Parameters.AddWithValue("@KhachHang", KhachHang);
            cmd.Parameters.AddWithValue("@NhanVienBaoGia", NhanVienBaoGia);
            cmd.Parameters.AddWithValue("@HinhThucThanhToan", HinhThucThanhToan);
            cmd.Parameters.AddWithValue("@HinhThucGiaoHang", HinhThucGiaoHang);
            cmd.Parameters.AddWithValue("@mota", mota);
            cmd.Parameters.AddWithValue("@noidung", noidung);
            cmd.Parameters.AddWithValue("@giamgia", giamgia);
            cmd.Parameters.AddWithValue("@formmau", formmau);
            cmd.Parameters.AddWithValue("@thoihangiaohang", thoihangiaohang);
            cmd.Parameters.AddWithValue("@diadiemgiaohang", diadiemgiaohang);
            cmd.Parameters.AddWithValue("@bcc", bcc);
            cmd.Parameters.AddWithValue("@attachfile", attachfile);
            cmd.Parameters.AddWithValue("@GhiChuKinhDoanh", GhiChuKinhDoanh);
        }
        public override int GetData(int IDin)
        {
            AccessDatabase ac = new AccessDatabase();
            string sql = "select * from BaoGia where " + IndexIDName + "=" + IDin;
            DataTable dt = ac.getTable(sql);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                if (row["SoBaoGia"] != null)
                {
                    SoBaoGia = row["SoBaoGia"].ToString();
                }
                if (row["NgayBaoGia"] != null)
                {
                    string str = row["NgayBaoGia"].ToString();
                    if (str.Length != 0)
                        NgayBaoGia = DateTime.Parse(str);
                }
                if (row["HieuLucBaoGia"] != null)
                {
                    string str = row["HieuLucBaoGia"].ToString();
                    if (str.Length != 0)
                        HieuLucBaoGia = int.Parse(str);
                } 
                if (row["GhiChu"] != null)
                {
                    GhiChu = row["GhiChu"].ToString();
                }
                if (row["KhachHang"] != null)
                {
                    string str = row["KhachHang"].ToString();
                    if (str.Length != 0)
                        KhachHang = int.Parse(str);
                }
                if (row["NhanVienBaoGia"] != null)
                {
                    string str = row["NhanVienBaoGia"].ToString();
                    if (str.Length != 0)
                        NhanVienBaoGia = int.Parse(str);
                }
                if (row["HinhThucThanhToan"] != null)
                {
                    HinhThucThanhToan = row["HinhThucThanhToan"].ToString();
                }
                if (row["HinhThucGiaoHang"] != null)
                {
                    HinhThucGiaoHang = row["HinhThucGiaoHang"].ToString();

                }
                if (row["mota"] != null) mota = row["mota"].ToString();
                if (row["noidung"] != null) noidung = row["noidung"].ToString();
                if (row["giamgia"] != null)
                {
                    string str = row["giamgia"].ToString();
                    if (str.Length != 0)
                        giamgia = int.Parse(str);
                }
                if (row["formmau"] != null) formmau = row["formmau"].ToString();
                if (row["thoihangiaohang"] != null)
                {
                    string str = row["thoihangiaohang"].ToString();
                    if (str.Length != 0)
                        thoihangiaohang = int.Parse(str);
                }
                if (row["diadiemgiaohang"] != null) diadiemgiaohang = row["diadiemgiaohang"].ToString();
                if (row["bcc"] != null) bcc = row["bcc"].ToString();
                if (row["attachfile"] != null) attachfile = row["attachfile"].ToString();
                if (row["GhiChuKinhDoanh"] != null) GhiChuKinhDoanh = row["GhiChuKinhDoanh"].ToString();
                
            }
            return 1;
        }
    }
}
