using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.Entities;
using RestApi.Models;
using RestApi.Repositories;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestApi.Test
{
    [TestClass]
    public class PostAuthorControllerTest : AuthorControllerBaseTest
    {
        public PostAuthorControllerTest()
        {

        }

        [TestMethod]
        public void when_post_new_author_should_return_Route_name_GetAuthor()
        {
            // Arrange           
            var authorDto = new AuthorForCreationDto { FirstName = "TestName", LastName="TestLastName" };

            // Act
            var response = _authorsController.CreateAuthor(authorDto);
            var createdResult = response.Result as CreatedAtRouteResult;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetAuthor", createdResult.RouteName);
            Assert.AreEqual(createdResult.StatusCode, 201);
        }
    }


}
