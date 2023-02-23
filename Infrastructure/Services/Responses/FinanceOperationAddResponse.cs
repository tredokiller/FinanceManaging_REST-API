namespace Infrastructure.Services.Responses;

public class FinanceOperationAddResponse
{
    public int Id { set; get; }
    public string Type { set; get; }
    public string Category { set; get; }
    public float Amount { set; get; }
    
    public DateTime Date { set; get; }
}