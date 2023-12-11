using System;
using System.ComponentModel;

namespace PlantaEmpacadora.Model
{
    public class Liq
    {
        [DisplayName("FECHA")]
        public DateTime? Fecha { get; set; }
        [DisplayName("LOTE")]
        public int Lote { get; set; }
        [DisplayName("TANQUEROS")]
        public int Tanqueros { get; set; }
        [DisplayName("RANGO FILETES")]
        public float Rangofiletes { get; set; }
        [DisplayName("PECES TOTALES RECIBIDOS")]
        public float PecesTotalesRecibidos { get; set; }
        [DisplayName("PECES A FILETEAR")]
        public int PecesFiletear { get; set; }
        [DisplayName("FINCA")]
        public float Finca { get; set; }
        [DisplayName("MAREL")]
        public float Marel { get; set; }
        [DisplayName("FILETEABLE")]
        public decimal? Filetable { get; set; }
        [DisplayName("DECLARADAS_A")]
        public float DeclaradasA { get; set; }
        [DisplayName("RECIBIDAS_B")]
        public float RecibidasB { get; set; }
        [DisplayName("DIFRENCIA_A_B")]
        public decimal? DiferenciaAB { get; set; }

        [DisplayName("DESCARTE_muertos")]
        public float DescarteMuertos { get; set; }
        [DisplayName("RECEPCION")]
        public decimal? Recepcion { get; set; }
        [DisplayName("TOTAL")]
        public decimal? Total { get; set; }
    }
}
