using Estacionamiento.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamiento.Clases
{
    public static class VehiculosIni
    {
        public static void CargarVehiculos(LugarEstacionamiento[,] lugares, VehiculoEstacionado[] vehiculos, ref int contadorVehiculos)
        {
            // Recorremos todos los lugares
            foreach (var lugar in lugares)
            {
                if (lugar.Ocupado)
                {
                    // Creamos un vehículo ficticio para cada lugar ocupado
                    VehiculoEstacionado v = new VehiculoEstacionado(
                        $"PLACA{lugar.NumeroLugar}",     // placa ficticia
                        $"Cliente{lugar.NumeroLugar}",   // nombre ficticio
                        lugar.NumeroLugar,
                        DateTime.Now.AddHours(-1)       // hora de entrada hace 1 hora
                    );

                    // Lo agregamos al arreglo de vehículos
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



