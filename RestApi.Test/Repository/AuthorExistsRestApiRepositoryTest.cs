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
            var set = new Mock<DbSet<Author>>();

            var authorList = new List<Author> { new Author { Id = Guid.NewGuid() } };

            var set1 = GetQueryableMockDbSet(authorList);


            _restApiContextMoq.Setup(c => c.Set<Author>()).Returns(set1);
            //_restApiContextMoq.Setup(c => c.Authors.Any(It.IsAny<Expression<Func<Author, bool>>>())).Returns(false);

        }

        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
          //  dbSet.Setup(x => x.Any(It.IsAny<Expression<Func<T, bool>>>())).Returns(false);

            return dbSet.Object;
        }

        [TestMethod]
        public void given_a_valid_authorRequestDto_should_return_ok_response()
        {
            //Arrange
            var authorId = Guid.Empty;

            // Act on Test
            var response = _restApiRepository.AuthorExists(authorId);

            // Assert the result         
            //Assert.(response);

        }
    }
}
