using Auth.Application.Interfaces.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Infra.Repositories
{
    public class UnitOfWorkAuth : IUnitOfWorkAuth
    {
        private readonly AuthDbContext _context;

        public UnitOfWorkAuth(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
