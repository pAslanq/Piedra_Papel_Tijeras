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
            this.WindowState = FormWindowState.Maximized;
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            JugarRonda(1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            JugarRonda(2);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            JugarRonda(3);
        }
        private void JugarRonda(int jugadaActualUsuario)
        {
            int jugadaBot = bot.PredecirSiguienteJugada(ultimaJugadaUsuario, matrizActual);
            EvaluarGanador(jugadaActualUsuario, jugadaBot);
            if (ultimaJugadaUsuario != 0)
            {
                ActualizarContadoresMatriz(ultimaJugadaUsuario, jugadaActualUsuario);
                await repository.GuardarAprendizajeAsync(matrizActual);
            }
            ultimaJugadaUsuario = jugadaActualUsuario;
        }
        private void ActualizarContadoresMatriz(int anterior, int actual)
        {
            if (anterior == 1) // Si antes jugó Piedra
            {
                if (actual == 1) matrizActual.Piedra_Luego_Piedra++;
                else if (actual == 2) matrizActual.Piedra_Luego_Papel++;
                else if (actual == 3) matrizActual.Piedra_Luego_Tijera++;
            }
            else if (anterior == 2) // Si antes jugó Papel
            {
                if (actual == 1) matrizActual.Papel_Luego_Piedra++;
                else if (actual == 2) matrizActual.Papel_Luego_Papel++;
                else if (actual == 3) matrizActual.Papel_Luego_Tijera++;
            }
            else if (anterior == 3) // Si antes jugó Tijera
            {
                if (actual == 1) matrizActual.Tijera_Luego_Piedra++;
                else if (actual == 2) matrizActual.Tijera_Luego_Papel++;
                else if (actual == 3) matrizActual.Tijera_Luego_Tijera++;
            }
        }
    }
}
