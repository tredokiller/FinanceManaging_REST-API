using System.ComponentModel.DataAnnotations;
using Domain.Entities.Enums;

namespace Infrastructure.Models.Requests;

public class IncomeExpensesUpdateRequest
{
    public int Id { set; get; }
    
    [Required , MaxLength(20)]
    public string Name { set; get; }
    
    public FinanceActivityEnum FinanceActivityType { set; get; }
}