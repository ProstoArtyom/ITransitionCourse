using WebApplication1.Models;

namespace WebApplication1.DataAccess.Repository.IRepository;

public interface IBoardRepository : IRepository<Board>
{
    void Update(Board obj);
}
