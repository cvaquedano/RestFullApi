using Microsoft.EntityFrameworkCore;
using Moq;
using RestApi.DbContexts;
using RestApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestApi.Test.Repository
{
    public class RestApiRepositoryBaseTest
    {
        protected readonly RestApiRepository _restApiRepository;

        protected readonly RestApiContext _restApiContext;
        protected Mock<RestApiContext> _restApiContextMoq = new Mock<RestApiContext>();

        protected static readonly Guid _invalidadAuthorId = new Guid("8FA566F0-8C0A-4DAA-A0B6-03CAD6D410BE");
        protected static readonly Guid _validadAuthorId = new Guid("9FA566F0-8C0A-4DAA-A0B6-03CAD6D410BE");


        public RestApiRepositoryBaseTest()
        {

            _restApiContext = _restApiContextMoq.Object;
            _restApiRepository = new RestApiRepository(_restApiContext);
        }

        protected Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            //  dbSet.Setup(x => x.Any(It.IsAny<Expression<Func<T, bool>>>())).Returns(false);

            return dbSet;
        }
    }
}
