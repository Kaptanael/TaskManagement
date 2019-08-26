import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { ITask } from '../_models/task';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  baseUrl = environment.apiUrl + 'tasks/';

  constructor(private http: HttpClient) { }

  getTasks(): Observable<ITask[]> {
    return this.http.get<ITask[]>(this.baseUrl + 'tasks');
  }

  getTask(id: number): Observable<ITask> {
    return this.http.get<ITask>(this.baseUrl + 'tasks/' + id);
  }

   getTasksByUserId(id: number): Observable<ITask[]> {
    return this.http.get<ITask[]>(this.baseUrl + 'tasksByUser/' + id);         
   }

  saveTask(task: ITask) {
    return this.http.post(this.baseUrl + 'addTask', task);
  }

  updateTask(task: ITask) {
    return this.http.put(this.baseUrl + 'updateTask', task);
  }

  deleteTask(id: number) {
    return this.http.delete(this.baseUrl + 'deleteTask/' + id);
  }

  isExistTaskName(name: string): Observable<HttpResponse<any>> {
    return this.http.get(this.baseUrl + 'taskNameExist', {
      params: new HttpParams()
        .set('name', name)
      , observe: 'response'
    });
  }

  isExistOldTaskName(oldName: string, name: string): Observable<HttpResponse<any>> {
    return this.http.get(this.baseUrl + 'oldTaskNameExist', {
      params: new HttpParams()
        .set('oldName', oldName)
        .set('name', name)
      , observe: 'response'
    });
  }
}
