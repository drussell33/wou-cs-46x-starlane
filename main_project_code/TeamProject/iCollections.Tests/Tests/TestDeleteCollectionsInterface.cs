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
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

// Baltazar Ortiz

namespace iCollections.Tests.Tests
{
    public class TestDeleteCollectionsInterface
    {
        Mock<IcollectionRepository> mockCollectionsRepo = new Mock<IcollectionRepository>();

        Mock<IIcollectionUserRepository> mockUsersRepo = new Mock<IIcollectionUserRepository>();

        Mock<IPhotoRepository> mockPhotosRepo = new Mock<IPhotoRepository>();

        [SetUp]
        public void Setup()
        {
            mockCollectionsRepo.Setup(m => m.GetAll()).Returns(new Collection[]{
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

             //mockCollectionsRepo.Setup(m => m.GetMostRecentiCollections(It.IsAny<int>(), It.IsAny<int>()))
             //                .Returns(new Collection[] { }.ToList());

            mockUsersRepo.Setup(m => m.GetAll()).Returns(new IcollectionUser[]{
                new IcollectionUser {Id = 1, FirstName = "Harold", LastName = "Martin", UserName = "haroldo", ProfilePicId = 1},
                new IcollectionUser {Id = 2, FirstName = "Michael", LastName = "Jordan", UserName = "jordan2", ProfilePicId = 2},
                new IcollectionUser {Id = 3, FirstName = "Gerardo", LastName = "Medina", UserName = "medinas", ProfilePicId = 4}
            }.AsQueryable<IcollectionUser>());

            mockUsersRepo.Setup(m => m.GetReadableID(It.IsAny<string>())).Returns(4);

            mockPhotosRepo.Setup(m => m.GetAll()).Returns(new Photo[]{
                new Photo {Id = 1, UserId = 1, PhotoGuid = new Guid(), Name = "prof1"},
                new Photo {Id = 2, UserId = 2, PhotoGuid = new Guid(), Name = "prof2"},
                new Photo {Id = 3, UserId = 3, PhotoGuid = new Guid(), Name = "prof3"}
            }.AsQueryable<Photo>());

        }

        [Test]
        public async Task DeleteCollections_CollectionsOwnerCanGoTo_DeleteScreen()
        {
            // fake the user manager
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "medinas@gmail.com",
                    Id = "aabbcc"
                });

            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("aabbcc");

            // fake the user repo and collections repo (the irepository methods)
            mockUsersRepo.Setup(m => m.GetIcollectionUserByIdentityId(It.IsAny<string>())).Returns(mockUsersRepo.Object.GetAll().ToList()[2]);
            mockCollectionsRepo.Setup(m => m.FindByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(mockCollectionsRepo.Object.GetAll().ToList()[11]));

            // need to handle '_userRepo.GetUserById(id ?? -1);'
            mockUsersRepo.Setup(m => m.GetUserById(It.IsAny<int>())).Returns(mockUsersRepo.Object.GetAll().ToList()[2]);

            var controller = new CollectionsController(null, mockUserManager.Object, mockUsersRepo.Object, null, mockPhotosRepo.Object, null ,mockCollectionsRepo.Object);
            var result = await controller.Delete(12);
            var actualResult = result as ViewResult;
            var collectionToBeDeleted = actualResult.Model as Collection;

            Assert.That(actualResult, Is.Not.Null);
            Assert.That(collectionToBeDeleted.Id, Is.EqualTo(mockCollectionsRepo.Object.GetAll().ToList()[11].Id));
        }

        [Test]
        public async Task DeleteCollections_NotCollectionOwnerGets_Error()
        {
            // fake the user manager
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "medinas@gmail.com",
                    Id = "aabbcc"
                });

            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("aabbcc");

            // fake the user repo and collections repo (the irepository methods)
            mockUsersRepo.Setup(m => m.GetIcollectionUserByIdentityId(It.IsAny<string>())).Returns(mockUsersRepo.Object.GetAll().ToList()[0]);
            mockCollectionsRepo.Setup(m => m.FindByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(mockCollectionsRepo.Object.GetAll().ToList()[12]));

            var controller = new CollectionsController(null, mockUserManager.Object, mockUsersRepo.Object, null, mockPhotosRepo.Object, null,mockCollectionsRepo.Object);
            var result = await controller.Delete(12);
            var actualResult = result as RedirectToActionResult;

            Assert.That(actualResult.ControllerName, Is.EqualTo("Error"));
            Assert.That(actualResult.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task DeleteCollections_CollectionDoesNotExistReturns_Error()
        {
            // fake the user manager
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "medinas@gmail.com",
                    Id = "aabbcc"
                });

            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("aabbcc");

            // fake the user repo and collections repo (the irepository methods)
            mockUsersRepo.Setup(m => m.GetIcollectionUserByIdentityId(It.IsAny<string>())).Returns(mockUsersRepo.Object.GetAll().ToList()[0]);
            mockCollectionsRepo.Setup(m => m.FindByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Collection>(null));

            var controller = new CollectionsController(null, mockUserManager.Object, mockUsersRepo.Object, null, mockPhotosRepo.Object, null, mockCollectionsRepo.Object);
            var result = await controller.Delete(77);
            var actualResult = result as RedirectToActionResult;

            Assert.That(actualResult.ControllerName, Is.EqualTo("Error"));
            Assert.That(actualResult.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public async Task DeleteCollections_PassingInNullReturns_Error()
        {
            // fake the user manager
            var mockStore = new Mock<IUserStore<IdentityUser>>();
            mockStore.Setup(x => x.FindByIdAsync("aabbcc", CancellationToken.None))
                .ReturnsAsync(new IdentityUser()
                {
                    UserName = "medinas@gmail.com",
                    Id = "aabbcc"
                });

            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("aabbcc");

            // fake the user repo and collections repo (the irepository methods)
            mockUsersRepo.Setup(m => m.GetIcollectionUserByIdentityId(It.IsAny<string>())).Returns(mockUsersRepo.Object.GetAll().ToList()[0]);
            mockCollectionsRepo.Setup(m => m.FindByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Collection>(null));

            var controller = new CollectionsController(null, mockUserManager.Object, mockUsersRepo.Object, null, mockPhotosRepo.Object, null ,mockCollectionsRepo.Object);
            var result = await controller.Delete(null);
            var actualResult = result as RedirectToActionResult;

            Assert.That(actualResult.ControllerName, Is.EqualTo("Error"));
            Assert.That(actualResult.ActionName, Is.EqualTo("Index"));
        }
    }
}