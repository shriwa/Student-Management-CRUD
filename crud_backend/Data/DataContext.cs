using crud_backend.Models.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace crud_backend.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {
        
        }

        public DbSet<Student> Students { get; set; }
    }
}
