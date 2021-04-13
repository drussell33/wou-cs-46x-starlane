using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using iCollections.Utilities;

namespace iCollections.Tests.Tests
{
    class TestSplitBySpace
    {
        [SetUp]
        public void Setup()
        { }

        [Test]
        public void SplitBySpace_SplitsUsingSpaceDelimiter_True()
        {
            //arrange
            string input = "this is a test";

            //act
            string[] output = StringUtilities.SplitBySpace(input);

            //assert
            Assert.That(output, Is.EqualTo(new string[] { "this", "is", "a", "test" }));
        }


        [Test]
        public void SplitBySpace_SplitsIntoCorrectNumberOfStrings_True()
        {
            //arrange
            string input = "this is a test";

            //act
            int output = StringUtilities.SplitBySpace(input).Length;

            //assert
            Assert.That(output, Is.EqualTo(4));
        }

        [Test]
        public void SplitBySpace_SplitsStringsUsingSpaceStringLiteral_True()
        {
            //arrange
            string input = $"this{" "}test";

            //act
            string[] output = StringUtilities.SplitBySpace(input);

            //assert
            Assert.That(output, Is.EqualTo(new string[] { "this","test" }));
        }

        [Test]
        public void SplitBySpace_DoesNotSplitsStringWithoutSpace_True()
        {
            //arrange
            string input = "this_is_a_test";

            //act
            string[] output = StringUtilities.SplitBySpace(input);

            //assert
            Assert.That(output, Is.EqualTo(new string[] { "this_is_a_test" }));
        }

        [Test]
        public void SplitBySpace_IgnoresEmptyString_True()
        {
            //arrange
            string input = "";

            //act
            string[] output = StringUtilities.SplitBySpace(input);

            //assert
            Assert.That(output, Is.EqualTo(new string[] { "" }));
        }

        [Test]
        public void SplitBySpace_IgnoresSpacesThatPrecedesWords_True()
        {
            //arrange
            string input = " Test";

            //act
            string[] output = StringUtilities.SplitBySpace(input);

            //assert
            Assert.That(output, Is.EqualTo(new string[] { "Test" }));
        }

        [Test]
        public void SplitBySpace_IgnoresSpacesthatFollowWords_True()
        {
            //arrange
            string input = "Test ";

            //act
            string[] output = StringUtilities.SplitBySpace(input);

            //assert
            Assert.That(output, Is.EqualTo(new string[] { "Test" }));
        }
    }
}
