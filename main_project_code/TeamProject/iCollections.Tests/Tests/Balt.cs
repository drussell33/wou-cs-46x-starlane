using NUnit.Framework;
using iCollections.Controllers;
using iCollections.Models;
using Microsoft.AspNetCore.Mvc;

// Baltazar Ortiz

namespace iCollections.Tests.Tests
{
    public class Balt
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ViewPhotosController_UserWithNoCollectionsReturns_NoCollections()
        {
            var userPageController = new UserPageController(null, null, null, null);
            // var result = userPageController.Index("ZaydenC");
            var result = userPageController.Index("hermes");
            var userProfile = (result as ViewResult).ViewData.Model as UserProfile;

            Assert.That(userProfile.recentCollections.Count, Is.EqualTo(0));
        }

    }
}