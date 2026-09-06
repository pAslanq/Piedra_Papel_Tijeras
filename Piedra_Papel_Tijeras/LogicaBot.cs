using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Piedra_Papel_Tijeras
{
    internal class LogicaBot
    {
        private Random ram = new Random();

        public int Predecir(int ultima, Contadores matriz)
        {
            if (ultima == 0) return ram.Next(1, 4);
            int recPiedra = 0, recPapel = 0, recTijera = 0;

            switch (ultima)
            {

                case 1:
                    recPiedra = matriz.Piedra_Piedra;
                    recPapel = matriz.Piedra_Papel;
                    recTijera = matriz.Piedra_Tijera;
                break;

                case 2:
                    recPiedra = matriz.Papel_Piedra;
                    recPapel = matriz.Papel_Papel;
                    recTijera = matriz.Papel_Tijera;
                break;

                case 3:
                    recPiedra = matriz.Tijera_Piedra;
                    recPapel = matriz.Tijera_Papel;
                    recTijera = matriz.Tijera_Tijera; 
                break;
            }

            int predic = 1;
            if(recPapel > recPiedra && recPapel >= recTijera)   predic = 2;
            else if(recTijera > recPiedra && recTijera > recPapel) predic = 3;

            switch (predic)
            {
                case 1:     return 2;   break;
                case 2:     return 3;   break;
                case 3:     return 1;   break;
                default:    return ram.Next(1, 4); break;

            }
        }
    }
}
