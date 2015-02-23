using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using MyNotebook.Domain.Entities;

namespace MyNotebook.Site.Models
{
    public class ModelFactory
    {
        internal NoteModel Create(Note note)
        {
            return new NoteModel
            {
                Id = note.Id,
                Body = note.Body,
                CreatedDate = note.CreatedDate,
                Heading = note.Heading,
                LastModifiedDate = note.LastModifiedDate,
                UserId = note.UserId
            };
        }
    }
}