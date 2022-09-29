using RestApi.Resources;

namespace RestApi.App.DataSource;

public interface ICustomerService
{
    Task<int> Create(CustomerResource customer, CancellationToken cancellationToken);
    Task<CustomerResource> Get(int id, CancellationToken cancellationToken);
    Task Update(string id, CustomerResource customer, CancellationToken cancellationToken);
    Task Remove(string id, CancellationToken cancellationToken);
}