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
        private BD repository = new BD();
        private Contadores matrizActual = new Contadores();
        private LogicaBot logicaBot = new LogicaBot();

        private int ultimaJugadaUsuario = 0;
        public FormGame()
        {
            InitializeComponent();
            this.SetVisibleCore(false);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

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
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            JugarRondaAsync(2);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            JugarRondaAsync(3);
        }
        private async Task JugarRondaAsync(int jugadaActualUsuario)
        {
            int jugadaBot = logicaBot.Predecir(ultimaJugadaUsuario, matrizActual);
            EvaluarGanador(jugadaActualUsuario, jugadaBot);
            if (ultimaJugadaUsuario != 0)
            {
                ActualizarContadoresMatriz(ultimaJugadaUsuario, jugadaActualUsuario);
                await repository.ContadoresAsync(matrizActual);
            }
            ultimaJugadaUsuario = jugadaActualUsuario;
        }
        private void ActualizarContadoresMatriz(int anterior, int actual)
        {
            if (anterior == 1) // Si antes jugó Piedra
            {
                if (actual == 1) matrizActual.Piedra_Piedra++;
                else if (actual == 2) matrizActual.Piedra_Papel++;
                else if (actual == 3) matrizActual.Piedra_Tijera++;
            }
            else if (anterior == 2) // Si antes jugó Papel
            {
                if (actual == 1) matrizActual.Papel_Piedra++;
                else if (actual == 2) matrizActual.Papel_Papel++;
                else if (actual == 3) matrizActual.Papel_Tijera++;
            }
            else if (anterior == 3) // Si antes jugó Tijera
            {
                if (actual == 1) matrizActual.Tijera_Piedra++;
                else if (actual == 2) matrizActual.Tijera_Papel++;
                else if (actual == 3) matrizActual.Tijera_Tijera++;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

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

    }
}
