using iCollections.Controllers;
using iCollections.Data.Abstract;
using iCollections.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace iCollections.Tests.Tests
{
    class TestCollectionsControllerTests
    {
        Mock<IcollectionRepository> mockCollectionsRepo = new Mock<IcollectionRepository>();

        Mock<IFavoriteCollectionRepository> mockFavoriteCollectionsRepo = new Mock<IFavoriteCollectionRepository>();

        Mock<IIcollectionUserRepository> mockUsersRepo = new Mock<IIcollectionUserRepository>();

        Mock<IPhotoRepository> mockPhotosRepo = new Mock<IPhotoRepository>();

        [SetUp]
        public void Setup()
        {
            mockCollectionsRepo.Setup(m => m.GetAll()).Returns(new Collection[]{
                new Collection {Id = 1, Name = "Collection1", UserId = 1, DateMade = new DateTime(2004, 11, 2, 8, 3, 0), Visibility = 1},
                new Collection {Id = 2, Name = "My Fish", UserId = 1, DateMade = new DateTime(2015, 9, 1, 5, 5, 0), Visibility = 1},
                new Collection {Id = 3, Name = "My Beer", UserId = 1, DateMade = new DateTime(2017, 4, 23, 23, 10, 0), Visibility = 1},
                new Collection {Id = 4, Name = "My Tools", UserId = 1, DateMade = new DateTime(2006, 5, 9, 11, 40, 0), Visibility = 1},
                new Collection {Id = 5, Name = "My Friends", UserId = 1, DateMade = new DateTime(2007, 2, 10, 2, 45, 0), Visibility = 1},
                new Collection {Id = 6, Name = "Collection2", UserId = 2, DateMade = new DateTime(2019, 7, 15, 14, 59, 0), Visibility = 1},
                new Collection {Id = 7, Name = "My Trophyies", UserId = 2, DateMade = new DateTime(1996, 8, 22, 19, 54, 0), Visibility = 1},
                new Collection {Id = 8, Name = "My Plants", UserId = 2, DateMade = new DateTime(2005, 12, 17, 1, 32, 0), Visibility = 1},
                new Collection {Id = 9, Name = "My Cards", UserId = 2, DateMade = new DateTime(2006, 4, 14, 22, 23, 0), Visibility = 1},
                new Collection {Id = 10, Name = "My Games", UserId = 2, DateMade = new DateTime(2013, 5, 6, 4, 7, 0), Visibility = 1},
                new Collection {Id = 11, Name = "Collection3", UserId = 3, DateMade = new DateTime(2009, 2, 3, 7, 5, 0), Visibility = 1},
                new Collection {Id = 12, Name = "My Stickers", UserId = 3, DateMade = new DateTime(2017, 3, 9, 9, 22, 0), Visibility = 1},
                new Collection {Id = 13, Name = "My Stamps", UserId = 3, DateMade = new DateTime(2009, 10, 10, 15, 28, 0), Visibility = 1},
                new Collection {Id = 14, Name = "My Posters", UserId = 3, DateMade = new DateTime(2014, 11, 21, 18, 52, 0), Visibility = 1},
                new Collection {Id = 15, Name = "My Funco Pops", UserId = 3, DateMade = new DateTime(2012, 6, 2, 7, 31, 0), Visibility = 1}
            }.AsQueryable<Collection>());

            mockFavoriteCollectionsRepo.Setup(m => m.GetAll()).Returns(new FavoriteCollection[]{
                new FavoriteCollection {Id = 1, DateMade = new DateTime(2018, 01, 05, 2, 44, 55), Name = "My Favorites", Route = "MyFavorites", UserId = 1, CollectId = 11, Visibility = 1},
                new FavoriteCollection {Id = 2, DateMade = new DateTime(2018, 01, 05, 2, 44, 55), Name = "My Favorites", Route = "MyFavorites", UserId = 2, CollectId = 1, Visibility = 1},
                new FavoriteCollection {Id = 3, DateMade = new DateTime(2018, 01, 05, 2, 44, 55), Name = "My Favorites", Route = "MyFavorites", UserId = 3, CollectId = 9, Visibility = 1},
                new FavoriteCollection {Id = 4, DateMade = new DateTime(2018, 01, 05, 2, 44, 55), Name = "My Favorites", Route = "MyFavorites", UserId = 2, CollectId = 6, Visibility = 1},
                new FavoriteCollection {Id = 5, DateMade = new DateTime(2018, 01, 05, 2, 44, 55), Name = "My Favorites", Route = "MyFavorites", UserId = 3, CollectId = 5, Visibility = 1},
                new FavoriteCollection {Id = 6, DateMade = new DateTime(2018, 01, 05, 2, 44, 55), Name = "My Favorites", Route = "MyFavorites", UserId = 2, CollectId = 2, Visibility = 1},
                new FavoriteCollection {Id = 7, DateMade = new DateTime(2018, 01, 05, 2, 44, 55), Name = "My Favorites", Route = "MyFavorites", UserId = 1, CollectId = 7, Visibility = 1},
            }.AsQueryable<FavoriteCollection>());


            mockUsersRepo.Setup(m => m.GetAll()).Returns(new IcollectionUser[]{
                new IcollectionUser {Id = 1, FirstName = "Kareem", LastName = "Marty", UserName = "kmart", ProfilePicId = 1},
                new IcollectionUser {Id = 2, FirstName = "Juniper", LastName = "Lily", UserName = "jlil", ProfilePicId = 2},
                new IcollectionUser {Id = 3, FirstName = "Rose", LastName = "Andrews", UserName = "rosa", ProfilePicId = 3}
            }.AsQueryable<IcollectionUser>());

            mockUsersRepo.Setup(m => m.GetReadableID(It.IsAny<string>())).Returns(4);

            mockPhotosRepo.Setup(m => m.GetAll()).Returns(new Photo[]{
                new Photo {Id = 1, UserId = 1, PhotoGuid = new Guid(), Name = "profile 1"},
                new Photo {Id = 2, UserId = 2, PhotoGuid = new Guid(), Name = "profile 2"},
                new Photo {Id = 3, UserId = 3, PhotoGuid = new Guid(), Name = "profile 3"}
            }.AsQueryable<Photo>());

        }

        [Test]
        public async Task AddCollectionToFavorite_UserCanAddCollectionNotTheirOwn_True()
        {
            // fake the user manager
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "kmart@gmail.com",
                    Id = "aabbcc"
                });

            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("aabbcc");

            // fake the user repo and collections repo (the irepository methods)
            mockUsersRepo.Setup(m => m.GetIcollectionUserByUsername(It.IsAny<string>())).Returns(mockUsersRepo.Object.GetAll().ToList()[0]);
            mockCollectionsRepo.Setup(m => m.FindByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(mockCollectionsRepo.Object.GetAll().ToList()[7]));
            mockUsersRepo.Setup(m => m.GetUserById(It.IsAny<int>())).Returns(mockUsersRepo.Object.GetAll().ToList()[2]);
            mockFavoriteCollectionsRepo.Setup(m => m.GetMyFavoritesByUser(It.IsAny<IcollectionUser>())).Returns((mockFavoriteCollectionsRepo.Object.GetAll().ToList()));




            var controller = new CollectionsController(null, mockUserManager.Object, mockUsersRepo.Object, null, mockPhotosRepo.Object, mockFavoriteCollectionsRepo.Object, mockCollectionsRepo.Object);
            var result = await controller.AddFavoriteAsync(12, "jlil", "kmart");
            var actualResult = result as JsonResult;


            var collectionToBeFavorited = JsonSerializer.Serialize(actualResult.Value);


            Assert.That(collectionToBeFavorited, Is.Not.Null);
            Assert.That(collectionToBeFavorited, Is.EqualTo(@"{""activeuser"":""kmart"",""collection"":12,""result"":""Added to Favorites!""}"));

        }

        [Test]
        public async Task AddCollectionToFavorite_UserCanAddCollectionTheirOwn_True()
        {
            // fake the user manager
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "kmart@gmail.com",
                    Id = "aabbcc"
                });

            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("aabbcc");

            // fake the user repo and collections repo (the irepository methods)
            mockUsersRepo.Setup(m => m.GetIcollectionUserByUsername(It.IsAny<string>())).Returns(mockUsersRepo.Object.GetAll().ToList()[0]);
            mockCollectionsRepo.Setup(m => m.FindByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(mockCollectionsRepo.Object.GetAll().ToList()[0]));
            mockUsersRepo.Setup(m => m.GetUserById(It.IsAny<int>())).Returns(mockUsersRepo.Object.GetAll().ToList()[2]);
            mockFavoriteCollectionsRepo.Setup(m => m.GetMyFavoritesByUser(It.IsAny<IcollectionUser>())).Returns((mockFavoriteCollectionsRepo.Object.GetAll().ToList()));




            var controller = new CollectionsController(null, mockUserManager.Object, mockUsersRepo.Object, null, mockPhotosRepo.Object, mockFavoriteCollectionsRepo.Object, mockCollectionsRepo.Object);
            var result = await controller.AddFavoriteAsync(12, "jlil", "kmart");
            var actualResult = result as JsonResult;


            var collectionToBeFavorited = JsonSerializer.Serialize(actualResult.Value);


            Assert.That(collectionToBeFavorited, Is.Not.Null);
            Assert.That(collectionToBeFavorited, Is.EqualTo(@"{""activeuser"":""kmart"",""collection"":12,""result"":""Added to Favorites!""}"));

        }


        [Test]
        public async Task AddCollectionToFavorite_CanAddCollectionAlreadyInFavorites_False()
        {
            // fake the user manager
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "kmart@gmail.com",
                    Id = "aabbcc"
                });

            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("aabbcc");

            // fake the user repo and collections repo (the irepository methods)
            mockUsersRepo.Setup(m => m.GetIcollectionUserByUsername(It.IsAny<string>())).Returns(mockUsersRepo.Object.GetAll().ToList()[0]);
            mockCollectionsRepo.Setup(m => m.GetICollectionsForThisUser(It.IsAny<int>())).Returns(mockCollectionsRepo.Object.GetAll().ToList());
            mockUsersRepo.Setup(m => m.GetUserById(It.IsAny<int>())).Returns(mockUsersRepo.Object.GetAll().ToList()[2]);
            mockFavoriteCollectionsRepo.Setup(m => m.GetMyFavoritesByUser(It.IsAny<IcollectionUser>())).Returns((mockFavoriteCollectionsRepo.Object.GetAll().ToList()));




            var controller = new CollectionsController(null, mockUserManager.Object, mockUsersRepo.Object, null, mockPhotosRepo.Object, mockFavoriteCollectionsRepo.Object, mockCollectionsRepo.Object);
            var result = await controller.AddFavoriteAsync(0, "jlil", "kmart");
            var actualResult = result as JsonResult;


            var collectionToBeFavorited = JsonSerializer.Serialize(actualResult.Value);


            Assert.That(collectionToBeFavorited, Is.Not.Null);
            Assert.That(collectionToBeFavorited, Is.EqualTo(@"{""activeuser"":""kmart"",""collection"":0,""result"":""Added to Favorites!""}"));

        }


    }
}
