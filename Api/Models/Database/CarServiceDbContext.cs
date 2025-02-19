using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.Database;

public partial class CarServiceDbContext : DbContext
{
    private IConfiguration _configuration;

    public CarServiceDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public CarServiceDbContext(DbContextOptions<CarServiceDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ImportOrder> ImportOrders { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderMaterialClient> OrderMaterialClients { get; set; }

    public virtual DbSet<OrderMaterialService> OrderMaterialServices { get; set; }

    public virtual DbSet<OrderService> OrderServices { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            String connection = _configuration.GetValue<String>("ConnectionStrings:DefaultConnection")!;
            optionsBuilder.UseNpgsql(connection);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.Id)
                .HasConstraintName("client___fk");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IsAdmin)
                .HasDefaultValue(false)
                .HasColumnName("is_admin");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.Id)
                .HasConstraintName("employee___fk");
        });

        modelBuilder.Entity<ImportOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("import_order_pkey");

            entity.ToTable("import_order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarBrand)
                .HasMaxLength(100)
                .HasColumnName("car_brand");
            entity.Property(e => e.CarModel)
                .HasMaxLength(100)
                .HasColumnName("car_model");
            entity.Property(e => e.CarNumber)
                .HasMaxLength(15)
                .HasColumnName("car_number");
            entity.Property(e => e.CarVin)
                .HasMaxLength(17)
                .HasColumnName("car_vin");
            entity.Property(e => e.ClientFullname)
                .HasMaxLength(300)
                .HasColumnName("client_fullname");
            entity.Property(e => e.ClientMaterials)
                .HasMaxLength(500)
                .HasColumnName("client_materials");
            entity.Property(e => e.DateComplete).HasColumnName("date_complete");
            entity.Property(e => e.DateCreate).HasColumnName("date_create");
            entity.Property(e => e.EmployeeFullname)
                .HasMaxLength(300)
                .HasColumnName("employee_fullname");
            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.Materials)
                .HasMaxLength(500)
                .HasColumnName("materials");
            entity.Property(e => e.Services)
                .HasMaxLength(500)
                .HasColumnName("services");

            entity.HasOne(d => d.Gu).WithMany(p => p.ImportOrders)
                .HasForeignKey(d => d.Guid)
                .HasConstraintName("import_order_guid_fkey");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_pkey");

            entity.ToTable("material");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_pkey");

            entity.ToTable("order");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarBrand)
                .HasMaxLength(100)
                .HasColumnName("car_brand");
            entity.Property(e => e.CarModel)
                .HasMaxLength(100)
                .HasColumnName("car_model");
            entity.Property(e => e.CarNumber)
                .HasMaxLength(10)
                .HasColumnName("car_number");
            entity.Property(e => e.CarVin)
                .HasMaxLength(17)
                .HasColumnName("car_vin");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.DateComplete).HasColumnName("date_complete");
            entity.Property(e => e.DateCreate).HasColumnName("date_create");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("order_client_id_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("order_employee_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("order_status_id_fkey");
        });

        modelBuilder.Entity<OrderMaterialClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_material_client_pkey");

            entity.ToTable("order_material_client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderMaterialClients)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_material_client___fk");
        });

        modelBuilder.Entity<OrderMaterialService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_material_service_pkey");

            entity.ToTable("order_material_service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Material).WithMany(p => p.OrderMaterialServices)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("order_material_service_material_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderMaterialServices)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_material_service_order_id_fkey");
        });

        modelBuilder.Entity<OrderService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_service_pkey");

            entity.ToTable("order_service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.ExecutorId).HasColumnName("executor_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");

            entity.HasOne(d => d.Executor).WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.ExecutorId)
                .HasConstraintName("order_service_executor_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_service_order_id_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("order_service_service_id_fkey");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("person_pkey");

            entity.ToTable("person");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(100)
                .HasColumnName("middle_name");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_pkey");

            entity.ToTable("service");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.ToTable("status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("transaction_pkey");

            entity.ToTable("transaction");

            entity.Property(e => e.Guid).HasColumnName("guid");
            entity.Property(e => e.DateTime)
                .HasMaxLength(30)
                .HasColumnName("date_time");
            entity.Property(e => e.Imported).HasColumnName("imported");
            entity.Property(e => e.Reason)
                .HasMaxLength(100)
                .HasColumnName("reason");
            entity.Property(e => e.RowsCount).HasColumnName("rows_count");
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .HasColumnName("table_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}