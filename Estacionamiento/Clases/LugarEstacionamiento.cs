using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamiento.Clases
{
    /// <summary>
    /// DEGM 11/03/2026
    /// Esta clase representa un lugar de estacionamiento, con su número, tipo, precio por
    /// </summary>
    public class LugarEstacionamiento
    {
        public int NumeroLugar { get; set; }
        public string Tipo { get; set; }
        public decimal PrecioPorHora { get; set; }
        public bool Ocupado { get; set; }

        public LugarEstacionamiento(int numero, string tipo, decimal precio, bool ocupado)
        {
            NumeroLugar = numero;
            Tipo = tipo;
            PrecioPorHora = precio;
            Ocupado = ocupado;
        }

        public void MostrarInfo()
        {
            Console.WriteLine($"Lugar {NumeroLugar} - {Tipo} - ${PrecioPorHora}/hora - {(Ocupado ? "OCUPADO" : "LIBRE")}");
        }
    }
}




