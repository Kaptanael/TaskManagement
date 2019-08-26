import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IUser } from '../_models/user';

@Injectable({
    providedIn: 'root'
})
export class UserService {
    baseUrl = environment.apiUrl + 'users/';

    constructor(private http: HttpClient) { }

    getUserNames(): Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'usernames');
    }

    getUsers(): Observable<IUser[]> {
        return this.http.get<IUser[]>(this.baseUrl + 'users');
    }

    getUser(id): Observable<IUser> {
        return this.http.get<IUser>(this.baseUrl + 'user/' + id);
    }
}