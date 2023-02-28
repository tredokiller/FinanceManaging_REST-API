namespace Infrastructure.Models.Requests;

public class FinanceOperationAddRequest
{
    public int CategoryId { set; get; }
    public float Amount { set; get; }
    
    public DateTime Date { set; get; }
}