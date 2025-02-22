using ControllRR.Domain.Entities;

namespace ControllRR.Domain.Interfaces;

public interface IApplicationUserRepository
{
  Task<List<ApplicationUser>> FindAllAsync();
  Task<ApplicationUser> FindByIdAsync(int id);
  Task InsertAsync(ApplicationUser applicationUser);
  //  Task SaveChangesAsync();
  Task RemoveAsync(int id);
}