using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace iCollections.Models
{
    public partial class FavoriteCollection
    {
        public FavoriteCollection()
        {
            Collections = new HashSet<Collection>();
        }

        public int Id { get; set; }
        public DateTime? DateMade { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public int? UserId { get; set; }
        public int Visibility { get; set; }

        public virtual IcollectionUser User { get; set; }
        public virtual ICollection<Collection> Collections { get; set; }
    }
}
