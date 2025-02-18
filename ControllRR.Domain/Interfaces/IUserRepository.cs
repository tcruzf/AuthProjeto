using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IUserRepository
{
      Task<List<ApplicationUser>> FindAllAsync();
      Task<ApplicationUser> FindByIdAsync(int id);
      Task InsertAsync(ApplicationUser user);

    //  Task SaveChangesAsync();
      Task RemoveAsync(int id);
      Task UpdateAsync(ApplicationUser user);
}