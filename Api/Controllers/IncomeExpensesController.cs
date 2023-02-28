using Domain.Entities;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class IncomeExpensesController : ControllerBase
{
    private readonly IIncomeExpenseService _incomeExpensesService;


    public IncomeExpensesController(IIncomeExpenseService incomeExpensesRepository)
    {
        _incomeExpensesService = incomeExpensesRepository ?? throw new ArgumentNullException(nameof(incomeExpensesRepository));
    }
    
    
    /// <response code="200">Successful Response</response>
    [HttpGet("GetAllIncomeExpenses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IEnumerable<IncomeExpenseCategory>> GetIncomeExpenses()
    {
        return _incomeExpensesService.GetIncomeExpenseTypes();
    }
    
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Response By Id</response>
    [HttpGet("GetIncomeExpense")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IncomeExpenseCategory>> GetIncomeExpense(int id)
    {
        if (id <= 0)
        {
            throw new BadHttpRequestException(nameof(id));
        }
        return await _incomeExpensesService.GetIncomeExpenseType(id);
    }
    
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Request</response>
    [HttpPost("CreateIncomeExpenseType")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IncomeExpensesAddResponse>> CreateIncomeExpenseType(IncomeExpensesAddRequest type)
    {
        if (type == null)
        {
            throw new BadHttpRequestException(nameof(type));
        }
        return await _incomeExpensesService.CreateIncomeExpenseType(type);
    }
    
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Response By Id</response>
    [HttpPost("RemoveIncomeExpenseType")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public void RemoveIncomeExpenseType(int id)
    {
        if (id <= 0)
        {
            throw new BadHttpRequestException(nameof(id));
        }
        _incomeExpensesService.RemoveIncomeExpenseType(id);
    }
    
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Request</response>
    [HttpPut("UpdateIncomeExpenseType")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IncomeExpensesUpdateResponse>> UpdateIncomeExpenseType(IncomeExpensesUpdateRequest type)
    {
        if (type == null)
        {
            throw new BadHttpRequestException(nameof(type));
        }
        return await _incomeExpensesService.UpdateIncomeExpenseType(type);
    }
}