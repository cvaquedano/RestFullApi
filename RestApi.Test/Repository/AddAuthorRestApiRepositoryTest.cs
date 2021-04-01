using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApi.DbContexts;
using RestApi.Entities;
using RestApi.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestApi.Test.Repository
{
    [TestClass]
    public class AddAuthorRestApiRepositoryTest : RestApiRepositoryBaseTest
    {
        public AddAuthorRestApiRepositoryTest()
        {

        }


        [TestMethod]
        public void given_a_invalid_author_should_thow_Agument_Exception()
        {
            //Arrange           
            Author author = null;           

            try
            {
                // Act on Test
                _restApiRepository.AddAuthor(author);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(nameof(author), ex.ParamName);
            }

        }


        //[TestMethod]
        //public void given_a_valid_author_should_()
        //{
        //    var authorList = new List<Author> { new Author { Id = Guid.NewGuid() } };

        //    // Arrange
        //    var testObject = new Author();

        //    var context = new Mock<RestApiContext>();
        //    var dbSetMock = new Mock<DbSet<Author>>();
        //    context.Setup(x => x.Set<Author>()).Returns(dbSetMock.Object);           

        //    dbSetMock.Setup(d => d.Add(It.IsAny<Author>())).Callback<Author>((s) => authorList.Add(s));

        //    // Act
        //    var repository = new RestApiRepository(context.Object);
        //    repository.AddAuthor(testObject);

        //    //Assert
        //    context.Verify(x => x.Set<Author>());
        //    dbSetMock.Verify(x => x.Add(It.Is<Author>(y => y == testObject)));





           

        //    //var set = GetQueryableMockDbSet(authorList);
        //    //_restApiContextMoq.Setup(c => c.Set<Author>()).Returns(set.Object);

        //    //// Arrange
        //    //var testObject = new Author { Id = Guid.NewGuid(), FirstName = "Test", LastName = "Test" }; 

        //    ////var context = new Mock<DbContext>();

        //    //_restApiContextMoq.Setup(x => x.Set<Author>()).Returns(set.Object);

        //    //// Act
        //    //_restApiRepository.AddAuthor(testObject);

        //    ////Assert
        //    //_restApiContextMoq.Verify(x => x.Set<Author>());
        //    //set.Verify(x => x.Add(It.Is<Author>(y => y == testObject)));

        //}

    }
}
