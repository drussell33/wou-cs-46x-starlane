using iCollections.Models;
using iCollections.Data.Abstract;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;


namespace iCollections.Data.Concrete
{
    public class KeywordRepository : Repository<Keyword>, IKeywordRepository
    {
        public KeywordRepository(ICollectionsDbContext ctx) : base(ctx)
        {

        }
 

    }
}