import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { User } from './models/user';
import { UserDetail } from './models/userdetail';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { Router, Routes } from '@angular/router';
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class UserService {

  helper = new JwtHelperService();
  //decodedToken = this.helper.decodeToken(localStorage.getItem("user")!)
  isAdmin?: boolean | false;
  private userSubject: BehaviorSubject<User> | any;
  public user: Observable<User> | any;
  //currentUser: User = {
  //  id: null || this.decodedToken?.userId,
  //  username: null || this.decodedToken?.nameid,
  //  firstName: null || this.decodedToken?.firstName,
  //  lastName: null || this.decodedToken?.lastName,
  //  password: null || this.decodedToken?.password,
  //  role: null || this.decodedToken?.role,
  //  numberGroups: null || this.decodedToken?.numberGroups
  //}

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  private usersUrl = `${environment.dbURL}User`;

  constructor(
    private http: HttpClient,
    private router: Router,
  ) {
    this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')!));
    this.user = this.userSubject.asObservable();
  }

  public get userValue(): User {
    return this.userSubject.value;
  }

  getUser(id: number): Observable<User> {
    const url = `${this.usersUrl}/${id}`;
    return this.http.get<User>(url);
  }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.usersUrl}/all`);
  }

  getUserwithGroup(id: number): Observable<UserDetail> {
    const url = `${this.usersUrl}/userwithgroup/${id}`;
    return this.http.get<UserDetail>(url);
  }

  addUser(user: User): Observable<User> {
    return this.http.post<User>(`${this.usersUrl}/register`, user, this.httpOptions).pipe
      (catchError(this.handleError1));
  }
  handleError1(error: HttpErrorResponse) {
    return throwError(error.error);
  }

  updateUser(id: number, user: User): Observable<any> {
    const url = `${this.usersUrl}/${id}`;
    return this.http.put<User>(url, user, this.httpOptions)
  }
  updateUserWhenLeave(id: number, user: User): Observable<any> {
    const url = `${this.usersUrl}/leavegroup/${id}`;
    return this.http.put<User>(url, user, this.httpOptions)
  }


  deleteUser(id: number): Observable<User> {
    const url = `${this.usersUrl}/${id}`;
    return this.http.delete<User>(url, this.httpOptions);
  }

  login(username: string, password: string) {
    return this.http.post<User>(`${this.usersUrl}/login`, { username, password })
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('user', JSON.stringify(user));
        this.userSubject.next(user);
        if (user.role == "admin") this.isAdmin = true;
        console.log(user.role);
        return user;
      }));
  }

  loggedIn() {
    return localStorage.getItem('user');
  }


  logout() {
    // remove user from local storage and set current user to null
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/login']);
  }
}
