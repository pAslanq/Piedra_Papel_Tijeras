using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Piedra_Papel_Tijeras
{
    
    public partial class FormGame : Form
    {

        private const int LIMITE_APRENDIZAJE = 200;
        bool siderbarExpand; // Variable para controlar el estado del sidebar
        private BD repository = new BD();
        private Contadores matrizActual = new Contadores();
        private LogicaBot logicaBot = new LogicaBot();
        int countJugador = 0;
        int countBot = 0;


        private int ultimaJugadaUsuario = 0;
        public FormGame()
        {
            InitializeComponent();
            this.SetVisibleCore(false);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

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
        private async Task FormGame_LoadAsync(object sender, EventArgs e)
        {
            matrizActual = await repository.ObtenerContadoresAsync();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
            Form1 form1 = new Form1();
            this.Visible = false;
            form1.Visible = true;

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
            int jugadaBot = logicaBot.Predecir(ultimaJugadaUsuario, matrizActual);
            string ganador = EvaluarGanador(jugadaActualUsuario, jugadaBot);
            ContadoresVisibles(ganador);
            if (jugadaBot == 1) pictureBox8.Image = Properties.Resources.ManoPiedraRotada;
            else if (jugadaBot == 2) pictureBox8.Image = Properties.Resources.ManoPapelRotada;
            else if (jugadaBot == 3) pictureBox8.Image = Properties.Resources.ManoTijeraRotada;
            if(ganador == "¡Ganaste tú!")
            {
                pictureBox5.Image = Properties.Resources.ChangoHappy;
                pictureBox9.Image = Properties.Resources.BotRed;
            } else if(ganador == "¡Gana el Bot!")
            {
                pictureBox5.Image = Properties.Resources.ChangoSad;
                pictureBox9.Image = Properties.Resources.BotHappy;
            } else
            {
                pictureBox5.Image = Properties.Resources.ChangoAngry;
                pictureBox9.Image = Properties.Resources.BotDamage;
            }
            await Task.Delay(2000);
            pictureBox5.Image = Properties.Resources.Chango;
            pictureBox9.Image = Properties.Resources.Bot;
            if (ultimaJugadaUsuario != 0)
            {
                ActualizarContadoresMatriz(ultimaJugadaUsuario, jugadaActualUsuario);
                await repository.ContadoresAsync(matrizActual);
            }
            ultimaJugadaUsuario = jugadaActualUsuario;
        }
        private void ActualizarContadoresMatriz(int anterior, int actual)
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
            Console.WriteLine($"[IA Aprendizaje] Transiciones registradas: {matrizActual.Total_Aprendizaje} / {LIMITE_APRENDIZAJE}");
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Total_Aprendizaje = {matrizActual.Total_Aprendizaje}");
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
                   label3.Text = ("HUMANO:" + ++countJugador).ToString();
                    break;
            case "¡Gana el Bot!":
                    label4.Text = ("BOT:" + ++countBot).ToString();
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
            //Controla el maximo y minimo del sidebar, expandiendolo o contrayendolo dependiendo de su estado
            if (siderbarExpand)
            {
                Sidebar.Width -= 3;
                if (Sidebar.Width == Sidebar.MinimumSize.Width)
                {
                    siderbarExpand = false;
                    Sidebar_Timer.Stop();
                }
            }
            else
            {
                Sidebar.Width += 3;
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
    }
}
