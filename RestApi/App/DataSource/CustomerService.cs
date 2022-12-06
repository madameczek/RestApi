using System.Data;
using RestApi.Resources;

namespace RestApi.App.DataSource;

public class CustomerService : ICustomerService
{
    public Task<int> Create(CustomerResource resource, CancellationToken cancellationToken)
    {
        return Task.FromResult(3);
    }

    public Task<CustomerResource> Get(int id, CancellationToken cancellationToken)
    {
        throw new RowNotInTableException();
    }

    public Task Update(string id, CustomerResource resource, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Remove(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}