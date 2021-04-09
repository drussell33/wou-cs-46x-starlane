using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCollections.Models
{
    public class BrowseList
    {
        public IcollectionUser loggedInUser { get; set; }

        public List<Collection> searchResults { get; set; }

        public List<CollectionKeyword> suggestedKeywords { get; set; }

    }
}
