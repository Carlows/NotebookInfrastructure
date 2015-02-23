using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNotebook.Domain.Entities;

namespace MyNotebook.Domain.Repositories
{
    public interface INotesRepository
    {
        Note GetNote(int id);
        IEnumerable<Note> GetUsersNotes(string userId);
        int? CreateNote(Note note);
        void UpdateNote(Note note);
        void DeleteNode(int id);
    }
}
