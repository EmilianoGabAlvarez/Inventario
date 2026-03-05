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
            
            while (true)
            {
               
                Console.WriteLine(" -- MENU PRINCIPAL -- ");
                Console.WriteLine();
                Console.WriteLine("1- Agregar PRODUCTOS  ");
                Console.WriteLine("2- Mostrar todos los PRODUCTO");
                Console.WriteLine("3- Mostrar PRODUCTO por ID");  
                Console.WriteLine("4- Dar de baja un PRODUCTO");  // Falta  
                Console.WriteLine("5- Actualizar STOCK por ID");  // Falta  
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

                    default:
                        Console.WriteLine("Gracias por usar nuestro software !");
                        return;

                }
            }
        }



    }
}
