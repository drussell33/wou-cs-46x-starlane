using System;
using System.Collections.Generic;

#nullable disable

namespace iCollections.Models
{
    public partial class IcollectionUser
    {
        public IcollectionUser()
        {
            Collections = new HashSet<Collection>();
            FollowFollowedNavigations = new HashSet<Follow>();
            FollowFollowerNavigations = new HashSet<Follow>();
            FriendsWithUser1s = new HashSet<FriendsWith>();
            FriendsWithUser2s = new HashSet<FriendsWith>();
            Photos = new HashSet<Photo>();
        }

        public int Id { get; set; }
        public string AspnetIdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime DateJoined { get; set; }
        public string AboutMe { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Follow> FollowFollowedNavigations { get; set; }
        public virtual ICollection<Follow> FollowFollowerNavigations { get; set; }
        public virtual ICollection<FriendsWith> FriendsWithUser1s { get; set; }
        public virtual ICollection<FriendsWith> FriendsWithUser2s { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
