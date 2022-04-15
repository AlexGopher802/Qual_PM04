using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GasDeliveryApi.Models
{
    public partial class GasDeliveryDBContext : DbContext
    {
        public GasDeliveryDBContext()
        {
        }

        public GasDeliveryDBContext(DbContextOptions<GasDeliveryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressDelivery> AddressDeliveries { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<OrderCompo> OrderCompos { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Ordered> Ordereds { get; set; }
        public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=194.32.248.98;user id=gas-client;password=Gas3434!;persistsecurityinfo=True;database=GasDeliveryDB", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.35-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            modelBuilder.Entity<AddressDelivery>(entity =>
            {
                entity.ToTable("AddressDelivery");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.ApartmentNum)
                    .HasColumnType("int(11)")
                    .HasColumnName("apartmentNum");

                entity.Property(e => e.FloorNum)
                    .HasColumnType("int(11)")
                    .HasColumnName("floorNum");

                entity.Property(e => e.FrontDoorNum)
                    .HasColumnType("int(11)")
                    .HasColumnName("frontDoorNum");

                entity.Property(e => e.Intercom)
                    .HasMaxLength(20)
                    .HasColumnName("intercom")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.HasIndex(e => e.PersonalInfoId, "personalInfoId");

                entity.HasIndex(e => e.UserId, "userId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.PersonalInfoId)
                    .HasColumnType("int(11)")
                    .HasColumnName("personalInfoId");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("userId");

                entity.HasOne(d => d.PersonalInfo)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.PersonalInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Client_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Client_ibfk_2");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Driver");

                entity.HasIndex(e => e.PersonalInfoId, "personalInfoId");

                entity.HasIndex(e => e.UserId, "userId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.PersonalInfoId)
                    .HasColumnType("int(11)")
                    .HasColumnName("personalInfoId");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("userId");

                entity.HasOne(d => d.PersonalInfo)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.PersonalInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Driver_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Driver_ibfk_2");
            });

            modelBuilder.Entity<OrderCompo>(entity =>
            {
                entity.HasIndex(e => e.OrderId, "orderId");

                entity.HasIndex(e => e.ProductId, "productId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("orderId");

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("productId");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("quantity");

                entity.Property(e => e.Sum).HasColumnName("sum");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderCompos)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderCompos_ibfk_1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderCompos)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderCompos_ibfk_2");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Ordered>(entity =>
            {
                entity.ToTable("Ordered");

                entity.HasIndex(e => e.AddressId, "addressId");

                entity.HasIndex(e => e.ClientId, "clientId");

                entity.HasIndex(e => e.DriverId, "driverId");

                entity.HasIndex(e => e.StatusId, "statusId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.AddressId)
                    .HasColumnType("int(11)")
                    .HasColumnName("addressId");

                entity.Property(e => e.ClientId)
                    .HasColumnType("int(11)")
                    .HasColumnName("clientId");

                entity.Property(e => e.DateDelivery)
                    .HasColumnType("date")
                    .HasColumnName("dateDelivery");

                entity.Property(e => e.DesiredTimeFrom)
                    .HasColumnType("time")
                    .HasColumnName("desiredTimeFrom");

                entity.Property(e => e.DesiredTimeTo)
                    .HasColumnType("time")
                    .HasColumnName("desiredTimeTo");

                entity.Property(e => e.DriverId)
                    .HasColumnType("int(11)")
                    .HasColumnName("driverId");

                entity.Property(e => e.ExactTime)
                    .HasColumnType("time")
                    .HasColumnName("exactTime");

                entity.Property(e => e.StatusId)
                    .HasColumnType("int(11)")
                    .HasColumnName("statusId");

                entity.Property(e => e.Sum).HasColumnName("sum");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ordered_ibfk_1");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ordered_ibfk_3");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("Ordered_ibfk_2");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Ordereds)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ordered_ibfk_4");
            });

            modelBuilder.Entity<PersonalInfo>(entity =>
            {
                entity.ToTable("PersonalInfo");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("firstName")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("lastName")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(40)
                    .HasColumnName("patronymic")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Phone, "phone")
                    .IsUnique();

                entity.HasIndex(e => e.RoleId, "roleId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_ibfk_1");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasIndex(e => e.Name, "name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
