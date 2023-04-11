using Domain.Entities;
using Infrastructure.Models.Exceptions;
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
    /// <summary>
    /// Get All IncomeExpense types
    /// </summary>
    /// <remarks>
    /// Get all IncomeExpense types
    /// </remarks>
    [HttpGet("GetAllIncomeExpenses")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IEnumerable<IncomeExpenseCategory>> GetIncomeExpenses()
    {
        return _incomeExpensesService.GetIncomeExpenseTypes();
    }
    
    
    /// <summary>
    /// Get IncomeExpense type
    /// </summary>
    /// <remarks>
    /// Get one IncomeExpense type
    /// </remarks>
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
            throw new BadRequestException(BadRequestException.WrongIdMessage);
        }
        return await _incomeExpensesService.GetIncomeExpenseType(id);
    }
    
    /// <summary>
    /// Create IncomeExpenses Type
    /// </summary>
    /// <remarks>
    /// Creating incomeExpenses type
    /// 
    ///     Post /CreateIncomeExpenseType
    ///     {
    ///        "name": "Car",
    ///        "financeActivityType" : 1
    ///     }
    /// 
    /// </remarks>
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
            throw new BadRequestException();
        }
        return await _incomeExpensesService.CreateIncomeExpenseType(type);
    }
    
    
    /// <summary>
    /// Remove IncomeExpenses Type
    /// </summary>
    /// <remarks>
    /// Removing incomeExpenses type
    /// 
    ///     Post /RemoveIncomeExpenseType
    ///     {
    ///        "id": 1
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Response By Id</response>
    [HttpDelete("RemoveIncomeExpenseType")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task RemoveIncomeExpenseType(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException(BadRequestException.WrongIdMessage);
        }
        await _incomeExpensesService.RemoveIncomeExpenseType(id);
    }
    
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Request</response>
    /// <summary>
    /// Update IncomeExpenses Type
    /// </summary>
    /// <remarks>
    /// Updating incomeExpenses type
    /// 
    ///     Put /UpdateIncomeExpenseType
    ///     {
    ///        "id": 1,
    ///        "name": "Deposit",
    ///        "financeActivityType": 1,
    ///     }
    /// 
    /// </remarks>
    [HttpPut("UpdateIncomeExpenseType")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IncomeExpensesUpdateResponse>> UpdateIncomeExpenseType(IncomeExpensesUpdateRequest type)
    {
        if (type == null)
        {
            throw new BadRequestException();
        }
        return await _incomeExpensesService.UpdateIncomeExpenseType(type);
    }
}