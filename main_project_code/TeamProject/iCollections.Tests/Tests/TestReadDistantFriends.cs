// Baltazar Ortiz

using NUnit.Framework;
using iCollections.Controllers;
using iCollections.Models;
using Microsoft.AspNetCore.Mvc;
using iCollections.Data.Abstract;
using Moq;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace iCollections.Tests.Tests
{
    public class TestReadDistantFriends
    {
        Mock<IFriendsWithRepository> friendsRelationships = new Mock<IFriendsWithRepository>();
        Mock<IcollectionRepository> collections = new Mock<IcollectionRepository>();

        [SetUp]
        public void Setup()
        {

        }


        private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
            return mockSet;
        }

        public void ReadDistantFriends_GivenNoFriendsAdds_NoSecondHandFriendsNorFriendsCollections()
        {
            var brock = new IcollectionUser { Id = 1, FirstName = "Brock" };
            var lily = new IcollectionUser { Id = 2, FirstName = "Lily" };
            var john = new IcollectionUser { Id = 3, FirstName = "John" };
            var damon = new IcollectionUser { Id = 4, FirstName = "Damon" };
            var will = new IcollectionUser { Id = 5, FirstName = "Will" };
            var franklin = new IcollectionUser { Id = 6, FirstName = "Franklin" };
            var grant = new IcollectionUser { Id = 7, FirstName = "Grant" };

            // create relationships
            friendsRelationships.Setup(m => m.GetAll()).Returns(new FriendsWith[]{
                new FriendsWith{Id = 1, User1Id = 1, User2Id = 2, Began = new DateTime(2011, 3, 24, 10, 0, 0), User1 = brock, User2 = lily},
                new FriendsWith{Id = 2, User1Id = 2, User2Id = 1, Began = new DateTime(2011, 3, 24, 10, 0, 0), User1 = lily, User2 = brock},
                new FriendsWith{Id = 3, User1Id = 3, User2Id = 4, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = john, User2 = damon},
                new FriendsWith{Id = 4, User1Id = 4, User2Id = 3, Began = new DateTime(2011, 9, 2, 5, 0, 0), User1 = damon, User2 = john}
            }.AsQueryable<FriendsWith>());

            // make collections
            collections.Setup(m => m.GetAll()).Returns(new Collection[]{
                new Collection {Id = 1, Name = "Collection1", UserId = 1, DateMade = new DateTime(2004, 11, 2, 8, 3, 0)},
                new Collection {Id = 2, Name = "My Fish", UserId = 1, DateMade = new DateTime(2015, 9, 1, 5, 5, 0)},
                new Collection {Id = 3, Name = "My Beer", UserId = 1, DateMade = new DateTime(2017, 4, 23, 23, 10, 0)},
                new Collection {Id = 4, Name = "My Tools", UserId = 1, DateMade = new DateTime(2006, 5, 9, 11, 40, 0)},
                new Collection {Id = 5, Name = "My Friends", UserId = 1, DateMade = new DateTime(2007, 2, 10, 2, 45, 0)},
                new Collection {Id = 6, Name = "Collection2", UserId = 2, DateMade = new DateTime(2019, 7, 15, 14, 59, 0)},
                new Collection {Id = 7, Name = "My Trophyies", UserId = 2, DateMade = new DateTime(1996, 8, 22, 19, 54, 0)},
                new Collection {Id = 8, Name = "My Plants", UserId = 2, DateMade = new DateTime(2005, 12, 17, 1, 32, 0)},
                new Collection {Id = 9, Name = "My Cards", UserId = 2, DateMade = new DateTime(2006, 4, 14, 22, 23, 0)},
                new Collection {Id = 10, Name = "My Games", UserId = 2, DateMade = new DateTime(2013, 5, 6, 4, 7, 0)},
                new Collection {Id = 11, Name = "Collection3", UserId = 3, DateMade = new DateTime(2009, 2, 3, 7, 5, 0)},
                new Collection {Id = 12, Name = "My Stickers", UserId = 3, DateMade = new DateTime(2017, 3, 9, 9, 22, 0)},
                new Collection {Id = 13, Name = "My Stamps", UserId = 3, DateMade = new DateTime(2009, 10, 10, 15, 28, 0)},
                new Collection {Id = 14, Name = "My Posters", UserId = 3, DateMade = new DateTime(2014, 11, 21, 18, 52, 0)},
                new Collection {Id = 15, Name = "My Funco Pops", UserId = 3, DateMade = new DateTime(2012, 6, 2, 7, 31, 0)}
            }.AsQueryable<Collection>());

            // make a myfriends with no friendships
            var myfriends = new List<IcollectionUser>();

            // myfriendsfriends, myfriendscollections = new list()
            var secondHandFriendships = new List<FriendsWith>();
            var myFriendsCollections = new List<Collection>();

            // mock userId
            int theId = 2;

            // we need to find a way for IFriendsWithRelationship to execute the queries
            // either fake the repo function call or something else
            DatabaseHelper.ReadDistantFriends(myfriends, secondHandFriendships, myFriendsCollections, theId, friendsRelationships.Object, collections.Object);

            Assert.That(secondHandFriendships.Count, Is.EqualTo(0));
            Assert.That(myFriendsCollections.Count, Is.EqualTo(0));
        }
    }

    // these two are the functions i need to test
    // dbHelper.ReadDistantFriends(myFriends, myFriendsFriends, theirCollections, userId);
    // dbHelper.ReadFollowees(whoIFollow, topFollow, followeesCollections, userId);
}