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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonfrom2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            this.Hide(); // Ocultar el formulario actual
            f2.ShowDialog(); // Mostrar el nuevo formulario como modal
            this.Dispose(); // Liberar recursos del formulario actual

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Crear una nueva instancia de Form1 si el usuario decide regresar
            Form1 nuevoForm = new Form1();
            nuevoForm.Show();
            this.Dispose();

        }
    }
}
