using System;
using System.Collections.Generic;

#nullable disable

namespace iCollections.Models
{
    public partial class Keyword
    {
        public Keyword()
        {
            CollectionKeywords = new HashSet<CollectionKeyword>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CollectionKeyword> CollectionKeywords { get; set; }
    }
}
