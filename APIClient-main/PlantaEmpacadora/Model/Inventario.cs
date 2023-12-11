using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantaEmpacadora.Model
{
    public class Inventario
    {
        public int id { get; set; }
        public string articulo { get; set; }
        public string categoria { get; set; }
        public string descripcion { get; set; }
        public string unidad { get; set; }
        public float cantidad { get; set; }
        public string fcorte { get; set; }
    }
}
