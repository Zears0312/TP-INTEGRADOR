using System.Collections.Generic;

public class Pedido
{
    private List<Producto> _productos;
    private string _estado;

    public string Estado
    {
        get { return _estado; }
        private set { _estado = value; }
    }

    public Pedido()
    {
        _productos = new List<Producto>();
        _estado = "Pendiente";
    }

    public bool AgregarProducto(Producto p, int cant)
    {
        if (p.DescontarStock(cant))
        {
            for (int i = 0; i < cant; i++)
            {
                _productos.Add(p);
            }
            return true;
        }
        return false;
    }

    public decimal CalcularTotal(decimal impuesto)
    {
        decimal subtotal = 0.0m;

        foreach (Producto producto in _productos)
        {
            subtotal += producto.Precio;
        }

        decimal total = subtotal + (subtotal * impuesto);
        return total;
    }

    public void CompletarPago()
    {
        _estado = "Completado";
    }
}