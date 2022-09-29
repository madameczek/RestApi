namespace RestApi.Domain;

public class Customer : BaseEntity<int>
{
    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public bool IsDeleted { get; set; }
}