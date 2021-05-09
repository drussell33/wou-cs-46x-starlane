using System;
using System.Collections.Generic;

#nullable disable

namespace iCollections.Models
{
    public partial class Favorite
    {
        public Favorite()
        {
            FavoriteCollections = new HashSet<FavoriteCollection>();
        }

        public int Id { get; set; }
        public DateTime? DateMade { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public int? UserId { get; set; }
        public int Visibility { get; set; }

        public virtual IcollectionUser User { get; set; }
        public virtual ICollection<FavoriteCollection> FavoriteCollections { get; set; }
    }
}
