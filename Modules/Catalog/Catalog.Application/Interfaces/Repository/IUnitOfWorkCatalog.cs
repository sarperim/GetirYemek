using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Interfaces.Repository
{
    public interface IUnitOfWorkCatalog
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
