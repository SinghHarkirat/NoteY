using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes_maker
{
    public class NoteContext:DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options):base(options)
        {

        }
        public DbSet<Note> Notes { get; set; }
    }
}
