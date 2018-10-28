using BL.Rentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Rentas
{
    public partial class FormClientes : Form
    {
        ClientesBL _clientes;
        public FormClientes()
        {
            InitializeComponent();
            _clientes = new ClientesBL();
            listaClientesBindingSource.DataSource = _clientes.ObtenerClientes();

        }

        private void FormClientes_Load(object sender, EventArgs e)
        {

        }

        private void listaClientesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaClientesBindingSource.EndEdit();
            var cliente = (Cliente)listaClientesBindingSource.Current;

            var resultado = _clientes.GuardarCliente(cliente);

            if (resultado.Exito==true)
            {
                listaClientesBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
            }
            else
            {
                MessageBox.Show(resultado.Mensaj);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _clientes.AgregarCliente();
            listaClientesBindingSource.MoveLast();
            DeshabilitarHabilitarBotones(false);
        }

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButtonCancelar.Visible = !valor;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            
            if (idTextBox.Text != "")
            {
                var resultado = MessageBox.Show("¿Desea eliminar este cliente?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Eliminar(id);
                }
            }
        }

        private void Eliminar(int id)
        {
            
            var resultado = _clientes.EliminarCliente(id);

            if (resultado == true)
            {
                listaClientesBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el cliente!!");
            }
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarHabilitarBotones(true);
            Eliminar(0);
        }
    }
}
