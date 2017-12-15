using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Executor.Formularios.Visualizacao
{
    public class MapaBits
    {
        public int CoordenadaVertical;
        public int CoordenadaHorizontal;
        public int NumeroBitsRegiao;
        public double Normalizado;


        public MapaBits(int CoordenadaVertical, int CoordenadaHorizontal, int NumeroBitsRegiao, double Normalizado)
        {
            this.CoordenadaVertical = CoordenadaVertical;
            this.CoordenadaHorizontal = CoordenadaHorizontal;
            this.NumeroBitsRegiao = NumeroBitsRegiao;
            this.Normalizado = Normalizado;

        }
    }
}
