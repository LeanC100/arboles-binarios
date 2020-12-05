using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArbolesEjemploCSharp
{
    public partial class Form1 : Form
    {
        Nodo raiz;
        Nodo seleccionado;
        public Form1()
        {
            InitializeComponent();
        }

        Nodo crearNodo(string n_nodo)
        {
                return new Nodo(n_nodo);
        }

        void EvaluarArbol()
        {
            this.lblAltura.Text = $"Altura:{Alto(raiz)}";
            int inicio = 0;
            this.lblAncho.Text = $"Ancho:{Ancho(raiz,ref inicio)}";
        }


        int Ancho(Nodo n, ref int ancho)
        {
            if (n.Derecha == null && n.Izquierda == null)
                ancho += 1;

            if (n.Derecha != null)  Ancho(n.Derecha, ref ancho);
            if (n.Izquierda != null)  Ancho(n.Izquierda, ref ancho);

            return ancho;
        }
        int Alto(Nodo n)
        {
            if (n == null) return 0;

            int izq = Alto(n.Izquierda) + 1;
            int der = Alto(n.Derecha) + 1;
            return Math.Max(izq, der);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (raiz != null)
            {
                DialogResult r = MessageBox.Show("Se eliminará el árbol, desea continuar?", "Consulta", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    string nombre = Interaction.InputBox("Ingrese nombre del nodo");
                    if (nombre.Length != 0) raiz = crearNodo(nombre);
                    else MessageBox.Show("Debe ingresar un nombre");
                }
            }
            else
            {
                string nombre = Interaction.InputBox("Ingrese nombre del nodo");
                if (nombre.Length != 0) raiz = crearNodo(nombre); 
                else MessageBox.Show("Debe ingresar un nombre");
            }
            CambiarSeleccion(raiz); 
            LlenarTreeView();
        }


        public void LlenarTreeView()
        {
            treeView1.Nodes.Clear();
            MostrarNodo(raiz, null, string.Empty);
            treeView1.ExpandAll(); //para mostrar el treeviee desplegado
            EvaluarArbol();
        }

        public void MostrarNodo(Nodo n, TreeNode tnpadre, string lado)
        {
            if (n == null) return;

            TreeNode nuevo = new TreeNode();
            if (tnpadre == null && lado==String.Empty)
            {
                //si entra en este  if, es porque Nodo es el raiz
                tnpadre = new TreeNode();
                nuevo.Text = n.Nombre;
                nuevo.Tag = n;
                treeView1.Nodes.Add(nuevo);
            }
            else
            {
               
                nuevo.Text = $"{lado} - {n.Nombre}";
                nuevo.Tag = n;
                
                tnpadre.Nodes.Add(nuevo);
            }

            if (n.Derecha != null) MostrarNodo(n.Derecha, nuevo, "D");
            if (n.Izquierda != null) MostrarNodo(n.Izquierda, nuevo, "I");
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CambiarSeleccion((Nodo)e.Node.Tag);
        }


        void CambiarSeleccion(Nodo n)
        {
            seleccionado = n;
            this.lblNombreNodo.Text = n.Nombre;
        }
         
        private void button2_Click(object sender, EventArgs e)
        {
            if (seleccionado != null || raiz == null)
            {
                if (seleccionado.Derecha != null)
                {
                    DialogResult r = MessageBox.Show("Se eliminará el árbol, desea continuar?", "Consulta", MessageBoxButtons.YesNo);
                    if (r == DialogResult.Yes)
                    {
                        string nombre = Interaction.InputBox("Ingrese nombre del nodo");
                        if (nombre.Length != 0) seleccionado.Derecha = crearNodo(nombre);
                        else MessageBox.Show("Debe ingresar un nombre");
                    }
                }
                else
                {
                    string nombre = Interaction.InputBox("Ingrese nombre del nodo");
                    if (nombre.Length != 0) seleccionado.Derecha = crearNodo(nombre);
                    else MessageBox.Show("Debe ingresar un nombre");
                }
                LlenarTreeView();
            }
            else MessageBox.Show("Debe tener algun nodo seleccionado");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (seleccionado != null || raiz ==null)
            {
                if (seleccionado.Izquierda != null)
                {
                    DialogResult r = MessageBox.Show("Se eliminará el árbol, desea continuar?", "Consulta", MessageBoxButtons.YesNo);
                    if (r == DialogResult.Yes)
                    {
                        string nombre = Interaction.InputBox("Ingrese nombre del nodo");
                        if (nombre.Length != 0) seleccionado.Izquierda = crearNodo(nombre);
                        else MessageBox.Show("Debe ingresar un nombre");
                    }
                }
                else
                {
                    string nombre = Interaction.InputBox("Ingrese nombre del nodo");
                    if (nombre.Length != 0) seleccionado.Izquierda = crearNodo(nombre);
                    else MessageBox.Show("Debe ingresar un nombre");
                }
                LlenarTreeView();
            }
            else MessageBox.Show("Debe tener algun nodo seleccionado");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (raiz != null)
            {
                this.txtRecorrido.Text = string.Empty;
                RecorridoPreorden(raiz);
            }
            else
            {
                this.txtRecorrido.Text = "El arbol esta vacio";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (raiz != null)
            {
                this.txtRecorrido.Text = string.Empty;
                RecorridoInOrden(raiz);
            }
            else
            {
                this.txtRecorrido.Text = "El arbol esta vacio";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (raiz != null)
            {
                this.txtRecorrido.Text = string.Empty;
                RecorridoPostOrden(raiz);
            }
            else
            {
                this.txtRecorrido.Text = "El arbol esta vacio";
            }
        }
        void RecorridoPreorden(Nodo n)
        {
            Visitar(n);
            if (n.Izquierda != null) RecorridoPreorden(n.Izquierda);
            if (n.Derecha != null) RecorridoPreorden(n.Derecha);
        }
        void RecorridoInOrden(Nodo n)
        {
            if (n.Izquierda != null) RecorridoInOrden(n.Izquierda);
            Visitar(n);
            if (n.Derecha != null) RecorridoInOrden(n.Derecha);
        }
        void RecorridoPostOrden(Nodo n)
        {
            if (n.Izquierda != null) RecorridoPostOrden(n.Izquierda);
            if (n.Derecha != null) RecorridoPostOrden(n.Derecha);
            Visitar(n);

        }

        void Visitar(Nodo n)
        {
            this.txtRecorrido.Text += "-" + n.Nombre;
        }

    }
}
