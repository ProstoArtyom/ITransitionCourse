namespace WebApplication1.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    IApplicationUserRepository ApplicationUser { get; }
    void Save();
}
