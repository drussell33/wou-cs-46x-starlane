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
        public string Name { get; set; }
        public string ImageLocation { get; set; }
    }


    public class SeedPhotos
    {
        /// <summary>
        /// Data to be used to seed the FujiUsers and ASPNetUsers tables
        /// </summary>
        public static readonly UserPhotoData[] UserPhotoData = new UserPhotoData[]
        {
            new UserPhotoData { Name = "profile_pic_1", ImageLocation = "/images/profile_pics/profile_pic_1.jpg" },
            new UserPhotoData { Name = "profile_pic_2", ImageLocation = "/images/profile_pics/profile_pic_2.jpg" },
            new UserPhotoData { Name = "profile_pic_3", ImageLocation = "/images/profile_pics/profile_pic_3.jpg" },
            new UserPhotoData { Name = "profile_pic_4", ImageLocation = "/images/profile_pics/profile_pic_4.jpg" },
            new UserPhotoData { Name = "profile_pic_5", ImageLocation = "/images/profile_pics/profile_pic_5.jpg" },
            new UserPhotoData { Name = "profile_pic_6", ImageLocation = "/images/profile_pics/profile_pic_6.jpg" },
            new UserPhotoData { Name = "profile_pic_7", ImageLocation = "/images/profile_pics/profile_pic_7.jpg" },
            new UserPhotoData { Name = "profile_pic_8", ImageLocation = "/images/profile_pics/profile_pic_8.jpg" },
        };

        public static readonly string[][] UserPhotoDataOrganized = 
        {
            new string[]
            {
                "/images/profile_pics/profile_pic_1.jpg",
                "/images/profile_pics/profile_pic_2.jpg",
                "/images/profile_pics/profile_pic_3.jpg",
                "/images/profile_pics/profile_pic_4.jpg",
                "/images/profile_pics/profile_pic_5.jpg",
                "/images/profile_pics/profile_pic_6.jpg",
                "/images/profile_pics/profile_pic_7.jpg",
                "/images/profile_pics/profile_pic_8.jpg",

            },
            new string[]
            {
                "/images/card_pics/IMG-0782.JPG",
                "/images/card_pics/IMG-0783.JPG",
                "/images/card_pics/IMG-0784.JPG",
                "/images/card_pics/IMG-0785.JPG",
                "/images/card_pics/IMG-0786.JPG",
                "/images/card_pics/IMG-0787.JPG",
                "/images/card_pics/IMG-0788.JPG",
                "/images/card_pics/IMG-0789.JPG",
                "/images/card_pics/IMG-0790.JPG",
                "/images/card_pics/IMG-0791.JPG",
                "/images/card_pics/IMG-0792.JPG",
                "/images/card_pics/IMG-0793.JPG",
                "/images/card_pics/IMG-0797.JPG",
                "/images/card_pics/IMG-0798.JPG",
                "/images/card_pics/IMG-0799.JPG",
                "/images/card_pics/IMG-0800.JPG",
                "/images/card_pics/IMG-0801.JPG",
                "/images/card_pics/IMG-0802.JPG",
                "/images/card_pics/IMG-0803.JPG",
                "/images/card_pics/IMG-0804.JPG",
                "/images/card_pics/IMG-0805.JPG",
                "/images/card_pics/IMG-0806.JPG",
                "/images/card_pics/IMG-0807.JPG",
                "/images/card_pics/IMG-0808.JPG",
                "/images/card_pics/IMG-0809.JPG",
                "/images/card_pics/IMG-0810.JPG",
                "/images/card_pics/IMG-0811.JPG",
                "/images/card_pics/IMG-0812.JPG",
                "/images/card_pics/IMG-0813.JPG",
                "/images/card_pics/IMG-0814.JPG",
                "/images/card_pics/IMG-0815.JPG",
                "/images/card_pics/IMG-0816.JPG",
                "/images/card_pics/IMG-0817.JPG",
                "/images/card_pics/IMG-0818.JPG",
                "/images/card_pics/IMG-0819.JPG",
                "/images/card_pics/IMG-0820.JPG",
                "/images/card_pics/IMG-0821.JPG",
                "/images/card_pics/IMG-0822.JPG",
                "/images/card_pics/IMG-0823.JPG",
                "/images/card_pics/IMG-0824.JPG",
                "/images/card_pics/IMG-0825.JPG",
                "/images/card_pics/IMG-0826.JPG",
                "/images/card_pics/IMG-0827.JPG",
                "/images/card_pics/IMG-0828.JPG",
                "/images/card_pics/IMG-0829.JPG",
                "/images/card_pics/IMG-0830.JPG",
                "/images/card_pics/IMG-0831.JPG",
                "/images/card_pics/IMG-0832.JPG",
                "/images/card_pics/IMG-0833.JPG",
                "/images/card_pics/IMG-0834.JPG",
                "/images/card_pics/IMG-0835.JPG"
            },

            new string[]
            {
                "/images/dogtoy_pics/IMG-0869.JPG",
                "/images/dogtoy_pics/IMG-0870.JPG",
                "/images/dogtoy_pics/IMG-0871.JPG",
                "/images/dogtoy_pics/IMG-0872.JPG",
                "/images/dogtoy_pics/IMG-0873.JPG",
                "/images/dogtoy_pics/IMG-0874.JPG",
                "/images/dogtoy_pics/IMG-0875.JPG",
                "/images/dogtoy_pics/IMG-0876.JPG",
                "/images/dogtoy_pics/IMG-0877.JPG",
                "/images/dogtoy_pics/IMG-0878.JPG",
                "/images/dogtoy_pics/IMG-0879.JPG",
                "/images/dogtoy_pics/IMG-0880.JPG",
                "/images/dogtoy_pics/IMG-0881.JPG",
                "/images/dogtoy_pics/IMG-0882.JPG",
            },

            new string[]
            {
                "/images/fish_pics/bluegill.png",
                "/images/fish_pics/boot.png",
                "/images/fish_pics/marlin.png",
                "/images/fish_pics/fish2.png",
                "/images/fish_pics/fish3.png",
                "/images/fish_pics/fish4.png",
                "/images/fish_pics/salmon.png",
                "/images/fish_pics/rockcod.png",
            },

            new string[]
            {
                "/images/puzzel_pics/image_123923953(1).JPG",
                "/images/puzzel_pics/image_123923953(10).JPG",
                "/images/puzzel_pics/image_123923953(11).JPG",
                "/images/puzzel_pics/image_123923953(12).JPG",
                "/images/puzzel_pics/image_123923953(13).JPG",
                "/images/puzzel_pics/image_123923953(14).JPG",
                "/images/puzzel_pics/image_123923953(15).JPG",
                "/images/puzzel_pics/image_123923953(16).JPG",
                "/images/puzzel_pics/image_123923953(17).JPG",
                "/images/puzzel_pics/image_123923953(18).JPG",
                "/images/puzzel_pics/image_123923953(19).JPG",
                "/images/puzzel_pics/image_123923953(2).JPG",
                "/images/puzzel_pics/image_123923953(20).JPG",
                "/images/puzzel_pics/image_123923953(21).JPG",
                "/images/puzzel_pics/image_123923953(22).JPG",
                "/images/puzzel_pics/image_123923953(23).JPG",
                "/images/puzzel_pics/image_123923953(24).JPG",
                "/images/puzzel_pics/image_123923953(25).JPG",
                "/images/puzzel_pics/image_123923953(26).JPG",
                "/images/puzzel_pics/image_123923953(27).JPG",
                "/images/puzzel_pics/image_123923953(28).JPG",
                "/images/puzzel_pics/image_123923953(3).JPG",
                "/images/puzzel_pics/image_123923953(4).JPG",
                "/images/puzzel_pics/image_123923953(5).JPG",
                "/images/puzzel_pics/image_123923953(6).JPG",
                "/images/puzzel_pics/image_123923953(7).JPG",
                "/images/puzzel_pics/image_123923953(8).JPG",
                "/images/puzzel_pics/image_123923953(9).JPG",
                "/images/puzzel_pics/image_123923953.JPG",
            },

            new string[]
            {
                "/images/tool_pics/IMG-0837.JPG",
                "/images/tool_pics/IMG-0838.JPG",
                "/images/tool_pics/IMG-0839.JPG",
                "/images/tool_pics/IMG-0840.JPG",
                "/images/tool_pics/IMG-0841.JPG",
                "/images/tool_pics/IMG-0842.JPG",
                "/images/tool_pics/IMG-0843.JPG",
                "/images/tool_pics/IMG-0844.JPG",
                "/images/tool_pics/IMG-0845.JPG",
                "/images/tool_pics/IMG-0846.JPG",
                "/images/tool_pics/IMG-0847.JPG",
                "/images/tool_pics/IMG-0848.JPG",
                "/images/tool_pics/IMG-0849.JPG",
                "/images/tool_pics/IMG-0850.JPG",
                "/images/tool_pics/IMG-0851.JPG",
                "/images/tool_pics/IMG-0852.JPG",
                "/images/tool_pics/IMG-0853.JPG",
                "/images/tool_pics/IMG-0854.JPG",
                "/images/tool_pics/IMG-0855.JPG",
                "/images/tool_pics/IMG-0856.JPG",
                "/images/tool_pics/IMG-0857.JPG",
                "/images/tool_pics/IMG-0858.JPG",
            },

            new string[]
            {
                "/images/dogtoy_pics/IMG-0869.JPG",
                "/images/dogtoy_pics/IMG-0870.JPG",
                "/images/dogtoy_pics/IMG-0871.JPG",
                "/images/dogtoy_pics/IMG-0872.JPG",
                "/images/dogtoy_pics/IMG-0873.JPG",
                "/images/dogtoy_pics/IMG-0874.JPG",
                "/images/dogtoy_pics/IMG-0875.JPG",
                "/images/dogtoy_pics/IMG-0876.JPG",
                "/images/dogtoy_pics/IMG-0877.JPG",
                "/images/dogtoy_pics/IMG-0878.JPG",
                "/images/dogtoy_pics/IMG-0879.JPG",
                "/images/dogtoy_pics/IMG-0880.JPG",
                "/images/dogtoy_pics/IMG-0881.JPG",
                "/images/dogtoy_pics/IMG-0882.JPG",
            },

            new string[]
            {
                "/images/puzzel_pics/image_123923953(1).JPG",
                "/images/puzzel_pics/image_123923953(10).JPG",
                "/images/puzzel_pics/image_123923953(11).JPG",
                "/images/puzzel_pics/image_123923953(12).JPG",
                "/images/puzzel_pics/image_123923953(13).JPG",
                "/images/puzzel_pics/image_123923953(14).JPG",
                "/images/puzzel_pics/image_123923953(15).JPG",
                "/images/puzzel_pics/image_123923953(16).JPG",
                "/images/puzzel_pics/image_123923953(17).JPG",
                "/images/puzzel_pics/image_123923953(18).JPG",
                "/images/puzzel_pics/image_123923953(19).JPG",
                "/images/puzzel_pics/image_123923953(2).JPG",
                "/images/puzzel_pics/image_123923953(20).JPG",
                "/images/puzzel_pics/image_123923953(21).JPG",
                "/images/puzzel_pics/image_123923953(22).JPG",
                "/images/puzzel_pics/image_123923953(23).JPG",
                "/images/puzzel_pics/image_123923953(24).JPG",
                "/images/puzzel_pics/image_123923953(25).JPG",
                "/images/puzzel_pics/image_123923953(26).JPG",
                "/images/puzzel_pics/image_123923953(27).JPG",
                "/images/puzzel_pics/image_123923953(28).JPG",
                "/images/puzzel_pics/image_123923953(3).JPG",
                "/images/puzzel_pics/image_123923953(4).JPG",
                "/images/puzzel_pics/image_123923953(5).JPG",
                "/images/puzzel_pics/image_123923953(6).JPG",
                "/images/puzzel_pics/image_123923953(7).JPG",
                "/images/puzzel_pics/image_123923953(8).JPG",
                "/images/puzzel_pics/image_123923953(9).JPG",
                "/images/puzzel_pics/image_123923953.JPG",
            },

            new string[]
            {
                "/images/tool_pics/IMG-0837.JPG",
                "/images/tool_pics/IMG-0838.JPG",
                "/images/tool_pics/IMG-0839.JPG",
                "/images/tool_pics/IMG-0840.JPG",
                "/images/tool_pics/IMG-0841.JPG",
                "/images/tool_pics/IMG-0842.JPG",
                "/images/tool_pics/IMG-0843.JPG",
                "/images/tool_pics/IMG-0844.JPG",
                "/images/tool_pics/IMG-0845.JPG",
                "/images/tool_pics/IMG-0846.JPG",
                "/images/tool_pics/IMG-0847.JPG",
                "/images/tool_pics/IMG-0848.JPG",
                "/images/tool_pics/IMG-0849.JPG",
                "/images/tool_pics/IMG-0850.JPG",
                "/images/tool_pics/IMG-0851.JPG",
                "/images/tool_pics/IMG-0852.JPG",
                "/images/tool_pics/IMG-0853.JPG",
                "/images/tool_pics/IMG-0854.JPG",
                "/images/tool_pics/IMG-0855.JPG",
                "/images/tool_pics/IMG-0856.JPG",
                "/images/tool_pics/IMG-0857.JPG",
                "/images/tool_pics/IMG-0858.JPG",
            },

        };

    }
}
