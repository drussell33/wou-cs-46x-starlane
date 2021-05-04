// Baltazar Ortiz - OrderLists()

using NUnit.Framework;
using iCollections.Controllers;
using iCollections.Models;
using System;
using System.Collections.Generic;

namespace iCollections.Tests.Tests
{
    public class TestOrderLists
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void OrderLists_GivenNullListsThrows_Exception()
        {
            List<FriendsWith> l = null;
            List<Follow> j = null;
            List<Collection> k = null;
            Assert.Throws<ArgumentNullException>(() => DatabaseHelper.OrderLists(ref l, ref j, ref k));
        }

        [Test]
        public void OrderLists_GivenEmptyListsReturns_EmptyLists()
        {
            var secondHandFriends = new List<FriendsWith>();
            var secondHandFollowees = new List<Follow>();
            var interestedCollections = new List<Collection>();
            DatabaseHelper.OrderLists(ref secondHandFriends, ref secondHandFollowees, ref interestedCollections);
            Assert.That(secondHandFriends.Count, Is.EqualTo(0));
            Assert.That(secondHandFollowees.Count, Is.EqualTo(0));
            Assert.That(interestedCollections.Count, Is.EqualTo(0));
        }

        bool AnalyzeRelationshipList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                var currentBegan = (DateTime)list[i].GetType().GetProperty("Began").GetValue(list[i]);
                var nextBegan = (DateTime)list[i + 1].GetType().GetProperty("Began").GetValue(list[i + 1]);
                if (currentBegan < nextBegan) return false;
            }
            return true;
        }

        bool AnalyzeCollections(List<Collection> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                var currentBegan = list[i].DateMade;
                var nextBegan = list[i + 1].DateMade;
                if (currentBegan < nextBegan) return false;
            }
            return true;
        }

        [Test]
        public void OrderLists_GivenInOrderListsReturns_InOrderLists()
        {
            var brock = new IcollectionUser { Id = 1, FirstName = "Brock" };
            var lily = new IcollectionUser { Id = 2, FirstName = "Lily" };
            var john = new IcollectionUser { Id = 3, FirstName = "John" };
            var damon = new IcollectionUser { Id = 4, FirstName = "Damon" };
            var will = new IcollectionUser { Id = 5, FirstName = "Will" };
            var franklin = new IcollectionUser { Id = 6, FirstName = "Franklin" };
            var grant = new IcollectionUser { Id = 7, FirstName = "Grant" };

            var secondHandFriends = new List<FriendsWith> {
                new FriendsWith{Id = 1, User1Id = 2, User2Id = 5, Began = new DateTime(2012, 3, 21, 9, 0, 0), User1 = lily, User2 = will},
                new FriendsWith{Id = 2, User1Id = 5, User2Id = 2, Began = new DateTime(2012, 3, 21, 9, 0, 0), User1 = will, User2 = lily},
                new FriendsWith{Id = 3, User1Id = 2, User2Id = 3, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = lily, User2 = john},
                new FriendsWith{Id = 4, User1Id = 3, User2Id = 2, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = john, User2 = lily},
                new FriendsWith{Id = 5, User1Id = 2, User2Id = 4, Began = new DateTime(2016, 10, 23, 13, 0, 0), User1 = lily, User2 = damon},
                new FriendsWith{Id = 6, User1Id = 4, User2Id = 2, Began = new DateTime(2016, 10, 23, 13, 0, 0), User1 = damon, User2 = lily}
            };

            var secondHandFollowees = new List<Follow>{
                new Follow {Id = 1, Follower = 5,Followed = 3,Began = new DateTime(2002, 7, 2, 4, 0, 0),FollowedNavigation = john,FollowerNavigation = will},
                new Follow {Id = 2, Follower = 4,Followed = 3,Began = new DateTime(2009, 4, 23, 12, 0, 0),FollowedNavigation = john,FollowerNavigation = damon},
                new Follow {Id = 3, Follower = 4,Followed = 2,Began = new DateTime(2012, 4, 23, 12, 0, 0),FollowedNavigation = lily,FollowerNavigation = damon},
                new Follow {Id = 4, Follower = 3,Followed = 5,Began = new DateTime(2021, 7, 2, 4, 0, 0),FollowedNavigation = will,FollowerNavigation = john}
            };

            var interestedCollections = new List<Collection>{
                new Collection {Id = 7, Name = "My Trophyies", UserId = 3, DateMade = new DateTime(1996, 8, 22, 19, 54, 0), User = john},
                new Collection {Id = 8, Name = "My Plants", UserId = 3, DateMade = new DateTime(2005, 12, 17, 1, 32, 0), User = john},
                new Collection {Id = 9, Name = "My Cards", UserId = 3, DateMade = new DateTime(2006, 4, 14, 22, 23, 0), User = john},
                new Collection {Id = 4, Name = "My Tools", UserId = 2, DateMade = new DateTime(2006, 5, 9, 11, 40, 0), User = lily},
                new Collection {Id = 5, Name = "My Friends", UserId = 2, DateMade = new DateTime(2007, 2, 10, 2, 45, 0), User = lily},
                new Collection {Id = 11, Name = "Collection3", UserId = 4, DateMade = new DateTime(2009, 2, 3, 7, 5, 0), User = damon},
                new Collection {Id = 13, Name = "My Stamps", UserId = 5, DateMade = new DateTime(2009, 10, 10, 15, 28, 0), User = will},
                new Collection {Id = 15, Name = "My Funco Pops", UserId = 5, DateMade = new DateTime(2012, 6, 2, 7, 31, 0), User = will},
                new Collection {Id = 10, Name = "My Games", UserId = 4, DateMade = new DateTime(2013, 5, 6, 4, 7, 0), User = damon},
                new Collection {Id = 14, Name = "My Posters", UserId = 5, DateMade = new DateTime(2014, 11, 21, 18, 52, 0), User = will},
                new Collection {Id = 12, Name = "My Stickers", UserId = 4, DateMade = new DateTime(2017, 3, 9, 9, 22, 0), User = damon},
                new Collection {Id = 6, Name = "Collection2", UserId = 3, DateMade = new DateTime(2019, 7, 15, 14, 59, 0), User = john}
            };

            DatabaseHelper.OrderLists(ref secondHandFriends, ref secondHandFollowees, ref interestedCollections);
            Assert.That(AnalyzeRelationshipList<FriendsWith>(secondHandFriends));
            Assert.That(AnalyzeRelationshipList<Follow>(secondHandFollowees));
            Assert.That(AnalyzeCollections(interestedCollections));
        }

        [Test]
        public void OrderLists_GivenInReverseOrderListsReturns_InOrderLists()
        {
            var brock = new IcollectionUser { Id = 1, FirstName = "Brock" };
            var lily = new IcollectionUser { Id = 2, FirstName = "Lily" };
            var john = new IcollectionUser { Id = 3, FirstName = "John" };
            var damon = new IcollectionUser { Id = 4, FirstName = "Damon" };
            var will = new IcollectionUser { Id = 5, FirstName = "Will" };
            var franklin = new IcollectionUser { Id = 6, FirstName = "Franklin" };
            var grant = new IcollectionUser { Id = 7, FirstName = "Grant" };

            var secondHandFriends = new List<FriendsWith> {
                new FriendsWith{Id = 6, User1Id = 4, User2Id = 2, Began = new DateTime(2016, 10, 23, 13, 0, 0), User1 = damon, User2 = lily},
                new FriendsWith{Id = 5, User1Id = 2, User2Id = 4, Began = new DateTime(2016, 10, 23, 13, 0, 0), User1 = lily, User2 = damon},
                new FriendsWith{Id = 4, User1Id = 3, User2Id = 2, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = john, User2 = lily},
                new FriendsWith{Id = 3, User1Id = 2, User2Id = 3, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = lily, User2 = john},
                new FriendsWith{Id = 2, User1Id = 5, User2Id = 2, Began = new DateTime(2012, 3, 21, 9, 0, 0), User1 = will, User2 = lily},
                new FriendsWith{Id = 1, User1Id = 2, User2Id = 5, Began = new DateTime(2012, 3, 21, 9, 0, 0), User1 = lily, User2 = will}
            };

            var secondHandFollowees = new List<Follow>{
                new Follow {Id = 4, Follower = 3,Followed = 5,Began = new DateTime(2021, 7, 2, 4, 0, 0),FollowedNavigation = will,FollowerNavigation = john},
                new Follow {Id = 3, Follower = 4,Followed = 2,Began = new DateTime(2012, 4, 23, 12, 0, 0),FollowedNavigation = lily,FollowerNavigation = damon},
                new Follow {Id = 2, Follower = 4,Followed = 3,Began = new DateTime(2009, 4, 23, 12, 0, 0),FollowedNavigation = john,FollowerNavigation = damon},
                new Follow {Id = 1, Follower = 5,Followed = 3,Began = new DateTime(2002, 7, 2, 4, 0, 0),FollowedNavigation = john,FollowerNavigation = will}
            };

            var interestedCollections = new List<Collection>{
                new Collection {Id = 6, Name = "Collection2", UserId = 3, DateMade = new DateTime(2019, 7, 15, 14, 59, 0), User = john},
                new Collection {Id = 12, Name = "My Stickers", UserId = 4, DateMade = new DateTime(2017, 3, 9, 9, 22, 0), User = damon},
                new Collection {Id = 14, Name = "My Posters", UserId = 5, DateMade = new DateTime(2014, 11, 21, 18, 52, 0), User = will},
                new Collection {Id = 10, Name = "My Games", UserId = 4, DateMade = new DateTime(2013, 5, 6, 4, 7, 0), User = damon},
                new Collection {Id = 15, Name = "My Funco Pops", UserId = 5, DateMade = new DateTime(2012, 6, 2, 7, 31, 0), User = will},
                new Collection {Id = 13, Name = "My Stamps", UserId = 5, DateMade = new DateTime(2009, 10, 10, 15, 28, 0), User = will},
                new Collection {Id = 11, Name = "Collection3", UserId = 4, DateMade = new DateTime(2009, 2, 3, 7, 5, 0), User = damon},
                new Collection {Id = 5, Name = "My Friends", UserId = 2, DateMade = new DateTime(2007, 2, 10, 2, 45, 0), User = lily},
                new Collection {Id = 4, Name = "My Tools", UserId = 2, DateMade = new DateTime(2006, 5, 9, 11, 40, 0), User = lily},
                new Collection {Id = 9, Name = "My Cards", UserId = 3, DateMade = new DateTime(2006, 4, 14, 22, 23, 0), User = john},
                new Collection {Id = 8, Name = "My Plants", UserId = 3, DateMade = new DateTime(2005, 12, 17, 1, 32, 0), User = john},
                new Collection {Id = 7, Name = "My Trophyies", UserId = 3, DateMade = new DateTime(1996, 8, 22, 19, 54, 0), User = john},                
            };

            DatabaseHelper.OrderLists(ref secondHandFriends, ref secondHandFollowees, ref interestedCollections);
            Assert.That(AnalyzeRelationshipList<FriendsWith>(secondHandFriends));
            Assert.That(AnalyzeRelationshipList<Follow>(secondHandFollowees));
            Assert.That(AnalyzeCollections(interestedCollections));
        }
    }
}