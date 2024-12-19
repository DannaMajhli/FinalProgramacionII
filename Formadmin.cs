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
    public partial class Formadmin : Form
    {
        List<registros> dato;
        List<products> data;

        public Formadmin()
        {
            InitializeComponent();
        }
       

        private void Formadmin_Load(object sender, EventArgs e)
        {

        }

        private string correoUsuario;
        public Formadmin(string correo)
        {
            InitializeComponent();
            correoUsuario = correo;

            // Ahora puedes usar correoUsuario como clave o hacer las validaciones necesarias
            MessageBox.Show($"Bienvenido al panel de administrador. Correo: {correoUsuario}");
        }

        private void buttonRefrescar_Click(object sender, EventArgs e)
        {
            ProdBD obj = new ProdBD();
            data = obj.consult();
            this.richTextBox1.Clear();
            data.ForEach(p =>
            {
                this.richTextBox1.AppendText("id=" + p.Id + " Namepicture=" + p.Namepicture + " =Productdescription" + p.Productdescription + " Price:" + p.Price + " Stock=" + p.Stock + "\n");
            });

            obj.Disconnect();
        }

        private void buttondelete_Click(object sender, EventArgs e)
        {
            ProdBD obj = new ProdBD();
            int totalProductos = obj.obtenerTotalProductos();  // Método que debes crear para obtener el número total de productos

            // Si el numero de productos es menor o igual a 6, no permitir la eliminacion
            if (totalProductos <= 6)
            {
                MessageBox.Show("No se puede eliminar el producto, ya que hay 6 o menos productos en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  // Detener la eliminacion
            }

            int id = Convert.ToInt32(textBoxiddelete.Text); // ID tomado del campo de texto


            // Mostrar un cuadro de diálogo de confirmacion
            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar este registro?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            // Evaluar la respuesta del usuario
            if (resultado == DialogResult.Yes)
            {
                // Lógica para eliminar el registro

                obj.eliminar(id);
                obj.Disconnect();
                MessageBox.Show("Registro eliminado exitosamente.");
            }
            else
            {
                // El usuario cancelo la acción
                MessageBox.Show("Eliminación cancelada.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Close();
            form2.ShowDialog();
            this.Show();

        }

        private void buttonMODIFICAR_Click(object sender, EventArgs e)
        {
            Formmod formmod = new Formmod();
            formmod.ShowDialog();
        }

        private void button1morestock_Click(object sender, EventArgs e)
        {
            ProdBD obj = new ProdBD();
            data = obj.consult();
            for (int i = 0; i < data.Count - 1; i++)
            {
                for (int j = 0; j < data.Count - 1 - i; j++)
                {
                    if (data[j].Stock < data[j + 1].Stock)
                    {
                        // Intercambiar los elementos
                        var temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }
            this.richTextBox2.Clear();
            data.ForEach(p =>
            {
                this.richTextBox2.AppendText("Namepicture=" + p.Namepicture + " Stock=" + p.Stock + "\n");
            });

            obj.Disconnect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdmonBD admonBD = new AdmonBD();

            int usuarioact = admonBD.usuarioact;
            registros usuarioactivo = admonBD.const_reguser(usuarioact);

            if (usuarioactivo != null)
            {
                MessageBox.Show($"Usuario Activo:\n" +
                       //   $"ID: {usuarioactivo.Id}\n" +
                       $"Nombre: {usuarioactivo.Name}",
                       // $"Cuenta: {usuarioactivo.Cuenta}\n" +
                       //  $"Monto: {usuarioactivo.Monto}",
                       "Usuario Act",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se encontró un usuario activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            admonBD.Disconnect();
        }

        private void GuardarCompra()
        {
            try
            {


                MessageBox.Show("Compra realizada con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la compra: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GuardarCompra();
        }
        private decimal totalVentas = 0;

        private void button4_Click(object sender, EventArgs e)
        {
            Formventa fmventa = new Formventa();
            fmventa.ShowDialog();
            this.Hide();
        }

        private void buttonAGREGAR_Click(object sender, EventArgs e)
        {
            Formadd formadd = new Formadd();
            formadd.ShowDialog();
        }

        private void buttonRefrescar_Click_1(object sender, EventArgs e)
        {
            ProdBD obj = new ProdBD();
            data = obj.consult();
            this.richTextBox1.Clear();
            data.ForEach(p =>
            {
                this.richTextBox1.AppendText("id=" + p.Id + " Namepicture=" + p.Namepicture + " =Productdescription" + p.Productdescription + " Price:" + p.Price + " Stock=" + p.Stock + "\n");
            });

            obj.Disconnect();
        }

        private void buttonIrGrafica_Click(object sender, EventArgs e)
        {
            // Aquí puedes pasar el dato que desees como argumento al nuevo formulario.
            string cuenta = correoUsuario; // Por ejemplo, usa correoUsuario como parámetro si aplica.

            FormGrafica formGrafica = new FormGrafica(cuenta);
            this.Hide();
            formGrafica.ShowDialog();
            this.Show();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
