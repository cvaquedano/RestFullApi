using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestApi.Entities;
using RestApi.Models;
using RestApi.Repositories;
using System;
using System.Collections.Generic;

namespace RestApi.Controllers
{


    [ApiController]
    [Route("api/authors/{authorId}/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IRestApiRepository _restApiRepository;
        private readonly IMapper _mapper;
        public BooksController(IRestApiRepository restApiRepository, IMapper mapper)
        {
            _mapper = mapper ??  throw new ArgumentException(nameof(mapper));
            _restApiRepository = restApiRepository ?? throw new ArgumentNullException(nameof(restApiRepository));
        }

        [HttpGet(Name = "GetBooksForAuthor")]
        public ActionResult<IEquatable<BookDto>> GetBooksForAuthor(Guid authorId)
        {
            if (!_restApiRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var booksForAuhthorFromRepo = _restApiRepository.GetBooks(authorId);
            return Ok(_mapper.Map<IEnumerable<BookDto>>(booksForAuhthorFromRepo));
        }

        [HttpGet("{bookId:guid}", Name = "GetBookForAuthor")]
        public ActionResult<IEquatable<BookDto>> GetBooksByAuthor(Guid authorId, Guid bookId)
        {
            if (!_restApiRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var booksForAuhthorFromRepo = _restApiRepository.GetBook(authorId, bookId);

            if (booksForAuhthorFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BookDto>(booksForAuhthorFromRepo));
        }

        [HttpPost(Name = "CreateBookForAuthor")]
        public ActionResult<BookDto> CreateBookForAuthor(Guid authorId, BookForCreationDto book)
        {
            if (!_restApiRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var bookEntity = _mapper.Map<Book>(book);
            _restApiRepository.AddBook(authorId, bookEntity);
            _restApiRepository.Save();

            var bookToReturn = _mapper.Map<BookDto>(bookEntity);
            return CreatedAtRoute("GetBookForAuthor", new { authorId, bookId = bookToReturn.Id }, bookToReturn);

        }

        [HttpPut("{BookId}")]
        public IActionResult UpdateBookForAuthor(Guid authorId,
            Guid bookId,
            BookForUpdateDto book)
        {
            if (!_restApiRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var bookForAuthorFromRepo = _restApiRepository.GetBook(authorId, bookId);
            if (bookForAuthorFromRepo == null)
            {
                var bookToAdd = _mapper.Map<Book>(book);
                bookToAdd.Id = bookId;
                _restApiRepository.AddBook(authorId, bookToAdd);
                _restApiRepository.Save();

                var bookToReturn = _mapper.Map<BookDto>(bookToAdd);
                return CreatedAtRoute("GetBookForAuthor",
                    new { authorId, bookId = bookToReturn.Id },
                    bookToReturn);
            }

            _mapper.Map(book, bookForAuthorFromRepo);
            _restApiRepository.UpdateBook(bookForAuthorFromRepo);
            _restApiRepository.Save();

            return NoContent();
        }

        

        [HttpDelete("{bookId}")]

        public ActionResult DeleteBookForAuthor(Guid authorId, Guid bookId)
        {
            if (!_restApiRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var bookForAuthorFromRepo = _restApiRepository.GetBook(authorId, bookId);
            if (bookForAuthorFromRepo == null)
            {
                return NotFound();

            }

            _restApiRepository.DeleteBook(bookForAuthorFromRepo);
            _restApiRepository.Save();

            return NoContent();

        }

    }
}
