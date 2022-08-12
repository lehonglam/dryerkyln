using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tdin20.tdinCode;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;
using DryerKiln;

namespace Tdin20.tdinCode
{
    public partial class QuyTrinhSayGo : Form
    {
        fChuongtrinhSay fChuongTrinhSayTuDong = new fChuongtrinhSay();
        createNewProject fDulieuGo = new createNewProject();
        string workingDirectory = "";
        bool loadncc = false;
        bool loadDlg=false;
        static public config        configTdin = new config();
        static public dryerLibLocal dryerLocal = new dryerLibLocal();
        TreeNode selectedNode = null;
        DataTable dmSpBaoGia;
        private dryerlib dry = new dryerlib();

        string nameGiaiDoan = "GiaiDoan", nameThoiGian = "thoigian", nameNhietDo = "nhietdo", 
               nameDoAmGo = "DoAmGo", nameDoMoVen = "DoMoVen",nameDoAmMoiTruong = "DoAmMoiTruong";
        string nameSeriesNhietDo = "nhietdo", nameSeriesDoamMoiTruong = "DoamMoiTruong", nameSeriesDoAmGo = "DoAmGo";

        public QuyTrinhSayGo()
        {
            configTdin.readConfig();

            InitializeComponent();
            ///configTdin = new QuyTrinhSayGo.configTdin();
            dmSpBaoGia = new DataTable();
            dmSpBaoGia.Columns.Add(nameGiaiDoan);
            dmSpBaoGia.Columns.Add(nameThoiGian);
            dmSpBaoGia.Columns.Add(nameNhietDo);
            dmSpBaoGia.Columns.Add(nameDoAmGo);
            dmSpBaoGia.Columns.Add(nameDoMoVen);
            dmSpBaoGia.Columns.Add(nameDoAmMoiTruong);
        }
        void initDoThi()
        {
            chart1SayGo.Series.Add(nameSeriesNhietDo);
            chart1SayGo.Series[nameSeriesNhietDo].Name = nameSeriesNhietDo;
            chart1SayGo.Series[nameSeriesNhietDo].ChartType = SeriesChartType.Line;


            chart1SayGo.Series.Add(nameSeriesDoamMoiTruong);
            chart1SayGo.Series[nameSeriesDoamMoiTruong].Name = nameSeriesDoamMoiTruong;
            chart1SayGo.Series[nameSeriesDoamMoiTruong].ChartType = SeriesChartType.Line;

            chart1SayGo.Series.Add(nameSeriesDoAmGo);
            chart1SayGo.Series[nameSeriesDoAmGo].Name = nameSeriesDoAmGo;
            chart1SayGo.Series[nameSeriesDoAmGo].ChartType = SeriesChartType.Line;
        }
        void CreateTreeCheDoSay()
        {
            treeViewCheDoSay.Nodes.Clear();

            TreeNodeCollection root = treeViewCheDoSay.Nodes;
            string sqlNhom = "SELECT Id,Ten FROM CheDoSay ";
            try
            {
                SqlConnection con = new SqlConnection(QuyTrinhSayGo.configTdin.connectionString());
                SqlDataAdapter ad = new SqlDataAdapter(sqlNhom, con);
                DataTable dt = new DataTable(); ;
                ad.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    TreeNode node = new TreeNode();
                    node.Text = row["Ten"].ToString();
                    node.Tag = row["Id"];
                    root.Add(node);
                }
                // Giai phong doi tuong
                ad.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "HttpCall in AccessDatabase.cs");
            }
        }
        void Init()
        {
            upDataConfig(); 
            //CreateTreeCheDoSay();
            textBoxDoAmBanDau.Text = dry.DoAmDauVao.ToString();
            textBoxDoAmCuoiCung.Text = dry.DoAmDauRa.ToString();
            textBoxTenCheDoSay.Text = dry.Ten;
            comboBoxCheDoSay.Text = dry.CheDoSay.ToString();
            comboBoxLoaiGoSay.Text = dry.NhomGo.ToString();
        }
        void upDataConfig()
        {
            // giay
            double t = configTdin.thoigiandocCom;
            t=t/60/1000;
            textBox1doccongcom.Text = t.ToString("f2");
            // gio    
            t = configTdin.thoigiandaochieuquat;
            t = t /60/60/1000;
            textBoxthoigiandaochieuquat.Text = t.ToString("f2");

            // phut
            t=configTdin.thoigiannghiChoBatQuat;
            t=t/60/1000;
            textBoxthoigiannghiChoBatQuat.Text = t.ToString("f2");

            // don vi gio
            t =configTdin.thoigianchayquatsautatlo;
            t = t / 60/60 / 1000;
            textBoxthoigianchayquatsautatlo.Text = t.ToString("f2");

            // giay
            t = configTdin.thoigianChodungPhunAm;
            t = t / 1000;
            textBoxthoigianChodungPhunAm.Text = t.ToString("f2");
        }
        private void buttonGhiLaiConfig(object sender, EventArgs e)
        {
            // tinh thoi don vi giay
            double t = convertText.ToDouble(textBox1doccongcom.Text);
            t = t *60* 1000;
            configTdin.thoigiandocCom = (int)t ;

            t = convertText.ToDouble(textBoxthoigiandaochieuquat.Text);
            // don vi gio
            t = t * 60 * 60 * 1000;
            configTdin.thoigiandaochieuquat = (int)t;

            // don vi phut
            t = convertText.ToDouble(textBoxthoigiannghiChoBatQuat.Text);
            t = t * 60 * 1000;
            configTdin.thoigiannghiChoBatQuat = (int)t;

            // don vi gio
            t = convertText.ToDouble(textBoxthoigianchayquatsautatlo.Text);
            t = t * 60 * 60 * 1000;
            configTdin.thoigianchayquatsautatlo = (int)t;

            // giay
            t = convertText.ToDouble(textBoxthoigianChodungPhunAm.Text);
            t = t * 1000;
            configTdin.thoigianChodungPhunAm = (int)t;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void buttonTinhToan_Click(object sender, EventArgs e)
        {
            saveData();
            dry.tinhToan();
            upData();
        }
        private void ThemMoiCheDoSay(object sender, EventArgs e)
        {
            if (false == dry.addnew())
            {
                MessageBox.Show("Loi khong them duoc csdl");
                return;
            };
            CreateTreeCheDoSay();
            upData();
        }
        private void XoaCheDoSayChon(object sender, EventArgs e)
        {

        }
        void UpDataGrid()
        {
            dmSpBaoGia.Clear();
            double thoigian = 0;
            for (int i = dry.luoc; i <= dry.gdu; i++)
            {
                DataRow dr = dmSpBaoGia.NewRow();

                dr[nameGiaiDoan] = "Giai đoạn " + (i+1).ToString();
                dr[nameThoiGian] = string.Format("{0:0} h", dry.TgGd[i]); 
                thoigian += dry.TgGd[i];
                dr[nameNhietDo] = dry.NhietDoGd[i];
                dr[nameDoAmGo] = string.Format("{0:0} %", dry.DoAmGd[i]); 
                dr[nameDoAmMoiTruong] = dry.DoAmMoiTruongGd[i];
                if (i == dry.luoc)
                {
                    dr[nameGiaiDoan] = "Giai đoạn luộc";
                    dr[nameDoMoVen] = "Đóng";
                }
                else if (i == dry.gdu)
                {
                    dr[nameGiaiDoan] = "Giai đoạn ủ";
                    dr[nameDoMoVen] = "Mở 50 %";
                }
                else
                {
                    double domo = 100.0 / dry.gdu * i;
                    dr[nameDoMoVen] = string.Format("Mở {0:0} %", domo);
                }

                dmSpBaoGia.Rows.Add(dr);
            }

            // thêm phần tổng
            DataRow drt = dmSpBaoGia.NewRow();

            drt[nameGiaiDoan] = "";
            drt[nameThoiGian] = thoigian+" h";
            drt[nameNhietDo] = "";
            drt[nameDoAmGo] = "";
            drt[nameDoMoVen] = "";
            drt[nameDoAmMoiTruong] = "";
            dmSpBaoGia.Rows.Add(drt);

            dataGridViewDuLieuSay.DataSource = dmSpBaoGia;
            dataGridViewDuLieuSay.Columns[nameThoiGian].HeaderText = "Thời gian sấy(h)";
            dataGridViewDuLieuSay.Columns[nameNhietDo].HeaderText = "Nhiệt độ(độ C)";
            dataGridViewDuLieuSay.Columns[nameDoAmGo].HeaderText = "Độ ẩm gỗ dự kiến(%)";
            dataGridViewDuLieuSay.Columns[nameDoMoVen].HeaderText = "Độ mở ven(%)";
            dataGridViewDuLieuSay.Columns[nameDoAmMoiTruong].HeaderText = "Độ ẩm môi trường(%)";
            dataGridViewDuLieuSay.Columns["GiaiDoan"].HeaderText = "Giai đoạn";

        }
        void upData()
        {
            textBoxDoAmBanDau.Text = dry.DoAmDauVao.ToString();
            textBoxDoAmCuoiCung.Text = dry.DoAmDauRa.ToString();
            textBoxTenCheDoSay.Text = dry.Ten;
            comboBoxCheDoSay.Text = dry.CheDoSay.ToString();
            comboBoxLoaiGoSay.Text = dry.NhomGo.ToString();
            textBoxDoDaygosay.Text = dry.DoDayGoSay.ToString();
            textBoxGhiChu.Text = dry.ghichu;

            UpDataGrid();
        }
        void saveData()
        {
            // ghi lai du lieu
            dry.DoAmDauVao = convertText.ToDouble(textBoxDoAmBanDau.Text);
            dry.DoAmDauRa = convertText.ToDouble(textBoxDoAmCuoiCung.Text);
            dry.Ten = textBoxTenCheDoSay.Text;
            dry.CheDoSay = convertText.ToInt(comboBoxCheDoSay.Text);
            dry.NhomGo = convertText.ToInt(comboBoxLoaiGoSay.Text);
            dry.DoDayGoSay = convertText.ToDouble(textBoxDoDaygosay.Text);
            dry.ghichu=textBoxGhiChu.Text;
            // ghi lai item tree
            if (selectedNode != null)
                selectedNode.Text = dry.Ten;
        }
        protected void tree_selectItem(TreeNode node)
        {
            int id = (int)node.Tag;

            dry.GetData(id);

            upData();
        }
        private void nodeMouseDbleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selectedNode = e.Node;
            tree_selectItem(selectedNode);
        }
        private void RightClickSelect(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedNode = treeViewCheDoSay.GetNodeAt(e.X, e.Y);
                contextMenuStrip1.Show(treeViewCheDoSay, e.Location);
            }
        }
        private void buttonGhiLai_Click(object sender, EventArgs e)
        {
            if (selectedNode != null)
            {
                saveData();
                dry.UpdateToDatabase((int)selectedNode.Tag);
            }
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (selectedNode != null)
                dry.UpdateToDatabase((int)selectedNode.Tag);
            Close();
        }
        private void NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            selectedNode = e.Node;
            tree_selectItem(selectedNode);
        }

        private void buttonSayTuDong(object sender, EventArgs e)
        {
            fChuongtrinhSay f = new fChuongtrinhSay();
            f.Show();
        }
        private void conFigOpen(object sender, EventArgs e)
        {
            comboBox1CongCom.Text= QuyTrinhSayGo.configTdin.com;
        }
        private void selChange(object sender, EventArgs e)
        {
            QuyTrinhSayGo.configTdin.com = comboBox1CongCom.Text;
        }

        private void TaoChuongTrinhSay(object sender, EventArgs e)
        {
            // tao chuong trinh say moi
            TaoChuongTringSayMoi();
        }
        // Sửa thông tin menu thành menu thuộc tính chương trình sấy
        void setMenuTep()
        {
            string file = System.IO.Path.GetFileName(dryerLocal.fileName);
            ghiLạiToolStripMenuItem.Text = "Lưu " + file;
            ghiLạiThànhTệpKhácToolStripMenuItem.Text = "Lưu " + file + " thành...";
        }
        void TaoChuongTringSayMoi()
        {
            createNewProject f = fDulieuGo;
            f.dryLocal = dryerLocal;

            if (DialogResult.Cancel == f.ShowDialog()) return;

            fChuongTrinhSayTuDong.setThongTinChung(dryerLocal.getThongTin());
            // su thong tin menu 
            setMenuTep();
        }
        void ThoatChuongTrinh()
        {
            // hoi xem co ghi du lieu lai khong
            if (DialogResult.OK == MessageBox.Show("Bạn có ghi lại dữ liệu không?"))
            {
                dryerLocal.write();
            };

            // ghi lai du lieu cònig
            configTdin.writeConfig();
        }
        void open()
        {
            // mo chuong trinh say
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Chọn tệp ";
                dialog.Filter = "Files(*.con)|*.con|All files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;
                dialog.CheckFileExists = true;
                if (workingDirectory != String.Empty) dialog.InitialDirectory = workingDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    dryerLocal.fileName = dialog.FileName;
                    setMenuTep();
                }
            }
        }
        void save()
        {
            dryerLocal.write();
        }
        void saveAs()
        {
            // chon tep ghi lại
            // mo chuong trinh say
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Chọn tệp ";
                dialog.Filter = "Files(*.con)|*.con|All files (*.*)|*.*";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = true;
                dialog.CheckFileExists = true;
                if (workingDirectory != String.Empty) dialog.InitialDirectory = workingDirectory;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    dryerLocal.fileName = dialog.FileName;
                    // kiem tra tep da ton tại chua
                    save();
                }
            }
        }

        private void FomChuuongTrinhClose(object sender, FormClosedEventArgs e)
        {
            ThoatChuongTrinh();
        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }



        private void tabPage1ThuVienTinhToan_Click(object sender, EventArgs e)
        {

        }

        private void buttonDocTreeDatabase(object sender, EventArgs e)
        {
            CreateTreeCheDoSay();
        }

        void loadForm_ControlForm(TabPage tab, Form a)
        {
            a.TopLevel = false;
            a.Visible = true;
            a.Dock = DockStyle.Fill;
            a.FormBorderStyle = FormBorderStyle.None;
            tab.Controls.Add(a);
        }
        
        void loadForm_DuLieuGo()
        {
            loadForm_ControlForm(tabPage2DuLieuGo, fDulieuGo);
        }
        void loadForm_NCC()
        {

            loadForm_ControlForm(tabPage8TrungTam, fChuongTrinhSayTuDong);
            fChuongTrinhSayTuDong.setThongTinChung(dryerLocal.getThongTin());
        }
        private void EnterTrungTam(object sender, EventArgs e)
        {
            if (loadncc == false)
            {
                loadncc = true;
                loadForm_NCC();
            }
        }
        private void MoDuLieuGo(object sender, EventArgs e)
        {
            if (loadDlg == false)
            {
                loadDlg = true;
                loadForm_DuLieuGo();
            }
            fDulieuGo.updata();// updata();
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void OpenDoThiSay(object sender, EventArgs e)
        {
            // ve do thi say
            chart1SayGo.Series.Clear();
            Series nhietdo = new Series("Nhiệt độ");
            //DataPoint datapoint=.d;

            foreach (Point p in QuyTrinhSayGo.dryerLocal.listNhietDo)
            {
                nhietdo.Points.AddXY(p.X,p.Y);
            }
            chart1SayGo.Series.Add(nhietdo);
        }

        private void thoátChươngTrìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ghi lai du lieu config
            // ghi lai du lieu say
            save();
            Close();
        }

        private void ghiLạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dryerLocal.write();
        }

        private void mởChươngTrìnhSấyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();
        }

        private void ghiLạiThànhTệpKhácToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAs();
        }

        private void thôngTinBảnQuyềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.ShowDialog();
        }

        private void docCom(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1DocCongCom.Start();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timer1DocCongCom.Stop();
        }

        private void button3SayTuDong_Click(object sender, EventArgs e)
        {

        }





    }
}
