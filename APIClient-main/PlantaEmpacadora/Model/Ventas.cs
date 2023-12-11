using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantaEmpacadora.Model
{
    public class Ventas
    {
        public int id { get; set; }
        public string fecha { get; set; }
        public float cantidad { get; set; }
        public string codigo { get; set; }
        public string producto { get; set; }
        public string mercado { get; set; }
        public string tipo { get; set; }

    }
}
