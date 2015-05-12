using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_CatalogoCD
{
    public partial class GUI : Form
    {
        string[] ficheroXML;
        static Catalogo c;
        List<int> listaCodigos;

        public GUI()
        {
            InitializeComponent();
            Inicializar();
        }

        private void Inicializar()
        {
            ficheroXML = new string[] { };
            listaCodigos = new List<int>();
            try
            {
                c = new Catalogo();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    btnBoton1.Enabled = true;
                    btnBoton1.Text = "Leer DVD´s";
                    btnBoton2.Enabled = false;
                    break;
                case 1:
                    btnBoton1.Enabled = true;
                    btnBoton1.Text = "Leer en formato XML";
                    btnBoton2.Enabled = false;
                    break;
                case 2:
                    btnBoton1.Enabled = true;
                    btnBoton1.Text = "Añadir DVD al azar";
                    btnBoton2.Enabled = false;
                    break;
                case 3:
                    btnBoton1.Text = "Eliminar DVD";
                    btnBoton1.Enabled = false;
                    btnBoton2.Enabled = false;
                    break;
                case 4:
                    btnBoton1.Text = "Modificar DVD";
                    btnBoton1.Enabled = false;
                    btnBoton2.Enabled = false;
                    break;
                case 5:
                    btnBoton1.Enabled = true;
                    btnBoton2.Enabled = true;
                    btnBoton1.Text = "Volcar";
                    btnBoton2.Text = "Elegir fichero";
                    break;
                case 6:
                    btnBoton1.Enabled = true;
                    btnBoton1.Text = "Listar por país";
                    btnBoton2.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void RellenarListBox()
        {
            listaCodigos.Clear();
            comboBox1.Items.Clear();
            foreach (var item in c.CatalogoDVD)
            {
                listaCodigos.Add(item.Codigo);
                comboBox1.Items.Add(item.Codigo + ", " + item.Titulo + ", " + item.Artista + ", " + item.Pais + ", " + item.Compania + ", " + item.Precio + ", " + item.Anio);
            }
        }

        private void Opciones()
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    c.LeerDVD();
                    RellenarListBox();
                    break;
                case 1:
                    c.LeerDVD();
                    ficheroXML = c.Xml.Split('\n');
                    comboBox1.Items.Clear();
                    comboBox1.Items.AddRange(ficheroXML);
                    break;
                case 2:
                    c.AddEntrada(tbxCodigo.Text);
                    RellenarListBox();
                    break;
                case 3:
                    c.BorrarDVD(tbxCodigo.Text);
                    RellenarListBox();
                    break;
                case 4:
                    dvd unDVD = c.LeerDVD(tbxCodigo.Text);
                    unDVD.Titulo = tbxTitulo.Text;
                    unDVD.Artista = tbxArtista.Text;
                    unDVD.Pais = tbxPais.Text;
                    unDVD.Compania = tbxCompania.Text;
                    unDVD.Precio = Convert.ToDecimal(tbxPrecio.Text);
                    unDVD.Anio = Convert.ToUInt16(tbxAnio.Text);
                    c.ActualizarDVD(unDVD);
                    break;
                case 5:
                    string fXml = c.Xml;
                    c.XmlAFichero();
                    break;
                case 6:
                    c.FiltrarPorPais();
                    RellenarListBox();
                    break;
                default:
                    break;
            }
        }

        private void btnBoton1_Click(object sender, EventArgs e)
        {
            Opciones();
        }

        private void btnBoton2_Click(object sender, EventArgs e)
        {
            Opciones();
        }

        private void tbxNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void lbxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvd unDVD = new dvd();
            unDVD = c.LeerDVD(listaCodigos.ElementAt(lbxClientes.SelectedIndex).ToString());

            tbxCodigo.Text = unDVD.Codigo.ToString();
            tbxTitulo.Text = unDVD.Titulo;
            tbxArtista.Text = unDVD.Artista;
            tbxPais.Text = unDVD.Pais;
            tbxCompania.Text = unDVD.Compania;
            tbxPrecio.Text = unDVD.Precio.ToString();
            tbxAnio.Text = unDVD.Anio.ToString();            
        }
    }
}
