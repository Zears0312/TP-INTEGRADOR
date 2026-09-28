using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Producto> inventario = new List<Producto>();
        inventario.Add(new Producto("Notebook", 1500.00m, 3));
        inventario.Add(new Producto("Auriculares", 50.00m, 10));
        inventario.Add(new Producto("Monitor", 250.00m, 5));

        Pedido pedidoActual = new Pedido();
        bool ejecutando = true;

        while (ejecutando)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE COMPRAS ===");
            Console.WriteLine("1. Ver Inventario");
            Console.WriteLine("2. Agregar Producto al Pedido");
            Console.WriteLine("3. Ver Resumen y Pagar");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            string entrada = Console.ReadLine();
            int opcion;

            if (int.TryParse(entrada, out opcion))
            {
                switch (opcion)
                {
                    case 1:
                        MostrarInventario(inventario);
                        break;
                    case 2:
                        GestionarCompra(inventario, pedidoActual);
                        break;
                    case 3:
                        MostrarResumen(pedidoActual);
                        break;
                    case 4:
                        ejecutando = false;
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Error: Debe ingresar un número entero.");
            }

            if (ejecutando && opcion != 4)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    static void MostrarInventario(List<Producto> inventario)
    {
        Console.WriteLine("\n--- INVENTARIO DISPONIBLE ---");
        for (int i = 0; i < inventario.Count; i++)
        {
            Producto p = inventario[i];
            Console.WriteLine((i + 1) + ". " + p.Nombre + " | Precio: $" + p.Precio + " | Stock: " + p.Stock);
        }
    }

    static void GestionarCompra(List<Producto> inventario, Pedido pedido)
    {
        MostrarInventario(inventario);
        Console.Write("Ingrese el número del producto: ");

        string entradaSeleccion = Console.ReadLine();
        int seleccion;

        if (int.TryParse(entradaSeleccion, out seleccion))
        {
            int indiceReal = seleccion - 1;

            if (indiceReal >= 0 && indiceReal < inventario.Count)
            {
                Producto productoElegido = inventario[indiceReal];

                Console.Write("Ingrese la cantidad que desea comprar: ");
                string entradaCantidad = Console.ReadLine();
                int cantidad;

                if (int.TryParse(entradaCantidad, out cantidad))
                {
                    bool agregado = pedido.AgregarProducto(productoElegido, cantidad);
                    if (agregado)
                    {
                        Console.WriteLine("Producto agregado con éxito");
                    }
                    else
                    {
                        Console.WriteLine("Stock insuficiente para realizar la operación.");
                    }
                }
                else
                {
                    Console.WriteLine("Cantidad no válida.");
                }
            }
            else
            {
                Console.WriteLine("El número de producto seleccionado no existe.");
            }
        }
        else
        {
            Console.WriteLine("Selección no válida.");
        }
    }

    static void MostrarResumen(Pedido pedido)
    {
        Console.WriteLine("--- RESUMEN DEL PEDIDO ---");
        
        decimal impuesto = 0.21m; 
        decimal totalFinal = pedido.CalcularTotal(impuesto);

        Console.WriteLine("Estado actual: " + pedido.Estado);
        Console.WriteLine("Total a pagar (con IVA): $" + totalFinal);

        if (pedido.Estado == "Pendiente")
        {
            Console.Write("¿Desea realizar el pago? (s/n): ");
            string respuesta = Console.ReadLine();

            if (respuesta != null)
            {
                string respuestaLimpia = respuesta.Trim().ToLower();
                if (respuestaLimpia == "s" || respuestaLimpia == "si")
                {
                    pedido.CompletarPago();
                    Console.WriteLine("Pago exitoso. Estado del pedido: " + pedido.Estado);
                }
            }
        }
    }
}