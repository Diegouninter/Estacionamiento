using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Estacionamiento.Clases;

namespace Estacionamiento
{
    /// <summary>
    /// DEGM 11/03/2026
    ///Este es el menu de mi codigo, aqui se crean los lugares de estacionamiento, se cargan los vehiculos y se muestra el menu para interactuar con el sistema.
    /// </summary>
    class Program
    {
        static void Main()
        {

            LugarEstacionamiento[,] lugares = new LugarEstacionamiento[3, 4];

        decimal precioEconomico = 20, precioPreferencial = 35, precioVIP = 50;
        for (int piso = 0; piso < 3; piso++)
        {
            for (int lugar = 0; lugar < 4; lugar++)
            {
                int numeroLugar = (piso + 1) * 100 + (lugar + 1);
                string tipo = piso == 0 ? "Económico" : piso == 1 ? "Preferencial" : "VIP";
                decimal precio = piso == 0 ? precioEconomico : piso == 1 ? precioPreferencial : precioVIP;

                bool ocupado = (lugar % 2 == 0); 

                lugares[piso, lugar] = new LugarEstacionamiento(numeroLugar, tipo, precio, ocupado);
            }
        }

        VehiculoEstacionado[] vehiculos = new VehiculoEstacionado[12];
        int contadorVehiculos = 0;

        VehiculosIni.CargarVehiculos(lugares, vehiculos, ref contadorVehiculos);

        int opcion;
            do
            {
                Console.WriteLine("\n=== SISTEMA DE ESTACIONAMIENTO ===");
                Console.WriteLine("1. Ver disponibilidad de lugares");
                Console.WriteLine("2. Registrar entrada de vehículo");
                Console.WriteLine("3. Registrar salida de vehículo");
                Console.WriteLine("4. Ver vehículos estacionados");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción inválida.");
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("\n--- Disponibilidad ---");
                        foreach (var lugar in lugares)
                            lugar.MostrarInfo();
                        break;

                    case 2:
                        Console.WriteLine("\n--- Lugares disponibles ---");
                        foreach (var lugar in lugares)
                            if (!lugar.Ocupado) lugar.MostrarInfo();

                        Console.Write("Ingrese número de lugar: ");
                        if (!int.TryParse(Console.ReadLine(), out int numLugar))
                        {
                            Console.WriteLine("Número inválido.");
                            break;
                        }

                        LugarEstacionamiento lugarSeleccionado = null;
                        for (int i = 0; i < lugares.GetLength(0); i++)
                        {
                            for (int j = 0; j < lugares.GetLength(1); j++)
                            {
                                if (lugares[i, j].NumeroLugar == numLugar)
                                {
                                    lugarSeleccionado = lugares[i, j];
                                    break;
                                }
                            }
                            if (lugarSeleccionado != null) break;
                        }

                        if (lugarSeleccionado == null)
                        {
                            Console.WriteLine("Lugar no encontrado.");
                            break;
                        }

                        if (lugarSeleccionado.Ocupado)
                        {
                            Console.WriteLine("El lugar ya está ocupado.");
                            break;
                        }

                        Console.Write("Ingrese placa: ");
                        string placa = Console.ReadLine();
                        Console.Write("Ingrese nombre del cliente: ");
                        string nombre = Console.ReadLine();

                        lugarSeleccionado.Ocupado = true;
                        VehiculoEstacionado nuevo = new VehiculoEstacionado(placa, nombre, lugarSeleccionado.NumeroLugar, DateTime.Now);
                        bool agregado = false;
                        for (int i = 0; i < vehiculos.Length; i++)
                        {
                            if (vehiculos[i] == null)
                            {
                                vehiculos[i] = nuevo;
                                contadorVehiculos++;
                                agregado = true;
                                break;
                            }
                        }

                        if (agregado)
                            Console.WriteLine("Vehículo registrado correctamente.");
                        else
                            Console.WriteLine("No hay espacio en la lista de vehículos.");

                        break;

                    case 3:
                        Console.Write("Ingrese placa o número de lugar para registrar salida: ");
                        string entrada = Console.ReadLine();

                        int lugarNum;
                        VehiculoEstacionado veh = null;
                        int vehIndex = -1;

                        if (int.TryParse(entrada, out lugarNum))
                        {
                            for (int i = 0; i < vehiculos.Length; i++)
                            {
                                if (vehiculos[i] != null && vehiculos[i].LugarAsignado == lugarNum)
                                {
                                    veh = vehiculos[i]; vehIndex = i; break;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < vehiculos.Length; i++)
                            {
                                if (vehiculos[i] != null && vehiculos[i].Placa.Equals(entrada, StringComparison.OrdinalIgnoreCase))
                                {
                                    veh = vehiculos[i]; vehIndex = i; break;
                                }
                            }
                        }

                        if (veh == null)
                        {
                            Console.WriteLine("Vehículo no encontrado.");
                            break;
                        }

                        LugarEstacionamiento lugarVeh = null;
                        for (int i = 0; i < lugares.GetLength(0); i++)
                        {
                            for (int j = 0; j < lugares.GetLength(1); j++)
                            {
                                if (lugares[i, j].NumeroLugar == veh.LugarAsignado)
                                {
                                    lugarVeh = lugares[i, j]; break;
                                }
                            }
                            if (lugarVeh != null) break;
                        }

                        decimal costo = 0;
                        if (lugarVeh != null)
                        {
                            costo = veh.CalcularCosto(lugarVeh.PrecioPorHora);
                            lugarVeh.Ocupado = false;
                        }

                        // Remover vehículo
                        if (vehIndex >= 0) { vehiculos[vehIndex] = null; contadorVehiculos--; }

                        Console.WriteLine($"Salida registrada. Total a pagar: ${costo}");

                        break;

                    case 4:
                        Console.WriteLine("\n--- Vehículos estacionados ---");
                        foreach (var v in vehiculos)
                        {
                            if (v != null)
                            {
                                Console.WriteLine($"Placa: {v.Placa} | Cliente: {v.NombreCliente} | Lugar: {v.LugarAsignado} | Entrada: {v.HoraEntrada}");
                            }
                        }
                        break;

                    case 5:
                        Console.WriteLine("Saliendo...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != 5);
        }
    }
}

