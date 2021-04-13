using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iCollections.Models;

namespace iCollections.Data
{
    public interface IDbContext
    {
        DbSet<Follow> Follows { get; }
        DbSet<IcollectionUser> IcollectionUsers { get; }
        int SaveChanges();
    }
}
