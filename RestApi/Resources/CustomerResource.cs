using System.ComponentModel.DataAnnotations;

namespace RestApi.Resources;

public class CustomerResource
{
    [MinLength(2)]
    [StringLength(200)] 
    public string FirstName { get; set; } = null!;
    
    [MinLength(2)]
    [StringLength(200)]
    public string LastName { get; set; } = null!;
    
    public bool IsDeleted { get; set; }
}