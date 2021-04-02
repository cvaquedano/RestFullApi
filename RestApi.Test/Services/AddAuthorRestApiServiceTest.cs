using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.Entities;
using RestApi.Repositories;
using System;

namespace RestApi.Test.Services
{
    [TestClass]
    public class AddAuthorRestApiServiceTest : RestApiServiceBaseTest
    {
        public AddAuthorRestApiServiceTest()
        {
            SetUpRestApiRepositoryMoq(restApiRepositoryMoq);
        }
        private static void SetUpRestApiRepositoryMoq(Mock<IRestApiRepository> restApiRpositoryMoq)
        {
            restApiRpositoryMoq.Setup(c => c.AddAuthor( It.IsAny<Author>()));
        }
        [TestMethod]
        public void given_a_invalid_authorId_should_thow_Agument_Exception()
        {
            //Arrange
            Author authorEntity = null;

            try
            {
                // Act on Test
               _restApiService.AddAuthor(authorEntity);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("author", ex.ParamName);
            }

        }

        [TestMethod]
        public void given_a_valid_authorId_should_call_repository_once()
        {
            //Arrange
            var authorObject = new Author { Id = _validadAuthorId, FirstName = "ValidFirstName", LastName = "ValidLastName" };

            // Act on Test
            _restApiService.AddAuthor(authorObject);
            restApiRepositoryMoq.Verify(x => x.AddAuthor(authorObject), Times.Once());


        }
    }
}
