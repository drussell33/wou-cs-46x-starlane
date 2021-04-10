using System;
using System.Collections.Generic;
using System.IO;

#nullable disable

namespace iCollections.Models
{
    public partial class Photo
    {
        public Photo()
        {
            CollectionPhotoes = new HashSet<CollectionPhoto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateUploaded { get; set; }
        //added in sprint 3
        public Guid PhotoGuid { get; set; }
        //SQL version
        //[PhotoGUID] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID()

        public virtual IcollectionUser User { get; set; }
        public virtual ICollection<CollectionPhoto> CollectionPhotoes { get; set; }

        public string ToViewableFormat() {
            var extension = Path.GetExtension(Name).Replace(".", "");
            string imageBase64Data = Convert.ToBase64String(Data);
            //string imageDataURL = string.Format("data:image/{0};base64,{1}", extension, imageBase64Data);
            string imageDataURL = $"data:image/{extension};base64,{imageBase64Data}";
            return imageDataURL;
        }
    }
}
