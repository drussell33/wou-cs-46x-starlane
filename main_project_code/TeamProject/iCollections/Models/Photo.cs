using System;
using System.Collections.Generic;

#nullable disable

namespace iCollections.Models
{
    public partial class Photo
    {
        public Photo()
        {
            CollectionPhotos = new HashSet<CollectionPhoto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public int? UserId { get; set; }
        public DateTime DateUploaded { get; set; }

        public virtual IcollectionUser User { get; set; }
        public virtual ICollection<CollectionPhoto> CollectionPhotos { get; set; }
    }
}
