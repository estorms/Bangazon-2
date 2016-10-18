//this using statement (EntityFrameworkCore) allows us to make BangazonContent a derived claass of DBContext, which represents a session with a database
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using Bangazon.Models;
//what is the exact difference between System.Linq and DbContext?
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bangazon.Data
{
    public class BangazonContext : DbContext
    {


        //If I understand this correctly, in the method below, we're making a custom constructor for an instance of BangazonContext, which itself derives from DbContext. Then, we are passing into that custom constructor a type of DBContextOptions(must be a type on the DbContext class itself?) and then running the ContextOptions SPECIFIC TO the BangazonContext through the base methods defined in DbContext, so that we don't have to instantiate an instance of DBContext itself to get to those methods. So every time we create a new instance of BangazonContext, we are making DbContextOptions available to the methods involved in that instance

        //DbContext would therefore appear to be an inherently virtual super-class

        
        public BangazonContext(DbContextOptions<BangazonContext> options)
            : base(options)
        { }

        //This is where we are building the database, using the DbContext method of DbSet. DbSet exists in memory, not in database yet
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<LineItem> LineItem { get; set; }

//here, we are overriding another virtual method that exists on DbContext(ModelBuilder) and using a lambda to specify that each time we create a new instance of any of the following entities (use this word to represent objects and predefined strongly-typed types), such as orders or customers, it has an additional property of DataCreated (built by database) and default value that does NOT equal null, which is what SQL defaults to.

//OnMOdelCreating is a method on DBContext, ModelBuilder is a method on Microsoft.EntityFrameworkCore that we are passing into the OnModelCreating method in order to override the default vaules in that method

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

            modelBuilder.Entity<Order>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                modelBuilder.Entity<PaymentType>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                modelBuilder.Entity<Product>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                modelBuilder.Entity<LineItem>();
                
        }
    }

}