using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace iCollections.Models
{
    /*public class IcollectionUserMetaDta
    {
        [Remote(action: "IsUserNameAvailable", controller: "ICollectionsUsers", ErrorMessage = "Username is not available")]
        [Required(ErrorMessage = "Please Enter UserName")]
        public string UserName { get; set; }
    }
    [MetadataType(typeof(IcollectionUserMetaDta))]
    public partial class IcollectionUser
    {

    }*/
    public partial class IcollectionUser
    {
        public IcollectionUser()
        {
            Collections = new HashSet<Collection>();
            FavoriteCollections = new HashSet<FavoriteCollection>();
            FollowFollowedNavigations = new HashSet<Follow>();
            FollowFollowerNavigations = new HashSet<Follow>();
            FriendsWithUser1 = new HashSet<FriendsWith>();
            FriendsWithUser2 = new HashSet<FriendsWith>();
            Photos = new HashSet<Photo>();
        }

        public int Id { get; set; }
        public string AspnetIdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Remote(action: "IsUserNameAvailable", controller: "ICollectionsUsers", ErrorMessage = "Username is not available")]
        [Required(ErrorMessage = "Please Enter UserName")]
        [MinLength(2)]
        public string UserName { get; set; }
        public DateTime? DateJoined { get; set; }
        public string AboutMe { get; set; }
        public int? ProfilePicId { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<FavoriteCollection> FavoriteCollections { get; set; }
        /// <summary>
        /// Represents Users followed to this User.
        /// </summary>
        public virtual ICollection<Follow> FollowFollowedNavigations { get; set; }
        /// <summary>
        /// Represents Users this User follows.
        /// </summary>
        public virtual ICollection<Follow> FollowFollowerNavigations { get; set; }
        public virtual ICollection<FriendsWith> FriendsWithUser1 { get; set; }
        public virtual ICollection<FriendsWith> FriendsWithUser2 { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

        public IcollectionUser SelectOtherUser(IcollectionUser user2, int myId)
        {
            if (this.Id == myId) return user2;
            return this;
        }
    }
}
