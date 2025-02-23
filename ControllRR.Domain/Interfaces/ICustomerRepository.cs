using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;


public interface ICustomerRepository
{
    Task<List<Customer>> FindAllAsync();
}