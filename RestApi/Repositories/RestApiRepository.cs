using RestApi.DbContexts;
using RestApi.Entities;
using RestApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Repositories
{
    public class RestApiRepository : IRestApiRepository, IDisposable
    {
        private readonly RestApiContext _context;

        public RestApiRepository(RestApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
          
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

            _context.Authors.Add(author);
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
            _context.Books.Add(book);
        }

        public bool AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Authors.Any(a => a.Id == authorId);
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Remove(author);
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }

      

        public Author GetAuthor(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Authors.FirstOrDefault(a => a.Id == authorId);
        }

        public PagedList<Author> GetAuthors(Models.AuthorRequestDto authorRequestDto)
        {
            if (authorRequestDto is null)
            {
                throw new ArgumentNullException(nameof(authorRequestDto));
            }

            var collection = _context.Authors as IQueryable<Author>;


            if (!string.IsNullOrWhiteSpace(authorRequestDto.SearchQuery))
            {
                authorRequestDto.SearchQuery = authorRequestDto.SearchQuery.Trim();
                collection = collection.Where(a => a.FirstName.Contains(authorRequestDto.SearchQuery)
                || a.LastName.Contains(authorRequestDto.SearchQuery));
            }

            //if (!string.IsNullOrWhiteSpace(authorRequestDto.OrderBy))
            //{
            //    var authorPropertyMappingDiccionary = _propertyMappingService.GetPropertyMapping<AuthorDto, Author>();

            //    collection = collection.ApplySort(authorRequestDto.OrderBy, authorPropertyMappingDiccionary);
            //}
            return PagedList<Author>.Create(collection, authorRequestDto.PageNumber, authorRequestDto.PageSize);

           
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

            return _context.Books
              .Where(c => c.AuthorId == authorId && c.Id == bookId).FirstOrDefault();
        }

        public IEnumerable<Book> GetBooks(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Books
                        .Where(c => c.AuthorId == authorId)
                        .OrderBy(c => c.Title).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateAuthor(Author author)
        {
           // throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            //throw new NotImplementedException();
        }

        public void Dispose()
        {
            //Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
