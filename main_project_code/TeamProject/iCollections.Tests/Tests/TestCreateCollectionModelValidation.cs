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

        [SetUp]
        public void Setup()
        {
           
        }

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


    }
}