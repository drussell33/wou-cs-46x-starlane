using System;
using System.Collections.Generic;

#nullable disable

namespace iCollections.Models
{
    public partial class CollectionPhoto
    {
        public int Id { get; set; }
        public int? CollectId { get; set; }
        public int? PhotoId { get; set; }
        public int PhotoRank { get; set; }
        public DateTime? DateAdded { get; set; }
        //added in sprint 3
        public string Title { get; set; }
        //added in sprint 3
        public string Description { get; set; }

        public virtual Collection Collect { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
