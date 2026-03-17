using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario
{
    internal class Menu
    {
        
        public void menu()
        {
        ProductoManager prod=new ProductoManager();
        prod.CargarDatos();
        int guardados=prod.CantidadGuardada();
            Console.WriteLine($"{guardados} Es la cantidad de productos guardados");
            Console.WriteLine();
            MenuReporte menuReportes=new MenuReporte(prod);
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" -- MENU PRINCIPAL -- ");
                Console.WriteLine();
                Console.WriteLine("1- Agregar PRODUCTOS  ");
                Console.WriteLine("2- Mostrar todos los PRODUCTO");
                Console.WriteLine("3- Mostrar PRODUCTO por ID");  
                Console.WriteLine("4- Dar de baja un PRODUCTO");    
                Console.WriteLine("5- Actualizar STOCK por ID");  
                Console.WriteLine("6- Buscar por palabra");  
                Console.WriteLine("7- Menu reportes");  
                Console.WriteLine();
                Console.WriteLine("0- Para salir");
                Console.WriteLine();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                        case "1":prod.AgregarListadoProductos();
                        break;
                        
                        case "2" :prod.mostrarListadoProductos();
                        break;

                        case "3" :prod.mostarProductoPorId();
                        break;
                        
                        case "4" :prod.BajaxID();
                        break;

                        case "5" :prod.actualizarStockXid();
                        break;

                        case "6" :prod.buscarXpalabra();
                        break;

                        case "7" :menuReportes.mostrarMenu();
                        break;

                    default:
                        Console.WriteLine("Gracias por usar nuestro software !");
                        return;

                }
            }
        }



    }
}
