using Ecommerce.Core.Entity.Files;
using Ecommerce.Core.Entity.Others;

namespace Ecommerce.RepositoryLayer.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<Paths> PathsRepository { get; }
    public IBaseRepository<Images> ImagesRepository { get; }
    public IBaseRepository<City> CityRepository { get; }

    //-----------------------------------------------------------------------------------
    int SaveChanges();

    Task<int> SaveChangesAsync();
}