using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.Entities;
using RestApi.Repositories;
using RestApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestApi.Test
{
    [TestClass]
    public class DeleteAuthorControllerTest : AuthorControllerBaseTest
    {
        public DeleteAuthorControllerTest()
        {
            SetUpRestApiRepositoryMoq(restApiServiceMoq);
        }
        private static void SetUpRestApiRepositoryMoq(Mock<IRestApiService> restApiServiceMoq)
        {
            restApiServiceMoq.Setup(c => c.GetAuthor(_invalidadAuthorId)).Returns((Author)null);

            var authorObject = new Author { Id = _validadAuthorId, FirstName = "ValidFirstName", LastName = "ValidLastName" };
            restApiServiceMoq.Setup(c => c.GetAuthor(_validadAuthorId)).Returns(authorObject);
        }

        [TestMethod]
        public void when_delete_a_author_with_valid_author_id_should_return_no_content_result()
        {
            // Arrange 
            var authorId = _validadAuthorId;

            // Act
            var response = _authorsController.DeleteAuthor(authorId);


            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NoContentResult));
        }
        [TestMethod]
        public void when_delete_a_author_with_invalid_author_id_should_return_no_found_result()
        {
            // Arrange 
            var authorId = _invalidadAuthorId;

            // Act
            var response = _authorsController.DeleteAuthor(authorId);


            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }
    }
}
