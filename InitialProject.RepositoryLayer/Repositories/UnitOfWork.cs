using Ecommerce.Core;
using Ecommerce.Core.Entity.ApplicationData;
using Ecommerce.Core.Entity.Files;
using Ecommerce.Core.Entity.Others;
using Ecommerce.RepositoryLayer.Interfaces;

namespace Ecommerce.RepositoryLayer.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IBaseRepository<ApplicationUser> Users { get; private set; }
    public IBaseRepository<ApplicationRole> Roles { get; private set; }

    public IBaseRepository<Paths> PathsRepository { get; set; }
    public IBaseRepository<City> CityRepository { get; set; }
    public IBaseRepository<Images> ImagesRepository { get; set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        PathsRepository = new BaseRepository<Paths>(context);
        ImagesRepository = new BaseRepository<Images>(context);
        CityRepository = new BaseRepository<City>(context);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}