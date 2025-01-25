using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IUserRepository
{
     Task<List<User>> FindAllAsync();
    Task<User> FindByIdAsync(int id);
    Task InsertAsync(User user);

     Task SaveChangesAsync();
      Task RemoveAsync(int id);
}