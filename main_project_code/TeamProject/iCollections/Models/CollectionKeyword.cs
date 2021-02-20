using System;
using System.Collections.Generic;

#nullable disable

namespace iCollections.Models
{
    public partial class CollectionKeyword
    {
        public int Id { get; set; }
        public int? CollectId { get; set; }
        public int? KeywordId { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual Collection Collect { get; set; }
        public virtual Keyword Keyword { get; set; }
    }
}
