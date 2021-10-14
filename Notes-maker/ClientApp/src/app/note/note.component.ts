import { Component, OnInit } from '@angular/core';
import { Note } from './note';
import { NoteService } from './note.service';

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent implements OnInit {
  notes: Note[];

  constructor(private _service:NoteService) { }

  ngOnInit() {
    this._service.getNotes().subscribe(note=>this.notes=note);
  }

  deleteNote(id){
    const array=this.notes;
    if(confirm("Do you really want to delete?"))
    {
      let value!:Note;
      let index=-1;
      for(var i=0;i<this.notes.length;i++)
      {
        if(this.notes[i].id==id) 
        {
          value=this.notes[i];
          index=i;
        }
      }
    this._service.deleteNote(id).subscribe(x=>{
      this.notes=this.notes.filter((value,index,array:Note[])=> {
        return value.id!==id;
    });
    });
  }
}
}
