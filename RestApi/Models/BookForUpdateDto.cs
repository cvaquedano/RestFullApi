using System.ComponentModel.DataAnnotations;

namespace RestApi.Models
{
    public class BookForUpdateDto
    {
        [Required(ErrorMessage = "You sould fill out a title")]
        [MaxLength(100, ErrorMessage = "The title shouldn't have more than 100 character")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "The description shouldn't have more than 1500 character")]
        public  string Description { get; set; }
    }
}
