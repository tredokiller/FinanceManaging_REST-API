using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<IncomeExpenseCategory> IncomeExpenseTypes { set; get; }
    public DbSet<FinanceOperation> FinanceOperations { set; get; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FinanceOperation>()
            .HasOne(p => p.CategoryType)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .HasPrincipalKey(p => p.Id);
        
        modelBuilder.Entity<IncomeExpenseCategory>().HasData(
            new IncomeExpenseCategory
            {
                Id = 1,
                Name = "Salary",
                FinanceActivityType  = FinanceActivityEnum.Income
            });
        
        modelBuilder.Entity<IncomeExpenseCategory>().HasData(
            new IncomeExpenseCategory
            {
                Id = 2,
                Name = "Deposit",
                FinanceActivityType  = FinanceActivityEnum.Income
            });
        
        modelBuilder.Entity<IncomeExpenseCategory>().HasData(
            new IncomeExpenseCategory
            {
                Id = 3,
                Name = "Public Utilities",
                FinanceActivityType  = FinanceActivityEnum.Expense
            });
        
        modelBuilder.Entity<IncomeExpenseCategory>().HasData(
            new IncomeExpenseCategory
            {
                Id = 4,
                Name = "Online Subscriptions",
                FinanceActivityType  = FinanceActivityEnum.Expense
            });
    }
}