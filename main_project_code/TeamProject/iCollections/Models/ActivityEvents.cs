using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace iCollections.Models
{   public class ActivityEvents
    {
        public List<Collection> recentCollections { get; set; }

        public List<FriendsWith> recentFriendships { get; set; }

        public List<Follow> recentFollows { get; set; }

    }
}