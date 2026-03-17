using Catalog.Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infra
{
    public class UnitOfWorkCatalog : IUnitOfWorkCatalog
    {
        private readonly CatalogDbContext _context;

        public UnitOfWorkCatalog(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
