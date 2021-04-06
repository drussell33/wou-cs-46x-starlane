using NUnit.Framework;
using iCollections.Controllers;
using iCollections.Models;
using System.Collections.Generic;
using System;

namespace iCollections.Tests
{
    public class TestRemoveDuplicates
    {
        [SetUp]
        public void Setup()
        {
        }

        public void SetupDirectFriends(List<IcollectionUser> directFriends)
        {
            directFriends.Add(new IcollectionUser
            {
                Id = 34,
                AspnetIdentityId = "wou",
                FirstName = "Will",
                LastName = "Scio",
                UserName = "Logger",
                DateJoined = DateTime.Now,
                AboutMe = "",
                ProfilePicId = 75
            });

            directFriends.Add(new IcollectionUser
            {
                Id = 38,
                AspnetIdentityId = "OSU",
                FirstName = "Franklin",
                LastName = "Jefferson",
                UserName = "Lion",
                DateJoined = DateTime.Now,
                AboutMe = "",
                ProfilePicId = 79
            });

            directFriends.Add(new IcollectionUser
            {
                Id = 81,
                AspnetIdentityId = "uo",
                FirstName = "Grant",
                LastName = "Sisters",
                UserName = "Cowboy",
                DateJoined = DateTime.Now,
                AboutMe = "",
                ProfilePicId = 745
            });
        }

        [Test]
        public void RemoveDuplicate_OneFriendshipRemoves_None()
        {
            List<IcollectionUser> directFriends = new List<IcollectionUser>();
            SetupDirectFriends(directFriends);
            List<FriendsWith> list = new List<FriendsWith>();
            list.Add(new FriendsWith{
                Id = 3345,
                User1Id = 81, 
                User2Id = 63, 
                Began = DateTime.Now
            });
            Assert.AreEqual(1, list.Count);
            DatabaseHelper.RemoveDuplicates(list, directFriends);
            Console.WriteLine(list.Count);
            // Assert.AreEqual(1, list.Count);
        }
    }
}