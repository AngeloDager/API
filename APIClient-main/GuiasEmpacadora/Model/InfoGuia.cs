using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiasEmpacadora.Model
{
    public class InfoGuia
    {
        public DateTime fecha { get; set; }
        public int co_guia { get; set; }
        public string movil { get; set; }
        public string placa { get; set; }
        public int co_cama { get; set; }
        public string camaronera { get; set; }
        public string piscina { get; set; }
        public int cant_bines { get; set; }
        public string fecha_salida { get; set; }
        public string fecha_llegada { get; set; }
    }

   
}
