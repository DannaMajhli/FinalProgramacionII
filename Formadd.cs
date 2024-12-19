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
    public partial class Formadd : Form
    {

        List<products> data;
        public void limpiar()
        {
            MessageBox.Show("entre a limpiar");


        }
        public Formadd()
        {
            InitializeComponent();
        }

        private void buttonadd_Click(object sender, EventArgs e)
        {
            int id;
            string namepicture;
            string product_description;
            int price;
            int stock;

            id = Convert.ToInt32(this.textBoxidadd.Text);
            namepicture = this.textBoxnamepictureadd.Text;
            product_description = this.textBoxpdadd.Text;
            price = Convert.ToInt32(this.textBoxpriceadd.Text);
            stock = Convert.ToInt32(this.textBoxstockadd.Text);

            ProdBD obj = new ProdBD();
            obj.insertar(id, namepicture, product_description, price, stock);
            limpiar();
            obj.Disconnect();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formadmin formadmin = new Formadmin();// Crear una instancia del segundo formulario
            this.Close();
            formadmin.ShowDialog(); // Mostrar el segundo formulario 
            this.Show();
        }
    }
}
