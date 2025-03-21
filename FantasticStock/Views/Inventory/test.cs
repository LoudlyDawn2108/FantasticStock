using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views.Inventory
{
    public partial class test : UserControl
    {
        public test()
        {
            InitializeComponent();
        }
        private void FlowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            //// Nếu panel2 chuyển xuống dòng (nằm dưới panel1)
            //if (panel2.Location.Y > panel1.Location.Y)
            //{
            //    // Đặt width cho panel1 bằng width của flowLayoutPanel
            //    panel1.Width = flowLayoutPanel1.Width - flowLayoutPanel1.Padding.Left - flowLayoutPanel1.Padding.Right - 6;
            //}
            //else
            //{
            //    // Nếu 2 panel nằm cạnh nhau, quay lại width ban đầu
            //    panel1.Width = 200; // hoặc giá trị width ban đầu của panel1
            //}
        }
    }

}
