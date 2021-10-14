using Notes_maker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes_maker.Services
{
    public class NoteService : INoteRepo
    {
        private readonly NoteContext _db;
        public NoteService(NoteContext db)
        {
            _db = db;
        }
       

        public List<Note> GetAllNotes()
        {
            return _db.Notes.ToList();
        }

        public Note GetNoteById(int id)
        {
            return _db.Notes.Where(x => x.Id == id).FirstOrDefault();
        }

        public void InsertNote(Note note)
        {
            _db.Notes.Add(note);
            _db.SaveChanges();
        }
        public void DeleteNote(int id)
        {
            var note = _db.Notes.Where(x => x.Id == id).FirstOrDefault();
            _db.Notes.Remove(note);
            _db.SaveChanges();
        }
    }
}
