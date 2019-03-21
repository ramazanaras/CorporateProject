using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateProject.DataAccess.Concrete.EntityFramework.Mappings;
using CorporateProject.Entities.Concrete;

//manage nugettan entity frameworkü yükle
namespace CorporateProject.DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext : DbContext
    {
        public NorthwindContext():base("Northwind_Name")
        {
            //hazır veritabanı olduğu için yani veritabanı zaten hazır olduğu için migrationı kapatıyoruz.yani entitylerde bir değişiklik olduğunda bunu veritabanına yansıtma
            Database.SetInitializer<NorthwindContext>(null);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //mappingimizi burda ekliyoruz
            modelBuilder.Configurations.Add(new ProductMap());

    
        }
    }
}
