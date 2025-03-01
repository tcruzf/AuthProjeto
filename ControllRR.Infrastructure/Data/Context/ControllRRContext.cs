///<summary>
///
///     Classe de configuração de banco de dados
///     Toda configuração explicita dos dados devem ser declarados aqui.
///     Relacionamentos,
///     Entidades que serão mapeadas
///     Relacionamentos personalizados
///  
///</sumary>

using System;
using Microsoft.EntityFrameworkCore;
using ControllRR.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ControllRR.Domain.Entities.BrazilianTaxs;

namespace ControllRR.Infrastructure.Data.Context;

public partial class ControllRRContext : IdentityDbContext<ApplicationUser>
{
    public ControllRRContext()
    {
    }

    public ControllRRContext(DbContextOptions<ControllRRContext> options)
        : base(options)
    {
    }
    //public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Device> Devices { get; set; }
    public virtual DbSet<Maintenance> Maintenances { get; set; }
    public virtual DbSet<Sector> Sectors { get; set; }
    public virtual DbSet<Document> Documents { get; set; }
    public virtual DbSet<MaintenanceNumberControl> MaintenanceNumberControls { get; set; }
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<Stock> Stocks { get; set; }
    public virtual DbSet<StockManagement> StockManagements { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Server> Servers { get; set; }
    public virtual DbSet<Login> Logins { get; set; }
    public virtual DbSet<ServerLogin> ServerLogins { get; set; }
    public virtual DbSet<MaintenanceProduct> MaintenanceProduct { get; set; }
    public virtual DbSet<SystemRoutine> SystemRoutines { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<PurchaseItem> PurchaseItems { get; set; }
    public virtual DbSet<SaleItem> SaleItems { get; set; }
    public virtual DbSet<FinancialRecord> FinancialRecords { get; set; }
    public virtual DbSet<TaxConfiguration> TaxConfigurations { get; set; }
    public virtual DbSet<CFOP> CFOPs {get; set;}
    public virtual DbSet<CNAE> CNAEs {get; set;}
    public virtual DbSet<COFINS> COFINs {get; set;}
    public virtual DbSet<CSOSN> CSOSNs {get; set;}
    public virtual DbSet<ICMS> ICMs {get; set;}
    public virtual DbSet<IcmsDesoneracao> IcmsDesoneracaos {get; set;}
    public virtual DbSet<IcmsModalidadeBC> IcmsModalidadeBCs {get; set;}
    public virtual DbSet<IcmsModalidadeST> IcmsModalidadeSTs {get; set;}
    public virtual DbSet<IcmsOrigem> IcmsOrigems {get; set;}
    public virtual DbSet<IPI> IPIs {get; set;}
    public virtual DbSet<IpiEnquadramento> IpiEnquadramentos {get; set;}
    public virtual DbSet<IpiOperacao> IpiOperacoes {get; set;}
    public virtual DbSet<NCM> NCMs {get; set;}
    public virtual DbSet<NFeSource> NFeSources {get; set;}
    public virtual DbSet<PIS> PIS {get; set;}




    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // String de conexão de fallback (para testes locais)
            optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=mypass;database=NEWGEN",
                new MySqlServerVersion(new Version(8, 0, 32)));
        }

        optionsBuilder.UseLazyLoadingProxies(false); // Desative o Lazy Loading
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<ServerLogin>()
            .HasKey(sl => new { sl.ServerId, sl.LoginId }); // Chave primária composta
        modelBuilder.Entity<ServerLogin>()
            .HasOne(sl => sl.Server)
            .WithMany(s => s.ServerLogins)
            .HasForeignKey(sl => sl.ServerId);
        modelBuilder.Entity<ServerLogin>()
            .HasOne(sl => sl.Login)
            .WithMany(l => l.ServerLogins)
            .HasForeignKey(sl => sl.LoginId);
        modelBuilder.Entity<StockManagement>()
            .HasOne(sm => sm.Maintenance)
            .WithMany()
            .HasForeignKey(sm => sm.MaintenanceId)
            .IsRequired(false); // Configura como relacionamento opcional
        modelBuilder.Entity<Stock>()
        .HasOne(s => s.Supplier)
        .WithMany()
        .HasForeignKey(s => s.SupplierId)
        .IsRequired(false); // Se Supplier for opcional
        // PurchaseOrder <-> PurchaseItem (Cascata)
        modelBuilder.Entity<PurchaseOrder>()
            .HasMany(po => po.Items)
            .WithOne(pi => pi.PurchaseOrder)
            .HasForeignKey(pi => pi.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Cascade);
        // PurchaseItem <-> Stock
        modelBuilder.Entity<PurchaseItem>()
            .HasOne(pi => pi.Stock)
            .WithMany()
            .HasForeignKey(pi => pi.StockId);
        // Sale <-> SaleItem (Cascata)
        modelBuilder.Entity<Sale>()
            .HasMany(s => s.Items)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
        // SaleItem <-> Stock
        modelBuilder.Entity<SaleItem>()
            .HasOne(si => si.Stock)
            .WithMany()
            .HasForeignKey(si => si.StockId);
        // FinancialRecord
        modelBuilder.Entity<FinancialRecord>()
            .HasKey(fr => fr.Id);
        // TaxConfiguration
        modelBuilder.Entity<TaxConfiguration>()
            .HasOne(tc => tc.Stock)
            .WithMany()
            .HasForeignKey(tc => tc.StockId)
            .IsRequired(false);
        // Preciso garantir que não terei problemas com campos que serão responsaveis por armazendar os valores monetarios
        modelBuilder.Entity<Stock>()
            .Property(s => s.PurchasePrice)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Stock>()
            .Property(s => s.SalePrice)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<PurchaseItem>()
            .Property(pi => pi.UnitPrice)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<SaleItem>()
            .Property(si => si.UnitPrice)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<FinancialRecord>()
        .Property(f => f.Amount)
        .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<FinancialRecord>()
            .Property(f => f.TotalTaxes)
            .HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Customer>()
            .HasIndex(c => c.CPF_CNPJ)
            .IsUnique()
            .HasFilter(null); // Para permitir NULLs (se aplicável)
        modelBuilder.Entity<Stock>()
            .Property(s => s.SupplierId)
            .IsRequired(false); // Torna a FK opcional
        modelBuilder.Entity<Supplier>()
            .HasIndex(s => s.CNPJ)
            .IsUnique();
        modelBuilder.Entity<Stock>()
            .Property(s => s.TaxRate)
            .HasColumnType("decimal(5,2)"); // 0.00 a 100.00%
        // Preciso garantir que estes campos essenciais sejam Not Null. 
        modelBuilder.Entity<Supplier>()
            .Property(s => s.CNPJ)
            .IsRequired();
        modelBuilder.Entity<Customer>()
            .Property(c => c.CPF_CNPJ)
            .IsRequired();
        modelBuilder.Entity<Stock>()
            .HasIndex(s => s.ProductName)
            .HasDatabaseName("IX_Stocks_ProductName");
        //
        modelBuilder.Entity<PurchaseOrder>()
            .HasIndex(p => p.OrderDate)
            .HasDatabaseName("IX_PurchaseOrders_OrderDate");
        modelBuilder.Entity<Stock>()
            .Property(s => s.PurchasePrice)
            .HasDefaultValue(0m);

        modelBuilder.Entity<Stock>()
            .Property(s => s.TaxRate)
            .HasDefaultValue(0m);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}