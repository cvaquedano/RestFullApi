using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Entities;
using System;
using System.Collections.Generic;

namespace RestApi.Test.Repository
{
    [TestClass]
    public class AuthorExistsRestApiRepositoryTest : RestApiRepositoryBaseTest
    {
        public AuthorExistsRestApiRepositoryTest()
        {          

            var authorList = new List<Author> { new Author { Id = Guid.NewGuid() } };

            var set = GetQueryableMockDbSet(authorList);
            _restApiContextMoq.Setup(c => c.Set<Author>()).Returns(set.Object);

        }

       

        [TestMethod]
        public void given_a_invalid_authorId_should_thow_Agument_Exception()
        {
            //Arrange
            var authorId = Guid.Empty;

            try
            {
                // Act on Test
                var response = _restApiRepository.AuthorExists(authorId);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(nameof(authorId), ex.ParamName);
            }

        }
    }
}
