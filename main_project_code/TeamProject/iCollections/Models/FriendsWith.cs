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
    }
}
