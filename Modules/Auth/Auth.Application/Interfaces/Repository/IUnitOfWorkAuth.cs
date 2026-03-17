using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Interfaces.Repository
{
    public interface IUnitOfWorkAuth
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
