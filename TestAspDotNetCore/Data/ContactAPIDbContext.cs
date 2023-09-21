using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TestAspDotNetCore.Models;

namespace TestAspDotNetCore.Data
{
    public class ContactAPIDbContext: DbContext
    {
        public ContactAPIDbContext(DbContextOptions<ContactAPIDbContext> options) : base(options) {

            try
            {
                var databaseCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreater != null)
                {
                    if (!databaseCreater.CanConnect())
                    {
                        databaseCreater.Create();
                    }
                    if (!databaseCreater.HasTables())
                    {
                        databaseCreater.CreateTables();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<Contacts> Contacts { get; set; }   
    }
}
