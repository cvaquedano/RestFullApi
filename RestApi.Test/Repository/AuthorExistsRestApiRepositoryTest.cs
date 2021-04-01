using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

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
