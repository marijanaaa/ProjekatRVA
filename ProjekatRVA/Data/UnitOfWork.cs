using Microsoft.Extensions.Logging;
using ProjekatRVA.Core.IConfiguration;
using ProjekatRVA.Core.IRepositories;
using ProjekatRVA.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace ProjekatRVA.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlannerDbContext _context;
        public IUserRepository Users { get; private set; }
        public IPlannerRepository Planners { get; private set; }
        public IEventRepository Events { get; private set; }
        public ILoggedUsersRepository LoggedUsers { get; private set; }
    

        public UnitOfWork(PlannerDbContext context, IUserRepository users, IPlannerRepository planners, IEventRepository events, ILoggedUsersRepository loggedUsers)
        {
            _context = context;
            Users = users;
            Planners = planners;
            Events = events;
            LoggedUsers = loggedUsers;
        }

        public async Task CompleteAsync() { 
            await _context.SaveChangesAsync();
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}
