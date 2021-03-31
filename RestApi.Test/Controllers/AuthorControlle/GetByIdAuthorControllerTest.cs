using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.Entities;
using RestApi.Repositories;
using System;
using System.Collections.Generic;

namespace RestApi.Test
{
    [TestClass]
    public class GetByIdAuthorControllerTest : AuthorControllerBaseTest
    {

        public GetByIdAuthorControllerTest()
        {
            SetUpRestApiRepositoryMoq(restApiRepositoryMoq);

        }

        private static void SetUpRestApiRepositoryMoq( Mock<IRestApiRepository> restApiRepositoryMoq)
        {         
            restApiRepositoryMoq.Setup(c => c.GetAuthor(_invalidadAuthorId)).Returns((Author)null);

            var authorObject = new Author { Id = _validadAuthorId, FirstName = "ValidFirstName", LastName = "ValidLastName" };
            restApiRepositoryMoq.Setup(c => c.GetAuthor(_validadAuthorId)).Returns(authorObject);
        }


        [TestMethod]
        public void given_a_invalid_authorId_should_return_a_not_found_response()
        {
            //Arrange
            var validMediaType = "application/json";

            // Act on Test
            var response = _authorsController.GetById(_invalidadAuthorId, validMediaType);

            // Assert the result         
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
           
        }
        [TestMethod]
        public void given_a_invalid_media_type_should_return_a_bad_request_response()
        {
            //Arrange
            var invalidMediaType = "invalidMediaType";

            // Act on Test
            var response = _authorsController.GetById(_invalidadAuthorId, invalidMediaType);

            // Assert the result         
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

        }

        [TestMethod]
        public void given_a_valid_authorId_and_valid_media_type_should_return_ok_response()
        {
            //Arrange
            var validMediaType = "application/json";

            // Act on Test
            var response = _authorsController.GetById(_validadAuthorId, validMediaType);

            // Assert the result         
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));

        }

        [TestMethod]
        public void given_a_hateo_media_type_should_contain_a_link_property_on_response()
        {
            //Arrange
            var validMediaType = "application/vnd.hateoas+json";

            // Act on Test
            var response = _authorsController.GetById(_validadAuthorId, validMediaType);
            var result = response as OkObjectResult;
            // Assert the result         
            Assert.IsNotNull(response);
            Assert.IsTrue(((IDictionary<string, object>)result.Value).ContainsKey("links"));

        }
    }
}
