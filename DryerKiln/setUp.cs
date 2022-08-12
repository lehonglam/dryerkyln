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
namespace DryerKiln
{
    public partial class setUp : Form
    {
        public setUp()
        {
            InitializeComponent();
        }

        private void buttonCaiDatDuLieu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng này chỉ sử dụng cho lần đầu tiên sử dụng chương trình. Sẽ thực hiện xóa toàn bộ dữ liệu");
            configDrykiln cf=new configDrykiln();
            cf.setUpDataBase();

        }
    }
}
