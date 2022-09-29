using RestApi.Resources;

namespace RestApi.App.DataSource;

public class CustomerService : ICustomerService
{
    public Task<int> Create(CustomerResource resource, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerResource> Get(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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