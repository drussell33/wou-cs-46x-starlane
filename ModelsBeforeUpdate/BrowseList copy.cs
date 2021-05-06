using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCollections.Models
{
    public class BrowseList
    {
        public IcollectionUser LoggedInUser { get; set; }

        public IcollectionUser VisitedUser { get; set; }

        public List<CollectionKeyword> SearchResults { get; set; }

        public IEnumerable<Keyword> SuggestedKeywords { get; set; }

    }
}
