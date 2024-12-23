﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_Programacion
{
    public class products
    {
        int id;
        string namepicture;
        string productdescription;
        int price;
        int stock;

        public products() { }
        public products(int id, string namepicture, string productdescription, int price, int stock)
        {

            this.Id = id;
            this.Namepicture = namepicture;
            this.Productdescription = productdescription;
            this.Price = price;
            this.Stock = stock;

        }

        // Nodo para Árbol Binario de Búsqueda(ABB)
        public class NodoProducto
        {
            public products Producto { get; set; }
            public NodoProducto Izquierdo { get; set; }
            public NodoProducto Derecho { get; set; }

            public NodoProducto(products producto)
            {
                Producto = producto;
                Izquierdo = null;
                Derecho = null;
            }
        }

        public int Id { get => id; set => id = value; }
        public string Namepicture { get => namepicture; set => namepicture = value; }
        public string Productdescription { get => productdescription; set => productdescription = value; }
        public int Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }
        public int Cantidad { get; internal set; }

        public class Venta
        {
            public string Usuario { get; set; }
            public DateTime Fecha { get; set; }
            public List<products> ProductosVendidos { get; set; } // Usamos la clase products
            public decimal Total { get; set; }
            public Venta Siguiente { get; set; } // Referencia al siguiente nodo

            public Venta(string usuario, DateTime fecha, List<products> productosVendidos, decimal total)
            {
                Usuario = usuario;
                Fecha = fecha;
                ProductosVendidos = productosVendidos;
                Total = total;
                Siguiente = null;
            }
        }
    }
}
