using ControllRR.Infrastructure.Data.Context;

namespace ControllRR.Infrastructure.Repositories;


public class GenericRepository<T> : BaseRepository<T> where T : class
{
    public GenericRepository(ControllRRContext context) : base(context) { }
}