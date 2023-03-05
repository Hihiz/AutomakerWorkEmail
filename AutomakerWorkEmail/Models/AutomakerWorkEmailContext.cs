using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AutomakerWorkEmail.Models
{
    public partial class AutomakerWorkEmailContext : DbContext
    {
        public AutomakerWorkEmailContext()
        {
        }

        public AutomakerWorkEmailContext(DbContextOptions<AutomakerWorkEmailContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientOrder> ClientOrders { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AutomakerWorkEmail;Trusted_Connection=True;");

                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Passport).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                //entity.Property(e => e.PhotoPath).HasMaxLength(1000);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_Role");
            });

            modelBuilder.Entity<ClientOrder>(entity =>
            {
                entity.ToTable("ClientOrder");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.DateDispatch).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.FinalCost).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TrackNumber).HasMaxLength(20);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientOrders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Client");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ClientOrders)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Services");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.ToTable("Worker");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                //entity.Property(e => e.PhotoPath).HasMaxLength(1000);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Worker_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
