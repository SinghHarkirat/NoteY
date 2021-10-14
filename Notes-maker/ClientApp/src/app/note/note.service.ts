import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
 
  constructor(private http:HttpClient) {

   }
   getNotes():Observable<any[]>{
    return this.http.get<any[]>(environment.baseUrl+"notes");
   }
   addNote(name:string):Observable<any>{
     return this.http.post<any>(environment.baseUrl+"notes",{name:name});
   }
   deleteNote(id):Observable<any>{
     return this.http.delete(environment.baseUrl+"notes/"+id);
   }
}
