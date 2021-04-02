using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.Entities;
using RestApi.Repositories;
using System;

namespace RestApi.Test.Services
{
    [TestClass]
    public class AuthorExistsRestApiServiceTest : RestApiServiceBaseTest
    {
        public AuthorExistsRestApiServiceTest()
        {
            SetUpRestApiRepositoryMoq(restApiRepositoryMoq);
        }
        private static void SetUpRestApiRepositoryMoq(Mock<IRestApiRepository> restApiRpositoryMoq)
        {
            restApiRpositoryMoq.Setup(c => c.AuthorExists(_invalidadAuthorId)).Returns(false);

            var authorObject = new Author { Id = _validadAuthorId, FirstName = "ValidFirstName", LastName = "ValidLastName" };
            restApiRpositoryMoq.Setup(c => c.AuthorExists(_validadAuthorId)).Returns(true);
        }
        [TestMethod]
        public void given_a_invalid_authorId_should_thow_Agument_Exception()
        {
            //Arrange
            var authorId = Guid.Empty;

            try
            {
                // Act on Test
                var response = _restApiService.AuthorExists(authorId);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(nameof(authorId), ex.ParamName);
            }

        }

        [TestMethod]
        public void given_a_invalid_authorId_should_return_false()
        {
            //Arrange
            var authorId = _invalidadAuthorId;

            // Act on Test
            var response = _restApiService.AuthorExists(authorId);
            Assert.IsFalse(response);
           

        }

        [TestMethod]
        public void given_a_valid_authorId_should_return_true()
        {
            //Arrange
            var authorId = _validadAuthorId;

            // Act on Test
            var response = _restApiService.AuthorExists(authorId);
            Assert.IsTrue(response);


        }
    }
}
