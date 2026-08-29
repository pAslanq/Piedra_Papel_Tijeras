using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piedra_Papel_Tijeras
{
    public class Contadores
    {
        public int Piedra_Piedra { get; set; } = 0;
        public int Piedra_Papel { get; set; } = 0;
        public int Piedra_Tijera { get; set; } = 0;

        public int Papel_Piedra { get; set; } = 0;
        public int Papel_Papel { get; set; } = 0;
        public int Papel_Tijera { get; set; } = 0;


        public int Tijera_Piedra { get; set; } = 0;
        public int Tijera_Papel { get; set; } = 0;
        public int Tijera_Tijera { get; set; } = 0;
    }
}
