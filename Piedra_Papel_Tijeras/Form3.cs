using System;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Piedra_Papel_Tijeras
{
    public partial class Form3 : Form
    {
        bool siderbarExpand;
        private BD bd;
        private Contadores matrizActual = new Contadores();
        private PrivateFontCollection coleccionFuentes;


        public Form3()
        {
            InitializeComponent();
            bd = new BD();
            this.Load += Form3_Load;
            HabilitarDobleBufer(this);
            this.FormBorderStyle = FormBorderStyle.None;
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
        FormGame form = new FormGame();
        private async void Form3_Load(object sender, EventArgs e)
        {
            if (FormGame.entrenando)     button3.Text = "DETENER ENTRENAMIENTO";
            else button3.Text = "ENTRENAR";

            try
            {
                GestorMusica.ReproducirEnBucle(Properties.Resources.Inicio, "Menu");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ERROR] No se pudo reproducir el sonido: {ex.Message}");
            }
            dataGridView1.DataSource = null;
            var contadores = await bd.ObtenerContadoresAsync();
            dataGridView1.DataSource = ConstruirMatrizAprendizaje(contadores);

            int total = contadores.Piedra_Piedra + contadores.Piedra_Papel + contadores.Piedra_Tijera
                      + contadores.Papel_Piedra + contadores.Papel_Papel + contadores.Papel_Tijera
                      + contadores.Tijera_Piedra + contadores.Tijera_Papel + contadores.Tijera_Tijera;

            label3.Text = $"JUGADAS APRENDIDAS: {total}";
        }

        private DataTable ConstruirMatrizAprendizaje(Contadores c)
        {
            var tabla = new DataTable();
            tabla.Columns.Add("Jugada Anterior");
            tabla.Columns.Add("-> PIEDRA");
            tabla.Columns.Add("-> PAPEL");
            tabla.Columns.Add("-> TIJERA");
            tabla.Columns.Add("TOTAL");

            AgregarFila(tabla, "PIEDRA", c.Piedra_Piedra, c.Piedra_Papel, c.Piedra_Tijera);
            AgregarFila(tabla, "PAPEL", c.Papel_Piedra, c.Papel_Papel, c.Papel_Tijera);
            AgregarFila(tabla, "TIJERA", c.Tijera_Piedra, c.Tijera_Papel, c.Tijera_Tijera);

            return tabla;
        }

        private void AgregarFila(DataTable tabla, string jugadaAnterior, int p, int pa, int t)
        {
            int total = p + pa + t;
            string Col(int valor) => total > 0 ? $"{valor} ({(valor * 100.0 / total):0.#}%)" : "0";

            tabla.Rows.Add(jugadaAnterior, Col(p), Col(pa), Col(t), total);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Visible = false;
            form1.Visible = true;
        }
        private void Sidebar_Timer_Tick(object sender, EventArgs e)
        {
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
            Form1 form1 = new Form1();
            this.Close();
            form1.Visible = true;

        }


        private async void button1_Click(object sender, EventArgs e)
        {
            matrizActual.ReiniciarContadores();
            await bd.ObtenerContadoresAsync(matrizActual);
            Form3_Load(sender, e);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            FormGame.entrenando = !FormGame.entrenando;

            if (FormGame.entrenando)
            {
                FormGame juego = new FormGame();
                juego.Show();
                this.Close();
                button3.Text = "Detener Entrenamiento";
            }
            else
            {
                FormGame juego = new FormGame();
                juego.Show();
                button3.Text = "ENTRENAR";
                this.Close();
            }
        }
    }
}