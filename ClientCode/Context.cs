
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity;

namespace ClientCode
{
    public class Context : DbContext
    {
        public Context(): base("Context")
        {
            
        }

        public Context(string databaseName) : base(databaseName)
        {
            
        }

        public Context(DbConnection createTransient) : base(createTransient, true)
        {
        }

        public DbSet<Data> Datas { get; set; }
    }

    public class Data
    {
        [Key]
        public int Id { get; set; }

        public string Info { get; set; }
    }
}
