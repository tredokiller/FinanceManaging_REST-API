using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Repository;
using FluentAssertions;
using Infrastructure.Models;
using Infrastructure.Services;
using Moq;

namespace Infrastructure.Tests;

[TestClass]
public class DateReportServiceTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowArgumentNullExceptionTest()
    {
        var service = new DateReportService(null, null);
    }


    [TestMethod]
    public async Task GetDailyReportSuccessTest()
    {
        var repo = new Mock<IRepository<FinanceOperation>>();

        var listOfOperations = new List<FinanceOperation>()
        {
            new FinanceOperation()
            {
                Date = new DateTime(2020, 12, 12),
                CategoryId = 1,
                Amount = 200
            }
        };

        repo.Setup((repository => repository.GetAll())).ReturnsAsync(listOfOperations);
        
        var incomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();

        var category = new IncomeExpenseCategory()
        {
            FinanceActivityType = FinanceActivityEnum.Income
        };

        incomeExpensesRepo.Setup((repository => repository.Get(1))).ReturnsAsync(category);
        var service = new DateReportService(repo.Object, incomeExpensesRepo.Object);

        var trueDateReport = new DateReport()
        {
            TotalExpense = 0,
            TotalIncome = 200,
            Operations = listOfOperations
        };
        
        var result = service.GetDailyReport(new DateTime(2020, 12, 12));

        result.Result.Should().BeEquivalentTo(trueDateReport);
    }
    
    
    [TestMethod]
    public async Task GetDatePeriodReportSuccessTest()
    {
        var repo = new Mock<IRepository<FinanceOperation>>();

        var listOfOperations = new List<FinanceOperation>()
        {
            new FinanceOperation()
            {
                Date = new DateTime(2020, 12, 12),
                CategoryId = 1,
                Amount = 200
            },
            new FinanceOperation()
            {
                Date = new DateTime(2020, 12, 12),
                CategoryId = 2,
                Amount = 200
            }
        };

        repo.Setup((repository => repository.GetAll())).ReturnsAsync(listOfOperations);
        
        var incomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();

        var category = new IncomeExpenseCategory()
        {
            FinanceActivityType = FinanceActivityEnum.Income
        };

        var expenseCategory = new IncomeExpenseCategory()
        {
            FinanceActivityType = FinanceActivityEnum.Expense
        };
        
        incomeExpensesRepo.Setup((repository => repository.Get(1))).ReturnsAsync(category);
        incomeExpensesRepo.Setup((repository => repository.Get(2))).ReturnsAsync(expenseCategory);
        var service = new DateReportService(repo.Object, incomeExpensesRepo.Object);

        var trueDateReport = new DateReport()
        {
            TotalExpense = 200,
            TotalIncome = 200,
            Operations = listOfOperations
        };
        
        var result = service.GetPeriodOfDatesReport(new DateTime(2020, 11, 12) , new DateTime(2020, 12, 13));

        result.Result.Should().BeEquivalentTo(trueDateReport);
    }
}