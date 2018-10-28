using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
   public  class ClientesBL
    {
        Contexto _contexto;
        public BindingList<Cliente> ListaClientes { get; set; }

        public ClientesBL()
        {
            _contexto = new Contexto();
            ListaClientes = new BindingList<Cliente>();
        }
        public BindingList<Cliente> ObtenerClientes()
        {
            _contexto.Clientes.Load();
            ListaClientes = _contexto.Clientes.Local.ToBindingList();
           return ListaClientes;
        }
        public Resul GuardarCliente(Cliente cliente)
        {
            var resultado = Validar(cliente);
            if(resultado.Exito==false)
            {
                return resultado;
            }
            if (cliente.Id==0)
            {
                cliente.Id = ListaClientes.Max(item => item.Id) + 1;
            }
            _contexto.SaveChanges();
            resultado.Exito = true;
            return resultado;
        }
        public void AgregarCliente()
        {
            var nuevoCliente = new Cliente();
            ListaClientes.Add(nuevoCliente);

        }
        public bool EliminarCliente(int id)
        {
            foreach (var cliente in ListaClientes.ToList())
            {
                if (cliente.Id == id)
                {
                    ListaClientes.Remove(cliente);
                    _contexto.SaveChanges();
                    return true;
                }
            }
            
            return false;
        }

        private Resul Validar(Cliente cliente)
        {
            var resultado = new Resul();
            resultado.Exito = true;

            if (cliente.Nombre == " ")
            {
                resultado.Mensaj = "Ingrese un nombre";
                resultado.Exito = false;
            }
            if (cliente.Email == " ")
            {
                resultado.Mensaj = "Ingrese un Correo Electronico";
                resultado.Exito = false;
            }
            if (cliente.Telefono <= 0)
            {
                resultado.Mensaj = "Ingrese un Numero de telefono";
                resultado.Exito = false;
            }
            if (cliente.Direccion == " ")
            {
                resultado.Mensaj = "Ingrese una Direccion";
                resultado.Exito = false;
            }

            return resultado;
        }
    }
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public long Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }

    }
    public class Resul
    {
        public bool Exito { get; set; }
        public string Mensaj { get; set; }
    }
}
