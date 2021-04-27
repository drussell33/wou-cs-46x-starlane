using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iCollections.Models;

namespace iCollections.Data
{
    public class TestContext : IDbContext
    {
        public TestContext()
        {
            this.Follows = new TestDbSet<Follow>();
            this.IcollectionUsers = new TestDbSet<IcollectionUser>();
        }
        public DbSet<Follow> Follows {get; set;}
        public DbSet<IcollectionUser> IcollectionUsers { get; set; }
        public int SaveChanges()
        {
            return 0;
        }
        public async Task<int> SaveChangesAsync()
        {
            return 0;
        }

        public class TestDbSet<T> : DbSet<T>
            where T : class
        {
            
        }
    }
}

