using NUnit.Framework;
using iCollections.Controllers;
using iCollections.Models;
using System.Collections.Generic;

namespace iCollections.Tests
{
    public class TestRemoveDuplicates
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RemoveDuplicate_OneFriendshipRemoves_None()
        {
            List<FriendsWith> list = new List<FriendsWith>();

            Assert.Pass();
        }
    }
}