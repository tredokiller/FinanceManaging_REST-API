namespace Infrastructure.Models.Responses;

public class FinanceOperationUpdateResponse
{
    public int Id { set; get; }
    public string Type { set; get; }
    public int CategoryId { set; get; }
    public float Amount { set; get; }
    
    public DateTime Date { set; get; }
}