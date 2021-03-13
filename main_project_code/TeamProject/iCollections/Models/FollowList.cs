using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCollections.Models
{
    public class FollowList
    {
        public string TargetUsername { get; set; }

        public string TargetFirstname { get; set; }

        public string TargetLastname { get; set; }
        public IEnumerable<Follow> Follows { get; set; }
    }
}
