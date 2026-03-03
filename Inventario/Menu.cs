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
                Console.WriteLine("1- Agregar productos  ");
                Console.WriteLine("2- Mostrar todos los productos");
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

                    default:
                        Console.WriteLine("Gracias !");
                        return;

                }
            }
        }



    }
}
