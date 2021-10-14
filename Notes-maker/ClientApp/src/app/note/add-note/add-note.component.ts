import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Component, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EventEmitter } from 'events';
import { Note } from '../note';
import { NoteService } from '../note.service';

@Component({
  selector: 'app-add-note',
  templateUrl: './add-note.component.html',
  styleUrls: ['./add-note.component.css']
})
export class AddNoteComponent implements OnInit {
  @Input() noteList:Note[];
noteForm:FormGroup;
  constructor(private _service:NoteService) { }

  ngOnInit() {
    
    this.noteForm=new FormGroup({
      noteText:new FormControl('',[Validators.required,Validators.minLength(2)])
    }
    )
  }
  addNote(){
   this._service.addNote(this.noteForm.controls['noteText'].value)
   .subscribe(newNote=>
    {
     console.log(newNote);
      this.noteList.push(newNote);
      this.noteForm.reset();
    }
    );
  }

}
