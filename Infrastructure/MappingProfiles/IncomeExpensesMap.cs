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
        CreateMap<IncomeExpensesAddRequest, IncomeExpenseCategory>();
        CreateMap<IncomeExpensesUpdateRequest, IncomeExpenseCategory>();
        CreateMap<IncomeExpenseCategory, IncomeExpensesUpdateResponse>(); 
    }
}