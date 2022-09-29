using System.ComponentModel.DataAnnotations;

namespace RestApi.Resources;

public record CustomerResource(
    [MinLength(2)]
    [StringLength(200)]
    string FirstName,
    
    [MinLength(2)]
    [StringLength(200)]
    string LastName,
    
    bool IsDeleted
);