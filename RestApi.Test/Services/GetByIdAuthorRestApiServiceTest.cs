using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.Entities;
using RestApi.Repositories;
using System;

namespace RestApi.Test.Services
{
    [TestClass]
    public class GetByIdAuthorRestApiServiceTest : RestApiServiceBaseTest
    {
        public GetByIdAuthorRestApiServiceTest()
        {
            SetUpRestApiRepositoryMoq(restApiRepositoryMoq);
        }
        private static void SetUpRestApiRepositoryMoq(Mock<IRestApiRepository> restApiRpositoryMoq)
        {
            restApiRpositoryMoq.Setup(c => c.GetAuthor(_validadAuthorId)).Returns(new Author { Id= _validadAuthorId });
        }
        [TestMethod]
        public void given_a_invalid_authorId_should_thow_Agument_Exception()
        {
            //Arrange
            var authorId = Guid.Empty;

            try
            {
                // Act on Test
                _restApiService.GetAuthor(authorId);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("authorId", ex.ParamName);
            }

        }

        [TestMethod]
        public void given_a_valid_authorId_should_call_repository_once()
        {
            //Arrange
            var authorId = _validadAuthorId;

            // Act on Test
            var response = _restApiService.GetAuthor(authorId);
            restApiRepositoryMoq.Verify(x => x.GetAuthor(authorId), Times.Once());

            Assert.AreEqual(response.Id, authorId);


        }
    }
}
