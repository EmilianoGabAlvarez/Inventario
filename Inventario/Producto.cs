using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inventario
{
    public class Producto
    {


        public int Id { get;  set; }
        public string nombre;
        public int stock;
        public decimal precio;  
        public bool estado;
        public Producto(int nuevoid, string nombre, decimal precio, int stock)
        {
            Id = nuevoid;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Estado = true;
        }
        public Producto() 
        {
        estado = true;
        }



        public string Nombre
        {
            get { return nombre; }
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new Exception("Debe ingresar un Nombre");
                    }
                    else if (value.Any(char.IsDigit))
                    {
                     throw new Exception("Debe ingresar un nombre sin numeros");   
                    }
                    nombre= value;
                }
                catch (Exception e)
                {

                    throw new Exception($"Error {e.Message}");
                }
            }
        }
        public decimal Precio
        {
            get { return precio; }

             set
            {

                if (value < 0)
                {
                    throw new Exception("Debe ingresar un valor real ");
                }
                precio = value;
            }
        }
        public int Stock
        {
            get { return stock; }

             set
            {
                if (value < 0)
                {
                    throw new Exception("Debe ingresar un stock mayor a 0 ");
                }
                stock = value;
            }
        }


        public bool Estado
        {
            get { return estado; }

             private set {
                estado = value;
            }
        }
    }
}

