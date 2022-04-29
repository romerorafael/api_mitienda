using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mitienda.Models;

namespace mitienda.Data
{
    public class mitiendaContext : DbContext
    {
        public mitiendaContext (DbContextOptions<mitiendaContext> options)
            : base(options)
        {
        }

        public DbSet<mitienda.Models.Clientes> Clientes { get; set; }
    }
}
