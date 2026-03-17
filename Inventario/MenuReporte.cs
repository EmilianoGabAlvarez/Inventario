using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario
{
    internal class MenuReporte
    {
        private ProductoManager _manager;

        public MenuReporte(ProductoManager manager)
        {
            _manager = manager;
        }
        public void mostrarMenu()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine(" -- MENU REPORTES -- ");
                Console.WriteLine();
                Console.WriteLine("1- Reporte valor de mercaderia por ID");
                Console.WriteLine("2- Reporte de pocas unidades");
                Console.WriteLine("3- Reporte de productos dado de baja");
                Console.WriteLine("0- Salir del menu reportes");


                string opc =Console.ReadLine() ;
                switch (opc)
                {
                    case "1":_manager.RmostrarValorTotalxID();
                        break;

                    case "2":
                        _manager.RmostrarBajoStock();
                        break;

                    case "3":_manager.RMostrarDadoDebaja();
                        break;

                    case "0":
                          return;       

                    default:
                        break;
                }

            }
        }
    }
}
