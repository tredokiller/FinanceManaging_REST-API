using AutoMapper;
using Domain.Entities;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;

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