using NUnit.Framework;
using iCollections.Controllers;
using iCollections.Models;
using System.Collections.Generic;
using System;
using GetStarted.Tests;

//Derek Russell: User Story for Sprint 4

namespace iCollections.Tests.Tests
{
    public class TestCreateCollectionModelValidation
    {
        private static CreateCollectionRoute MakeValidCollectionRoute()
        {
            return new CreateCollectionRoute
            {
                Route = "gallery_environment"
            };
        }

        private static CreateCollectionPublishing MakeValidCollectionPublishing()
        {
            return new CreateCollectionPublishing
            {
                CollectionName = "My New Collection",
                Visibility = "private",
                Description = null
            };
        }


        [SetUp]
        public void Setup()
        {
           
        }

        // Getting the Form Value for the Collection Route, which denotes the end environment view
        [Test]
        public void CreateCollectionRoute_DefaultIs_NOTValid()
        {
            // Arrange 
            CreateCollectionRoute a = new CreateCollectionRoute();
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void CreateCollectionRoute_RequiredNullablePropertiesAreNullMeansIs_NotValid()
        {
            // Arrange
            CreateCollectionRoute a = new CreateCollectionRoute
            {
                Route = null
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("Route"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void CreateCollectionRoute_AllRequiredPropertiesAreNonNullAndValidValuesIs_Valid()
        {
            // Arrange
            CreateCollectionRoute a = MakeValidCollectionRoute();
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.Valid, Is.True);
        }

        // Derek Russell
        // User Story ID: 177878474, Sprint 5, 4 Points.
        // Getting the Form Value for the Collection Publishing, which denotes the end environment view
        [Test]
        public void CreateCollectionPublishing_DefaultIs_NOTValid()
        {
            // Arrange 
            CreateCollectionPublishing a = new CreateCollectionPublishing();
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.Valid, Is.False);
        }
        [Test]
        public void CreateCollectionPublishing_RequiredFieldsThatAreNullWontWorkForModel_NotValid()
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = null,
                Visibility = null,
                Description = null
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("CollectionName"), Is.True);
            Assert.That(mv.ContainsFailureFor("Visibility"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void CreateCollectionPublishing_AllRequiredPropertiesAreNonNullAndValidValuesIs_Valid()
        {
            // Arrange
            CreateCollectionPublishing a = MakeValidCollectionPublishing();
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.Valid, Is.True);
        }

        [TestCase("I have Special ?!@*&% Characters!")]
        [TestCase("I have Special Characters ! ")]
        [TestCase("I have Special Characters : ")]
        [TestCase("I have Special Characters ? ")]
        [TestCase("I have Special Characters ^ ")]
        [TestCase("I have Special Characters @ ")]
        [TestCase("THis one will have way more then the 30 character limit on the title")]
        [TestCase("No")]
        [TestCase("I")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        public void CreateCollectionPublishing_CollectionNameMustOnlyContainLettersNumbersAndSpaces_NOTValid(string s)
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = s,
                Visibility = "private",
                Description = null
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("CollectionName"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }


        [TestCase("aaa")]
        [TestCase("111")]
        [TestCase("A few")]
        [TestCase("A few more words with numbers1")]
        [TestCase("16543 8755 and words")]
        [TestCase("16543 8755")]
        [TestCase("123456789123456789123456789130")]
        public void CreateCollectionPublishing_COllectionTitleMustbeThreeCHaracterMinAndThirtyMax_IsValid(string s)
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = s,
                Visibility = "private",
                Description = "I have Special accepted Characters"
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("CollectionName"), Is.False);
            Assert.That(mv.Valid, Is.True);
        }




        [TestCase("I have Special ?!@*&% Characters!")]
        [TestCase("I have Special Characters ! ")]
        [TestCase("I have Special Characters : ")]
        [TestCase("I have Special Characters ? ")]
        [TestCase("I have Special Characters ^ ")]
        [TestCase("I have Special Characters @ ")]
        [TestCase("THis one will have way more then the 60 character limit on the description bu tthats jsut the way it goes I think this is over 60 at this point")]
        [TestCase(" ")]
        [TestCase("    ")]
        public void CreateCollectionPublishing_CollectionDescriptionMustOnlyContainLettersNumbersAndSpaces_NOTValid(string s)
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = "I have accepted Characters",
                Visibility = "private",
                Description = s
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("Description"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }




        [TestCase("A")]
        [TestCase(null)]
        [TestCase("A few")]
        [TestCase("A few more words with numbers 111")]
        [TestCase("1")]
        [TestCase("16543 8755 and words")]
        [TestCase("16543 8755")]
        [TestCase("123456789123456789123456789130123456789123456789123456789130")]
        public void CreateCollectionPublishing_CollectionDescriptionMustOnlyContainLettersNumbersAndSpaces_IsValid(string s)
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = "I am a safe Title",
                Visibility = "private",
                Description = s
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("Description"), Is.False);
            Assert.That(mv.Valid, Is.True);
        }



    }
}