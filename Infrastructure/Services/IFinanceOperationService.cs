using Domain.Entities;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;

namespace Infrastructure.Services;

public interface IFinanceOperationService
{
    Task<IEnumerable<FinanceOperation>> GetFinanceOperations();

    Task<FinanceOperation> GetFinanceOperation(int id);

    Task<FinanceOperationAddResponse> CreateFinanceOperation(FinanceOperationAddRequest type);

    void RemoveFinanceOperation(int id);

    Task<FinanceOperationUpdateResponse> UpdateFinanceOperation(FinanceOperationUpdateRequest type);
}