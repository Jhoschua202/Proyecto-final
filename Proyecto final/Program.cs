using System;
using System.Collections.Generic;

class RestauranteJhoschua
{
    static void Main()
    {
        Console.Write("Bienvenido, ingrese su nombre por favor: ");
        string cliente = Console.ReadLine();
        Console.WriteLine($"¡Bienvenido {cliente} a Restaurante Jhoschua!");

        List<string> platillosSeleccionados = new List<string>();
        List<double> costosPlatillos = new List<double>();

        int opcionMenu;
        do
        {
            MostrarMenuPrincipal();
            opcionMenu = ObtenerOpcionUsuario(4);

            switch (opcionMenu)
            {
                case 1:
                    ProcesarCategoria("Entradas", new List<string> { "Sopa de Tomate", "Nachos", "Ensalada" },
                                      new List<double> { 5.99, 7.99, 6.50 }, platillosSeleccionados, costosPlatillos);
                    break;
                case 2:
                    ProcesarCategoria("Platos Fuertes", new List<string> { "Pizza", "Hamburguesa", "Pasta" },
                                      new List<double> { 12.99, 10.50, 13.99 }, platillosSeleccionados, costosPlatillos);
                    break;
                case 3:
                    ProcesarCategoria("Postres", new List<string> { "Helado", "Pastel de chocolate", "Fruta fresca" },
                                      new List<double> { 4.50, 5.75, 3.99 }, platillosSeleccionados, costosPlatillos);
                    break;
                case 4:
                    Console.WriteLine("Generando la cuenta final...");
                    MostrarCuentaFinal(platillosSeleccionados, costosPlatillos);
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                    break;
            }

        } while (opcionMenu != 4);
    }

    static void MostrarMenuPrincipal()
    {
        Console.WriteLine("\n=============MENÚ PRINCIPAL=============");
        Console.WriteLine("Seleccione una categoría:");
        Console.WriteLine("1. Entradas");
        Console.WriteLine("2. Platos Fuertes");
        Console.WriteLine("3. Postres");
        Console.WriteLine("4. Salir");
        Console.Write("Ingrese su opción: ");
    }

    static int ObtenerOpcionUsuario(int maxOpciones)
    {
        int opcion;
        while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > maxOpciones)
        {
            Console.Write($"Opción no válida. Por favor ingrese un número entre 1 y {maxOpciones}: ");
        }
        return opcion;
    }

    static void ProcesarCategoria(string categoria, List<string> platillos, List<double> precios, List<string> platillosSeleccionados, List<double> costosPlatillos)
    {
        Console.WriteLine($"\nBienvenido a la Categoría: {categoria}");
        int opcionPlatillo;

        do
        {
            MostrarPlatillos(platillos, precios);
            opcionPlatillo = ObtenerOpcionUsuario(platillos.Count + 1);

            if (opcionPlatillo == platillos.Count + 1)
            {
                Console.WriteLine("Regresando al menú principal...");
                break;
            }

            double precioBase = precios[opcionPlatillo - 1];

            Console.Write("Ingrese la cantidad que desea ordenar (máximo 50): ");
            int cantidad = ObtenerCantidadUsuario(50);

            double descuento = CalcularDescuento(cantidad);
            double precioFinal = precioBase * cantidad * (1 - descuento);

            // Agregamos lógica de combo
            if (cantidad >= 3)
            {
                Console.WriteLine("¡Felicidades! Has activado un combo especial.");
                precioFinal *= 0.90;  // Descuento adicional por combo
            }

            platillosSeleccionados.Add($"{platillos[opcionPlatillo - 1]} x{cantidad}");
            costosPlatillos.Add(precioFinal);

            MostrarResumenCompra(precioBase, descuento, precioFinal, cantidad);
            Console.Write("¿Desea seguir comprando?: ");
            Console.Write("1 SI: ");
            Console.Write("2 NO: ");


            string seguirComprando = Console.ReadLine().ToLower();
            if (seguirComprando != "1")
            {
                Console.WriteLine("Generando la cuenta final...");
                MostrarCuentaFinal(platillosSeleccionados, costosPlatillos);
                return;
            }

        } while (opcionPlatillo != platillos.Count + 1);
    }

    static void MostrarPlatillos(List<string> platillos, List<double> precios)
    {
        Console.WriteLine("\nSeleccione un platillo:");
        for (int i = 0; i < platillos.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {platillos[i]} - L{precios[i]:F2}");
        }
        Console.WriteLine($"{platillos.Count + 1}. Regresar al menú principal");
    }

    static int ObtenerCantidadUsuario(int maxCantidad)
    {
        int cantidad;
        while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad < 1 || cantidad > maxCantidad)
        {
            Console.Write($"Cantidad no válida. Ingrese un número entre 1 y {maxCantidad}: ");
        }
        return cantidad;
    }

    static double CalcularDescuento(int cantidad)
    {
        if (cantidad >= 10)
            return 0.10;  // Descuento del 10% si compra 10 o más
        else if (cantidad >= 5)
            return 0.05;  // Descuento del 5% si compra entre 5 y 9
        return 0;
    }

    static void MostrarResumenCompra(double precioUnitario, double descuento, double precioFinal, int cantidad)
    {
        Console.WriteLine($"\nResumen de su compra:");
        Console.WriteLine($"Precio unitario: L{precioUnitario:F2}");
        Console.WriteLine($"Descuento aplicado: {descuento * 100}%");
        Console.WriteLine($"Precio total por {cantidad} unidades: L{precioFinal:F2}\n");
    }

    static void MostrarCuentaFinal(List<string> platillos, List<double> costos)
    {
        double total = 0;
        Console.WriteLine("\n==============CUENTA FINAL==============");
        for (int i = 0; i < platillos.Count; i++)
        {
            Console.WriteLine($"{platillos[i]} - L{costos[i]:F2}");
            total += costos[i];
        }
        Console.WriteLine($"TOTAL A PAGAR: L{total:F2}\n¡Gracias por su compra!");
    }
}