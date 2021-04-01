using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestApi.Entities;
using RestApi.Helpers;
using RestApi.Models;
using RestApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using RestApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IRestApiService _restApiService;
        private readonly IMapper _mapper;

        public AuthorsController(IRestApiService restApiService, IMapper mapper)
        {
            _mapper = mapper ??
              throw new ArgumentException(nameof(mapper));
            _restApiService = restApiService ?? throw new ArgumentNullException(nameof(restApiService));
        }
        // GET: api/<AuthorsController>
        [HttpGet(Name = "GetAuthors")]
        public IActionResult Get([FromQuery] AuthorRequestDto authorRequestDto)
        {
            var authorsFromRepo = _restApiService.GetAuthors(authorRequestDto);
            var paginationMetadata = new
            {
                totalCount = authorsFromRepo.TotalCount,
                pageSize = authorsFromRepo.PageSize,
                currentPage = authorsFromRepo.CurrentPage,
                totalPages = authorsFromRepo.TotalPages,

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));

            var links = CreateLinksForAuthors(authorRequestDto, authorsFromRepo.HasNext, authorsFromRepo.HasPrevious);
            var authorToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo).ShapeData();

            var shapedAuthorsWithLinks = authorToReturn.Select(author =>
            {
                var authorAsDictionary = author as IDictionary<string, object>;
                var authorLinks = CreateLinksForAuthor((Guid)authorAsDictionary["Id"]);
                authorAsDictionary.Add("links", authorLinks);
                return authorAsDictionary;

            });

            var linkedCollectionResource = new
            {
                value = shapedAuthorsWithLinks,
                links
            };


            return Ok(linkedCollectionResource);
        }

        [Produces("application/json", "application/vnd.hateoas+json")]
        [HttpGet("{authorId:guid}", Name = "GetAuthor")]
        public IActionResult GetById(Guid authorId, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parseMediaType))
            {
                return BadRequest();
            }

            var authorFromRepo = _restApiService.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            var includeLinks = parseMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            IEnumerable<LinkDto> links = new List<LinkDto>();

            if (includeLinks)
            {
                links = CreateLinksForAuthor(authorId);
            }


            var authorToReturn = _mapper.Map<AuthorDto>(authorFromRepo).ShapeData() as IDictionary<string, object>;


            if (includeLinks)
            {
                authorToReturn.Add("links", links);
            }

            return Ok(authorToReturn);
        }

        // POST api/<AuthorsController>
        [HttpPost(Name = "CreateAuthor")]
        [Consumes("application/vnd.cvaque+json")]
        public ActionResult<AuthorDto> CreateAuthor([FromBody] AuthorForCreationDto author)
        {
           
            var authorEntity = _mapper.Map<Author>(author);
            _restApiService.AddAuthor(authorEntity);
            _restApiService.Save();

            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);

            var links = CreateLinksForAuthor(authorToReturn.Id);

            var linkedResourceToReturn = authorToReturn.AsDictionary();

            linkedResourceToReturn.Add("links", links);

            return CreatedAtRoute("GetAuthor",
                new { authorId = linkedResourceToReturn["Id"] },
                linkedResourceToReturn);

           
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{authorId}")]
        public IActionResult UpdateAuthor(Guid authorId, AuthorForUpdateDto author)
        {
            if (!_restApiService.AuthorExists(authorId))
            {
                return NotFound();
            }
            var authorFromRepo = _restApiService.GetAuthor(authorId);

            _mapper.Map(author, authorFromRepo);
            _restApiService.UpdateAuthor(authorFromRepo);
            _restApiService.Save();

            return NoContent();
        }

      

        [HttpDelete("{authorId}", Name = "DeleteAuthor")]
        public ActionResult DeleteAuthor(Guid authorId)
        {


            var authorFromRepo = _restApiService.GetAuthor(authorId);
            if (authorFromRepo == null)
            {
                return NotFound();

            }
            _restApiService.DeleteAuthor(authorFromRepo);
            _restApiService.Save();

            return NoContent();

        }


        private IEnumerable<LinkDto> CreateLinksForAuthor(Guid authorId)
        {
            var links = new List<LinkDto>();
            var url = Url.Link("GetAuthor", new { authorId });
            var linkDto = new LinkDto(url, "self", "GET");
            links.Add(linkDto);

            links.Add(new LinkDto(Url.Link("DeleteAuthor", new { authorId }), "delete_author", "DELETE"));

            links.Add(new LinkDto(Url.Link("CreateBookForAuthor", new { authorId }), "create_book_for_author", "POST"));

            links.Add(new LinkDto(Url.Link("GetBooksForAuthor", new { authorId }), "books", "GET"));

            return links;

        }
        private IEnumerable<LinkDto> CreateLinksForAuthors(AuthorRequestDto authorRequestDto, bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDto>
            {
                new LinkDto(CreateAuthorsResourceUri(authorRequestDto, ResourceUriType.Current),
                "self",
                "GET")
            };

            if (hasPrevious)
            {
                links.Add(new LinkDto(CreateAuthorsResourceUri(authorRequestDto, ResourceUriType.PreviousPage),
                "previousPage",
                "GET"));
            }
            if (hasNext)
            {
                links.Add(new LinkDto(CreateAuthorsResourceUri(authorRequestDto, ResourceUriType.NextPage),
                "nextPage",
                "GET"));
            }


            return links;
        }

        private string CreateAuthorsResourceUri(AuthorRequestDto authorRequestDto,
          ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetAuthors",
                        new
                        {
                           
                            pageNumber = authorRequestDto.PageNumber - 1,
                            pageSize = authorRequestDto.PageSize,
                            searchQuery = authorRequestDto.SearchQuery

                        });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAuthors",
                        new
                        {
                           
                            pageNumber = authorRequestDto.PageNumber + 1,
                            pageSize = authorRequestDto.PageSize,
                            searchQuery = authorRequestDto.SearchQuery

                        });

                case ResourceUriType.Current:
                default:
                    return Url.Link("GetAuthors",
                       new
                       {
                          
                           pageNumber = authorRequestDto.PageNumber,
                           pageSize = authorRequestDto.PageSize,
                           searchQuery = authorRequestDto.SearchQuery

                       });
            }
        }


    }
}
