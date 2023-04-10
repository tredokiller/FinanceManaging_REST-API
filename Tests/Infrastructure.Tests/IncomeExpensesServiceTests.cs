using AutoMapper;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Repository;
using FluentAssertions;
using Infrastructure.Models;
using Infrastructure.Models.Exceptions;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Infrastructure.Services;
using Moq;

namespace Infrastructure.Tests;

[TestClass]
public class IncomeExpensesServiceTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowArgumentNullExceptionTest()
    {
        var service = new IncomeExpenseService(null, null);
    }


    [TestMethod]
    public async Task GetIncomeExpenseTypesSuccessTest()
    {
        var repo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();

        var incomeExpenseCategories = new List<IncomeExpenseCategory>()
        {
            new IncomeExpenseCategory()
            {
                Id = 1,
                FinanceActivityType = FinanceActivityEnum.Expense,
                Name = "Xbox Ultimate"
            },
            new IncomeExpenseCategory()
            {
                Id = 1,
                FinanceActivityType = FinanceActivityEnum.Income,
                Name = "Deposit"
            }
        };


        repo.Setup(repository => repository.GetAll()).ReturnsAsync(incomeExpenseCategories);
        
        var service = new IncomeExpenseService(repo.Object, mapper.Object);

        var result = service.GetIncomeExpenseTypes();

        result.Result.Should().BeEquivalentTo(incomeExpenseCategories);

    }


    [TestMethod]
    public async Task GetIncomeExpenseTypeSuccessTest()
    {
        var repo = new Mock<IRepository<IncomeExpenseCategory>>();

        var operation = new IncomeExpenseCategory()
        {
            Id = 1,
            FinanceActivityType = FinanceActivityEnum.Expense,
            Name = "Xbox Ultimate"
        };  


        repo.Setup((repository => repository.Get(1))).ReturnsAsync(operation);
        var mapper = new Mock<IMapper>();
        
        var service = new IncomeExpenseService(repo.Object, mapper.Object);
        
        var result = service.GetIncomeExpenseType(1);

        result.Result.Should().BeEquivalentTo(operation);
    }
    
    [TestMethod]
    public async Task GetIncomeExpenseTypeBadRequestTest()
    {
        var repo = new Mock<IRepository<IncomeExpenseCategory>>();
        
        var mapper = new Mock<IMapper>();
        
        var service = new IncomeExpenseService(repo.Object, mapper.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => service.GetIncomeExpenseType(-1));
    }
    
    [TestMethod]
    public async Task GetIncomeExpenseTypeNotFoundedTest()
    {
        var repo = new Mock<IRepository<IncomeExpenseCategory>>();
        
        var mapper = new Mock<IMapper>();
        
        var service = new IncomeExpenseService(repo.Object, mapper.Object);

        await Assert.ThrowsExceptionAsync<NotFoundException>(() => service.GetIncomeExpenseType(100));
    }       
          
    [TestMethod]
    public async Task CreateIncomeExpenseTypeBadRequestTest()
    {
        var repo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();
        
        var service = new IncomeExpenseService(repo.Object, mapper.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => service.CreateIncomeExpenseType(null));
    }
    
    
    [TestMethod]
    public async Task CreateIncomeExpenseTypeSuccessTest()
    {
        var category = new IncomeExpensesAddRequest()
        {
            Name = "Car",
            FinanceActivityType = FinanceActivityEnum.Expense
        };

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<IncomeExpensesAddRequest, IncomeExpenseCategory>(It.IsAny<IncomeExpensesAddRequest>())).Returns(new IncomeExpenseCategory());
        
        mockMapper.Setup(m => m.Map<IncomeExpenseCategory, IncomeExpensesAddResponse>(It.IsAny<IncomeExpenseCategory>())).Returns(new IncomeExpensesAddResponse());

        var mockIncomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();
        mockIncomeExpensesRepo.Setup(m => m.Get(It.IsAny<int>())).ReturnsAsync(new IncomeExpenseCategory { FinanceActivityType = FinanceActivityEnum.Income, Id = 1, Name = "Salary" });
        

        var financeOperationService = new IncomeExpenseService(mockIncomeExpensesRepo.Object, mockMapper.Object);
        
        var result = await financeOperationService.CreateIncomeExpenseType(category);

        result.Should().BeOfType<IncomeExpensesAddResponse>();
    }
    
    
    
    [TestMethod]
    public void RemoveIncomeExpenseTypeBadRequestTest()
    {
        var repo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();
        
        var service = new IncomeExpenseService(repo.Object, mapper.Object);

        Assert.ThrowsExceptionAsync<BadRequestException>(() => service.RemoveIncomeExpenseType(-1));
    }
    
    
    
    [TestMethod]
    public void RemoveIncomeExpenseTypeNotFoundTest()
    {
        var repo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();
        
        repo.Setup((repository => repository.Get(100))).ReturnsAsync(null as IncomeExpenseCategory);


        var service = new IncomeExpenseService(repo.Object, mapper.Object);
        
        Assert.ThrowsExceptionAsync<NotFoundException>(() => service.RemoveIncomeExpenseType(100));
    }



    [TestMethod]
    public async Task UpdateIncomeExpenseTypeBadRequestTest()
    {
        var repo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();
        
        var service = new IncomeExpenseService(repo.Object, mapper.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => service.UpdateIncomeExpenseType(null));
    }


    [TestMethod]
    public async Task UpdateIncomeExpenseTypeSuccessTest()
    {
        var mapperMock = new Mock<IMapper>();
        var incomeExpensesRepositoryMock = new Mock<IRepository<IncomeExpenseCategory>>();
        
        var request = new IncomeExpensesUpdateRequest()
        {
            Id = 1,
            FinanceActivityType = FinanceActivityEnum.Income,
            Name = "Deposit"
        };

        var categoryToUpdate = new IncomeExpenseCategory()
        {
            Id = 2,
            FinanceActivityType = FinanceActivityEnum.Income,
            Name = "Deposittttt"

        };

        var updatedCategory = new IncomeExpenseCategory()
        {
            Id = 1,
            FinanceActivityType = FinanceActivityEnum.Income,
            Name = "Deposit"
        };
        
        incomeExpensesRepositoryMock.Setup(x => x.Update(It.IsAny<IncomeExpenseCategory>())).ReturnsAsync(updatedCategory);
        

        mapperMock
            .Setup(m => m.Map<IncomeExpensesUpdateRequest, IncomeExpenseCategory>(request))
            .Returns(categoryToUpdate);

        mapperMock
            .Setup(m => m.Map<IncomeExpenseCategory, IncomeExpensesUpdateResponse>(updatedCategory))
            .Returns(new IncomeExpensesUpdateResponse()
            {
                Id = 1,
                FinanceActivityType = FinanceActivityEnum.Income,
                Name = "Deposit"
                
            });

        var service = new IncomeExpenseService(incomeExpensesRepositoryMock.Object, mapperMock.Object);

        
        var result = await service.UpdateIncomeExpenseType(request);

       
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(IncomeExpensesUpdateResponse));
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Deposit", result.Name);
        Assert.AreEqual(FinanceActivityEnum.Income, result.FinanceActivityType);
    }
    
    
}