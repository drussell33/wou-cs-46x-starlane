using System;
using System.Collections.Generic;

#nullable disable

namespace iCollections.Models
{
    public partial class Follow
    {
        public int Id { get; set; }
        public int? Follower { get; set; }
        public int? Followed { get; set; }
        public DateTime? Began { get; set; }

        public virtual IcollectionUser FollowedNavigation { get; set; }
        public virtual IcollectionUser FollowerNavigation { get; set; }
    }
}
