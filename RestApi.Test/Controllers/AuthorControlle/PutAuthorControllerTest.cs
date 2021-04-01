using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.Models;
using RestApi.Repositories;
using RestApi.Services;

namespace RestApi.Test
{
    [TestClass]
    public class PutAuthorControllerTest : AuthorControllerBaseTest
    {
        public PutAuthorControllerTest()
        {
            SetUpRestApiRepositoryMoq(restApiServiceMoq);
        }
        private  void SetUpRestApiRepositoryMoq(Mock<IRestApiService> restApiServiceMoq)
        {
            restApiServiceMoq.Setup(c => c.AuthorExists(_invalidadAuthorId)).Returns(false);
            restApiServiceMoq.Setup(c => c.AuthorExists(_validadAuthorId)).Returns(true);
        }


        [TestMethod]
        public void when_put_a_author_with_valid_author_id_should_return_no_content_result()
        {
            // Arrange 
            var authorId = _validadAuthorId;
            var authorDto = new AuthorForUpdateDto { FirstName = "TestName", LastName = "TestLastName" };

            // Act
            var response = _authorsController.UpdateAuthor(authorId,authorDto);
          

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NoContentResult));
        }
        [TestMethod]
        public void when_put_a_author_with_invalid_author_id_should_return_no_found_result()
        {
            // Arrange 
            var authorId = _invalidadAuthorId;
            var authorDto = new AuthorForUpdateDto { FirstName = "TestName", LastName = "TestLastName" };

            // Act
            var response = _authorsController.UpdateAuthor(authorId, authorDto);


            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }
    }
}
