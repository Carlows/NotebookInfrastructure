using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using MyNotebook.Domain.Caching;
using MyNotebook.Domain.Entities;
using MyNotebook.Domain.Logging;

namespace MyNotebook.Domain.Repositories.Impl
{
    public class NotesRepository : INotesRepository
    {
        private const string NotesCacheKey = "Notes";

        private MyNotebookDbContext _dbContext;
        private ICacheProvider _cacheProvider;

        public NotesRepository(MyNotebookDbContext dbContext, ICacheProvider cacheProvider)
        {
            _dbContext = dbContext;
            _cacheProvider = cacheProvider;
        }

        public Note GetNote(int id)
        {
            try
            {
                var note = GetAllNotes().SingleOrDefault(n => n.Id == id);
                if (note != null)
                    return note;

                note = _dbContext.Notes.Find(id);
                if (note != null) // Cache missed something that is in the database
                    _cacheProvider.InvalidateCacheItem(NotesCacheKey);

                return note;
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return null;
            }
        }

        public IEnumerable<Note> GetUsersNotes(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return null;

                return GetAllNotes().Where(n => n.UserId.Equals(userId)).ToList();
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return null;
            }
        }

        public int? CreateNote(Note note)
        {
            try
            {
                if (note == null)
                    return null;

                _dbContext.Notes.Add(note);
                _dbContext.SaveChanges();

                _cacheProvider.InvalidateCacheItem(NotesCacheKey);

                return note.Id;
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
                return null;
            }
        }

        public void UpdateNote(Note note)
        {
            try
            {
                if (note == null)
                    return;

                _dbContext.Entry(note).State = EntityState.Modified;
                _dbContext.SaveChanges();

                _cacheProvider.InvalidateCacheItem(NotesCacheKey);
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
            }
        }

        public void DeleteNode(int id)
        {
            try
            {
                var note = GetNote(id);
                if (note == null)
                    return;

                _dbContext.Notes.Remove(note);
                _dbContext.SaveChanges();

                _cacheProvider.InvalidateCacheItem(NotesCacheKey);
            }
            catch (Exception e)
            {
                DomainEventSource.Log.Failure(e.Message);
            }
        }

        private IEnumerable<Note> GetAllNotes()
        {
            return _cacheProvider.TryGet(NotesCacheKey, () =>
                _dbContext.Notes.ToList()) ?? new List<Note>();
        }
    }
}
