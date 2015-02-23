using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using MyNotebook.Domain.Entities;
using MyNotebook.Domain.Repositories;
using MyNotebook.Site.Models;

namespace MyNotebook.Site.Controllers
{
    /// <summary>
    /// Endpoint for getting note related objects.
    /// </summary>
    [RoutePrefix("api/Notes")]
    public class NotesController : ApiController
    {
        private INotesRepository _notesRepository;
        private ModelFactory _modelFactory;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="notesRepository">the notes repository to use</param>
        /// <param name="modelFactory">the model factory to use</param>
        public NotesController(INotesRepository notesRepository, ModelFactory modelFactory)
        {
            _notesRepository = notesRepository;
            _modelFactory = modelFactory;
        }

        [Route("", Name="GetNotesByUserId")]
        public IEnumerable<NoteModel> GetUsersNotes()
        {
            return _notesRepository.GetUsersNotes(User.Identity.GetUserId()).Select(_modelFactory.Create);
        }

    }
}