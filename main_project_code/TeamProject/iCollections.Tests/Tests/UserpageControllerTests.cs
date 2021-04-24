using NUnit.Framework;
using iCollections.Controllers;
using iCollections.Models;
using Microsoft.AspNetCore.Mvc;
using iCollections.Data.Abstract;
using Moq;

// Baltazar Ortiz

namespace iCollections.Tests.Tests
{
    public class UserpageControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UserpageController_UserWithNoCollectionsReturns_NoCollections()
        {
            Mock<IcollectionRepository> mockUserRepo = new Mock<IcollectionRepository>();
            // mockUserRepo.Setup(m => m.GetMostRecentiCollections(It.IsAny<int>(), It.IsAny<int>()))
            //             .Returns(null);

            var userPageController = new UserPageController(null, null);
            // var result = userPageController.Index("ZaydenC");
            var result = userPageController.Index("hermes");
            var userProfile = (result as ViewResult).ViewData.Model as UserProfile;

            Assert.That(userProfile.recentCollections.Count, Is.EqualTo(0));
        }

    }
}