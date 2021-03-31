using System.Collections.Generic;

namespace RestApi.Models
{
    public class AuthorForCreationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

      

        public ICollection<BookForCreationDto> Books { get; set; }
            = new List<BookForCreationDto>();
    }
}
