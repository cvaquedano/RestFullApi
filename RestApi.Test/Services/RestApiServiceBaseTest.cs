using Moq;
using RestApi.Repositories;
using RestApi.Services;
using System;

namespace RestApi.Test.Services
{
    public class RestApiServiceBaseTest
    {
        protected readonly IRestApiRepository _restApiRepository;       
        protected Mock<IRestApiRepository> restApiRepositoryMoq = new Mock<IRestApiRepository>();

        protected readonly RestApiService _restApiService;

        protected static readonly Guid _invalidadAuthorId = new Guid("8FA566F0-8C0A-4DAA-A0B6-03CAD6D410BE");
        protected static readonly Guid _validadAuthorId = new Guid("9FA566F0-8C0A-4DAA-A0B6-03CAD6D410BE");


        public RestApiServiceBaseTest()
        {
            _restApiRepository = restApiRepositoryMoq.Object;

            _restApiService = new RestApiService(_restApiRepository);
        }
    }
}
