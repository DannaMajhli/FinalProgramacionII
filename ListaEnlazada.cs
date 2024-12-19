using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_Programacion
{
    public class ListaEnlazada
    {
        public class Producto
        {
            public string Descripcion { get; set; }
            public int Existencias { get; set; }
            public int Precio { get; set; }
            public Producto Next { get; set; }

            public Producto(string descripcion, int existencias, int precio)
            {
                Descripcion = descripcion;
                Existencias = existencias;
                Precio = precio;
                Next = null;
            }
        }

        public Producto Head { get; private set; }

        public ListaEnlazada()
        {
            Head = null;
        }

        public void AgregarProducto(string descripcion, int existencias, int precio)
        {
            Producto nuevoProducto = new Producto(descripcion, existencias, precio);

            if (Head == null)
            {
                Head = nuevoProducto;
            }
            else
            {
                Producto actual = Head;
                while (actual.Next != null)
                {
                    actual = actual.Next;
                }
                actual.Next = nuevoProducto;
            }
        }

        public void OrdenarPorExistencias()
        {
            if (Head == null || Head.Next == null)
                return;

            bool swapped;
            do
            {
                swapped = false;
                Producto actual = Head;

                while (actual.Next != null)
                {
                    if (actual.Existencias > actual.Next.Existencias)
                    {
                        string tempDescripcion = actual.Descripcion;
                        int tempExistencias = actual.Existencias;
                        int tempPrecio = actual.Precio;

                        actual.Descripcion = actual.Next.Descripcion;
                        actual.Existencias = actual.Next.Existencias;
                        actual.Precio = actual.Next.Precio;

                        actual.Next.Descripcion = tempDescripcion;
                        actual.Next.Existencias = tempExistencias;
                        actual.Next.Precio = tempPrecio;

                        swapped = true;
                    }
                    actual = actual.Next;
                }
            } while (swapped);
        }
    }
}
