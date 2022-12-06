using System.ComponentModel.DataAnnotations;

namespace RestApi.Resources;

public class CustomerResource
{
    [MinLength(2)]
    [StringLength(200)]
    public string FirstName { get; set; }
    
    [MinLength(2)]
    [StringLength(200)]
    public string LastName { get; set; }
    
    public bool IsDeleted { get; set; }
}