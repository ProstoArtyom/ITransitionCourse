using WebApplication1.DataAccess.Data;
using WebApplication1.DataAccess.Repository.IRepository;

namespace WebApplication1.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public IBoardRepository Board { get; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Board = new BoardRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
