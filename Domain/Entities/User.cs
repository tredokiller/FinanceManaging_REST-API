namespace Domain.Entities;

public class User : BaseEntity
{
    public string Name { set; get; }
    public string Password { set; get; }
}