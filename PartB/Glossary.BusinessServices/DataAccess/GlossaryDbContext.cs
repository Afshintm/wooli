using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Glossary.BusinessServices.DataAccess
{
    public class GlossaryDbContext : DbContext
    {
        public GlossaryDbContext(DbContextOptions<GlossaryDbContext> options) : base(options)
        {
        }

        public DbSet<Models.GlossaryItem> GlossaryItems { get; set; }
    }
}
