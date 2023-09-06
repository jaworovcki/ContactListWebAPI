using ContactListWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListWebAPI.DataAccess
{
    public class ContactDataContext : DbContext
    {
        public DbSet<Person> People { get; set; } = null!;
        public ContactDataContext(DbContextOptions<ContactDataContext> options)
            : base(options)
        {
        }
    }
}
