using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Tittle can't be more than 50 characters")]
        [MinLength(5, ErrorMessage = "Tittle must be at least 5 characters")]
        public string Tittle { get; set; } = "";

        [Required]
        [MaxLength(500, ErrorMessage = "Content can't be more than 500 characters")]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters")]
        public string Content { get; set; } = "";
    }
}