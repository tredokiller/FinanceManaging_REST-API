using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using Infrastructure.Models.Exceptions;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;

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
        if (id <=0)
        {
            throw new BadRequestException(BadRequestException.WrongIdMessage);
        }
        var result = await _incomeExpensesRepository.Get(id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result;
    }

    public async Task<IncomeExpensesAddResponse> CreateIncomeExpenseType(IncomeExpensesAddRequest request)
    {
        if (request == null)
        {
            throw new BadRequestException();
        }
        IncomeExpenseCategory entityCategory = _mapper.Map<IncomeExpensesAddRequest, IncomeExpenseCategory>(request);
        
        IncomeExpensesAddResponse response = _mapper.Map<IncomeExpenseCategory , IncomeExpensesAddResponse>(await _incomeExpensesRepository.Create(entityCategory));

        return response;
    }

    public async Task RemoveIncomeExpenseType(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException(BadRequestException.WrongIdMessage);
        }

        var type = _incomeExpensesRepository.Get(id);

        if (type.Result == null)
        {
            throw new NotFoundException();
        }

        await _incomeExpensesRepository.Remove(type.Result);
    }

    public async Task<IncomeExpensesUpdateResponse> UpdateIncomeExpenseType(IncomeExpensesUpdateRequest request)
    {
        if (request == null)
        {
            throw new BadRequestException();
        }

        IncomeExpenseCategory entityCategory = _mapper.Map<IncomeExpensesUpdateRequest, IncomeExpenseCategory>(request);
        
        IncomeExpensesUpdateResponse response = _mapper.Map<IncomeExpenseCategory , IncomeExpensesUpdateResponse>(await _incomeExpensesRepository.Update(entityCategory));
        
        return response;
    }
}