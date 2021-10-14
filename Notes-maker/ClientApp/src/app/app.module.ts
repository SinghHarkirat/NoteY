import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NoteComponent } from './note/note.component';
import { AddNoteComponent } from './note/add-note/add-note.component';


@NgModule({
  declarations: [
    AppComponent,
    NoteComponent,
    AddNoteComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {path:'',component:NoteComponent },
     
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
