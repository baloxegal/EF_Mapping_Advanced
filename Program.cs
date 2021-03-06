using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Mapping_Advanced
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationContext())
            {
                context.Users.AddRange(new Manager { Departament = "IT", Name = "Vasea" },
                                       new Manager { Departament = "IT", Name = "Petea" },
                                       new Manager { Departament = "IT", Name = "Ghena" },
                                       new Employee { Salary = 2000, Name = "Aleftina" },
                                       new Employee { Salary = 4000, Name = "Agripina" },
                                       new User {Name = "Felotea" });
                context.SaveChanges();

                var hierarchy = context.Users.ToList();
                foreach(var v in hierarchy)
                {
                    Console.WriteLine($"Id: {v.Id}, Name: {v.Name}, Salary: {(v as Employee)?.Salary ?? 0}," +
                                        $" Departament: {(v as Manager)?.Departament ?? "NULL"}");
                }

                //var hierarchy_1 = context.Employees.ToList();
                var hierarchy_1 = context.Users.OfType<Employee>().ToList();
                foreach (var v in hierarchy_1)
                {
                    Console.WriteLine($"Id: {v.Id}, Name: {v.Name}, Salary: {(v as Employee)?.Salary ?? 0}");
                }

                var hierarchy_2 = context.Users.OfType<Manager>().ToList();
                foreach (var v in hierarchy_2)
                {
                    Console.WriteLine($"Id: {v.Id}, Name: {v.Name}, Departament: {(v as Manager)?.Departament ?? "NULL"}");
                }

                try
                {
                    var context1 = new ApplicationContext();
                    var context2 = new ApplicationContext();
                    var e = context1.Find<Employee>(4);
                    var f = context2.Find<Employee>(4);
                    e.Salary = 6000;
                    context1.Update(e);
                    context1.SaveChanges();
                    f.Salary = 10000;
                    context2.Update(f);
                    context2.SaveChanges();
                }
                catch (DbUpdateConcurrencyException d)
                {
                    Console.WriteLine(d.Message);
                }
                var g = context.Find<Employee>(4);
                Console.WriteLine("\nSalary is: " + g.Salary);
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    //[Table("Employees")]
    public class Employee : User
    {
        public int Salary { get; set; }
        public byte[] RowVersion { get; set; }
    }

    //[Table("Managers")]
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Employee>();
            //modelBuilder.Entity<Manager>();
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Manager>().ToTable("Managers");

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public class EmployeeConfig : IEntityTypeConfiguration<Employee>
        {
            public void Configure(EntityTypeBuilder<Employee> builder)
            {
                builder.Property(x => x.RowVersion)
                    .IsRowVersion();
            }
        }
    }
}
