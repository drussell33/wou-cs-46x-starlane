﻿using System;
using System.Collections.Generic;

#nullable disable

namespace iCollections.Models
{
    public partial class Collection
    {
        public Collection()
        {
            CollectionKeywords = new HashSet<CollectionKeyword>();
            CollectionPhotoes = new HashSet<CollectionPhoto>();
            //added in sprint 5 to incorporate favorite collections
            FavoriteCollections = new HashSet<FavoriteCollection>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Visibility { get; set; }
        public int? UserId { get; set; }
        
        public DateTime? DateMade { get; set; }
        //added in sprint 3
        public string Route { get; set; }

        public string Description { get; set; }

        public virtual IcollectionUser User { get; set; }
        public virtual ICollection<CollectionKeyword> CollectionKeywords { get; set; }
        public virtual ICollection<CollectionPhoto> CollectionPhotoes { get; set; }
        public virtual ICollection<FavoriteCollection> FavoriteCollections { get; set; }
    }
}
