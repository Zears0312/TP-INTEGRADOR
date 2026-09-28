using System;

public class Producto
{
    public string Nombre {get; private set;}
    public decimal Precio {get; private set;}
    public int Stock {get; private set;}
    public Producto(string nombre, decimal precio, int stock)
    {
        Nombre = nombre;
        Precio = precio;
        Stock = stock;
    }
    public bool DescontarStock(int cantidad)
    {
       if(Stock >= cantidad)
        {
            Stock -= cantidad;
            return true;
        }
        return false; 
    }
}