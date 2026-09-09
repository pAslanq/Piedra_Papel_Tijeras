using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Piedra_Papel_Tijeras
{
    
    public partial class FormGame : Form
    {

        private const int LIMITE_APRENDIZAJE = 200;
        bool siderbarExpand; 
        private BD repository = new BD();
        private Contadores matrizActual = new Contadores();
        private LogicaBot logicaBot = new LogicaBot();
        int countJugador = 0;
        int countBot = 0;


        private int ultimaJugadaUsuario = 0;
        public FormGame()
        {
            InitializeComponent();
            this.Load += async (s, e) => await CargarDatosAsync();
            this.SetVisibleCore(false);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            JugarRondaAsync(1);
            pictureBox6.Image = Properties.Resources.ManoPiedra; 
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            JugarRondaAsync(2);
            pictureBox6.Image = Properties.Resources.ManoPapel;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            JugarRondaAsync(3);
            pictureBox6.Image = Properties.Resources.ManoTijera;
        }
        private async Task JugarRondaAsync(int jugadaActualUsuario)
        {
            pictureBox6.Visible = true;
            pictureBox8.Visible = true;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            int jugadaBot = logicaBot.Predecir(ultimaJugadaUsuario, matrizActual);
            string ganador = EvaluarGanador(jugadaActualUsuario, jugadaBot);
            ContadoresVisibles(ganador);
            if (jugadaBot == 1)
            {
                pictureBox8.Image = Properties.Resources.ManoPiedraRotada;
            }
            else if (jugadaBot == 2)
            {
                pictureBox8.Image = Properties.Resources.ManoPapelRotada;
            }
            else if (jugadaBot == 3)
            {
                pictureBox8.Image = Properties.Resources.ManoTijeraRotada;
            }
            if(ganador == "¡Ganaste tú!")
            {
                pictureBox5.Image = Properties.Resources.ChangoHappy;
                pictureBox9.Image = Properties.Resources.BotNojao;
                ReproducirSonido(Properties.Resources.win_monkey);
            } else if(ganador == "¡Gana el Bot!")
            {
                pictureBox5.Image = Properties.Resources.ChangoSad;
                pictureBox9.Image = Properties.Resources.BotHappy;
                ReproducirSonido(Properties.Resources.mad_monkey);
            } else
            {
                pictureBox5.Image = Properties.Resources.ChangoAngry;
                pictureBox9.Image = Properties.Resources.BotThink;
                ReproducirSonido(Properties.Resources.tite);
            }
            await Task.Delay(800);
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox6.Image = Properties.Resources.ManoPiedra;
            pictureBox8.Image = Properties.Resources.ManoPiedraRotada;
            pictureBox6.Visible = false;
            pictureBox8.Visible = false;
            pictureBox5.Image = Properties.Resources.Chango;
            pictureBox9.Image = Properties.Resources.Bot;
            await Task.Delay(200);
            pictureBox10.Image = null;


            if (ultimaJugadaUsuario != 0)
            {
                ActualizarContadoresMatriz(ultimaJugadaUsuario, jugadaActualUsuario);
                
                await repository.ObtenerContadoresAsync(matrizActual);

                System.Diagnostics.Debug.WriteLine($"Nuevo total de jugadas: {matrizActual.Total_Aprendizaje}");
            }

            ultimaJugadaUsuario = jugadaActualUsuario;
        }
        public void ActualizarContadoresMatriz(int anterior, int actual)
        {
            if (matrizActual.Total_Aprendizaje >= LIMITE_APRENDIZAJE)
            {
                return;
            }
            matrizActual.Total_Aprendizaje++;
            if (anterior == 1) 
            {
                if (actual == 1) matrizActual.Piedra_Piedra++;
                else if (actual == 2) matrizActual.Piedra_Papel++;
                else if (actual == 3) matrizActual.Piedra_Tijera++;
            }
            else if (anterior == 2) 
            {
                if (actual == 1) matrizActual.Papel_Piedra++;
                else if (actual == 2) matrizActual.Papel_Papel++;
                else if (actual == 3) matrizActual.Papel_Tijera++;
            }
            else if (anterior == 3)
            {
                if (actual == 1) matrizActual.Tijera_Piedra++;
                else if (actual == 2) matrizActual.Tijera_Papel++;
                else if (actual == 3) matrizActual.Tijera_Tijera++;
            }
        }


        private string EvaluarGanador(int usuario, int bot)
        {
            if (usuario == bot)
                return "¡Empate!";
            if ((usuario == 1 && bot == 3) || 
                (usuario == 2 && bot == 1) || 
                (usuario == 3 && bot == 2))   
            {
                return "¡Ganaste tú!";
            }
            return "¡Gana el Bot!";
        }
        private void ContadoresVisibles(string resultado) {
            
            switch (resultado) { 
            case "¡Ganaste tú!":
                   label3.Text = (++countJugador).ToString();
                   pictureBox10.Image = Properties.Resources.YouWin;
                    break;
            case "¡Gana el Bot!":
                    label4.Text = (++countBot).ToString();
                    pictureBox10.Image = Properties.Resources.YouLouse;
                    break;
            default:
                    pictureBox10.Image = Properties.Resources.NoWinner;
                    break;
            }

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form1 Principal = new Form1();
            Principal.Visible = true;
            this.Visible = false;
        }

        private void Sidebar_Timer_Tick_1(object sender, EventArgs e)
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

        private void ReproducirSonido(Stream audioStream)
        {
            try
            {
                if (audioStream != null)
                {
                    SoundPlayer player = new SoundPlayer(audioStream);
                    player.Play(); // Play() ejecuta el sonido en segundo plano sin congelar la UI
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al reproducir audio: {ex.Message}");
            }
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                // Traemos el acumulado histórico de Firebase y lo asignamos a la variable principal
                matrizActual = await repository.ObtenerContadoresAsync();

                System.Diagnostics.Debug.WriteLine($"[ÉXITO] Se cargaron {matrizActual.Total_Aprendizaje} jugadas de la BD.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ERROR] No se pudo cargar de Firebase: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 aprendizaje = new Form3();
            aprendizaje.Visible = true;
            this.Visible = false;
        }
    }
}
