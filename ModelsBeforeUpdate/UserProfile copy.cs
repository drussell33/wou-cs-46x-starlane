using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCollections.Models
{
    public class UserProfile
    {
        public IcollectionUser ProfileVisitor { get; set; }

        public IcollectionUser ProfileOwner { get; set; }

        public List<Collection> recentCollections { get; set; }

    }
}
