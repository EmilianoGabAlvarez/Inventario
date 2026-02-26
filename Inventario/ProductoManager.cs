using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inventario
{
    internal class ProductoManager
    {
        private List<Producto> _listaProductos = new List<Producto>();
        private readonly string _nombreArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "productos.json");
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };


        public void GuardarDatos()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(_listaProductos, _options);

                File.WriteAllText(_nombreArchivo, jsonString);

                Console.WriteLine("\n💾 Datos guardados en el disco correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error al guardar el archivo: {ex.Message}");
            }
        }

        public void CargarDatos()
        {
            try
            {
                if (File.Exists(_nombreArchivo))
                {
                    string jsonString = File.ReadAllText(_nombreArchivo);

                    _listaProductos = JsonSerializer.Deserialize<List<Producto>>(jsonString, _options)
                                     ?? new List<Producto>();

                    Console.WriteLine("\n📂 Datos cargados exitosamente desde el JSON");
                }
                else
                {
                    Console.WriteLine("\nℹ️ No se encontró archivo previo. Se iniciará un inventario vacío");
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Error al leer el archivo: {ex.Message}");
            }
        }

        public void CantidadGuardada()
        {
            if (_listaProductos.Count > 0)
            {
                 int num= _listaProductos.Max(p => p.Id);
            }
            else { Producto.SetContador(0); }

        }
        public string AgregarNombre()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese el nombre del producto");
                    string nombre = Console.ReadLine();
                    if (nombre.Any(char.IsDigit))
                    {
                        Console.WriteLine("\n Debe ingresar el nombre del producto sin numeros");
                        continue;
                    }
                    else if (string.IsNullOrEmpty(nombre))
                    {
                        Console.WriteLine("\n No se puede ingresar un nombre en blanco");
                        continue;
                    }
                    return nombre;
                }
                catch (Exception e)
                {

                    Console.WriteLine($"\nℹ️ Error: {e.Message}");
                }
            }
        }

        public decimal AgregarPrecio()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese el IMPORTE unitario del producto");
                    decimal importe = Decimal.Parse(Console.ReadLine());
                    if (importe < 0)
                    {
                        Console.WriteLine("El IMPORTE UNITARIO DEBE SER POSTIVO");
                        continue;
                    }
                    return importe;
                }
                catch (Exception)
                {

                    Console.WriteLine($"Debe ingresar un valor numerico para el importe");
                    continue;
                }
            }
        }
        public int AgregarStock()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese el STOCK: ");
                    int stock = int.Parse(Console.ReadLine());
                    if (stock < 0)
                    {
                        Console.WriteLine("Debe ingresar un STOCK positivo");
                        continue;
                    }
                    return stock;
                }
                catch (Exception)
                {

                    Console.WriteLine("Debe ingresar solo numeros en el STOCK");
                    continue;
                }
            }
        }
        public void AgregarProducto()
        {
            int id;
            string guardaNombre = AgregarNombre();
            decimal guardarPrecio = AgregarPrecio();
            int guardarStock = AgregarStock();
            Producto prod = new Producto(guardaNombre,guardarPrecio,guardarStock);
            _listaProductos.Add(prod);
            Console.WriteLine();
            Console.WriteLine("Producto cargado exitosamente");
            Console.WriteLine();

        }

        public void AgregarListadoProductos()
        {
            int cant = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese la cantidad de Productos a guardar");
                    cant = int.Parse(Console.ReadLine());
                    if (cant < 0)
                    {
                        Console.WriteLine("Debe ingresar un numero positivo de productos a guardar");
                        continue;
                    }
                    break;
                }
                catch (Exception)
                {

                    Console.WriteLine("Debe ingresar solamente la cantidad de productos a guardar");
                    continue;
                }
            }
            for (int i = 0; i < cant; i++)
            {
                try
                {
                    AgregarProducto();
                }
                catch (Exception)
                {
                    Console.WriteLine("No se pudo agregar, intente nuevamente");
                    i--;
                }
            }
            GuardarDatos();
        }

        public void mostrarListadoProductos()
        {
            foreach (var item in _listaProductos)
            {
                Console.WriteLine("ID: "+item.Id);
                Console.WriteLine("Nombre: "+ item.Nombre); 
                Console.WriteLine("Precio: "+ item.Precio); 
                Console.WriteLine("Stock: "+ item.Stock);
                Console.WriteLine();
            }
        }

    }
}

