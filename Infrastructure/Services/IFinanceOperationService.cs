using Domain.Entities;
using Infrastructure.Services.Requests;
using Infrastructure.Services.Responses;

namespace Infrastructure.Services;

public interface IFinanceOperationService
{
    Task<IEnumerable<FinanceOperation>> GetFinanceOperations();

    Task<FinanceOperation> GetFinanceOperation(int id);

    Task<FinanceOperationAddResponse> CreateFinanceOperation(FinanceOperationAddRequest type);

    void RemoveFinanceOperation(int id);

    Task<FinanceOperationUpdateResponse> UpdateFinanceOperation(FinanceOperationUpdateRequest type);
}