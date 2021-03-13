using System;
using System.Collections.Generic;

#nullable disable

namespace iCollections.Models
{
    public partial class FriendsWith
    {
        public int Id { get; set; }
        public int? User1Id { get; set; }
        public int? User2Id { get; set; }
        public DateTime? Began { get; set; }

        public virtual IcollectionUser User1 { get; set; }
        public virtual IcollectionUser User2 { get; set; }

        public bool isKeyInFriendship(int key)
        {
            return key == User1.Id || key == User2.Id;
        }
    }
}
