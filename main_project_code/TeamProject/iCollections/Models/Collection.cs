using System;
using System.Collections.Generic;

#nullable disable

namespace iCollections.Models
{
    public partial class Collection
    {
        public Collection()
        {
            CollectionKeywords = new HashSet<CollectionKeyword>();
            CollectionPhotos = new HashSet<CollectionPhoto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Visibility { get; set; }
        public int? UserId { get; set; }
        public byte[] DateMade { get; set; }

        public virtual IcollectionUser User { get; set; }
        public virtual ICollection<CollectionKeyword> CollectionKeywords { get; set; }
        public virtual ICollection<CollectionPhoto> CollectionPhotos { get; set; }
    }
}
