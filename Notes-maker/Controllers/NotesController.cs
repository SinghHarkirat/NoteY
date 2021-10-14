using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes_maker.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Notes_maker
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private  List<Note> _notes;
        private readonly INoteRepo _repo;
        public NotesController(INoteRepo repo)
        {
            _notes = new List<Note>();
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            _notes = _repo.GetAllNotes();
            return Ok(_notes);
        }
        [HttpGet("{id}")]
        public IActionResult GetNote(int id)
        {
            var item = _repo.GetNoteById(id);
            if(item==null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult AddNote([FromBody]Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            _repo.InsertNote(note);
            return CreatedAtAction(nameof(Get), note);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var item = _repo.GetNoteById(id);
            if (item == null)
            {
                return NotFound();
            }
            _repo.DeleteNote(id);
            return NoContent();
        }
       
    }
   
}
