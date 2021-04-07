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
            list.Add(new FriendsWith
            {
                Id = 3345,
                User1Id = 81,
                User2Id = 63,
                User1 = directFriends[1],
                User2 = new IcollectionUser { Id = 63 },
                Began = DateTime.Now
            });
            DatabaseHelper.RemoveDuplicates(list, directFriends);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void RemoveDuplicate_SizeTwoDuplicateListReturns_OneFriendship()
        {
            var brock = new IcollectionUser { Id = 52, FirstName = "Brock" };
            var lily = new IcollectionUser { Id = 4, FirstName = "Lily" };
            var john = new IcollectionUser { Id = 14, FirstName = "John" };
            var damon = new IcollectionUser { Id = 78, FirstName = "Damon" };

            List<FriendsWith> mutuals = new List<FriendsWith>();
            mutuals.Add(new FriendsWith { Id = 1324, User1Id = brock.Id, User2Id = lily.Id, User1 = brock, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 132, User1Id = lily.Id, User2Id = brock.Id, User1 = lily, User2 = brock });

            DatabaseHelper.RemoveDuplicates(mutuals, new List<IcollectionUser>());

            Assert.That(mutuals.Count, Is.EqualTo(1));

        }

        [Test]
        public void RemoveDuplicate_SizeTwoDuplicateListWithDirectFriendsReturns_OneFriendship()
        {
            var brock = new IcollectionUser { Id = 52, FirstName = "Brock" };
            var lily = new IcollectionUser { Id = 4, FirstName = "Lily" };
            var john = new IcollectionUser { Id = 14, FirstName = "John" };
            var damon = new IcollectionUser { Id = 78, FirstName = "Damon" };

            List<FriendsWith> mutuals = new List<FriendsWith>();
            mutuals.Add(new FriendsWith { Id = 1324, User1Id = brock.Id, User2Id = lily.Id, User1 = brock, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 132, User1Id = lily.Id, User2Id = brock.Id, User1 = lily, User2 = brock });

            var personals = new List<IcollectionUser>();
            personals.Add(brock);

            DatabaseHelper.RemoveDuplicates(mutuals, personals);

            Assert.That(mutuals.Count, Is.EqualTo(1));

        }

        [Test]
        public void RemoveDuplicate_ThreeDuplicateElementsInListReturns_OneFriendship()
        {
            var brock = new IcollectionUser { Id = 52, FirstName = "Brock" };
            var lily = new IcollectionUser { Id = 4, FirstName = "Lily" };
            var john = new IcollectionUser { Id = 14, FirstName = "John" };
            var damon = new IcollectionUser { Id = 78, FirstName = "Damon" };

            List<FriendsWith> mutuals = new List<FriendsWith>();
            mutuals.Add(new FriendsWith { Id = 1324, User1Id = brock.Id, User2Id = lily.Id, User1 = brock, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 132, User1Id = lily.Id, User2Id = brock.Id, User1 = lily, User2 = brock });
            mutuals.Add(new FriendsWith { Id = 1324, User1Id = brock.Id, User2Id = lily.Id, User1 = brock, User2 = lily });

            DatabaseHelper.RemoveDuplicates(mutuals, new List<IcollectionUser>());

            Assert.That(mutuals.Count, Is.EqualTo(1));

        }
    }
}