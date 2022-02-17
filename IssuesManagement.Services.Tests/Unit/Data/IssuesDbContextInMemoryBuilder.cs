using IssuesManagement.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IssuesManagement.Services.Tests.Unit
{
    public static class IssuesDbContextInMemoryBuilder
    {
        /* This method allows to create a InMemoryDatabase for IssuesDbContext. 
         * It will returns a new db context for a new database each time */
        public static IssuesDbContext BuildInMemoryDbContext()
        {
            var services = new ServiceCollection();

            services.AddEntityFrameworkInMemoryDatabase();

            var _contextOptions = new DbContextOptionsBuilder<IssuesDbContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
              .Options;

            var db = new IssuesDbContext(_contextOptions);

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
    }
}
