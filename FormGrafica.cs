using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal_Programacion
{
    public partial class FormGrafica : Form
    {
        public FormGrafica(string cuentaIngresada)
        {
            InitializeComponent();
        }

        // Método para obtener los productos y almacenarlos en una lista enlazada
        private ListaEnlazada ObtenerProductosEnLista()
        {
            ProdBD bd = new ProdBD();  // Crear instancia de conexión a la base de datos
            List<(string descripcion, int existencias, int precio)> datos = bd.ObtenerDatosGrafica();

            // Crear la lista enlazada
            ListaEnlazada lista = new ListaEnlazada();

            // Agregar los productos a la lista enlazada
            foreach (var dato in datos)
            {
                lista.AgregarProducto(dato.descripcion, dato.existencias, dato.precio);
            }

            // Ordenar la lista por existencias
            lista.OrdenarPorExistencias();

            return lista;
        }


         private void CambiarDeFormulario()
         {
             Formadmin formadmin = new Formadmin();// Crear una instancia del segundo formulario
            this.Close();
            formadmin.ShowDialog(); // Mostrar el segundo formulario 
            this.Show();
        }
        
         private void button1_Click(object sender, EventArgs e)
         {
             CambiarDeFormulario();
         }
        
         private void FormGrafica_Load(object sender, EventArgs e)
         {
            ProdBD bd = new ProdBD();

            // Obtener los productos ordenados en una lista enlazada
            ListaEnlazada listaOrdenada = ObtenerProductosEnLista();

            // Configurar la gráfica
            chartProductos.Series.Clear();  // Limpiar las series anteriores
            chartProductos.Titles.Add("Productos vs Existencias");  // Título de la gráfica
            chartProductos.Series.Add("Existencias");  // Agregar una serie para existencias

            // Configurar eje X e Y
            chartProductos.ChartAreas[0].AxisX.Title = "Productos";
            chartProductos.ChartAreas[0].AxisY.Title = "Existencias";

            // Llenar los datos en la gráfica con los productos de la lista enlazada
            ListaEnlazada.Producto actual = listaOrdenada.Head;
            while (actual != null)
            {
                chartProductos.Series["Existencias"].Points.AddXY(actual.Descripcion, actual.Existencias);
                actual = actual.Next;  // Avanzar al siguiente nodo
            }

            bd.Disconnect();

        }

         private void buttonActualizar_Click(object sender, EventArgs e)
         {
            // Obtener los productos ordenados en una lista enlazada
            ListaEnlazada listaOrdenada = ObtenerProductosEnLista();

            // Limpiar las series anteriores
            chartProductos.Series.Clear();

            // Crear una nueva serie llamada "Existencias"
            var serie = chartProductos.Series.Add("Existencias");
            serie.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            // Configurar el eje X e Y (opcional, si ya está configurado en otro lugar)
            chartProductos.ChartAreas[0].AxisX.Title = "Productos";
            chartProductos.ChartAreas[0].AxisY.Title = "Existencias";

            // Llenar los datos en la gráfica con los productos de la lista enlazada
            ListaEnlazada.Producto actual = listaOrdenada.Head;
            while (actual != null)
            {
                serie.Points.AddXY(actual.Descripcion, actual.Existencias);
                actual = actual.Next;  // Avanzar al siguiente nodo
            }
        }

         private void chartProductos_Click(object sender, EventArgs e)
         {

         }
         
    }
}
