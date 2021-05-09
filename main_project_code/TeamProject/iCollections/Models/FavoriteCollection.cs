using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace iCollections.Models
{
    public partial class FavoriteCollection
    {
        public int Id { get; set; }
        public int? CollectionId { get; set; }
        public int? FavoriteId { get; set; }
        public DateTime? DateAdded { get; set; }

        public virtual Collection Collection { get; set; }
        public virtual Favorite Favorite { get; set; }
    }
}
