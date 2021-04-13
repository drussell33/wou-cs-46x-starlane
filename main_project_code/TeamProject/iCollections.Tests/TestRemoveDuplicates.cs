using NUnit.Framework;
using iCollections.Controllers;
using iCollections.Models;
using System.Collections.Generic;
using System;

namespace iCollections.Tests
{
    public class TestRemoveDuplicates
    {
        Dictionary<string, IcollectionUser> users;

        [SetUp]
        public void Setup()
        {
            users = new Dictionary<string, IcollectionUser>();
            users.Add("brock", new IcollectionUser { Id = 52, FirstName = "Brock" });
            users.Add("lily", new IcollectionUser { Id = 4, FirstName = "Lily" });
            users.Add("john", new IcollectionUser { Id = 14, FirstName = "John" });
            users.Add("damon", new IcollectionUser { Id = 78, FirstName = "Damon" });
            users.Add("will", new IcollectionUser { Id = 34, FirstName = "Will" });
            users.Add("franklin", new IcollectionUser { Id = 38, FirstName = "Franklin" });
            users.Add("grant", new IcollectionUser { Id = 81, FirstName = "Grant" });
        }

        [Test]
        public void RemoveDuplicate_NullListsThrow_Exception()
        {
            List<FriendsWith> mutuals = null;
            Assert.Throws<NullReferenceException>(() => DatabaseHelper.RemoveDuplicates(ref mutuals, null));
        }

        [Test]
        public void RemoveDuplicate_YourFriendsDontHaveOtherFriendsBesideYouReturns_EmptyList()
        {
            var brock = users["brock"];
            var lily = users["lily"];
            var john = users["john"];
            var damon = users["damon"];

            List<FriendsWith> mutuals = new List<FriendsWith>();

            var personals = new List<IcollectionUser>();
            personals.Add(lily);
            personals.Add(brock);
            personals.Add(john);
            personals.Add(damon);

            DatabaseHelper.RemoveDuplicates(ref mutuals, personals);

            Assert.That(mutuals.Count, Is.EqualTo(0));

        }

        [Test]
        public void RemoveDuplicate_OneFriendshipRemoves_None()
        {
            List<IcollectionUser> directFriends = new List<IcollectionUser>();
            List<FriendsWith> list = new List<FriendsWith>();
            directFriends.Add(users["will"]);
            directFriends.Add(users["franklin"]);
            directFriends.Add(users["grant"]);
            list.Add(new FriendsWith
            {
                Id = 3345,
                User1Id = 81,
                User2Id = 14,
                User1 = users["grant"],
                User2 = users["john"]
            });
            DatabaseHelper.RemoveDuplicates(ref list, directFriends);
            Assert.AreEqual(1, list.Count);
        }

        [Test]
        public void RemoveDuplicate_SizeTwoDuplicateListWithDirectFriendsReturns_OneFriendship()
        {
            var brock = users["brock"];
            var lily = users["lily"];
            var john = users["john"];
            var damon = users["damon"];

            List<FriendsWith> mutuals = new List<FriendsWith>();
            mutuals.Add(new FriendsWith { Id = 1324, User1Id = brock.Id, User2Id = lily.Id, User1 = brock, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 132, User1Id = lily.Id, User2Id = brock.Id, User1 = lily, User2 = brock });

            var personals = new List<IcollectionUser>();
            personals.Add(brock);

            DatabaseHelper.RemoveDuplicates(ref mutuals, personals);

            Assert.That(mutuals.Count, Is.EqualTo(1));

        }

        [Test]
        public void RemoveDuplicate_ThreeDuplicateElementsInListReturns_OneFriendship()
        {
            var brock = users["brock"];
            var lily = users["lily"];
            var john = users["john"];
            var damon = users["damon"];

            List<FriendsWith> mutuals = new List<FriendsWith>();
            mutuals.Add(new FriendsWith { Id = 1324, User1Id = brock.Id, User2Id = lily.Id, User1 = brock, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 132, User1Id = lily.Id, User2Id = brock.Id, User1 = lily, User2 = brock });
            mutuals.Add(new FriendsWith { Id = 13244, User1Id = brock.Id, User2Id = lily.Id, User1 = brock, User2 = lily });

            var personals = new List<IcollectionUser>();
            personals.Add(lily);

            DatabaseHelper.RemoveDuplicates(ref mutuals, personals);

            Assert.That(mutuals.Count, Is.EqualTo(1));

        }

        [Test]
        public void RemoveDuplicate_AllMutualsAreDirectFriendsOfMineReturns_SmallerList()
        {
            var brock = users["brock"];
            var lily = users["lily"];
            var john = users["john"];
            var damon = users["damon"];

            List<FriendsWith> mutuals = new List<FriendsWith>();
            mutuals.Add(new FriendsWith { Id = 1324, User1Id = brock.Id, User2Id = lily.Id, User1 = brock, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 132, User1Id = lily.Id, User2Id = brock.Id, User1 = lily, User2 = brock });
            mutuals.Add(new FriendsWith { Id = 1325, User1Id = john.Id, User2Id = damon.Id, User1 = john, User2 = damon });
            mutuals.Add(new FriendsWith { Id = 13, User1Id = damon.Id, User2Id = john.Id, User1 = damon, User2 = john });
            mutuals.Add(new FriendsWith { Id = 13245, User1Id = brock.Id, User2Id = damon.Id, User1 = brock, User2 = damon });
            mutuals.Add(new FriendsWith { Id = 1, User1Id = damon.Id, User2Id = brock.Id, User1 = damon, User2 = brock });
            // (brock, lily), (lily, brock), (john, damon), (damon, john), (brock, damon), (damon, brock) / {lily, brock, john, damon}
            // => (brock, lily), (john, damon), (brock, damon)

            var personals = new List<IcollectionUser>();
            personals.Add(lily);
            personals.Add(brock);
            personals.Add(john);
            personals.Add(damon);

            DatabaseHelper.RemoveDuplicates(ref mutuals, personals);

            Assert.That(mutuals.Count, Is.EqualTo(3));
        }

        [Test]
        public void RemoveDuplicate_LongMutualListWithAVeryPopularFriendReturns_SmallerList()
        {
            var brock = users["brock"];
            var lily = users["lily"];
            var john = users["john"];
            var damon = users["damon"];
            var will = users["will"];
            var franklin = users["franklin"];
            var grant = users["grant"];

            // (lily, brock), (lily, john), (lily, damon), (lily, will), (lily, franklin), (lily, grant),
            // (brock, lily), (john, lily), (damon, lily), (will, lily), (franklin, lily), (grant, lily),
            // (damon, john), (john, damon) =>
            // (lily, brock), (lily, john), (lily, damon), (lily, will), (lily, franklin), (lily, grant), (brock, john)
            List<FriendsWith> mutuals = new List<FriendsWith>();
            mutuals.Add(new FriendsWith { Id = 1, User1Id = lily.Id, User2Id = brock.Id, User1 = lily, User2 = brock });
            mutuals.Add(new FriendsWith { Id = 2, User1Id = lily.Id, User2Id = john.Id, User1 = lily, User2 = john });
            mutuals.Add(new FriendsWith { Id = 3, User1Id = lily.Id, User2Id = damon.Id, User1 = lily, User2 = damon });
            mutuals.Add(new FriendsWith { Id = 4, User1Id = lily.Id, User2Id = will.Id, User1 = lily, User2 = will });
            mutuals.Add(new FriendsWith { Id = 5, User1Id = lily.Id, User2Id = franklin.Id, User1 = lily, User2 = franklin });
            mutuals.Add(new FriendsWith { Id = 6, User1Id = lily.Id, User2Id = grant.Id, User1 = lily, User2 = grant });

            mutuals.Add(new FriendsWith { Id = 7, User1Id = brock.Id, User2Id = lily.Id, User1 = brock, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 8, User1Id = john.Id, User2Id = lily.Id, User1 = john, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 9, User1Id = damon.Id, User2Id = lily.Id, User1 = damon, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 10, User1Id = will.Id, User2Id = lily.Id, User1 = will, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 11, User1Id = franklin.Id, User2Id = lily.Id, User1 = franklin, User2 = lily });
            mutuals.Add(new FriendsWith { Id = 12, User1Id = grant.Id, User2Id = lily.Id, User1 = grant, User2 = lily });

            mutuals.Add(new FriendsWith { Id = 13, User1Id = damon.Id, User2Id = john.Id, User1 = damon, User2 = john });
            mutuals.Add(new FriendsWith { Id = 14, User1Id = john.Id, User2Id = damon.Id, User1 = john, User2 = damon });

            var personals = new List<IcollectionUser>();
            personals.Add(lily);
            personals.Add(damon);

            DatabaseHelper.RemoveDuplicates(ref mutuals, personals);

            Assert.That(mutuals.Count, Is.EqualTo(7));
        }
    }
}