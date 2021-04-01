using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.Entities;
using RestApi.Helpers;
using RestApi.Models;
using RestApi.Repositories;
using RestApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestApi.Test
{
    [TestClass]
    public class GetAuthorControllerTest : AuthorControllerBaseTest
    {
      
        private static AuthorRequestDto authorRequestDto = new AuthorRequestDto();

        public GetAuthorControllerTest()
        {
            SetUpRestApiServiceMoq(restApiServiceMoq);
        }

        private static void SetUpRestApiServiceMoq(Mock<IRestApiService> restApiRepositoryMoq)
        {
            var collection = new List<Author> { new Author { } };
            var pagedList = PagedList<Author>.Create(collection.AsQueryable(), authorRequestDto.PageNumber, authorRequestDto.PageSize);
            restApiRepositoryMoq.Setup(c => c.GetAuthors(authorRequestDto)).Returns(pagedList);
        }

        [TestMethod]
        public void given_a_valid_authorRequestDto_should_return_ok_response()
        {
            //Arrange
           

            // Act on Test
            var response = _authorsController.Get(authorRequestDto);

            // Assert the result         
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));

        }

        [TestMethod]
        public void given_a_valid_authorRequestDto_return_should_contain_a_pagination()
        {
            //Arrange


            // Act on Test
            var response = _authorsController.Get(authorRequestDto);

            // Assert the result         
            Assert.IsNotNull(response);
            //Assert.IsTrue(response.Headers.Contains("X-Pagination"));

        }
    }
}
