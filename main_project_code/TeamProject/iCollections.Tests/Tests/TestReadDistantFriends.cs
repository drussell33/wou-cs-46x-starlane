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

        [SetUp]
        public void Setup() { 
            
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

            // make a myfriends with no friendships
            var myfriends = new List<IcollectionUser>();

            // myfriendsfriends, myfriendscollections = new list()
            var secondHandFriendships = new List<FriendsWith>();
            var myFriendsCollections = new List<Collection>();

            // mock userId
            int theId = 2;

            DatabaseHelper.ReadDistantFriends(myfriends, secondHandFriendships, myFriendsCollections, theId);

            Assert.That(secondHandFriendships.Count, Is.EqualTo(0));
            Assert.That(myFriendsCollections.Count, Is.EqualTo(0));
        }
    }

    // these two are the functions i need to test
    // dbHelper.ReadDistantFriends(myFriends, myFriendsFriends, theirCollections, userId);
    // dbHelper.ReadFollowees(whoIFollow, topFollow, followeesCollections, userId);
}