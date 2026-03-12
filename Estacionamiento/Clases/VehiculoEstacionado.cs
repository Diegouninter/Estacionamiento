using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Estacionamiento.Clases
{
    /// <summary>
    /// DEGM 11/03/2026
    /// Esta clase representa un vehículo estacionado, con su placa, nombre del cliente, lugar asignado y hora de entrada. También tiene métodos para calcular el tiempo estacionado y el costo basado en la tarifa por hora.
    /// </summary>
    public class VehiculoEstacionado
    {
        public string Placa { get; set; }
        public string NombreCliente { get; set; }
        public int LugarAsignado { get; set; }
        public DateTime HoraEntrada { get; set; }

        public VehiculoEstacionado(string placa, string nombre, int lugarAsignado, DateTime horaEntrada)
        {
            Placa = placa;
            NombreCliente = nombre;
            LugarAsignado = lugarAsignado;
            HoraEntrada = horaEntrada;
        }

        public TimeSpan CalcularTiempo()
        {
            return DateTime.Now - HoraEntrada;
        }

        public decimal CalcularCosto(decimal tarifaPorHora)
        {
            double horas = Math.Ceiling(CalcularTiempo().TotalHours);
            return (decimal)horas * tarifaPorHora;
        }
    }
}







