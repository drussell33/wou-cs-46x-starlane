// Baltazar Ortiz - ReadDistantFriends()

using NUnit.Framework;
using iCollections.Controllers;
using iCollections.Models;
using iCollections.Data.Abstract;
using Moq;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using iCollections.Data;
using iCollections.Data.Concrete;

namespace iCollections.Tests.Tests
{
    public class TestReadFollowees
    {
        Mock<IFollowRepository> followsRelationships = new Mock<IFollowRepository>();
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

        [Test]
        public void ReadFollowees_GivenNoFolloweesAdds_NoFolloweesNorCollections()
        {
            var brock = new IcollectionUser { Id = 1, FirstName = "Brock" };
            var lily = new IcollectionUser { Id = 2, FirstName = "Lily" };
            var john = new IcollectionUser { Id = 3, FirstName = "John" };
            var damon = new IcollectionUser { Id = 4, FirstName = "Damon" };
            var will = new IcollectionUser { Id = 5, FirstName = "Will" };
            var franklin = new IcollectionUser { Id = 6, FirstName = "Franklin" };
            var grant = new IcollectionUser { Id = 7, FirstName = "Grant" };

            // create relationships
            List<Follow> follows = new List<Follow>{
                new Follow {Id = 1, Follower = 2,Followed = 1,Began = new DateTime(2013, 4, 14, 11, 0, 0),FollowedNavigation = brock,FollowerNavigation = lily},
                new Follow {Id = 2, Follower = 3,Followed = 1,Began = new DateTime(2014, 2, 7, 11, 0, 0),FollowedNavigation = brock,FollowerNavigation = john},
                new Follow {Id = 3, Follower = 4,Followed = 1,Began = new DateTime(2015, 9, 3, 23, 0, 0),FollowedNavigation = brock,FollowerNavigation = damon},
                new Follow {Id = 4, Follower = 5,Followed = 1,Began = new DateTime(2020, 5, 4, 4, 0, 0),FollowedNavigation = brock,FollowerNavigation = will},
                new Follow {Id = 5, Follower = 4,Followed = 3,Began = new DateTime(2009, 4, 23, 12, 0, 0),FollowedNavigation = john,FollowerNavigation = damon},
                new Follow {Id = 6, Follower = 5,Followed = 3,Began = new DateTime(2002, 7, 2, 4, 0, 0),FollowedNavigation = john,FollowerNavigation = will}
            };

            List<Collection> cols = new List<Collection>{
                new Collection {Id = 1, Name = "Collection1", UserId = 1, DateMade = new DateTime(2004, 11, 2, 8, 3, 0), User = brock},
                new Collection {Id = 2, Name = "My Fish", UserId = 1, DateMade = new DateTime(2015, 9, 1, 5, 5, 0), User = brock},
                new Collection {Id = 3, Name = "My Beer", UserId = 1, DateMade = new DateTime(2017, 4, 23, 23, 10, 0), User = brock},
                new Collection {Id = 4, Name = "My Tools", UserId = 2, DateMade = new DateTime(2006, 5, 9, 11, 40, 0), User = lily},
                new Collection {Id = 5, Name = "My Friends", UserId = 2, DateMade = new DateTime(2007, 2, 10, 2, 45, 0), User = lily},
                new Collection {Id = 6, Name = "Collection2", UserId = 3, DateMade = new DateTime(2019, 7, 15, 14, 59, 0), User = john},
                new Collection {Id = 7, Name = "My Trophyies", UserId = 3, DateMade = new DateTime(1996, 8, 22, 19, 54, 0), User = john},
                new Collection {Id = 8, Name = "My Plants", UserId = 3, DateMade = new DateTime(2005, 12, 17, 1, 32, 0), User = john},
                new Collection {Id = 9, Name = "My Cards", UserId = 3, DateMade = new DateTime(2006, 4, 14, 22, 23, 0), User = john},
                new Collection {Id = 10, Name = "My Games", UserId = 4, DateMade = new DateTime(2013, 5, 6, 4, 7, 0), User = damon},
                new Collection {Id = 11, Name = "Collection3", UserId = 4, DateMade = new DateTime(2009, 2, 3, 7, 5, 0), User = damon},
                new Collection {Id = 12, Name = "My Stickers", UserId = 4, DateMade = new DateTime(2017, 3, 9, 9, 22, 0), User = damon},
                new Collection {Id = 13, Name = "My Stamps", UserId = 5, DateMade = new DateTime(2009, 10, 10, 15, 28, 0), User = will},
                new Collection {Id = 14, Name = "My Posters", UserId = 5, DateMade = new DateTime(2014, 11, 21, 18, 52, 0), User = will},
                new Collection {Id = 15, Name = "My Funco Pops", UserId = 5, DateMade = new DateTime(2012, 6, 2, 7, 31, 0), User = will}
            };

            List<IcollectionUser> whoIFollow = new List<IcollectionUser>();
            List<Follow> topFollow = new List<Follow>();
            List<Collection> followeesCollections = new List<Collection>();

            Mock<DbSet<Follow>> mockFriendsWithDbSet = GetMockDbSet(follows.AsQueryable());
            Mock<DbSet<Collection>> mockCollectionDbSet = GetMockDbSet(cols.AsQueryable());

            Mock<ICollectionsDbContext> mockContext = new Mock<ICollectionsDbContext>();
            mockContext.Setup(ctx => ctx.Follows).Returns(mockFriendsWithDbSet.Object);
            mockContext.Setup(ctx => ctx.Collections).Returns(mockCollectionDbSet.Object);
            mockContext.Setup(ctx => ctx.Set<Follow>()).Returns(mockFriendsWithDbSet.Object);
            mockContext.Setup(ctx => ctx.Set<Collection>()).Returns(mockCollectionDbSet.Object);

            // Arrange
            IFollowRepository followRepo = new FollowRepository(mockContext.Object);
            IcollectionRepository collectionRepo = new CollectionRepository(mockContext.Object);
            

            DatabaseHelper.ReadFollowees(whoIFollow, topFollow, followeesCollections, 1, followRepo, collectionRepo);
            Assert.That(topFollow.Count, Is.EqualTo(0));
            Assert.That(followeesCollections.Count, Is.EqualTo(0));
        }
    }

    // these two are the functions i need to test
    // dbHelper.ReadDistantFriends(myFriends, myFriendsFriends, theirCollections, userId);
    // dbHelper.ReadFollowees(whoIFollow, topFollow, followeesCollections, userId);
}