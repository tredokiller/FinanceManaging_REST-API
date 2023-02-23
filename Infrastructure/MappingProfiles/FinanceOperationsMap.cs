using AutoMapper;
using Domain.Entities;
using Infrastructure.Services.Requests;
using Infrastructure.Services.Responses;

namespace Infrastructure.MappingProfiles;

public class FinanceOperationsMap : Profile
{
    public FinanceOperationsMap()
    {
        CreateMap<FinanceOperation, FinanceOperationAddResponse>();
        CreateMap<IncomeExpenseCategory, FinanceOperationUpdateResponse>();
        CreateMap<FinanceOperationAddRequest, FinanceOperation>();
        CreateMap<FinanceOperationUpdateRequest, FinanceOperation>();
    } 
}