using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNotebook.Site.Models
{
    public class NoteModel
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string UserId { get; set; }
    }
}