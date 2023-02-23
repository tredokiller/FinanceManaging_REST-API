using AutoMapper;
using Domain.Entities;
using Infrastructure.Services.Requests;
using Infrastructure.Services.Responses;

namespace Infrastructure.MappingProfiles;

public class IncomeExpensesMap : Profile
{
    public IncomeExpensesMap()
    {
        CreateMap<IncomeExpenseCategory, IncomeExpensesAddResponse>();
        CreateMap<IncomeExpenseCategory, IncomeExpensesUpdateResponse>();
        CreateMap<IncomeExpensesAddRequest, IncomeExpenseCategory>();
        CreateMap<IncomeExpensesUpdateRequest, IncomeExpenseCategory>();
    }
}