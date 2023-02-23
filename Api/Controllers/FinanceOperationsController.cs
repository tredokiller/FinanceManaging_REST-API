using AutoMapper;
using Domain.Entities;
using Infrastructure.Services;
using Infrastructure.Services.Requests;
using Infrastructure.Services.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinanceOperationsController : ControllerBase
{
    private readonly IFinanceOperationService _financeOperationsService;


    public FinanceOperationsController(IFinanceOperationService incomeExpensesRepository)
    {
        _financeOperationsService = incomeExpensesRepository ?? throw new ArgumentNullException(nameof(incomeExpensesRepository));
    }


    [HttpGet("GetAllFinanceOperations")]
    public Task<IEnumerable<FinanceOperation>> GetAllFinanceOperations()
    {
        return _financeOperationsService.GetFinanceOperations();
    }


    [HttpGet("GetFinanceOperation")]
    public async Task<ActionResult<FinanceOperation>> GetFinanceOperation(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
        return await _financeOperationsService.GetFinanceOperation(id);
    }


    [HttpPost("CreateFinanceOperation")]
    public async Task<ActionResult<FinanceOperationAddResponse>> CreateFinanceOperation(FinanceOperationAddRequest type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        return await _financeOperationsService.CreateFinanceOperation(type);
    }


    [HttpPost("RemoveFinanceOperation")]
    public void RemoveFinanceOperation(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
        _financeOperationsService.RemoveFinanceOperation(id);
    }
    
    
    [HttpPut("UpdateFinanceOperation")]
    public async Task<ActionResult<FinanceOperationUpdateResponse>> UpdateFinanceOperation(FinanceOperationUpdateRequest type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        
        return await _financeOperationsService.UpdateFinanceOperation(type);
    }
}