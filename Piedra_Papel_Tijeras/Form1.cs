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
        private BD repository = new BD();
        private Contadores matrizActual = new Contadores();

        bool siderbarExpand; // Variable para controlar el estado del sidebar
        public Form1()
        {
            InitializeComponent();

            HabilitarDobleBufer(this);
        }

        private void HabilitarDobleBufer(Control control)
        {
            typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance)
                ?.SetValue(control, true, null);

            foreach (Control hijo in control.Controls)
            {
                HabilitarDobleBufer(hijo);
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                matrizActual = await repository.ObtenerContadoresAsync(); 
        
                System.Diagnostics.Debug.WriteLine($" Se cargaron {matrizActual.Total_Aprendizaje} jugadas de la BD."); 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($" No se pudo cargar de Firebase: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormGame NuevoJuego = new FormGame();
            NuevoJuego.Visible = true;
            this.Visible = false;
        }

        private void Sidebar_Timer_Tick(object sender, EventArgs e)
        {
            Sidebar.SuspendLayout();
            if (siderbarExpand)
            {
                Sidebar.Width -=10;
                if (Sidebar.Width == Sidebar.MinimumSize.Width)
                {
                    siderbarExpand = false;
                    Sidebar_Timer.Stop();
                }
            }
            else
            {
                Sidebar.Width +=10;
                if (Sidebar.Width == Sidebar.MaximumSize.Width)
                {
                    siderbarExpand = true;
                    Sidebar_Timer.Stop();
                }
            }
            Sidebar.ResumeLayout();
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            Sidebar_Timer.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 aprendizaje = new Form3();
            aprendizaje.Visible = true;
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
