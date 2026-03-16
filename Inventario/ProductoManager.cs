using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

                    Console.WriteLine("\n Datos cargados exitosamente desde el JSON");
                    Console.WriteLine();
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

        public int CantidadGuardada()
        {
            int num = 0;
            if (_listaProductos.Count > 0)
            {
                num = _listaProductos.Max(p => p.Id);
            }

            return num;
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
            int nuevoId;
            CargarDatos();
            if (_listaProductos.Count > 0) {
                nuevoId = _listaProductos.Max(x => x.Id);
            }
            else
            {
                nuevoId = 1;
            }
            int id = (nuevoId + 1);
            string guardaNombre = AgregarNombre();
            decimal guardarPrecio = AgregarPrecio();
            int guardarStock = AgregarStock();
            Producto prod = new Producto(id, guardaNombre, guardarPrecio, guardarStock);
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
                if (item.Estado)
                {
                    Console.WriteLine("ID: " + item.Id);
                    Console.WriteLine("Nombre: " + item.Nombre);
                    Console.WriteLine("Precio: " + item.Precio);
                    Console.WriteLine("Stock: " + item.Stock);
                    Console.WriteLine();
                }
            }
        }


        public void mostrarXProducto(Producto p)
        {
            Console.WriteLine("ID: " + p.Id);
            Console.WriteLine("Nombre: " + p.Nombre);
            Console.WriteLine("Precio: " + p.Precio);
            Console.WriteLine("Stock: " + p.Stock);
            Console.WriteLine();
        }

        public void mostarProductoPorId()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese el ID del producto a buscar");
                    int num = int.Parse(Console.ReadLine());
                    Producto prod = _listaProductos.Find(p => p.Id == num);
                    if (num == null)
                    {
                        Console.WriteLine("No se encontraron coincidencias con ese numero de ID");
                        continue;
                    }

                    else if (num < 0)
                    {
                        Console.WriteLine("Debe ingresar un numero mayor a 0");
                        continue;
                    }
                    mostrarXProducto(prod);
                    break;

                }
                catch (Exception)
                {

                    Console.WriteLine("Debe ingresar solo un numero sin letras ni simbolos");
                    continue;
                }
            }
        }
        public bool cambiaEstado(Producto prod)
        {
            prod.Estado = false;
            return true;

        }
        public void BajaxID()
        {
            mostrarListadoProductos();
            Console.WriteLine();
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese el ID de PRODUCTO que quiere dar de baja");
                    int num = int.Parse(Console.ReadLine());
                    Producto prod = _listaProductos.Find(p => p.Id == num);
                    if (prod == null)
                    {
                        Console.WriteLine("No se encontraron coincidencias con el ID ingresado, intente nuevamente");
                        continue;
                    }
                    if (cambiaEstado(prod))
                    {
                        GuardarDatos();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("El producto ya estaba dado de baja anteriormente");
                        continue;
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Debe ingresar un ID (Solo los id de la lista)");
                    continue;
                }
            }
        }

        public void actualizarStockXid()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrse el ID del PRODUCTO a a modificar STOCK");
                    int num = int.Parse(Console.ReadLine());
                    Producto prod = _listaProductos.Find(p => p.Id == num);
                    if (prod == null)
                    {
                        Console.WriteLine("No se encontraron coincidencias para el ID ingresado");
                        continue;
                    }
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Ingrse la cantidad del PRODUCTO a modificar");
                            int nuevostock = int.Parse(Console.ReadLine());
                            if (nuevostock <= 0)
                            {
                                Console.WriteLine("Debe ingresar una cantidad positiva de productos");
                                continue;
                            }
                            else
                            {
                                prod.stock = prod.stock + nuevostock;
                                GuardarDatos();
                                return;
                            }
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Debe ingresar solo numeros");
                            continue;
                        }

                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Debe ingresar solo numeros");
                    continue;
                }

            }
        }

        public void buscarXpalabra()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese una palabra para iniciar la busqueda");
                    string nombre = Console.ReadLine();
                    if (nombre.Any(char.IsDigit))
                    {
                        Console.WriteLine("Debe ingresar una palabra sin numeros ni simbolos");
                        continue;
                    }
                    var prod = _listaProductos.Where(p => p.Nombre.ToLower().Contains(nombre.ToLower())).ToList();
                    if (prod.Count > 0)
                    {
                        Console.WriteLine($"Coincidencias para la palabra{nombre}");
                        Console.WriteLine();
                        foreach (var item in prod)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine();
                        }
                        return;

                    }


                }
                catch (Exception)
                {

                    Console.WriteLine($""); ;
                }
            }
        }

        public void RmostrarBajoStock()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Ingres el minimo de stock a buscar ");
                    int minimo=int.Parse(Console.ReadLine());
                    Console.Clear();
                    bool encontro=false;
                    if (minimo < 0)
                    {
                        Console.WriteLine("Debe ingresarse un numero de STOCK positivo");
                        continue;
                    }
                    else if (minimo == 0) 
                    {
                        Console.WriteLine("No puede ser 0 el STOCK buscado");
                        continue;
                    }
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine($"Productos con menos de {minimo} unidades");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine();
                    foreach (var item in _listaProductos)
                    {
                        if (item.Stock <= minimo && item.Estado)
                        {
                            Console.WriteLine(item);
                            encontro = true;
                            Console.WriteLine();


                        }

                    }


                        if (!encontro)
                        {
                        Console.WriteLine($"No se encontraron productos con menos de {minimo} unidades");
                        }
             
                    
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Presione una tecla para continuar");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                catch (Exception)
                {

                    Console.WriteLine("Solo debe ingresarse un numero minimo de stock");
                    continue;
                }
            }
        }
        public void RmostrarValorTotalxID()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese el ID del PRODUCTO a calcular el reporte");
                    int numero = int.Parse(Console.ReadLine());
                    if (numero < 0)
                    {
                        Console.WriteLine("Debe ingresar un numero de ID positivo");
                        continue;
                    }
                    else if (numero == 0)
                    {
                        Console.WriteLine("Debe ser mayor a 0");
                        continue;
                    }
                    var buscado = _listaProductos.Find(p => p.Id == numero);
                    if (buscado == null)
                    {
                        Console.WriteLine("No se encontraron coincidencias para el ID ingresado");
                        continue;
                    }
                    else if (buscado.estado)
                    {
                        decimal sumaTotal = (buscado.Stock * buscado.Precio);
                        Console.Clear();
                        Console.WriteLine("Importe total almacenado para el producto " + buscado.Nombre);
                        Console.WriteLine();
                        Console.WriteLine("$" + sumaTotal);
                        Console.ReadKey ();
                        return;

                    }
                    else
                    {
                        Console.WriteLine("El producto se encuentra dado de baja");
                        continue;
                    }
                }
                catch (Exception)
                {

                    Console.WriteLine("Error: Debe ingresar solo un numero de ID");
                    continue;
                }
            }
        }
    }
}

