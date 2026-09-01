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
    public partial class Form1 : Form
    {
        bool siderbarExpand; // Variable para controlar el estado del sidebar
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormGame NuevoJuego = new FormGame();
            this.Visible = false;
            NuevoJuego.Visible = true;
        }

        private void Sidebar_Timer_Tick(object sender, EventArgs e)
        {
            //Controla el maximo y minimo del sidebar, expandiendolo o contrayendolo dependiendo de su estado
            if (siderbarExpand)
            {
                Sidebar.Width -= 10;
                if (Sidebar.Width == Sidebar.MinimumSize.Width)
                {
                    siderbarExpand = false;
                    Sidebar_Timer.Stop();
                }
            }
            else
            {
                Sidebar.Width += 10;
                if (Sidebar.Width == Sidebar.MaximumSize.Width)
                {
                    siderbarExpand = true;
                    Sidebar_Timer.Stop();
                }
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Sidebar_Timer.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra la aplicación
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Redirecciona a el form de los datos del aprendizaje (En proceso)
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Redirecciona a el form de entrenamiento (En proceso)
        }
    }
}
