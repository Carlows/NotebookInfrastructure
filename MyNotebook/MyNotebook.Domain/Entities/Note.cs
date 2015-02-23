using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotebook.Domain.Entities
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Heading { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
        [Required]
        [StringLength(36)]
        public string UserId { get; set; }
    }
}
