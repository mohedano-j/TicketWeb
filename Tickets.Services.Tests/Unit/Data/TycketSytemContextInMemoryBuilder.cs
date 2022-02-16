﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Services.Data;

namespace Tickets.Services.Tests.Unit
{
    public static class TycketSytemContextInMemoryBuilder
    {
        public static TicketSystemContext BuildInMemoryDbContext()
        {
            var services = new ServiceCollection();

            services.AddEntityFrameworkInMemoryDatabase();

            var _contextOptions = new DbContextOptionsBuilder<TicketSystemContext>()
              .UseInMemoryDatabase("TicketServiceTests")
              .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
              .Options;
            var db = new TicketSystemContext(_contextOptions);

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }
    }
}