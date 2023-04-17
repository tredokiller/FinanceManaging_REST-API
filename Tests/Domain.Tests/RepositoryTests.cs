using Domain.Context;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Repository;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tests;

[TestClass]
public class RepositoryTests
{
    
    private Task<ApplicationDbContext> GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var dataBaseContext = new ApplicationDbContext(options);
        dataBaseContext.Database.EnsureCreated();

        return Task.FromResult(dataBaseContext); 
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowsNullException()
    {
        var repo = new Repository<IncomeExpenseCategory>(null);
    }
    
    [TestMethod]
    public void GetAllSuccess()
    {
        var repo = new Repository<IncomeExpenseCategory>(GetDbContext().Result);
        
        repo.Create(CreateDefaultCategory());
        repo.Create(CreateDefaultCategory());
        
        var result = repo.GetAll();

        result.Result.Should().BeAssignableTo<IEnumerable<IncomeExpenseCategory>>();
    }
    
    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    public void GetSuccess(int id)
    {
        var repo = new Repository<IncomeExpenseCategory>(GetDbContext().Result);
        
        repo.Create(CreateDefaultCategory());
        repo.Create(CreateDefaultCategory());
        
        var result = repo.Get(id);

        result.Result.Should().BeOfType<IncomeExpenseCategory>();
    }
    
    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    public void GetNull(int id)
    {
        var repo = new Repository<IncomeExpenseCategory>(GetDbContext().Result);
        
        repo.Create(CreateDefaultCategory());
        repo.Create(CreateDefaultCategory());
        
        var result = repo.Get(id);

        result.Result.Should().BeNull();
    }
    
    [TestMethod]
    public void CreateSuccess()
    {
        var repo = new Repository<IncomeExpenseCategory>(GetDbContext().Result);
        
        repo.Create(CreateDefaultCategory());
        repo.Create(CreateDefaultCategory());


        var trueData = CreateDefaultCategory();
        
        var result = repo.Create(trueData);

        result.Result.Should().BeOfType<IncomeExpenseCategory>();
        result.Result.Should().BeEquivalentTo(trueData);
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void CreateThrowNullException()
    {
        var repo = new Repository<IncomeExpenseCategory>(GetDbContext().Result);
        
        var result = repo.Create(null);
    }
    
    
    
    [TestMethod]
    public void UpdateSuccess()
    {
        var repo = new Repository<IncomeExpenseCategory>(GetDbContext().Result);

        var oldData = CreateDefaultCategory();

        var updatedData = new IncomeExpenseCategory()
        {
            Id = oldData.Id,
            Name = "Deposit",
            FinanceActivityType = FinanceActivityEnum.Income
        };
        
        repo.Create(oldData);
        var result = repo.Update(updatedData);

        result.Result.Should().BeOfType<IncomeExpenseCategory>();
        result.Result.Should().BeEquivalentTo(updatedData);
    }
    
    
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UpdateThrowNullException()
    {
        var repo = new Repository<IncomeExpenseCategory>(GetDbContext().Result);
        
        var result = repo.Update(null);
    }
    
    
    [TestMethod]
    public void RemoveSuccess()
    {
        var repo = new Repository<IncomeExpenseCategory>(GetDbContext().Result);

        var data = CreateDefaultCategory();

        repo.Create(CreateDefaultCategory());
        repo.Create(CreateDefaultCategory());
        repo.Create(data);
        repo.Remove(data);

        var result = repo.Get(data.Id);

        result.Result.Should().BeNull();
    }
    
    
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RemoveThrowNullException()
    {
        var repo = new Repository<IncomeExpenseCategory>(GetDbContext().Result);
        
        repo.Remove(null);
    }




    private IncomeExpenseCategory CreateDefaultCategory()
    {
        var category = new IncomeExpenseCategory()
        {
            Name = "Something",
            FinanceActivityType = FinanceActivityEnum.Income,
        };

        return category;
    }
}

   