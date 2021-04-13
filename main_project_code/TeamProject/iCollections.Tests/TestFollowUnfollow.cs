using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCollections.Tests
{
    public class TestFollowUnfollow
    {
        [SetUp]
        public void Setup()
        {
        }

        public void SetupFollows()
        {
        }

        [Test]
        public void Follow_FollowDoesNotAlreadyExistReturns_Success()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void Follow_FollowDoesAlreadyExistReturns_Failure()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void Follow_UserDoesNotExistReturns_Failure()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void Follow_AuthorizedReturns_Success()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void Follow_UnauthorizedReturns_Failure()
        {
            Assert.IsTrue(false);
        }
    }   
}
