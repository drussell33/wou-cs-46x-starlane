using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCollections.Models
{
    public class FollowList
    {
        public IcollectionUser TargetUser { get; set; }
        public IEnumerable<Follow> Follows { get; set; }
    }
}
