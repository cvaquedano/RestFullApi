using Moq;
using RestApi.DbContexts;
using RestApi.Repositories;
using System;
using System.Collections.Generic;
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
    }
}
