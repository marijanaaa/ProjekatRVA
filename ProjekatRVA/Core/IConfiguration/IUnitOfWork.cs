using ProjekatRVA.Core.IRepositories;
using System;
using System.Threading.Tasks;

namespace ProjekatRVA.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IPlannerRepository Planners { get; }
        IEventRepository Events { get; }
        ILoggedUsersRepository LoggedUsers { get; }
        Task CompleteAsync();
        void Complete();
    }
}
