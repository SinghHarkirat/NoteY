using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes_maker.Interfaces
{
    public interface INoteRepo
    {
        List<Note> GetAllNotes();
        Note GetNoteById(int id);

        void InsertNote(Note note);

        void DeleteNote(int id);
    }
}
