using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace personal_accounting
{
    class AppContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public AppContext() : base("personal_accounting.Properties.Settings.personal_accounting_DBConnectionStrings") { }
    }
}
