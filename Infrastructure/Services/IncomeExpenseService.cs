using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using Infrastructure.Services.Requests;
using Infrastructure.Services.Responses;

namespace Infrastructure.Services;

public class IncomeExpenseService : IIncomeExpenseService
{
    private readonly IRepository<IncomeExpenseCategory> _incomeExpensesRepository;
    private readonly IMapper _mapper;

    public IncomeExpenseService(IRepository<IncomeExpenseCategory> incomeExpenses, IMapper mapper)
    {
        _incomeExpensesRepository = incomeExpenses ?? throw new ArgumentNullException(nameof(incomeExpenses));
        _mapper = mapper ?? throw new ArgumentNullException();
    }

    public async Task<IEnumerable<IncomeExpenseCategory>> GetIncomeExpenseTypes()
    {
        return await _incomeExpensesRepository.GetAll();
    }

    public async Task<IncomeExpenseCategory> GetIncomeExpenseType(int id)
    {
        return await _incomeExpensesRepository.Get(id);
    }

    public async Task<IncomeExpensesAddResponse> CreateIncomeExpenseType(IncomeExpensesAddRequest request)
    {
        IncomeExpenseCategory entityCategory = _mapper.Map<IncomeExpensesAddRequest, IncomeExpenseCategory>(request);
        
        IncomeExpensesAddResponse response = _mapper.Map<IncomeExpenseCategory , IncomeExpensesAddResponse>(await _incomeExpensesRepository.Create(entityCategory));

        return response;
    }

    public void RemoveIncomeExpenseType(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        var type = _incomeExpensesRepository.Get(id);

        _incomeExpensesRepository.Remove(type.Result);
    }

    public async Task<IncomeExpensesUpdateResponse> UpdateIncomeExpenseType(IncomeExpensesUpdateRequest request)
    {
        IncomeExpenseCategory entityCategory = _mapper.Map<IncomeExpensesUpdateRequest, IncomeExpenseCategory>(request);
        
        IncomeExpensesUpdateResponse response = _mapper.Map<IncomeExpenseCategory , IncomeExpensesUpdateResponse>(await _incomeExpensesRepository.Update(entityCategory));

        return response;
    }
}