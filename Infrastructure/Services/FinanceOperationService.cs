using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using Infrastructure.Models.Exceptions;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;

namespace Infrastructure.Services;

public class FinanceOperationService : IFinanceOperationService
{
    private readonly IRepository<FinanceOperation> _financeOperationsRepository;
    private readonly IRepository<IncomeExpenseCategory> _incomeExpensesRepository;
    private readonly IMapper _mapper;

    public FinanceOperationService(IRepository<FinanceOperation> financeOperationsRepository, IMapper mapper,
        IRepository<IncomeExpenseCategory> incomeExpensesRepository)
    {
        _financeOperationsRepository = financeOperationsRepository ??
                                       throw new ArgumentNullException(nameof(financeOperationsRepository));
        
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        
        _incomeExpensesRepository = incomeExpensesRepository ??
                                    throw new ArgumentNullException(nameof(incomeExpensesRepository));
    }

    public async Task<IEnumerable<FinanceOperation>> GetFinanceOperations()
    {
        return await _financeOperationsRepository.GetAll();
    }

    public async Task<FinanceOperation> GetFinanceOperation(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException(BadRequestException.WrongIdMessage);
        }
        var result = await _financeOperationsRepository.Get(id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result;
    }

    public async Task<FinanceOperationAddResponse> CreateFinanceOperation(FinanceOperationAddRequest type)
    {
        if (type == null)
        {
            throw new BadRequestException();
        }
        FinanceOperation entityType = _mapper.Map<FinanceOperationAddRequest, FinanceOperation>(type);

        entityType.CategoryType = _incomeExpensesRepository.Get(entityType.CategoryId).Result;
        
        entityType.Type = entityType.CategoryType.FinanceActivityType.ToString();
        entityType.Category = entityType.CategoryType.Name;
            
        var response =  _mapper.Map<FinanceOperation, FinanceOperationAddResponse>(await _financeOperationsRepository.Create(entityType));

        return response;
    }

    public void RemoveFinanceOperation(int id)
    {
        if (id <= 0)
        {
            throw new BadRequestException(BadRequestException.WrongIdMessage);
        }

        var type = _financeOperationsRepository.Get(id);

        if (type.Result == null)
        {
            throw new NotFoundException();
        }

        _financeOperationsRepository.Remove(type.Result);
    }

    public async Task<FinanceOperationUpdateResponse> UpdateFinanceOperation(FinanceOperationUpdateRequest type)
    {
        if (type == null)
        {
            throw new BadRequestException();
        }
        
        FinanceOperation entityType = _mapper.Map<FinanceOperationUpdateRequest, FinanceOperation>(type);

        FinanceOperationUpdateResponse response =
            _mapper.Map<FinanceOperation, FinanceOperationUpdateResponse>(
                await _financeOperationsRepository.Update(entityType));

        return response;
    }
    
    
}