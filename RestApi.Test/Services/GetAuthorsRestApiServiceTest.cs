using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.Entities;
using RestApi.Helpers;
using RestApi.Models;
using RestApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestApi.Test.Services
{
    [TestClass]
    public class GetAuthorsRestApiServiceTest : RestApiServiceBaseTest
    {
        public GetAuthorsRestApiServiceTest()
        {
            SetUpRestApiRepositoryMoq(restApiRepositoryMoq);
        }
        private static void SetUpRestApiRepositoryMoq(Mock<IRestApiRepository> restApiRpositoryMoq)
        {
            var authorRequestDto = new AuthorRequestDto();
            var list = new List<Author>();

            var collection = list.AsQueryable();

            var objectToReturnn = PagedList<Author>.Create(collection, authorRequestDto.PageNumber, authorRequestDto.PageSize);

            restApiRpositoryMoq.Setup(c => c.GetAuthors(It.IsAny<AuthorRequestDto>())).Returns(objectToReturnn);
        }
        [TestMethod]
        public void given_a_invalid_authorId_should_thow_Agument_Exception()
        {
            //Arrange
            AuthorRequestDto authorRequestDto = null;

            try
            {
                // Act on Test
                _restApiService.GetAuthors(authorRequestDto);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("authorRequestDto", ex.ParamName);
            }

        }

        [TestMethod]
        public void given_a_valid_authorId_should_call_repository_once()
        {
            //Arrange
            var authorRequestDto = new AuthorRequestDto();

            // Act on Test
            var response = _restApiService.GetAuthors(authorRequestDto);
            restApiRepositoryMoq.Verify(x => x.GetAuthors(authorRequestDto), Times.Once());

            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(PagedList<Author>));


        }
    }
}
