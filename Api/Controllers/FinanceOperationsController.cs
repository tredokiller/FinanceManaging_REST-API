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
public class FinanceOperationsController : ControllerBase
{
    private readonly IFinanceOperationService _financeOperationsService;
    
    public FinanceOperationsController(IFinanceOperationService financeOperationsService)
    {
        _financeOperationsService = financeOperationsService ?? throw new ArgumentNullException(nameof(financeOperationsService));
    }

    /// <response code="200">Successful Response</response>
    /// <summary>
    /// Get All Finance Operations
    /// </summary>
    /// <remarks>
    /// Multiple get all finance operations
    /// </remarks>
    [HttpGet("GetAllFinanceOperations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public Task<IEnumerable<FinanceOperation>> GetAllFinanceOperations()
    {
        return _financeOperationsService.GetFinanceOperations();
    }


    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Response By Id</response>
    /// <summary>
    /// Get one Finance Operation
    /// </summary>
    /// <remarks>
    /// Get one finance operation
    /// </remarks>
    [HttpGet("GetFinanceOperation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FinanceOperation>> GetFinanceOperation(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException(BadRequestException.WrongIdMessage);
        }
        
        var result= _financeOperationsService.GetFinanceOperation(id);

        return await result;
    }

    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Request</response>
    /// <summary>
    /// Create Finance Operation
    /// </summary>
    /// <remarks>
    /// Creation of finance operation
    /// 
    ///     POST /CreateFinanceOperation
    ///     {
    ///        "categoryId": "1",
    ///        "amount": "200",
    ///        "date": "2023-03-03T19:52:24.225Z"
    ///     }
    /// 
    /// </remarks>
    [HttpPost("CreateFinanceOperation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FinanceOperationAddResponse>> CreateFinanceOperation(FinanceOperationAddRequest type)
    {
        if (type == null)
        {
            throw new BadRequestException();
        }
        return await _financeOperationsService.CreateFinanceOperation(type);
    }

    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Response By Id</response>
    /// <summary>
    /// Remove Finance Operation
    /// </summary>
    /// <remarks>
    /// Removing finance operation
    /// 
    ///     POST /RemoveFinanceOperation
    ///      {
    ///         "id": "1"
    ///      }
    /// 
    /// </remarks>
    [HttpDelete("RemoveFinanceOperation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public void RemoveFinanceOperation(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException(BadRequestException.WrongIdMessage);
        }
        
        _financeOperationsService.RemoveFinanceOperation(id);
    }
    
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Request</response>
    /// <summary>
    /// Update Finance Operation
    /// </summary>
    /// <remarks>
    /// Updating finance operation
    /// 
    ///     Put /UpdateFinanceOperation
    ///     {
    ///        "id": 1,
    ///        "category": 2,
    ///        "amount": 200,
    ///        "date": "2023-03-03T20:01:18.746Z"
    ///     }
    /// 
    /// </remarks>
    [HttpPut("UpdateFinanceOperation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FinanceOperationUpdateResponse>> UpdateFinanceOperation(FinanceOperationUpdateRequest type)
    {
        if (type == null)
        {
            throw new BadRequestException();
        }
        
        return await _financeOperationsService.UpdateFinanceOperation(type);
    }
}