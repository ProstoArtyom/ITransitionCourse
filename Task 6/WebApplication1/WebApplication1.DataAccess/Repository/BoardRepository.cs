using WebApplication1.DataAccess.Data;
using WebApplication1.DataAccess.Repository.IRepository;
using WebApplication1.Models;

namespace WebApplication1.DataAccess.Repository;

public class BoardRepository : Repository<Board>, IBoardRepository
{
    private readonly ApplicationDbContext _db;
    public BoardRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Board obj)
    {
        _db.Update(obj);
    }
}
