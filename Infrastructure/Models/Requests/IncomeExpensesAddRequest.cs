using System.ComponentModel.DataAnnotations;
using Domain.Entities.Enums;

namespace Infrastructure.Models.Requests;

public class IncomeExpensesAddRequest
{
    [Required , MaxLength(20)]
    public string Name { set; get; }
    
    public FinanceActivityEnum FinanceActivityType { set; get; }
}