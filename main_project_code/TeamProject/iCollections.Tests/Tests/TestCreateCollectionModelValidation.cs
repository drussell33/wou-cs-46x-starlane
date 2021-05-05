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

        //Derek Russell: User Story for Sprint 5

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
                Visibility = "private",
                Description = null
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("CollectionName"), Is.True);
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

        [Test]
        public void CreateCollectionPublishing_CollectionNameMustOnlyContainLettersNumbersAndSpaces_NOTValid()
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = "I have Special ?!@*&% Characters!",
                Visibility = "private",
                Description = null
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("CollectionName"), Is.True);
            Assert.That(mv.ContainsFailureFor("Visibility"), Is.False);
            Assert.That(mv.ContainsFailureFor("Description"), Is.False);
            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void CreateCollectionPublishing_CollectionDescriptionMustOnlyContainLettersNumbersAndSpaces_NOTValid()
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = "I have Special accepted Characters",
                Visibility = "private",
                Description = "I have Special ?!@*&% Characters!"
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("Description"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void CreateCollectionPublishing_CollectionDescriptionMustOnlyContainLettersNumbersAndSpaces_IsValid()
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = "I am a safe Title 10",
                Visibility = "private",
                Description = "I have Special accepted Characters"
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("Description"), Is.False);
            Assert.That(mv.Valid, Is.True);
        }

        [Test]
        public void CreateCollectionPublishing_COllectionTitleMustbeThreeCHaracters_IsValid()
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = "yes",
                Visibility = "private",
                Description = "I have Special accepted Characters"
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("CollectionName"), Is.False);
            Assert.That(mv.Valid, Is.True);
        }

        [Test]
        public void CreateCollectionPublishing_COllectionTitleMustbeThreeCHaractersNot2_NotValid()
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = "no",
                Visibility = "private",
                Description = "I have Special accepted Characters"
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("CollectionName"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void CreateCollectionPublishing_COllectionTitleMustbeThreeCHaractersNot1_NotValid()
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = "I",
                Visibility = "private",
                Description = "I have Special accepted Characters"
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("CollectionName"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void CreateCollectionPublishing_COllectionTitleMustbeUnder30CHaracters_NotValid()
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = "I Have way to many words describing this stuff so I wont work",
                Visibility = "private",
                Description = "I have Special accepted Characters"
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("CollectionName"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }

        [Test]
        public void CreateCollectionPublishing_COllectionDescriptionMustbeUnder60CHaracters_NotValid()
        {
            // Arrange
            CreateCollectionPublishing a = new CreateCollectionPublishing
            {
                CollectionName = "Im a good title",
                Visibility = "private",
                Description = "I have Special accepted Characters But I have way to many things to say about my stuff, and we arent going to let this so the validation will block me as a title"
            };
            // Act
            ModelValidator mv = new ModelValidator(a);
            // Assert
            Assert.That(mv.ContainsFailureFor("Description"), Is.True);
            Assert.That(mv.Valid, Is.False);
        }
    }
}