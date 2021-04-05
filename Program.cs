using System;
using Microsoft.EntityFrameworkCore;

namespace EF_Mapping_Advanced
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationContext())
            {

            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Employee : User
    {
        public int Salary { get; set; }
    }
    public class Manager : User
    {
        public string Departament { get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=InheritanceDB;Trusted_Connection=True;");
        }
    }
}
