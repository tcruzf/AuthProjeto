using ControllRR.Domain.Entities;
using ControllRR.Domain.Interfaces;
using ControllRR.Infrastructure.Data.Context;

namespace ControllRR.Infrastructure.Repositories;


public class CustomerRepository : BaseRepository<CustomerRepository>, ICustomerRepository
{
    public CustomerRepository(ControllRRContext context) : base(context)
    {
        
    }

    public Task<List<Customer>> FindAllAsync()
    {
        throw new NotImplementedException();
    }
}