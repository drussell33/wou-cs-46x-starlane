using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCollections.Data
{
    /// <summary>
    /// Helper class to hold all the information we need to construct the users for this app.  Basically
    /// a union of FujiUser and IdentityUser.  Not great to have to duplicate this data but it is only for one-time seeding
    /// of the databases.  Does NOT hold passwords since we need to store those in a secret location.
    /// </summary>
    public class UserPhotoData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateUploaded { get; set; }
    }

    public class SeedPhotos
    {
        /// <summary>
        /// Data to be used to seed the FujiUsers and ASPNetUsers tables
        /// </summary>
        public static readonly UserPhotoData[] UserSeedData = new UserPhotoData[]
        {
        //new byte[] imageArray = System.IO.File.ReadAllBytes("~/images/profile_pics/profile_pic_4.jpg");
        //byte[] image = System.IO.File.ReadAllBytes ( Server.MapPath ( "noimage.png" ) );
        //new UserPhotoData { Id = 1, Name = "profile_picture", Data = , UserId = 1, DateUploaded = '05/29/2015 5:50 AM' },

        };
    }
}
