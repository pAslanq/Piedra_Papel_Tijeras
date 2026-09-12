using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piedra_Papel_Tijeras
{
    public class Contadores
    {
        public int Total_Aprendizaje { get; set; } = 0;

        public int Piedra_Piedra { get; set; } = 0;
        public int Piedra_Papel { get; set; } = 0;
        public int Piedra_Tijera { get; set; } = 0;

        public int Papel_Piedra { get; set; } = 0;
        public int Papel_Papel { get; set; } = 0;
        public int Papel_Tijera { get; set; } = 0;


        public int Tijera_Piedra { get; set; } = 0;
        public int Tijera_Papel { get; set; } = 0;
        public int Tijera_Tijera { get; set; } = 0;


        public void ReiniciarContadores()
        {
            Total_Aprendizaje = 0;
            Piedra_Piedra = 0; Piedra_Papel = 0; Piedra_Tijera = 0;
            Papel_Piedra = 0; Papel_Papel = 0; Papel_Tijera = 0;
            Tijera_Piedra = 0; Tijera_Papel = 0; Tijera_Tijera = 0;
        }
    }
}
