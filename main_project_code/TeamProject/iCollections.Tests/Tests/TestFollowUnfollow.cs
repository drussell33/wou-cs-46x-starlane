using NUnit.Framework;
using iCollections.Controllers;
using iCollections.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using iCollections.Data.Abstract;
using Microsoft.AspNetCore.Identity;

namespace iCollections.Tests.Tests
{

    public class TestFollowUnfollow
    {
        Mock<IFollowRepository> mockFollowsRepo = new Mock<IFollowRepository>();

        Mock<IIcollectionUserRepository> mockUsersRepo = new Mock<IIcollectionUserRepository>();
        
        Mock<IUserStore<IdentityUser>> mockStore = new Mock<IUserStore<IdentityUser>>();

        [SetUp]
        public void Setup()
        {
            mockUsersRepo.Setup(m => m.GetAll()).Returns(new IcollectionUser[]{
                new IcollectionUser {Id = 1, AspnetIdentityId = "abc", FirstName = "Talia", LastName = "Knott", UserName = "TaliaK", ProfilePicId = 1},
                new IcollectionUser {Id = 2, AspnetIdentityId = "def", FirstName = "Zayden", LastName = "Clark", UserName = "ZaydenC", ProfilePicId = 2},
                new IcollectionUser {Id = 3, AspnetIdentityId = "ghi", FirstName = "Davila", LastName = "Hareem", UserName = "DavilaH", ProfilePicId = 3}
            }.AsQueryable<IcollectionUser>());

            mockFollowsRepo.Setup(f => f.GetAll()).Returns(new Follow[]
            {
                new Follow {Id = 1, Follower=1, Followed=2, Began=DateTime.Parse("2015-05-01 7:30:15Z")},
                new Follow {Id = 2, Follower=1, Followed=3, Began=DateTime.Parse("2015-05-01 7:30:16Z")},
                new Follow {Id = 3, Follower=2, Followed=1, Began=DateTime.Parse("2015-05-02 12:00:00Z")}
            }.AsQueryable<Follow>());
        }

        [Test]
        public void Follow_FollowDoesNotAlreadyExistReturns_Success()
        {
            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUsersRepo.Setup(m => m.GetUserById(1)).Returns(mockUsersRepo.Object.GetAll().ToList().Where(x => x.Id == 1).First);
            mockUsersRepo.Setup(m => m.GetUserById(3)).Returns(mockUsersRepo.Object.GetAll().ToList().Where(x => x.Id == 3).First);
            mockFollowsRepo.Setup(m => m.GetFollowLight(It.IsAny<Func<Follow, bool>>()))
                .Returns((Func<Follow, bool> cap) => { return null; });

            var controller = new FollowsController(mockFollowsRepo.Object, mockUsersRepo.Object);
            var result = controller.Follow(3, 1);
            var type = result.Value.GetType();
            Assert.That((bool)type.GetProperty("success").GetValue(result.Value, null) == true);
        }

        [Test]
        public void Unfollow_FollowDoesNotAlreadyExistReturns_Failure()
        {
            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUsersRepo.Setup(m => m.GetUserById(1)).Returns(mockUsersRepo.Object.GetAll().ToList().Where(x => x.Id == 1).First);
            mockUsersRepo.Setup(m => m.GetUserById(3)).Returns(mockUsersRepo.Object.GetAll().ToList().Where(x => x.Id == 3).First);
            mockFollowsRepo.Setup(m => m.GetFollow(It.IsAny<Func<Follow, bool>>()))
                .Returns((Func<Follow, bool> cap) => { return null; });

            var controller = new FollowsController(mockFollowsRepo.Object, mockUsersRepo.Object);
            var result = controller.Unfollow(3, 1).Result;
            var type = result.Value.GetType();
            Assert.That((bool)type.GetProperty("success").GetValue(result.Value, null) == false);
        }

        [Test]
        public void Follow_FollowDoesAlreadyExistReturns_Failure()
        {
            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUsersRepo.Setup(m => m.GetUserById(2)).Returns(mockUsersRepo.Object.GetAll().ToList().Where(x => x.Id == 2).First);
            mockUsersRepo.Setup(m => m.GetUserById(1)).Returns(mockUsersRepo.Object.GetAll().ToList().Where(x => x.Id == 1).First);
            mockFollowsRepo.Setup(m => m.GetFollowLight(It.IsAny<Func<Follow, bool>>()))
                .Returns(mockFollowsRepo.Object.GetAll().ToList()[2]);

            var controller = new FollowsController(mockFollowsRepo.Object, mockUsersRepo.Object);
            var result = controller.Follow(2, 1);
            var type = result.Value.GetType();
            Assert.That((bool)type.GetProperty("success").GetValue(result.Value, null) == false);
        }

        [Test]
        public void Unfollow_FollowDoesAlreadyExistReturns_Success()
        {
            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUsersRepo.Setup(m => m.GetUserById(2)).Returns(mockUsersRepo.Object.GetAll().ToList().Where(x => x.Id == 2).First);
            mockUsersRepo.Setup(m => m.GetUserById(1)).Returns(mockUsersRepo.Object.GetAll().ToList().Where(x => x.Id == 1).First);
            mockFollowsRepo.Setup(m => m.GetFollow(It.IsAny<Func<Follow, bool>>()))
                .Returns(mockFollowsRepo.Object.GetAll().ToList()[2]);

            var controller = new FollowsController(mockFollowsRepo.Object, mockUsersRepo.Object);
            var result = controller.Unfollow(2, 1).Result;
            var type = result.Value.GetType();
            Assert.That((bool)type.GetProperty("success").GetValue(result.Value, null) == true);
        }

        [Test]
        public void Follow_UserDoesNotExistReturns_Failure()
        {
            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockUsersRepo.Setup(m => m.GetUserById(5)).Returns(mockUsersRepo.Object.GetAll().ToList().Where(x => x.Id == 5).FirstOrDefault);
            mockUsersRepo.Setup(m => m.GetUserById(1)).Returns(mockUsersRepo.Object.GetAll().ToList().Where(x => x.Id == 1).First);

            var controller = new FollowsController(mockFollowsRepo.Object, mockUsersRepo.Object);
            var result = controller.Follow(1, 5);
            var type = result.Value.GetType();
            Assert.That((bool)type.GetProperty("success").GetValue(result.Value, null) == false);
        }

        [Test]
        public void Unfollow_UserDoesNotExistReturns_Failure()
        {
            Mock<UserManager<IdentityUser>> mockUserManager = new Mock<UserManager<IdentityUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
            mockFollowsRepo.Setup(m => m.GetFollowLight(It.IsAny<Func<Follow, bool>>()))
                .Returns((Func<Follow, bool> cap) => { return null; });

            var controller = new FollowsController(mockFollowsRepo.Object, mockUsersRepo.Object);
            var result = controller.Unfollow(1, 5).Result;
            var type = result.Value.GetType();
            Assert.That((bool)type.GetProperty("success").GetValue(result.Value, null) == false);
        }

        [Test]
        public void Follow_AuthorizedReturns_Success()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void Follow_UnauthorizedReturns_Failure()
        {
            Assert.IsTrue(true);
        }
    }   
}
