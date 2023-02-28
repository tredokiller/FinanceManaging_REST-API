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
public class FinanceOperationsController : ControllerBase
{
    private readonly IFinanceOperationService _financeOperationsService;


    public FinanceOperationsController(IFinanceOperationService incomeExpensesRepository)
    {
        _financeOperationsService = incomeExpensesRepository ?? throw new ArgumentNullException(nameof(incomeExpensesRepository));
    }

    /// <response code="200">Successful Response</response>
    [HttpGet("GetAllFinanceOperations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IEnumerable<FinanceOperation>> GetAllFinanceOperations()
    {
        return _financeOperationsService.GetFinanceOperations();
    }


    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Response By Id</response>
    [HttpGet("GetFinanceOperation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FinanceOperation>> GetFinanceOperation(int id)
    {
        if (id <= 0)
        {
            throw new BadHttpRequestException(nameof(id));
        }
        return await _financeOperationsService.GetFinanceOperation(id);
    }

    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Request</response>
    [HttpPost("CreateFinanceOperation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FinanceOperationAddResponse>> CreateFinanceOperation(FinanceOperationAddRequest type)
    {
        if (type == null)
        {
            throw new BadHttpRequestException(nameof(type));
        }
        return await _financeOperationsService.CreateFinanceOperation(type);
    }

    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Response By Id</response>
    [HttpPost("RemoveFinanceOperation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public void RemoveFinanceOperation(int id)
    {
        if (id <= 0)
        {
            throw new BadHttpRequestException(nameof(id));
        }
        _financeOperationsService.RemoveFinanceOperation(id);
    }
    
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Request</response>
    [HttpPut("UpdateFinanceOperation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FinanceOperationUpdateResponse>> UpdateFinanceOperation(FinanceOperationUpdateRequest type)
    {
        if (type == null)
        {
            throw new BadHttpRequestException(nameof(type));
        }
        
        return await _financeOperationsService.UpdateFinanceOperation(type);
    }
}