namespace Infrastructure.Services.Responses;

public class FinanceOperationUpdateResponse
{
    public int Id { set; get; }
    public string Type { set; get; }
    public int Category { set; get; }
    public float Amount { set; get; }
    
    public DateTime Data { set; get; }
}