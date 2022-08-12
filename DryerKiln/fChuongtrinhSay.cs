using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;
using Tdin20.tdinCode;

namespace Tdin20.tdinCode
{
    public partial class fChuongtrinhSay : Form
    {
        DataTable dmQuyTrinhSay;
        int tientrinh = 0;
        float nhietdo = 28;
        float doammoitruong = 85;
        float doamgo = 70;
        int giaidoansay = 1;
        Color colorOn = Color.Green;
        Color colorOff = Color.Blue;
        Color colorError = Color.Red;
        string textMo = "Mở";
        string textDong = "Đóng";
        string textError = "Lỗi";
        int demquatDungChoChay = 0;
        int demDungPhunAm=0;
        int trangthaiquat = 1;// =1 dang chay xuoi, =2 dang chay nguoc; =0: dang tat

        // trang thai dieu khien trong giai doan
        double nhietDoGiaiDoan = 70;
        double doAmMoiTruongGiaiDoan = 86;
        double doAmVGoGiaiDoan = 60;

        DateTime thoigianbatdausay;
        DateTime thoigianKetthucSay;
        Dictionary<int, float> dataNhietdo;
        Dictionary<int, float> dataDoAmMoiTruong;
        Dictionary<int, float> dataDoamGo;
        //docCom com = new docCom();
        
        public fChuongtrinhSay()
        {

            InitializeComponent();

            dataNhietdo = new Dictionary<int, float>();
            dataDoAmMoiTruong = new Dictionary<int, float>();
            dataDoamGo = new Dictionary<int, float>();
        }
        public void setThongTinChung(string text)
        {
            textBox1ThongTinSay.Text = text;
        }
        void setChuongTrinhgSay(DataTable data)
        {
            dmQuyTrinhSay = data;
        }

        void thucHienCheDoSay()
        {
            //chart1SayGo.Series['nhietdo']
            if (dmQuyTrinhSay.Rows.Count == 0) return;

            // kiem tra che do say
            /*
            int giaidoan=0;
            for(int i=0;i<dmQuyTrinhSay.Rows.Count;i++)
            {
                double doamgoTinhtoan=convertText.ToDouble(dmQuyTrinhSay.Rows[i][nameDoAmGo].ToString());
                if(doamgo>doamgoTinhtoan) continue;
                else 
                {
                    giaidoan=i;
                    break; // khi độ ẩm thực tế nhỏ hơn trong giai đoạn thì thoát khỏi vòng for 
                }
            }
            if (giaidoan < dmQuyTrinhSay.Rows.Count)
            {
                double doamgoTinhtoan = convertText.ToDouble(dmQuyTrinhSay.Rows[giaidoan][nameDoAmGo].ToString());
                // nếu độ ẩm môi trường < độ ẩm gia đoạn thì phun ẩm 30s
                double doamMoitruongTinhtoan = convertText.ToDouble(dmQuyTrinhSay.Rows[giaidoan][nameDoAmMoiTruong].ToString());
                if (doamMoitruongTinhtoan < doammoitruong) phunam30s();
            }
             * */

        }

        void ghiDulieuLeDoThi()
        {
            TimeSpan tp= thoigianKetthucSay.Subtract(thoigianbatdausay);
            QuyTrinhSayGo.dryerLocal.addPointDoAmGo((int)tp.TotalSeconds, (int)doamgo);
            QuyTrinhSayGo.dryerLocal.addPointNhietDo((int)tp.TotalSeconds, (int)nhietdo);
            QuyTrinhSayGo.dryerLocal.addPointDoAmMoiTruong((int)tp.TotalSeconds, (int)doammoitruong);
        }
        // tinh do am can bang
        double emc()
        {
            double wcb = 0;
            double B = 81;
            double n = 2;
            double b = 1;
            wcb = Math.Pow(B / b, 1 / n) * Math.Pow(doammoitruong / (100 / b - doammoitruong), 1 / n);
            return wcb;
        }
        private void docDulieuSayTimer(object sender, EventArgs e)
        {
            // Dọc dữ liệu sấy rồi hiển thị lên chương trình
            tientrinh = tientrinh + 1;
            docCom com = new docCom();
            com.docNhietDoDoAm(ref nhietdo, ref doammoitruong);
            com.docDoAmGo(ref doamgo);
            // ghi du lieu len
            textBoxNhietDo.Text = nhietdo.ToString();
            textBoxDoAmMoiTruong.Text = doammoitruong.ToString();
            textBoxDoAmGo.Text = doamgo.ToString();
            textBox1EMC.Text = emc().ToString("f1");

            ghiDulieuLeDoThi();

            // ghi du lieu thoi gian
            int x = timer1DocCongCom.Interval / 1000;
            thoigianKetthucSay=thoigianKetthucSay.AddSeconds(x);
            dateTimeNgayKetThucSay.Value = thoigianKetthucSay;
        }

        private void fChuongtrinhSay_Load(object sender, EventArgs e)
        {
            datthoiGianHoatDongToanBoTimer();
        }

        private void button1Start_Click(object sender, EventArgs e)
        {
            // bắt đầu sấy
            thoigianbatdausay=DateTime.Now;
            thoigianKetthucSay = DateTime.Now;

            dateTimeNgayKetThucSay.Value = thoigianbatdausay;
            dateTimePickerBaDauSay.Value = thoigianbatdausay;
           
            batDauSay();
        }

        private void button2Stop_Click(object sender, EventArgs e)
        {
            TamDungSay();
        }
        // dieu khien
        public void datthoiGianHoatDongToanBoTimer()
        {
            if (QuyTrinhSayGo.configTdin.thoigiandocCom > 0) 
                timer1DocCongCom.Interval = QuyTrinhSayGo.configTdin.thoigiandocCom;

            if (QuyTrinhSayGo.configTdin.thoigiandaochieuquat > 0) 
                timer1DaoChieuQuat.Interval = QuyTrinhSayGo.configTdin.thoigiandaochieuquat;

            if (QuyTrinhSayGo.configTdin.thoigianChodungPhunAm > 0) 
                timer1DemChoDungPhunAm.Interval = QuyTrinhSayGo.configTdin.thoigianChodungPhunAm;

            if (QuyTrinhSayGo.configTdin.thoigiannghiChoBatQuat > 0)
            {
                timer2QuatChayXuoi.Interval = QuyTrinhSayGo.configTdin.thoigiannghiChoBatQuat;
                timer2QuatChayNguoc.Interval = QuyTrinhSayGo.configTdin.thoigiannghiChoBatQuat;
            }

            if (QuyTrinhSayGo.configTdin.thoigianchayquatsautatlo > 0)
            {
                timer1Quat8Tieng.Interval = QuyTrinhSayGo.configTdin.thoigianchayquatsautatlo;
            }
            
        }
        void daochieuQuat()
        {
            if(trangthaiquat==0) 
                chayQuatXuoiKhoiDong();
            else if (trangthaiquat==1)// dang chay xuoi
            {
                chayQuatNguocKhoiDong();
            }
            else if(trangthaiquat==2)
            {
                chayQuatXuoiKhoiDong();
            }
        }
        void chayQuatXuoi8Tieng()
        {
            chayQuatXuoiKhoiDong();
            //timer1Quat8Tieng.Interval = QuyTrinhSayGo.configTdin.thoigianchayquatsautatlo;// 8 tieng
            timer1Quat8Tieng.Start();
            // bat quat tai time chay xuoi
        }
        void chayQuatXuoiKhoiDong()
        {
            // dung quat
            tatQuat();
            timer2QuatChayXuoi.Start();
            timer1DemChoQuatChay.Start();
            demquatDungChoChay = 0;
            // bat quat tai time chay xuoi
        }
        void chayQuatNguocKhoiDong()
        {
            // dung quat
            tatQuat();
            timer2QuatChayNguoc.Start();
            timer1DemChoQuatChay.Start();
            demquatDungChoChay = 0;
            // bat quat tai time chay xuoi
        }
        void chayQuatXuoi()
        {
            docCom com = new docCom();
            int r = com.ChayXuoiQuat();
            if (r == 1)// trng thai tot
            {
                setMo(textBoxQuatCHay);
                textBoxQuatCHay.Text = "Đang Chạy Xuôi";
            }
            else
            {
                setError(textBoxQuatCHay);
            }
            // dung tien trinh doi
            timer2QuatChayXuoi.Stop();
            timer1DemChoQuatChay.Stop();
            trangthaiquat = 1;
        }
        void chayQuatNguoc()
        {
            docCom com = new docCom();
            int r = com.ChayNguocQuat();
            if (r == 1)// trng thai tot
            {
                setMo(textBoxQuatCHay);
                textBoxQuatCHay.Text = "Đang Chạy Ngược";
            }
            else
            {
                setError(textBoxQuatCHay);
            }
            timer2QuatChayNguoc.Stop();
            timer1DemChoQuatChay.Stop();
            trangthaiquat = 2;
        }
        void tatQuat()
        {
            docCom com = new docCom();
            int r = com.tatQuat();
            if (r == 1)// trng thai tot
            {
                setDong(textBoxQuatCHay);
            }
            else
            {
                setError(textBoxQuatCHay);
            }
            timer2QuatChayNguoc.Stop();
            timer2QuatChayXuoi.Stop();
            textBoxQuatCHay.Text = "Dừng";
            trangthaiquat = 0;
            timer1DemChoQuatChay.Stop();
            demquatDungChoChay = 0;
            textBox2DemQuatChay.Text = demquatDungChoChay.ToString();


        }
        void MoVanXaAm()
        {
            docCom com = new docCom();
            int r = com.movanPhunAm();
            if (r == 1)// trng thai tot
            {
                setMo(textBox4VanXaAm);
            }
            else
            {
                setError(textBox4VanXaAm);
            }
        }
        void dongVanXaAm()
        {
            docCom com = new docCom();
            int r = com.DongvanPhunAm();
            if (r == 1)// trng thai tot
            {
                setDong(textBox4VanXaAm);
            }
            else
            {
                setError(textBox4VanXaAm);
            }
        }
        void moNhiet()
        {
            docCom com = new docCom();
            int r = com.movanNhiet();
            if (r == 1)// trng thai tot
            {
                setMo(textBox3TranThaiVanNhiet);
            }
            else
            {
                setError(textBox3TranThaiVanNhiet);
            }
        }
        void dongNhiet()
        {
            docCom com = new docCom();
            int r = com.DongvanNhiet();
            if (r == 1)// trng thai tot
            {
                setDong(textBox3TranThaiVanNhiet);
            }
            else
            {
                setError(textBox3TranThaiVanNhiet);
            }
        }
        void phunam30s()
        {
            MoVanXaAm();
            timer1PhunAm.Start();

            timer1DemChoDungPhunAm.Start();
            demDungPhunAm = 0;
        }
        void BatDauDocDuLieu()
        {
            timer1DocCongCom.Start();
        }
        void DungDocDuLieu()
        {
            timer1DocCongCom.Stop();
        }

        void batDauSay()
        {
            // Mo nhiet
            moNhiet();

            // Mo Ven phun am
            MoVanXaAm();

            // chayquat
            chayQuatXuoiKhoiDong();
            //timer1DaoChieuQuat.Interval = QuyTrinhSayGo.configTdin.thoigiandaochieuquat;// 1go
            timer1DaoChieuQuat.Start();
            //
            BatDauDocDuLieu();

        }
        void TamDungSay()
        {
            // Mo nhiet
            dongNhiet();

            // Mo Ven phun am
            dongVanXaAm();

            // chayquat
            tatQuat();
            timer1DaoChieuQuat.Stop();
            
            //
            DungDocDuLieu();
        }
        void DungSay()
        {
            // Mo nhiet
            dongNhiet();

            // Mo Ven phun am
            dongVanXaAm();

            // chayquat
            timer1DaoChieuQuat.Stop();
            chayQuatXuoi8Tieng();

            //
            DungDocDuLieu();
            // ghi ngay ket thuc say
            dateTimeNgayKetThucSay.Value = DateTime.Now;
        }
        void TatToanBoLo()
        {
            tatQuat();
            dongVanXaAm();
            dongNhiet();
        }
        // dieu khien theo giai doan
        void batDauGiaiDoan()
        {

        }

        void setMo(Control c)
        {
            c.Text = textMo;
            c.BackColor = colorOn;
        }
        void setDong(Control c)
        {
            c.Text = textDong;
            c.BackColor = colorOff;
        }
        void setError(Control c)
        {
            c.Text = textError;
            c.BackColor = colorError;
        }

        private void button1CapNhiet(object sender, EventArgs e)
        {
            moNhiet();
        }
        private void button1DongVanAm(object sender, EventArgs e)
        {
            dongVanXaAm();
        }
        private void button1DongVanNhiet(object sender, EventArgs e)
        {
            dongNhiet();
        }
        private void button1MoVanAm(object sender, EventArgs e)
        {
            MoVanXaAm();
        }

        private void fChuongtrinhSay_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        private void button1ChayQuatXuoi(object sender, EventArgs e)
        {
            chayQuatXuoiKhoiDong();
        }
        private void button1ChayQuatNguoc(object sender, EventArgs e)
        {
            chayQuatNguocKhoiDong();
        }
        private void button1DungQuat(object sender, EventArgs e)
        {
            tatQuat();
        }
        private void DocThoiGian(object sender, EventArgs e)
        {
            //thoigiansay += timer1DocThoiGian.Interval / 1000;
        }
        private void ChayQuatXuoi(object sender, EventArgs e)
        {
            chayQuatXuoi();
        }
        private void quatChayNguoc(object sender, EventArgs e)
        {
            chayQuatNguoc();
        }
        private void phunAm30Giay(object sender, EventArgs e)
        {
            dongVanXaAm();
            timer1PhunAm.Stop();
            timer1DemChoDungPhunAm.Stop();
            demDungPhunAm = 0;
        }
        private void button1PhunAm30S(object sender, EventArgs e)
        {
            phunam30s();
        }

        private void TatQuatSau8Tieng(object sender, EventArgs e)
        {
            tatQuat();
        }

        private void demQuatChay(object sender, EventArgs e)
        {
            demquatDungChoChay++;
            textBox2DemQuatChay.Text = demquatDungChoChay.ToString();
        }

        private void button1BatDauDocCom(object sender, EventArgs e)
        {
            //timer1DocCongCom.Interval = QuyTrinhSayGo.configTdin.thoigiandocCom;
            timer1DocCongCom.Start();
        }

        private void button2DungDocCongCom(object sender, EventArgs e)
        {
            timer1DocCongCom.Stop();
        }

        private void demChoDungPhunAm(object sender, EventArgs e)
        {
            demDungPhunAm++;
            textBox2DoiDongPhunAm.Text = demDungPhunAm.ToString();
        }

        private void DaoChieuQuat(object sender, EventArgs e)
        {
            daochieuQuat();
        }

        private void button2DungLoSay(object sender, EventArgs e)
        {
            DungSay();
        }

        private void button2ChuyenGiaiDoan(object sender, EventArgs e)
        {

        }
    }
}
