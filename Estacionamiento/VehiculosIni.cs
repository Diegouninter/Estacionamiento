using Estacionamiento.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamiento.Clases
{
    /// <summary>
    /// DEGM 11/03/2026
    /// Esta clase se encarga de cargar los vehículos estacionados en el sistema, utilizando la información de los lugares de estacionamiento. Para cada lugar ocupado, se crea un vehículo con datos ficticios y se agrega al arreglo de vehículos estacionados. También se actualiza el contador de vehículos para llevar un registro del número de vehículos actualmente estacionados.
    /// </summary>
    public static class VehiculosIni
    {
        public static void CargarVehiculos(LugarEstacionamiento[,] lugares, VehiculoEstacionado[] vehiculos, ref int contadorVehiculos)
        {
            foreach (var lugar in lugares)
            {
                if (lugar.Ocupado)
                {
                    VehiculoEstacionado v = new VehiculoEstacionado(
                        $"PLACA{lugar.NumeroLugar}",     
                        $"Cliente{lugar.NumeroLugar}",   
                        lugar.NumeroLugar,
                        DateTime.Now.AddHours(-1)       
                    );

                    for (int i = 0; i < vehiculos.Length; i++)
                    {
                        if (vehiculos[i] == null)
                        {
                            vehiculos[i] = v;
                            contadorVehiculos++;
                            break;
                        }
                    }
                }
            }
        }
    }
}



