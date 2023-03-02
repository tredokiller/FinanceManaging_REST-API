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
public class FinanceOperationServiceTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowArgumentNullExceptionTest()
    {
        var service = new FinanceOperationService(null, null , null);
    }


    [TestMethod]
    public async Task GetFinanceOperationsSuccessTest()
    {
        var repo = new Mock<IRepository<FinanceOperation>>();
        
        var incomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();

        var listOfOperations = new List<FinanceOperation>()
        {
            new FinanceOperation()
            {
                Date = new DateTime(2020, 11, 12),
                CategoryId = 1,
                Amount = 200
            },
            new FinanceOperation()
            {
                Date = new DateTime(2020, 11, 12),
                CategoryId = 2,
                Amount = 200
            }
        };


        repo.Setup(repository => repository.GetAll()).ReturnsAsync(listOfOperations);
        
        var service = new FinanceOperationService(repo.Object, mapper.Object, incomeExpensesRepo.Object);

        var result = service.GetFinanceOperations();

        result.Result.Should().BeEquivalentTo(listOfOperations);

    }


    [TestMethod]
    public async Task GetFinanceOperationSuccessTest()
    {
        var repo = new Mock<IRepository<FinanceOperation>>();

        var operation = new FinanceOperation()
        {
            Id = 1,
            Date = new DateTime(),
            Amount = 100,
            CategoryId = 1
        };


        repo.Setup((repository => repository.Get(1))).ReturnsAsync(operation);
        var incomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();
        
        var service = new FinanceOperationService(repo.Object, mapper.Object, incomeExpensesRepo.Object);
        
        var result = service.GetFinanceOperation(1);

        result.Result.Should().BeEquivalentTo(operation);
    }
    
    [TestMethod]
    public async Task GetFinanceOperationBadRequestTest()
    {
        var repo = new Mock<IRepository<FinanceOperation>>();
        
        var incomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();
        
        var service = new FinanceOperationService(repo.Object, mapper.Object, incomeExpensesRepo.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => service.GetFinanceOperation(-1));
    }
    
    [TestMethod]
    public async Task CreateFinanceOperationBadRequestTest()
    {
        var repo = new Mock<IRepository<FinanceOperation>>();
        
        var incomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();
        
        var service = new FinanceOperationService(repo.Object, mapper.Object, incomeExpensesRepo.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => service.CreateFinanceOperation(null));
    }
    
    
    [TestMethod]
    public async Task CreateFinanceOperationSuccessTest()
    {
        var financeOperation = new FinanceOperationAddRequest
        {
            CategoryId = 1,
            Amount = 100,
            Date = new DateTime(2022, 3, 1)
        };

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map<FinanceOperationAddRequest, FinanceOperation>(It.IsAny<FinanceOperationAddRequest>())).Returns(new FinanceOperation());
        
        mockMapper.Setup(m => m.Map<FinanceOperation, FinanceOperationAddResponse>(It.IsAny<FinanceOperation>())).Returns(new FinanceOperationAddResponse());

        var mockIncomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();
        mockIncomeExpensesRepo.Setup(m => m.Get(It.IsAny<int>())).ReturnsAsync(new IncomeExpenseCategory { FinanceActivityType = FinanceActivityEnum.Income, Id = 1, Name = "Salary" });

        var mockFinanceOperationsRepo = new Mock<IRepository<FinanceOperation>>();
        mockFinanceOperationsRepo.Setup(m => m.Create(It.IsAny<FinanceOperation>())).ReturnsAsync(new FinanceOperation());

        var financeOperationService = new FinanceOperationService(mockFinanceOperationsRepo.Object, mockMapper.Object, mockIncomeExpensesRepo.Object);
        
        var result = await financeOperationService.CreateFinanceOperation(financeOperation);

        result.Should().BeOfType<FinanceOperationAddResponse>();
    }
    
    
    
    [TestMethod]
    public void RemoveFinanceOperationBadRequestTest()
    {
        var repo = new Mock<IRepository<FinanceOperation>>();
        
        var incomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();
        
        var service = new FinanceOperationService(repo.Object, mapper.Object, incomeExpensesRepo.Object);

        Assert.ThrowsException<BadRequestException>(() => service.RemoveFinanceOperation(-1));
    }
    
    
    
    [TestMethod]
    public async Task RemoveFinanceOperationNotFoundTest()
    {
        var repo = new Mock<IRepository<FinanceOperation>>();
        
        var incomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();
        repo.Setup((repository => repository.Get(100))).ReturnsAsync(null as FinanceOperation);


        var service = new FinanceOperationService(repo.Object, mapper.Object, incomeExpensesRepo.Object);

        Assert.ThrowsException<NotFoundException>(() => service.RemoveFinanceOperation(100));
    }



    [TestMethod]
    public async Task UpdateFinanceOperationBadRequestTest()
    {
        var repo = new Mock<IRepository<FinanceOperation>>();
        
        var incomeExpensesRepo = new Mock<IRepository<IncomeExpenseCategory>>();
        var mapper = new Mock<IMapper>();
        
        var service = new FinanceOperationService(repo.Object, mapper.Object, incomeExpensesRepo.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => service.UpdateFinanceOperation(null));
    }


    [TestMethod]
    public async Task UpdateFinanceOperationSuccessTest()
    {
        var mapperMock = new Mock<IMapper>();
        var financeOperationsRepositoryMock = new Mock<IRepository<FinanceOperation>>();
        var incomeExpensesRepositoryMock = new Mock<IRepository<IncomeExpenseCategory>>();
        
        var request = new FinanceOperationUpdateRequest
        {
            Id = 1,
            Amount = 100,
            Category = 2,
            Date = new DateTime(2022, 02, 01)
        };

        var operationToUpdate = new FinanceOperation
        {
            Id = 1,
            Amount = 200,
            CategoryId = 1,
            Date = new DateTime(2022, 01, 01)
        };

        var updatedOperation = new FinanceOperation
        {
            Id = 1,
            Amount = 100,
            CategoryId = 1,
            Date = new DateTime(2022, 02, 01)
        };
        
        financeOperationsRepositoryMock
            .Setup(x => x.Update(It.IsAny<FinanceOperation>()))
            .ReturnsAsync(updatedOperation);
        

        mapperMock
            .Setup(m => m.Map<FinanceOperationUpdateRequest, FinanceOperation>(request))
            .Returns(operationToUpdate);

        mapperMock
            .Setup(m => m.Map<FinanceOperation, FinanceOperationUpdateResponse>(updatedOperation))
            .Returns(new FinanceOperationUpdateResponse
            {
                Id = 1,
                Amount = 100,
                Date = new DateTime(2022, 02, 01),
                Category = 1,
                
            });

        var financeOperationService = new FinanceOperationService(
            financeOperationsRepositoryMock.Object,
            mapperMock.Object, incomeExpensesRepositoryMock.Object
            );

        
        var result = await financeOperationService.UpdateFinanceOperation(request);

       
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(FinanceOperationUpdateResponse));
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual(100, result.Amount);
        Assert.AreEqual(1, result.Category);
        Assert.AreEqual(new DateTime(2022, 02, 01), result.Date);
    }
    
    
}