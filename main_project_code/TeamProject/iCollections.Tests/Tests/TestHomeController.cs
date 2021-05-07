using System.Collections.Generic;
using System;
using GetStarted.Tests;
using NUnit.Framework;
using Moq;
using iCollections.Data.Abstract;
using iCollections.Controllers;
using iCollections.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using iCollections.Data;
using iCollections.Data.Concrete;


//Derek Russell: User Story for Sprint 4

namespace iCollections.Tests.Tests
{
    public class TestHomeController
    {

        // a helper to make dbset queryable
        private Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> entities) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator());
            return mockSet;
        }

        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        [Ignore("Ignore a test")]
        public void HomeController_JustGetADamnCollection()
        {
            List<Collection> collections = new List<Collection>
                {
                    new Collection {Id = 10, Name = "First Collection Fish", Visibility = 1, UserId = 8, Route = "Ocean_environment"},
                    new Collection {Id = 13, Name = "Second Collection Dogs", Visibility = 1, UserId = 64, Route = "gallery_environment"},
                    new Collection {Id = 16, Name = "Third Collection Shoes", Visibility = 1, UserId = 3, Route = "gallery_environment"},
                };


            Mock<DbSet<Collection>> mockCollectionDbSet = GetMockDbSet(collections.AsQueryable());

            Mock<ICollectionsDbContext> mockContext = new Mock<ICollectionsDbContext>();
            mockContext.Setup(ctx => ctx.Collections).Returns(mockCollectionDbSet.Object);

            // Arrange 
            IcollectionRepository collectionRepo = new CollectionRepository(mockContext.Object);

            // Act
            Collection selectedCollection = collectionRepo.GetCollectionById(10);

            // Assert
            Assert.That(selectedCollection.Name, Is.EqualTo("First Collection Fish"));
        }



        [Test]
        [Ignore("Ignore a test")]
        public void HomeController_OceanEnvironmentViewModelHasListofPhotos()
        {
            
            // Mock the Collection repository
            //Mock<IcollectionRepository> mockCollectionRepo = new Mock<IcollectionRepository>();
            List<Collection> collections = new List<Collection>
                {
                    new Collection {Id = 10, Name = "First Collection Fish", Visibility = 1, UserId = 8, Route = "Ocean_environment"},
                    new Collection {Id = 13, Name = "Second Collection Dogs", Visibility = 1, UserId = 64, Route = "gallery_environment"},
                    new Collection {Id = 16, Name = "Third Collection Shoes", Visibility = 1, UserId = 3, Route = "gallery_environment"},
                };

            
            List<Photo> photos = new List<Photo>
                {
                    new Photo {Id = 40, Name = "First Photo Fish", Data = new byte[50], UserId = 8, PhotoGuid = new Guid()},
                    new Photo {Id = 41, Name = "Second Photo Fish", Data = new byte[50], UserId = 8, PhotoGuid = new Guid()},
                    new Photo {Id = 45, Name = "Third Photo Shoes", Data = new byte[50], UserId = 3, PhotoGuid = new Guid()},
                    new Photo {Id = 48, Name = "Fourth Photo Dogs", Data = new byte[50], UserId = 64, PhotoGuid = new Guid()},
                };

            List<CollectionPhoto> collectionPhotos = new List<CollectionPhoto>
                {
                    new CollectionPhoto {Id = 80, CollectId = 10, PhotoId = 40, PhotoRank = 1, Title = "First Photo FIsh", Description = "First Description"},
                    new CollectionPhoto {Id = 81, CollectId = 10, PhotoId = 41, PhotoRank = 2, Title = "Second Photo FIsh", Description = "Second Description"},
                    new CollectionPhoto {Id = 82, CollectId = 13, PhotoId = 45, PhotoRank = 1, Title = "Third Photo Shoes", Description = "Third Description"},
                    new CollectionPhoto {Id = 83, CollectId = 16, PhotoId = 48, PhotoRank = 1, Title = "Fourth Photo Dogs", Description = "Fourth Description"},
                };

            Mock<DbSet<Collection>> mockCollectionDbSet = GetMockDbSet(collections.AsQueryable());
            //Mock<DbSet<Photo>> mockPhotoDbSet = GetMockDbSet(photos.AsQueryable());
            //Mock<DbSet<CollectionPhoto>> mockCollectionPhotoDbSet = GetMockDbSet(collectionPhotos.AsQueryable());


            Mock<ICollectionsDbContext> mockContext = new Mock<ICollectionsDbContext>();
            mockContext.Setup(ctx => ctx.Collections).Returns(mockCollectionDbSet.Object);
            //mockContext.Setup(ctx => ctx.Photos).Returns(mockPhotoDbSet.Object);
            //mockContext.Setup(ctx => ctx.CollectionPhotos).Returns(mockCollectionPhotoDbSet.Object);
            // Arrange 
            IcollectionRepository collectionRepo = new CollectionRepository(mockContext.Object);

            // Act
            Collection selectedCollection = collectionRepo.GetCollectionById(10);

            // Assert
            Assert.That(selectedCollection.Name, Is.EqualTo("First Collection Fish"));
        }



        [Test]
        [Ignore("Ignore a test")]
        public void HomeController_OceanEnvironmentViewModelHasListofPhotosFirstTestMethod()
        {
            // Arrange 
            // Mock the Collection repository
            Mock<IcollectionRepository> mockCollectionRepo = new Mock<IcollectionRepository>();
            mockCollectionRepo.Setup(m => m.GetAll()).Returns(new Collection[]
                {
                    new Collection {Id = 10, Name = "First Collection Fish", Visibility = 1, UserId = 8, Route = "Ocean_environment"},
                    new Collection {Id = 13, Name = "Second Collection Dogs", Visibility = 1, UserId = 64, Route = "gallery_environment"},
                    new Collection {Id = 16, Name = "Third Collection Shoes", Visibility = 1, UserId = 3, Route = "gallery_environment"},
                }.AsQueryable<Collection>());

            Mock<IPhotoRepository> mockPhotoRepo = new Mock<IPhotoRepository>();
            mockPhotoRepo.Setup(m => m.GetAll()).Returns(new Photo[]
                {
                    new Photo {Id = 40, Name = "First Photo Fish", Data = null, UserId = 8, PhotoGuid = new Guid()},
                    new Photo {Id = 41, Name = "Second Photo Fish", Data = null, UserId = 8, PhotoGuid = new Guid()},
                    new Photo {Id = 45, Name = "Third Photo Shoes", Data = null, UserId = 3, PhotoGuid = new Guid()},
                    new Photo {Id = 48, Name = "Fourth Photo Dogs", Data = null, UserId = 64, PhotoGuid = new Guid()},
                }.AsQueryable<Photo>());

            Mock<ICollectionPhotoRepository> mockCollectionPhotoRepo = new Mock<ICollectionPhotoRepository>();
            mockCollectionPhotoRepo.Setup(m => m.GetAll()).Returns(new CollectionPhoto[]
                {
                    new CollectionPhoto {Id = 80, CollectId = 10, PhotoId = 40, PhotoRank = 1, Title = "First Photo FIsh", Description = "First Description"},
                    new CollectionPhoto {Id = 81, CollectId = 10, PhotoId = 41, PhotoRank = 2, Title = "Second Photo FIsh", Description = "Second Description"},
                    new CollectionPhoto {Id = 82, CollectId = 13, PhotoId = 45, PhotoRank = 1, Title = "Third Photo Shoes", Description = "Third Description"},
                    new CollectionPhoto {Id = 83, CollectId = 16, PhotoId = 48, PhotoRank = 1, Title = "Fourth Photo Dogs", Description = "Fourth Description"},
                }.AsQueryable<CollectionPhoto>());



            HomeController controller = new HomeController(null, null, null, mockPhotoRepo.Object, mockCollectionRepo.Object, mockCollectionPhotoRepo.Object);

            // Act
            IActionResult result = controller.Ocean_environment(10);
            List<RenderingPhoto> vm = (result as ViewResult).ViewData.Model as List<RenderingPhoto>;

            // Assert
            Assert.That(vm.Count, Is.EqualTo(2));
            //Assert.That(vm[0].Title, Is.EqualTo("First Photo FIsh"));
            //Assert.That(vm[0].Rank, Is.EqualTo(1));
            //Assert.That(vm[0].Description, Is.EqualTo("description1"));
            //Assert.That(vm[0].Data, Is.TypeOf('string'));
        }

        [Test]
        [Ignore("Ignore a test")]
        public void HomeController_OceanEnvironmentViewModelHasListofPhotosFirstTestMethod_FUCKINGWORK()
        {
            // Arrange 
            // Mock the Collection repository
            Mock<IcollectionRepository> mockCollectionRepo = new Mock<IcollectionRepository>();
            mockCollectionRepo.Setup(m => m.GetAll()).Returns(new Collection[]
                {
                    new Collection {Id = 10, Name = "First Collection Fish", Visibility = 1, UserId = 8, Route = "Ocean_environment"},
                    new Collection {Id = 13, Name = "Second Collection Dogs", Visibility = 1, UserId = 64, Route = "gallery_environment"},
                    new Collection {Id = 16, Name = "Third Collection Shoes", Visibility = 1, UserId = 3, Route = "gallery_environment"},
                }.AsQueryable<Collection>());

            mockCollectionRepo.Setup(m => m.GetCollectionById(10)).Returns(new Collection { Id = 10, Name = "First Collection Fish", Visibility = 1, UserId = 8, Route = "Ocean_environment" });

            Mock<IPhotoRepository> mockPhotoRepo = new Mock<IPhotoRepository>();
            mockPhotoRepo.Setup(m => m.GetAll()).Returns(new Photo[]
                {
                    new Photo {Id = 40, Name = "First Photo Fish", Data = null, UserId = 8, PhotoGuid = new Guid()},
                    new Photo {Id = 41, Name = "Second Photo Fish", Data = null, UserId = 8, PhotoGuid = new Guid()},
                    new Photo {Id = 45, Name = "Third Photo Shoes", Data = null, UserId = 3, PhotoGuid = new Guid()},
                    new Photo {Id = 48, Name = "Fourth Photo Dogs", Data = null, UserId = 64, PhotoGuid = new Guid()},
                }.AsQueryable<Photo>());

            Mock<ICollectionPhotoRepository> mockCollectionPhotoRepo = new Mock<ICollectionPhotoRepository>();
            mockCollectionPhotoRepo.Setup(m => m.GetAll()).Returns(new CollectionPhoto[]
                {
                    new CollectionPhoto {Id = 80, CollectId = 10, PhotoId = 40, PhotoRank = 1, Title = "First Photo FIsh", Description = "First Description"},
                    new CollectionPhoto {Id = 81, CollectId = 10, PhotoId = 41, PhotoRank = 2, Title = "Second Photo FIsh", Description = "Second Description"},
                    new CollectionPhoto {Id = 82, CollectId = 13, PhotoId = 45, PhotoRank = 1, Title = "Third Photo Shoes", Description = "Third Description"},
                    new CollectionPhoto {Id = 83, CollectId = 16, PhotoId = 48, PhotoRank = 1, Title = "Fourth Photo Dogs", Description = "Fourth Description"},
                }.AsQueryable<CollectionPhoto>());

           // mockCollectionPhotoRepo.Setup(m => m.GetAllCollectionPhotosbyCollectionId(10)).Returns(List<CollectionPhoto> collectionPhotos = new List<CollectionPhoto>
            //    {
             //       new CollectionPhoto {Id = 80, CollectId = 10, PhotoId = 40, PhotoRank = 1, Title = "First Photo FIsh", Description = "First Description"},
             //       new CollectionPhoto {Id = 81, CollectId = 10, PhotoId = 41, PhotoRank = 2, Title = "Second Photo FIsh", Description = "Second Description"}
             //   };

            HomeController controller = new HomeController(null, null, null, mockPhotoRepo.Object, mockCollectionRepo.Object, mockCollectionPhotoRepo.Object);

            // Arrange 
            //IcollectionRepository collectionRepo = new CollectionRepository(mockContext.Object);

            // Act
            //Collection selectedCollection = collectionRepo.GetCollectionById(10);
           // Collection selectedCollection = mockCollectionRepo.GetCollectionById(10);

            // Assert
            //Assert.That(selectedCollection.Name, Is.EqualTo("First Collection Fish"));
        }



    }
}