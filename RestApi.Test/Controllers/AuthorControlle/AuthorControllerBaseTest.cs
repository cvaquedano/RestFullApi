using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using RestApi.Controllers;
using RestApi.Profiles;
using RestApi.Repositories;
using System;

namespace RestApi.Test
{
    public class AuthorControllerBaseTest
    {
        protected readonly IRestApiRepository _restApiRepository;
        private IMapper _mapper;
        protected Mock<IRestApiRepository> restApiRepositoryMoq  = new Mock<IRestApiRepository>();   
        protected readonly AuthorsController _authorsController;

        protected static readonly Guid _invalidadAuthorId = new Guid("8FA566F0-8C0A-4DAA-A0B6-03CAD6D410BE");
        protected static readonly Guid _validadAuthorId = new Guid("9FA566F0-8C0A-4DAA-A0B6-03CAD6D410BE");

        public AuthorControllerBaseTest()
        {
            Mock<IUrlHelper> mockUrlHelper = SetUpMockUrlHelper();
            ControllerContext controllerContext = SetUpControllerContext();

            SetUpMapper();
                        

            _restApiRepository = restApiRepositoryMoq.Object;

            _authorsController = new AuthorsController(_restApiRepository, _mapper)
            {
                ControllerContext = controllerContext,
                Url = mockUrlHelper.Object
            };
        }

        private  ControllerContext SetUpControllerContext()
        {
            var response = new Mock<HttpResponse>();


            var testHeader = new Mock<IHeaderDictionary>();
            testHeader.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<StringValues>()));

            response.Setup(x => x.Headers).Returns(testHeader.Object);

            var httpContext = Mock.Of<HttpContext>(_ =>
                _.Response == response.Object
            );

            //Controller needs a controller context 
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            return controllerContext;
        }

        private  Mock<IUrlHelper> SetUpMockUrlHelper()
        {
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);

            mockUrlHelper.Setup(c => c.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("just a value");
            return mockUrlHelper;
        }

        private void SetUpMapper()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AuthorProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

    }
}
