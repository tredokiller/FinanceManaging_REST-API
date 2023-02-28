namespace Infrastructure.Models.Requests;

public class FinanceOperationUpdateRequest
{
    public int Category { set; get; }
    public float Amount { set; get; }
    
    public DateTime Data { set; get; }
}