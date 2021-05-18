using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBlogs.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(s => s.id);
            modelBuilder.Entity<Post>().HasKey(s => s.id);
            // CHAVE ESTRANGEIRA
            modelBuilder.Entity<Post>().HasOne(p => p.User).WithMany(b=>b.Posts).HasForeignKey(p=>p.userId);
            //Unico
            modelBuilder.Entity<User>().HasIndex(t => t.email).IsUnique();

            // NOT NULL
            modelBuilder.Entity<User>().Property(t => t.displayName).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.email).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.password).IsRequired();

            modelBuilder.Entity<Post>().Property(t => t.title).IsRequired();
            modelBuilder.Entity<Post>().Property(t => t.content).IsRequired();

            // CAN BE NULL
            //modelBuilder.Entity<Student>().Property(p => p.Heigth).IsOptional();
            // TAMANHO MÁXIMO
            //modelBuilder.Entity<User>().Property(p => p.email).HasMaxLength(80);
        }
    }
}
