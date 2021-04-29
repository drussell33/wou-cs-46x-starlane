// Baltazar Ortiz

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
using System.Security.Claims;

namespace iCollections.Tests.Tests
{
    public class TestReadDistantFriends
    {
        [SetUp]
        public void Setup() { }

        public void ReadDistantFriends_GivenNoFriends_Nothing()
        {
            // make a myfriends with no friendships

            // myfriendsfriends, myfriendscollections = new list()

            // mock userId
        }
    }

    // these two are the functions i need to test
    // dbHelper.ReadDistantFriends(myFriends, myFriendsFriends, theirCollections, userId);
    // dbHelper.ReadFollowees(whoIFollow, topFollow, followeesCollections, userId);
}