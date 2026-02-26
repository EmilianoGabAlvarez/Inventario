namespace Inventario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductoManager prodM = new ProductoManager();
            prodM.CargarDatos();
            prodM.CantidadGuardada();
            //prodM.AgregarListadoProductos();
            prodM.mostrarListadoProductos();
        }
    }
}
