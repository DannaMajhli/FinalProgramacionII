using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProyectoFinal_Programacion.ProdBD;
using static ProyectoFinal_Programacion.products;
using iText.Kernel.Pdf;
using iText.Layout; // Para la clase Document
using System.IO;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using System.Timers;



namespace ProyectoFinal_Programacion
{
    public partial class Formventa : Form
    {

        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\main-proyect";
        private string cuenta;
        List<products> datos;
        List<products> productosSeleccionados = new List<products>();

        private ArbolProductos arbolPorPrecio = new ArbolProductos(); // Árbol para orden por precio
        private ArbolProductos arbolPorId = new ArbolProductos();    // Árbol para búsqueda por ID
        public string cuentarec { get; set; }
        public Formventa()
        {
            InitializeComponent();
        }

        public Formventa(string cuenta)
        {
            InitializeComponent();
            this.cuenta = cuenta;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(txtProd.Text); // ID ingresado por el usuario
            int cantidad = Convert.ToInt32(txtCantidad.Text); // Cantidad ingresada por el usuario

            Console.WriteLine($"Buscando producto con ID: {idProducto}");

            // Buscar el producto por ID en el árbol
            var producto = arbolPorId.BuscarPorId(idProducto);

            if (producto != null)
            {
                producto.Cantidad = cantidad; // Actualizar la cantidad
                productosSeleccionados.Add(producto); // Agregarlo a la lista de productos seleccionados
                richTextBox2.AppendText($"{producto.Productdescription} - {producto.Cantidad} x {producto.Price} = {producto.Price * producto.Cantidad}\n");
                MessageBox.Show($"Producto {producto.Productdescription} agregado correctamente.");
            }
            else
            {
                MessageBox.Show("Producto no encontrado.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Formventa_Load(object sender, EventArgs e)
        {
            // Mostrar el nombre del usuario al cargar el formulario
            Console.WriteLine($"Usuario autenticado: {cuenta}");
            ProdBD bd = new ProdBD(); // Crear instancia de conexión a la base de datos
                                      // _ = bd.ObtenerDatosGrafica();

            bd.Disconnect();

         
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Close();
            form2.ShowDialog();
            this.Show(); ;

        }

        private void buttonVer_Click(object sender, EventArgs e)
        {
            ProdBD obj = new ProdBD();

            // Obtener productos de la base de datos
            datos = obj.ObtenerProductos();

            // Insertar productos en el árbol por precio
            foreach (var producto in datos)
            {
                arbolPorPrecio.Insertar(producto, (p1, p2) => p1.Price < p2.Price); // Criterio: Precio Ascendente
            }

            // Insertar productos en el árbol por ID
            foreach (var producto in datos)
            {
                arbolPorId.Insertar(producto, (p1, p2) => p1.Id < p2.Id); // Criterio: ID Ascendente
            }

            MessageBox.Show("Árboles creados correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Mostrar productos ordenados por precio en el RichTextBox
            this.richTextBox1.Clear();
            arbolPorPrecio.MostrarEnOrden(richTextBox1);

            obj.Disconnect();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int idProducto = Convert.ToInt32(txtProd.Text); // ID ingresado por el usuario
            int cantidadAEliminar = Convert.ToInt32(txtCantidad.Text); // Cantidad a eliminar ingresada por el usuario

            // Buscar el producto en la lista de productos seleccionados
            var producto = productosSeleccionados.FirstOrDefault(p => p.Id == idProducto);

            if (producto != null)
            {
                // Verificar si la cantidad a eliminar no excede la cantidad disponible
                if (producto.Cantidad >= cantidadAEliminar)
                {
                    // Si la cantidad a eliminar es igual a la cantidad total, lo eliminamos
                    if (producto.Cantidad == cantidadAEliminar)
                    {
                        productosSeleccionados.Remove(producto);
                    }
                    else
                    {
                        // De lo contrario, restamos la cantidad
                        producto.Cantidad -= cantidadAEliminar;
                    }

                    // Actualizar el RichTextBox para reflejar los cambios
                    richTextBox2.Clear();  // Limpiar el RichTextBox antes de actualizar
                    foreach (var p in productosSeleccionados)
                    {
                        richTextBox2.AppendText($"{p.Productdescription} - {p.Cantidad} x {p.Price} = {p.Price * p.Cantidad}\n");
                    }

                    MessageBox.Show($"Producto {producto.Productdescription} actualizado o eliminado correctamente.");
                }
                else
                {
                    MessageBox.Show($"La cantidad a eliminar excede la cantidad disponible. Disponible: {producto.Cantidad}");
                }
            }
            else
            {
                MessageBox.Show("Producto no encontrado en la lista.");
            }

        }

       

        private void buttonPagar_Click(object sender, EventArgs e)
        {
            if (productosSeleccionados.Count == 0)
            {
                MessageBox.Show("No has agregado productos.");
                return;
            }

            // Crear una instancia de la clase BaseDatos para obtener el nombre del usuario
            AdmonBD db = new AdmonBD();
            ProdBD prodBD = new ProdBD();
            string nombreUsuario = db.ObtenerNombreUsuario(cuenta); // Aquí "cuenta" es el numero de cuenta actual


            StringBuilder notaCompra = new StringBuilder();
            notaCompra.AppendLine($"Usuario: {nombreUsuario}");
            notaCompra.AppendLine($"Fecha: {DateTime.Now}");
            notaCompra.AppendLine("Detalle de la compra:");

            decimal totalCompra = 0;
            foreach (var producto in productosSeleccionados)
            {
                decimal subtotal = (decimal)(producto.Price * producto.Cantidad);
                decimal total = subtotal * 0.6m;
                totalCompra += total;




                prodBD.Actualizarstock(producto.Id, producto.Cantidad);
                notaCompra.AppendLine($"{producto.Productdescription} - {producto.Price} x {producto.Cantidad} = {total}");
            }

            registroVentas.AgregarVenta(nombreUsuario, DateTime.Now, new List<products>(productosSeleccionados), totalCompra);

            // Mostrar las ventas registradas en el RichTextBox
            richTextBox2.Text = "Venta registrada exitosamente.\n";
            registroVentas.MostrarVentas(richTextBox2);

            // Limpiar los productos seleccionados
            productosSeleccionados.Clear();


            notaCompra.AppendLine($"Total: {totalCompra}");
            richTextBox2.Text = notaCompra.ToString();
            GenerarPDF(notaCompra.ToString());
        }

        //---------------------------METODOS-------------------------------------------------------------------
        public void MostrarCuenta()
        {
            MessageBox.Show("Cuenta recibida en Formventa: " + cuentarec);
        }


        // Método para generar el PDF
        private void GenerarPDF(string contenido)
        {

            string rutaPDF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "compra.pdf");

            try
            {
                // Verificar si hay productos seleccionados
                if (productosSeleccionados.Count == 0)
                {
                    MessageBox.Show("No hay productos para generar el PDF.");
                    return;
                }

                // Crear el escritor de PDF
                using (PdfWriter writer = new PdfWriter(rutaPDF))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (Document documento = new Document(pdf))
                {
                    // Agregar contenido
                    documento.Add(new Paragraph("Nota de compra:"));
                    documento.Add(new Paragraph($"Fecha: {DateTime.Now}"));
                    documento.Add(new Paragraph("Productos adquiridos:"));

                    // Recorrer la lista de productos seleccionados
                    foreach (var producto in productosSeleccionados)
                    {
                        if (producto != null)
                        {
                            // Mostrar el producto solo si tiene cantidad positiva
                            if (producto.Cantidad > 0)
                            {
                                documento.Add(new Paragraph($"- {producto.Productdescription}, Cantidad: {producto.Cantidad}, Precio: {producto.Price}"));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Un producto tiene valores nulos. No se incluirá en el PDF.");
                        }
                    }

                    // Calcular el total, sumando solo los productos que tienen cantidad positiva
                    decimal total = productosSeleccionados.Where(p => p.Cantidad > 0).Sum(p => p.Price * p.Cantidad);
                    documento.Add(new Paragraph($"Total: {total}"));

                    documento.Close();
                    MessageBox.Show($"PDF generado correctamente en {rutaPDF}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar PDF: {ex.Message}");
            }

        }

        private RegistroVentas registroVentas = new RegistroVentas();

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            label8.Text = DateTime.Now.ToShortTimeString();
            label7.Text = DateTime.Now.ToShortDateString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
    }



