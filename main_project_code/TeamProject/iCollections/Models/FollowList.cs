using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCollections.Models
{
    public class FollowList
    {
        public string Target { get; set; }
        public IEnumerable<Follow> Follows { get; set; }
    }
}
