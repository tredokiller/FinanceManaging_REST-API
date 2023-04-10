using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models.Requests;

public class FinanceOperationUpdateRequest
{
    public int Id { set; get; }
    
    [Required]
    public int CategoryId { set; get; }
    
    [Required , Range(1f, float.MaxValue, ErrorMessage = "Value must be greater than zero.")]
    public float Amount { set; get; }
    
    public DateTime Date { set; get; }
}