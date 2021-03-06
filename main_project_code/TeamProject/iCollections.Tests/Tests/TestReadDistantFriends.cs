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

        [Test]
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
            List<FriendsWith> friendsWiths = new List<FriendsWith>{
                new FriendsWith{Id = 3, User1Id = 2, User2Id = 3, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = lily, User2 = john},
                new FriendsWith{Id = 4, User1Id = 3, User2Id = 2, Began = new DateTime(2011, 9, 2, 5, 0, 0), User1 = john, User2 = lily},
                new FriendsWith{Id = 5, User1Id = 2, User2Id = 4, Began = new DateTime(2011, 10, 23, 13, 0, 0), User1 = lily, User2 = damon},
                new FriendsWith{Id = 6, User1Id = 4, User2Id = 2, Began = new DateTime(2011, 10, 23, 13, 0, 0), User1 = damon, User2 = lily},
                new FriendsWith{Id = 7, User1Id = 2, User2Id = 5, Began = new DateTime(2011, 3, 21, 9, 0, 0), User1 = lily, User2 = will},
                new FriendsWith{Id = 8, User1Id = 5, User2Id = 2, Began = new DateTime(2011, 3, 21, 9, 0, 0), User1 = will, User2 = lily}
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

            List<IcollectionUser> directs = new List<IcollectionUser>();

            List<FriendsWith> secondHandFriends = new List<FriendsWith>();
            List<Collection> theirCollections = new List<Collection>();

            Mock<DbSet<FriendsWith>> mockFriendsWithDbSet = GetMockDbSet(friendsWiths.AsQueryable());
            Mock<DbSet<Collection>> mockCollectionDbSet = GetMockDbSet(cols.AsQueryable());

            Mock<ICollectionsDbContext> mockContext = new Mock<ICollectionsDbContext>();
            mockContext.Setup(ctx => ctx.FriendsWiths).Returns(mockFriendsWithDbSet.Object);
            mockContext.Setup(ctx => ctx.Collections).Returns(mockCollectionDbSet.Object);
            mockContext.Setup(ctx => ctx.Set<FriendsWith>()).Returns(mockFriendsWithDbSet.Object);
            mockContext.Setup(ctx => ctx.Set<Collection>()).Returns(mockCollectionDbSet.Object);

            // Arrange
            IFriendsWithRepository friendsRepo = new FriendsWithRepository(mockContext.Object);
            IcollectionRepository collectionRepo = new CollectionRepository(mockContext.Object);
            

            DatabaseHelper.ReadDistantFriends(directs, ref secondHandFriends, theirCollections, 1, friendsRepo, collectionRepo);

            Assert.That(secondHandFriends.Count, Is.EqualTo(0));
            Assert.That(theirCollections.Count, Is.EqualTo(0));
        }

        [Test]
        public void ReadDistantFriends_OneDirectFriendGets_HisFriendsAndCollections()
        {
            // I (brock) should not be in shf or in myFriendsCollections
            var brock = new IcollectionUser { Id = 1, FirstName = "Brock" };
            var lily = new IcollectionUser { Id = 2, FirstName = "Lily" };
            var john = new IcollectionUser { Id = 3, FirstName = "John" };
            var damon = new IcollectionUser { Id = 4, FirstName = "Damon" };
            var will = new IcollectionUser { Id = 5, FirstName = "Will" };
            var franklin = new IcollectionUser { Id = 6, FirstName = "Franklin" };
            var grant = new IcollectionUser { Id = 7, FirstName = "Grant" };

            // create relationships
            List<FriendsWith> friendsWiths = new List<FriendsWith>{
                new FriendsWith{Id = 1, User1Id = 1, User2Id = 2, Began = new DateTime(2011, 3, 24, 10, 0, 0), User1 = brock, User2 = lily},
                new FriendsWith{Id = 2, User1Id = 2, User2Id = 1, Began = new DateTime(2011, 3, 24, 10, 0, 0), User1 = lily, User2 = brock},
                new FriendsWith{Id = 3, User1Id = 2, User2Id = 3, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = lily, User2 = john},
                new FriendsWith{Id = 4, User1Id = 3, User2Id = 2, Began = new DateTime(2011, 9, 2, 5, 0, 0), User1 = john, User2 = lily},
                new FriendsWith{Id = 5, User1Id = 2, User2Id = 4, Began = new DateTime(2011, 10, 23, 13, 0, 0), User1 = lily, User2 = damon},
                new FriendsWith{Id = 6, User1Id = 4, User2Id = 2, Began = new DateTime(2011, 10, 23, 13, 0, 0), User1 = damon, User2 = lily},
                new FriendsWith{Id = 7, User1Id = 2, User2Id = 5, Began = new DateTime(2011, 3, 21, 9, 0, 0), User1 = lily, User2 = will},
                new FriendsWith{Id = 8, User1Id = 5, User2Id = 2, Began = new DateTime(2011, 3, 21, 9, 0, 0), User1 = will, User2 = lily}
            };

            List<Collection> cols = new List<Collection>{
                new Collection {Id = 1, Name = "Collection1", UserId = 1, DateMade = new DateTime(2004, 11, 2, 8, 3, 0), User = brock, Visibility = 1},
                new Collection {Id = 2, Name = "My Fish", UserId = 1, DateMade = new DateTime(2015, 9, 1, 5, 5, 0), User = brock, Visibility = 1},
                new Collection {Id = 3, Name = "My Beer", UserId = 1, DateMade = new DateTime(2017, 4, 23, 23, 10, 0), User = brock, Visibility = 1},
                new Collection {Id = 4, Name = "My Tools", UserId = 2, DateMade = new DateTime(2006, 5, 9, 11, 40, 0), User = lily, Visibility = 1},
                new Collection {Id = 5, Name = "My Friends", UserId = 2, DateMade = new DateTime(2007, 2, 10, 2, 45, 0), User = lily, Visibility = 1},
                new Collection {Id = 6, Name = "Collection2", UserId = 3, DateMade = new DateTime(2019, 7, 15, 14, 59, 0), User = john, Visibility = 1},
                new Collection {Id = 7, Name = "My Trophyies", UserId = 3, DateMade = new DateTime(1996, 8, 22, 19, 54, 0), User = john, Visibility = 1},
                new Collection {Id = 8, Name = "My Plants", UserId = 3, DateMade = new DateTime(2005, 12, 17, 1, 32, 0), User = john, Visibility = 1},
                new Collection {Id = 9, Name = "My Cards", UserId = 3, DateMade = new DateTime(2006, 4, 14, 22, 23, 0), User = john, Visibility = 1},
                new Collection {Id = 10, Name = "My Games", UserId = 4, DateMade = new DateTime(2013, 5, 6, 4, 7, 0), User = damon, Visibility = 1},
                new Collection {Id = 11, Name = "Collection3", UserId = 4, DateMade = new DateTime(2009, 2, 3, 7, 5, 0), User = damon, Visibility = 1},
                new Collection {Id = 12, Name = "My Stickers", UserId = 4, DateMade = new DateTime(2017, 3, 9, 9, 22, 0), User = damon, Visibility = 1},
                new Collection {Id = 13, Name = "My Stamps", UserId = 5, DateMade = new DateTime(2009, 10, 10, 15, 28, 0), User = will, Visibility = 1},
                new Collection {Id = 14, Name = "My Posters", UserId = 5, DateMade = new DateTime(2014, 11, 21, 18, 52, 0), User = will, Visibility = 1},
                new Collection {Id = 15, Name = "My Funco Pops", UserId = 5, DateMade = new DateTime(2012, 6, 2, 7, 31, 0), User = will, Visibility = 1}
            };

            List<IcollectionUser> directs = new List<IcollectionUser>();
            directs.Add(lily);

            List<FriendsWith> secondHandFriends = new List<FriendsWith>();
            List<Collection> theirCollections = new List<Collection>();

             Mock<DbSet<FriendsWith>> mockFriendsWithDbSet = GetMockDbSet(friendsWiths.AsQueryable());
            //mockApplesDbSet.Setup(a => a.Include("ApplesConsumeds")).Returns(mockApples.Object);
            Mock<DbSet<Collection>> mockCollectionDbSet = GetMockDbSet(cols.AsQueryable());

            Mock<ICollectionsDbContext> mockContext = new Mock<ICollectionsDbContext>();
            mockContext.Setup(ctx => ctx.FriendsWiths).Returns(mockFriendsWithDbSet.Object);
            mockContext.Setup(ctx => ctx.Collections).Returns(mockCollectionDbSet.Object);
            mockContext.Setup(ctx => ctx.Set<FriendsWith>()).Returns(mockFriendsWithDbSet.Object);
            mockContext.Setup(ctx => ctx.Set<Collection>()).Returns(mockCollectionDbSet.Object);

            // Arrange
            IFriendsWithRepository friendsRepo = new FriendsWithRepository(mockContext.Object);
            IcollectionRepository collectionRepo = new CollectionRepository(mockContext.Object);
            

            DatabaseHelper.ReadDistantFriends(directs, ref secondHandFriends, theirCollections, 1, friendsRepo, collectionRepo);

            Assert.That(secondHandFriends.Count, Is.EqualTo(3));
            Assert.That(theirCollections.Count, Is.EqualTo(2));
        }

        [Test]
        public void ReadDistantFriends_ManyDirectFriendsGets_TheirFriendsAndCollections()
        {
            // I (brock) should not be in shf or in myFriendsCollections
            var brock = new IcollectionUser { Id = 1, FirstName = "Brock" };
            var lily = new IcollectionUser { Id = 2, FirstName = "Lily" };
            var john = new IcollectionUser { Id = 3, FirstName = "John" };
            var damon = new IcollectionUser { Id = 4, FirstName = "Damon" };
            var will = new IcollectionUser { Id = 5, FirstName = "Will" };
            var franklin = new IcollectionUser { Id = 6, FirstName = "Franklin" };
            var grant = new IcollectionUser { Id = 7, FirstName = "Grant" };

            // create relationships
            List<FriendsWith> friendsWiths = new List<FriendsWith>{
                new FriendsWith{Id = 1, User1Id = 1, User2Id = 2, Began = new DateTime(2011, 3, 24, 10, 0, 0), User1 = brock, User2 = lily},
                new FriendsWith{Id = 2, User1Id = 2, User2Id = 1, Began = new DateTime(2011, 3, 24, 10, 0, 0), User1 = lily, User2 = brock},
                new FriendsWith{Id = 3, User1Id = 2, User2Id = 3, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = lily, User2 = john},
                new FriendsWith{Id = 4, User1Id = 3, User2Id = 2, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = john, User2 = lily},
                new FriendsWith{Id = 5, User1Id = 2, User2Id = 4, Began = new DateTime(2016, 10, 23, 13, 0, 0), User1 = lily, User2 = damon},
                new FriendsWith{Id = 6, User1Id = 4, User2Id = 2, Began = new DateTime(2016, 10, 23, 13, 0, 0), User1 = damon, User2 = lily},
                new FriendsWith{Id = 7, User1Id = 2, User2Id = 5, Began = new DateTime(2012, 3, 21, 9, 0, 0), User1 = lily, User2 = will},
                new FriendsWith{Id = 8, User1Id = 5, User2Id = 2, Began = new DateTime(2012, 3, 21, 9, 0, 0), User1 = will, User2 = lily},
                new FriendsWith{Id = 9, User1Id = 1, User2Id = 5, Began = new DateTime(2010, 6, 13, 9, 0, 0), User1 = brock, User2 = will},
                new FriendsWith{Id = 10, User1Id = 5, User2Id = 1, Began = new DateTime(2010, 6, 13, 9, 0, 0), User1 = will, User2 = brock},
                new FriendsWith{Id = 11, User1Id = 1, User2Id = 3, Began = new DateTime(2007, 1, 24, 9, 0, 0), User1 = brock, User2 = john},
                new FriendsWith{Id = 12, User1Id = 3, User2Id = 1, Began = new DateTime(2007, 1, 24, 9, 0, 0), User1 = john, User2 = brock}
            };

            List<Collection> cols = new List<Collection>{
                new Collection {Id = 1, Name = "Collection1", UserId = 1, DateMade = new DateTime(2004, 11, 2, 8, 3, 0), User = brock, Visibility = 1},
                new Collection {Id = 2, Name = "My Fish", UserId = 1, DateMade = new DateTime(2015, 9, 1, 5, 5, 0), User = brock, Visibility = 1},
                new Collection {Id = 3, Name = "My Beer", UserId = 1, DateMade = new DateTime(2017, 4, 23, 23, 10, 0), User = brock, Visibility = 1},
                new Collection {Id = 4, Name = "My Tools", UserId = 2, DateMade = new DateTime(2006, 5, 9, 11, 40, 0), User = lily, Visibility = 1},
                new Collection {Id = 5, Name = "My Friends", UserId = 2, DateMade = new DateTime(2007, 2, 10, 2, 45, 0), User = lily, Visibility = 1},
                new Collection {Id = 6, Name = "Collection2", UserId = 3, DateMade = new DateTime(2019, 7, 15, 14, 59, 0), User = john, Visibility = 1},
                new Collection {Id = 7, Name = "My Trophyies", UserId = 3, DateMade = new DateTime(1996, 8, 22, 19, 54, 0), User = john, Visibility = 1},
                new Collection {Id = 8, Name = "My Plants", UserId = 3, DateMade = new DateTime(2005, 12, 17, 1, 32, 0), User = john, Visibility = 1},
                new Collection {Id = 9, Name = "My Cards", UserId = 3, DateMade = new DateTime(2006, 4, 14, 22, 23, 0), User = john, Visibility = 1},
                new Collection {Id = 10, Name = "My Games", UserId = 4, DateMade = new DateTime(2013, 5, 6, 4, 7, 0), User = damon, Visibility = 1},
                new Collection {Id = 11, Name = "Collection3", UserId = 4, DateMade = new DateTime(2009, 2, 3, 7, 5, 0), User = damon, Visibility = 1},
                new Collection {Id = 12, Name = "My Stickers", UserId = 4, DateMade = new DateTime(2017, 3, 9, 9, 22, 0), User = damon, Visibility = 1},
                new Collection {Id = 13, Name = "My Stamps", UserId = 5, DateMade = new DateTime(2009, 10, 10, 15, 28, 0), User = will, Visibility = 1},
                new Collection {Id = 14, Name = "My Posters", UserId = 5, DateMade = new DateTime(2014, 11, 21, 18, 52, 0), User = will, Visibility = 1},
                new Collection {Id = 15, Name = "My Funco Pops", UserId = 5, DateMade = new DateTime(2012, 6, 2, 7, 31, 0), User = will, Visibility = 1}
            };

            List<IcollectionUser> directs = new List<IcollectionUser>();
            directs.Add(lily);
            directs.Add(john);
            directs.Add(will);

            List<FriendsWith> secondHandFriends = new List<FriendsWith>();
            List<Collection> theirCollections = new List<Collection>();

            Mock<DbSet<FriendsWith>> mockFriendsWithDbSet = GetMockDbSet(friendsWiths.AsQueryable());
            Mock<DbSet<Collection>> mockCollectionDbSet = GetMockDbSet(cols.AsQueryable());

            Mock<ICollectionsDbContext> mockContext = new Mock<ICollectionsDbContext>();
            mockContext.Setup(ctx => ctx.FriendsWiths).Returns(mockFriendsWithDbSet.Object);
            mockContext.Setup(ctx => ctx.Collections).Returns(mockCollectionDbSet.Object);
            mockContext.Setup(ctx => ctx.Set<FriendsWith>()).Returns(mockFriendsWithDbSet.Object);
            mockContext.Setup(ctx => ctx.Set<Collection>()).Returns(mockCollectionDbSet.Object);

            // Arrange
            IFriendsWithRepository friendsRepo = new FriendsWithRepository(mockContext.Object);
            IcollectionRepository collectionRepo = new CollectionRepository(mockContext.Object);
            

            DatabaseHelper.ReadDistantFriends(directs, ref secondHandFriends, theirCollections, 1, friendsRepo, collectionRepo);

            Assert.That(secondHandFriends.Count, Is.EqualTo(3));
            Assert.That(theirCollections.Count, Is.EqualTo(9));
        }

        [Test]
        public void ReadDistantFriends_DirectFriendsWithNoCollectionsGets_TheirFriendsAndNoCollections()
        {
            // I (brock) should not be in shf or in myFriendsCollections
            var brock = new IcollectionUser { Id = 1, FirstName = "Brock" };
            var lily = new IcollectionUser { Id = 2, FirstName = "Lily" };
            var john = new IcollectionUser { Id = 3, FirstName = "John" };
            var damon = new IcollectionUser { Id = 4, FirstName = "Damon" };
            var will = new IcollectionUser { Id = 5, FirstName = "Will" };
            var franklin = new IcollectionUser { Id = 6, FirstName = "Franklin" };
            var grant = new IcollectionUser { Id = 7, FirstName = "Grant" };

            // create relationships
            List<FriendsWith> friendsWiths = new List<FriendsWith>{
                new FriendsWith{Id = 1, User1Id = 1, User2Id = 2, Began = new DateTime(2011, 3, 24, 10, 0, 0), User1 = brock, User2 = lily},
                new FriendsWith{Id = 2, User1Id = 2, User2Id = 1, Began = new DateTime(2011, 3, 24, 10, 0, 0), User1 = lily, User2 = brock},
                new FriendsWith{Id = 3, User1Id = 2, User2Id = 3, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = lily, User2 = john},
                new FriendsWith{Id = 4, User1Id = 3, User2Id = 2, Began = new DateTime(2014, 9, 2, 5, 0, 0), User1 = john, User2 = lily},
                new FriendsWith{Id = 5, User1Id = 2, User2Id = 4, Began = new DateTime(2016, 10, 23, 13, 0, 0), User1 = lily, User2 = damon},
                new FriendsWith{Id = 6, User1Id = 4, User2Id = 2, Began = new DateTime(2016, 10, 23, 13, 0, 0), User1 = damon, User2 = lily},
                new FriendsWith{Id = 7, User1Id = 2, User2Id = 5, Began = new DateTime(2012, 3, 21, 9, 0, 0), User1 = lily, User2 = will},
                new FriendsWith{Id = 8, User1Id = 5, User2Id = 2, Began = new DateTime(2012, 3, 21, 9, 0, 0), User1 = will, User2 = lily},
                new FriendsWith{Id = 9, User1Id = 1, User2Id = 5, Began = new DateTime(2010, 6, 13, 9, 0, 0), User1 = brock, User2 = will},
                new FriendsWith{Id = 10, User1Id = 5, User2Id = 1, Began = new DateTime(2010, 6, 13, 9, 0, 0), User1 = will, User2 = brock},
                new FriendsWith{Id = 11, User1Id = 1, User2Id = 3, Began = new DateTime(2007, 1, 24, 9, 0, 0), User1 = brock, User2 = john},
                new FriendsWith{Id = 12, User1Id = 3, User2Id = 1, Began = new DateTime(2007, 1, 24, 9, 0, 0), User1 = john, User2 = brock}
            };

            List<Collection> cols = new List<Collection>{
                new Collection {Id = 1, Name = "Collection1", UserId = 1, DateMade = new DateTime(2004, 11, 2, 8, 3, 0), User = brock},
                new Collection {Id = 2, Name = "My Fish", UserId = 1, DateMade = new DateTime(2015, 9, 1, 5, 5, 0), User = brock},
                new Collection {Id = 3, Name = "My Beer", UserId = 1, DateMade = new DateTime(2017, 4, 23, 23, 10, 0), User = brock},
                new Collection {Id = 10, Name = "My Games", UserId = 4, DateMade = new DateTime(2013, 5, 6, 4, 7, 0), User = damon},
                new Collection {Id = 11, Name = "Collection3", UserId = 4, DateMade = new DateTime(2009, 2, 3, 7, 5, 0), User = damon},
                new Collection {Id = 12, Name = "My Stickers", UserId = 4, DateMade = new DateTime(2017, 3, 9, 9, 22, 0), User = damon}
            };

            List<IcollectionUser> directs = new List<IcollectionUser>();
            directs.Add(lily);
            directs.Add(john);
            directs.Add(will);

            List<FriendsWith> secondHandFriends = new List<FriendsWith>();
            List<Collection> theirCollections = new List<Collection>();

            Mock<DbSet<FriendsWith>> mockFriendsWithDbSet = GetMockDbSet(friendsWiths.AsQueryable());
            Mock<DbSet<Collection>> mockCollectionDbSet = GetMockDbSet(cols.AsQueryable());

            Mock<ICollectionsDbContext> mockContext = new Mock<ICollectionsDbContext>();
            mockContext.Setup(ctx => ctx.FriendsWiths).Returns(mockFriendsWithDbSet.Object);
            mockContext.Setup(ctx => ctx.Collections).Returns(mockCollectionDbSet.Object);
            mockContext.Setup(ctx => ctx.Set<FriendsWith>()).Returns(mockFriendsWithDbSet.Object);
            mockContext.Setup(ctx => ctx.Set<Collection>()).Returns(mockCollectionDbSet.Object);

            // Arrange
            IFriendsWithRepository friendsRepo = new FriendsWithRepository(mockContext.Object);
            IcollectionRepository collectionRepo = new CollectionRepository(mockContext.Object);
            

            DatabaseHelper.ReadDistantFriends(directs, ref secondHandFriends, theirCollections, 1, friendsRepo, collectionRepo);

            Assert.That(secondHandFriends.Count, Is.EqualTo(3));
            Assert.That(theirCollections.Count, Is.EqualTo(0));
        }
    }

    // these two are the functions i need to test
    // dbHelper.ReadDistantFriends(myFriends, myFriendsFriends, theirCollections, userId);
    // dbHelper.ReadFollowees(whoIFollow, topFollow, followeesCollections, userId);
}