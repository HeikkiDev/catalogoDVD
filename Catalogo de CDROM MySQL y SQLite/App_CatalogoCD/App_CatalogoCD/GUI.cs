﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_CatalogoCD
{
    public partial class GUI : Form
    {
        string ruta;
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
            ruta = string.Empty;
            ficheroXML = new string[] { };
            listaCodigos = new List<int>();
            try
            {
                c = new Catalogo();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //MessageBox.Show(e.Message, "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnBoton1.Visible = false;
            btnBoton2.Visible = false;
            SoloLecturaTbox(true);
        }

        #region MIS MÉTODOS
        private void RellenarListBox()
        {
            listaCodigos.Clear();
            lbxClientes.Items.Clear();
            foreach (var item in c.CatalogoDVD)
            {
                listaCodigos.Add(item.Codigo);
                lbxClientes.Items.Add(item.Codigo + ", " + item.Titulo + ", " + item.Artista + ", " + item.Pais + ", " + item.Compania + ", " + item.Precio + ", " + item.Anio);
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
                    lbxClientes.Items.Clear();
                    lbxClientes.Items.AddRange(ficheroXML);
                    listaCodigos.Clear(); //Para que no muestre info sin sentido en los textbox
                    LimpiarTBoxs();
                    break;
                case 2:
                    if (tbxCodigo.Text != string.Empty)
                    {
                        if (listaCodigos.Contains(Convert.ToInt32(tbxCodigo.Text)))
                            MessageBox.Show("Código ya existente, indique otro...", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                        {
                            c.AddEntrada(tbxCodigo.Text);
                            c.LeerDVD();
                            RellenarListBox();
                            lblEstado.Text = "DVD añadido con éxito...";
                        }
                    }
                    else
                        MessageBox.Show("Indique el código para el DVD aleatorio a introducir", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 3:
                    if (tbxCodigo.Text != string.Empty)
                    {
                        c.BorrarDVD(tbxCodigo.Text);
                        c.LeerDVD();
                        RellenarListBox();
                        lblEstado.Text = "DVD borrado con éxito...";
                    }
                    else
                        MessageBox.Show("Seleccione el DVD a borrar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 4:
                    if (tbxCodigo.Text != string.Empty)
                    {
                        dvd unDVD = c.LeerDVD(tbxCodigo.Text);
                        unDVD.Titulo = tbxTitulo.Text;
                        unDVD.Artista = tbxArtista.Text;
                        unDVD.Pais = tbxPais.Text;
                        unDVD.Compania = tbxCompania.Text;
                        unDVD.Precio = Convert.ToDecimal(tbxPrecio.Text);
                        unDVD.Anio = Convert.ToUInt16(tbxAnio.Text);
                        c.ActualizarDVD(unDVD);
                        c.LeerDVD();
                        RellenarListBox();
                        lblEstado.Text = "DVD modificado con éxito...";
                    }
                    else
                        MessageBox.Show("Seleccione el DVD a modificar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 5:
                    c.XmlAFichero(ruta);
                    lblEstado.Text = "Fichero volcado con éxito...";
                    break;
                case 6:
                    c.FiltrarPorPais();
                    RellenarListBox();
                    lblEstado.Text = "Siempre lista los de US...";
                    break;
                default:
                    break;
            }
        }

        private void LimpiarTBoxs()
        {
            tbxCodigo.Text = string.Empty;
            tbxTitulo.Text = string.Empty;
            tbxArtista.Text = string.Empty;
            tbxPais.Text = string.Empty;
            tbxCompania.Text = string.Empty;
            tbxPrecio.Text = string.Empty;
            tbxAnio.Text = string.Empty;
        }

        private void SoloLecturaTbox(bool condicion)
        {
            tbxCodigo.ReadOnly = condicion;
            tbxTitulo.ReadOnly = condicion;
            tbxPrecio.ReadOnly = condicion;
            tbxCompania.ReadOnly = condicion;
            tbxAnio.ReadOnly = condicion;
            tbxArtista.ReadOnly = condicion;
            tbxPais.ReadOnly = condicion;
        }
        #endregion

        #region MANEJADORES DE EVENTOS
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    btnBoton1.Visible = true;
                    btnBoton1.Text = "Leer DVD´s";
                    btnBoton2.Visible = false;
                    lblEstado.Text = "";
                    SoloLecturaTbox(true);
                    break;
                case 1:
                    btnBoton1.Visible = true;
                    btnBoton1.Text = "Leer en formato XML";
                    btnBoton2.Visible = false;
                    lblEstado.Text = "";
                    SoloLecturaTbox(true);
                    break;
                case 2:
                    btnBoton1.Visible = true;
                    btnBoton1.Text = "Añadir DVD al azar";
                    btnBoton2.Visible = false;
                    lblEstado.Text = "";
                    LimpiarTBoxs();
                    SoloLecturaTbox(true);
                    tbxCodigo.ReadOnly = false;
                    break;
                case 3:
                    btnBoton1.Text = "Eliminar DVD";
                    btnBoton1.Visible = true;
                    btnBoton2.Visible = false;
                    lblEstado.Text = "";
                    SoloLecturaTbox(true);
                    break;
                case 4:
                    btnBoton1.Text = "Modificar DVD";
                    btnBoton1.Visible = true;
                    btnBoton2.Visible = false;
                    lblEstado.Text = "";
                    SoloLecturaTbox(false);
                    tbxCodigo.ReadOnly = true;
                    break;
                case 5:
                    btnBoton1.Visible = true;
                    //btnBoton2.Enabled = true;
                    btnBoton2.Visible = true;
                    btnBoton1.Text = "Volcar";
                    btnBoton2.Text = "Elegir ruta";
                    lblEstado.Text = "";
                    SoloLecturaTbox(true);
                    break;
                case 6:
                    btnBoton1.Visible = true;
                    btnBoton1.Text = "Listar por país";
                    btnBoton2.Visible = false;
                    lblEstado.Text = "";
                    SoloLecturaTbox(true);
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
            SaveFileDialog obtenerRuta = new SaveFileDialog();

            obtenerRuta.Filter = "xml files (*.xml)|*.xml";

            if (obtenerRuta.ShowDialog() == DialogResult.OK)
            {
                ruta = obtenerRuta.FileName;
                using (File.Create(ruta)) ;
            }
                
        }

        private void tbxNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void tbxPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar==',')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void lbxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvd unDVD = new dvd();
            try
            {
                unDVD = c.LeerDVD(listaCodigos.ElementAt(lbxClientes.SelectedIndex).ToString());

                tbxCodigo.Text = unDVD.Codigo.ToString();
                tbxTitulo.Text = unDVD.Titulo;
                tbxArtista.Text = unDVD.Artista;
                tbxPais.Text = unDVD.Pais;
                tbxCompania.Text = unDVD.Compania;
                tbxPrecio.Text = unDVD.Precio.ToString();
                tbxAnio.Text = unDVD.Anio.ToString();
            }
            catch { }
        }
        #endregion
    }
}
