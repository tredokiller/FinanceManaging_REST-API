using AutoMapper;
using Domain.Entities;
using Infrastructure.Services;
using Infrastructure.Services.Requests;
using Infrastructure.Services.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomeExpensesController : ControllerBase
{
    private readonly IIncomeExpenseService _incomeExpensesService;


    public IncomeExpensesController(IIncomeExpenseService incomeExpensesRepository)
    {
        _incomeExpensesService = incomeExpensesRepository ?? throw new ArgumentNullException(nameof(incomeExpensesRepository));
    }


    [HttpGet("GetAllIncomeExpenses")]
    public Task<IEnumerable<IncomeExpenseCategory>> GetIncomeExpenses()
    {
        return _incomeExpensesService.GetIncomeExpenseTypes();
    }


    [HttpGet("GetIncomeExpense")]
    public async Task<ActionResult<IncomeExpenseCategory>> GetIncomeExpense(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
        return await _incomeExpensesService.GetIncomeExpenseType(id);
    }


    [HttpPost("CreateIncomeExpenseType")]
    public async Task<ActionResult<IncomeExpensesAddResponse>> CreateIncomeExpenseType(IncomeExpensesAddRequest type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        return await _incomeExpensesService.CreateIncomeExpenseType(type);
    }


    [HttpPost("RemoveIncomeExpenseType")]
    public void RemoveIncomeExpenseType(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }
        _incomeExpensesService.RemoveIncomeExpenseType(id);
    }
    
    
    [HttpPut("UpdateIncomeExpenseType")]
    public async Task<ActionResult<IncomeExpensesUpdateResponse>> UpdateIncomeExpenseType(IncomeExpensesUpdateRequest type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        return await _incomeExpensesService.UpdateIncomeExpenseType(type);
    }
}