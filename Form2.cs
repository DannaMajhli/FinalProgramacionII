using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ProyectoFinal_Programacion
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void buttonlogin_Click(object sender, EventArgs e)
        {
            AdmonBD obj = new AdmonBD();

            string cuenta = textBoxcuenta.Text; // Campo de cuenta
            string contraseña = textBoxcontraseña.Text; // Campo de contraseña
            string cuentacpy = "";

            try
            {
                string query = $"SELECT * FROM usuarios WHERE cuenta = '{cuenta}' AND contraseña = '{contraseña}'";
                MySqlCommand command = new MySqlCommand(query, obj.connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // Si se encuentra al menos un registro
                {
                    reader.Read(); // Lee la primera fila (ya que debería ser única)

                    string dbCuenta = reader.GetString("cuenta"); // Obtienen la cuenta desde la base de datos
                    cuentacpy = cuenta;
                    int usuarioId = reader.GetInt32("id");

                    AdmonBD.UsuarioActivoId = usuarioId;
                    MessageBox.Show($"Usuario activo ID: {AdmonBD.UsuarioActivoId}");

                    // Mensaje de depuración para ver los valores
                    //  MessageBox.Show("Cuenta desde la base de datos: " + dbCuenta);
                    MessageBox.Show("Cuenta ingresada: " + cuenta);

                    reader.Close(); // Cierra el reader despues de leer los datos

                    if (dbCuenta.Equals("DannaAvalos2345@gmail.com", StringComparison.OrdinalIgnoreCase)) // Compara insensiblemente a mayúsculas
                    {

                        // this.Hide(); // Ocultar este formulario
                        MessageBox.Show("Abriendo Formadmin...");
                        Formadmin formadmin = new Formadmin();
                        this.Hide(); // Ocultar este formulario
                        formadmin.ShowDialog();
                        formadmin.Show(); // Mostrar Formadmin
                        
                    }
                    else
                    {
                        MessageBox.Show("Abriendo Formventa...");
                        Formventa formV = new Formventa(); // Formulario para usuarios normales
                        this.Hide(); 
                        formV.ShowDialog();
                        formV.Show();

                    }
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Cuenta o contraseña incorrecta.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar usuario: " + ex.Message);
            }
            finally
            {
                obj.Disconnect();
            }
        }

        private void buttonloggout_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();// Crear una instancia del segundo formulario
            this.Close();
            form1.ShowDialog(); // Mostrar el segundo formulario 
            this.Show();
        }

    }
}
