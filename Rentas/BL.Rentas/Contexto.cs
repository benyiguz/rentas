using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Rentas
{
    public class Contexto: DbContext
    {
        public Contexto(): base()
        {

        }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
