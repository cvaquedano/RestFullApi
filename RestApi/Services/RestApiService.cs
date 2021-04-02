using RestApi.Entities;
using RestApi.Helpers;
using RestApi.Repositories;
using System;
using System.Collections.Generic;

namespace RestApi.Services
{
    public class RestApiService : IRestApiService
    {
        private readonly IRestApiRepository _restApiRepository;
      

        public RestApiService(IRestApiRepository restApiRepository)
        {
            _restApiRepository = restApiRepository ?? throw new ArgumentNullException(nameof(restApiRepository));

        }

        public void AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            author.Id = Guid.NewGuid();

            foreach (var book in author.Books)
            {
                book.Id = Guid.NewGuid();
            }
            _restApiRepository.AddAuthor(author);
            _restApiRepository.Save();
        }

        public void AddBook(Guid authorId, Book book)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            book.AuthorId = authorId;
            _restApiRepository.AddBook( authorId,  book);
            _restApiRepository.Save();
        }

        public bool AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _restApiRepository.AuthorExists(authorId);
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _restApiRepository.DeleteAuthor(author);
            _restApiRepository.Save();
        }

        public void DeleteBook(Book book)
        {
            _restApiRepository.DeleteBook(book);
            _restApiRepository.Save();
        }



        public Author GetAuthor(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _restApiRepository.GetAuthor(authorId);
        }

        public PagedList<Author> GetAuthors(Models.AuthorRequestDto authorRequestDto)
        {
            if (authorRequestDto is null)
            {
                throw new ArgumentNullException(nameof(authorRequestDto));
            }

            return _restApiRepository.GetAuthors(authorRequestDto);
        }

        public Book GetBook(Guid authorId, Guid bookId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            return _restApiRepository.GetBook(authorId,bookId);
        }

        public IEnumerable<Book> GetBooks(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _restApiRepository.GetBooks(authorId);
        }

        public bool Save()
        {
            return _restApiRepository.Save();
        }

        public void UpdateAuthor(Author author)
        {
            _restApiRepository.UpdateAuthor(author);
        }

        public void UpdateBook(Book book)
        {
            _restApiRepository.UpdateBook(book);
        }

    }
}
