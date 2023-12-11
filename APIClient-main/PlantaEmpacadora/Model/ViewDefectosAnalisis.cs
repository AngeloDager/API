using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantaEmpacadora.Model
{
    public class ViewDefectosAnalisis
    {
        public int id_dist { get; set; }
        public int lote { get; set; }
        public int guia { get; set; }
        public string camaronera_logistica { get; set; }
        public string piscina_logistica { get; set; }
        public string cam_logistica_propia { get; set; }
        public string camaronera_recepcion { get; set; }
        public string piscina_recepcion { get; set; }
        public string cam_recepcion_propia { get; set; }
        public string hora_llegada { get; set; }
        public string lugar_llegada { get; set; }
        public string hora_analisis { get; set; }
        public decimal gramaje { get; set; }
        public decimal porc_bueno_entero { get; set; }
        public decimal so2_ppm { get; set; }
        public decimal cabeza_floja { get; set; }
        public decimal hepatopancreas_reventado { get; set; }
        public decimal branquias_sucias { get; set; }
        public decimal flacidez { get; set; }
        public decimal mudado { get; set; }
        public decimal cabeza_descolgada { get; set; }
        public decimal cabeza_roja { get; set; }
        public decimal cabeza_anaranjada { get; set; }
        public decimal quebrado { get; set; }
        public decimal picado_fuerte { get; set; }
        public decimal picado_leve { get; set; }
        public decimal deforme { get; set; }
        public decimal deshidratado { get; set; }
        public decimal melanosis { get; set; }
        public decimal otra_especie { get; set; }
        public decimal juveniles { get; set; }
        public string cocido_europa { get; set; }
        public decimal cabeza_amargo_f { get; set; }
        public decimal cabeza_amargo_m { get; set; }
        public decimal cabeza_amargo_l { get; set; }
        public decimal cabeza_aceptable { get; set; }
        public decimal cola_amargo_f { get; set; }
        public decimal cola_amargo_m { get; set; }
        public decimal cola_amargo_l { get; set; }
        public decimal cola_aceptable { get; set; }
        public string otros_sabores { get; set; }
        public string establecimiento_analisis { get; set; }
        public string observaciones { get; set; }
        public string camaronera { get; set; }
        public string piscina { get; set; }
        public string cam_propia { get; set; }
    }
}
