using Domain.Entities;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;


namespace Infrastructure.Services;

public interface IIncomeExpenseService
{
    Task<IEnumerable<IncomeExpenseCategory>> GetIncomeExpenseTypes();

    Task<IncomeExpenseCategory> GetIncomeExpenseType(int id);

    Task<IncomeExpensesAddResponse> CreateIncomeExpenseType(IncomeExpensesAddRequest type);

    Task RemoveIncomeExpenseType(int id);

    Task<IncomeExpensesUpdateResponse> UpdateIncomeExpenseType(IncomeExpensesUpdateRequest type);
}