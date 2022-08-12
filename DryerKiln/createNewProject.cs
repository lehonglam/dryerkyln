using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tdin20.tdinCode;

namespace DryerKiln
{
    public partial class createNewProject : Form
    {
        public dryerLibLocal dryLocal = null;
        public createNewProject()
        {
            InitializeComponent();
        }
        public void updata()
        {
            if (dryLocal == null) return;

            textBox1FileName.Text = dryLocal.fileName;
            textBoxTenCheDoSay.Text = dryLocal.Ten;
            textBoxDoAmBanDau.Text = dryLocal.DoAmDauVao.ToString();
            textBoxDoAmCuoiCung.Text = dryLocal.DoAmDauRa.ToString();
            textBoxDoDaygosay.Text = dryLocal.DoDayGoSay.ToString();
            dateTimePickerNgayTao.Value = dryLocal.NgayKhoiTao;
            textBox2NguoiLap.Text = dryLocal.TacGia;
            comboBoxCheDoSay.Text = dryLocal.CheDoSay.ToString();
            comboBoxLoaiGoSay.Text = dryLocal.NhomGo.ToString();
        }
        public void savedata()
        {
            if (dryLocal == null) return;

            dryLocal.fileName = textBox1FileName.Text;
            dryLocal.Ten = textBoxTenCheDoSay.Text;
            dryLocal.DoAmDauVao =convertText.ToDouble(textBoxDoAmBanDau.Text);
            dryLocal.DoAmDauRa=convertText.ToDouble(textBoxDoAmCuoiCung.Text);
            dryLocal.DoDayGoSay=convertText.ToDouble(textBoxDoDaygosay.Text );
            dryLocal.NgayKhoiTao = dateTimePickerNgayTao.Value;
            dryLocal.TacGia=textBox2NguoiLap.Text;
            dryLocal.CheDoSay=convertText.ToInt(comboBoxCheDoSay.Text);
            dryLocal.NhomGo = convertText.ToInt(comboBoxLoaiGoSay.Text); ;
        }

        private void OK(object sender, EventArgs e)
        {
            savedata();
            Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }

        private void createNewProject_Load(object sender, EventArgs e)
        {
            textBox1FileName.Text = "say-" + DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString()+ ".con";
            if (dryLocal == null) return;
            updata();
        }
    }
}
