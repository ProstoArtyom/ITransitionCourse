namespace WebApplication1.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    IBoardRepository Board { get; }
    void Save();
}
