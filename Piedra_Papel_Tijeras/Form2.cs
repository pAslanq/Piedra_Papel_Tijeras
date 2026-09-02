using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Piedra_Papel_Tijeras
{
    public partial class FormGame : Form
    {
        public FormGame()
        {
            InitializeComponent();
            this.SetVisibleCore(false);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            Form1 form1 = new Form1();
            this.Visible = false;
            form1.Visible = true;

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
