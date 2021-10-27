import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { Router, Routes } from '@angular/router';
import { Group } from './models/group';
import { Membership } from './models/membership';
import { BehaviorSubject, Observable, throwError, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  private groupsUrl = `${environment.dbURL}Group`;
  private memberUrl = `${environment.dbURL}Membership`;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    private router: Router,
  ) { }
  createGroup(group: Group) {
    return this.http.post<Group>(`${this.groupsUrl}/create`, group, this.httpOptions).pipe
      (catchError(this.handleError1));
  }
  handleError1(error: HttpErrorResponse) {
    return throwError(error.error);
  }


  //update group - when a member hit join button, numberMember +1
  updateGroup(id: number, group: Group): Observable<any> {
    const url = `${this.groupsUrl}/update/${id}`;
    return this.http.put<Group>(url, group, this.httpOptions)
      ;
  }

  updateGroupWhenLeave(id: number, group: Group): Observable<any> {
    const url = `${this.groupsUrl}/leavegroup/${id}`;
    return this.http.put<Group>(url, group, this.httpOptions);
  }

  CreateMembership(membership: Membership) {
    const url = 'https://localhost:44326/api/Membership/create';
    return this.http.post<Membership>(url, membership, this.httpOptions).pipe
      (catchError(this.handleError2));
  }
  handleError2(error: HttpErrorResponse) {
    return throwError(error.error);
  }


  GetMembership(id: number): Observable<Membership> {
    const url = `${this.memberUrl}/${id}`;
    return this.http.get<Membership>(url);
  }

  deleteGroup(id: number): Observable<Group> {
    const url = `${this.groupsUrl}/delete/${id}`;
    return this.http.delete<Group>(url, this.httpOptions);
  }

  getallGroups(): Observable<Group[]> {
    return this.http.get<Group[]>(`${this.groupsUrl}/all`)
  }

  getGroupById(id: number): Observable<Group> {
    const url = `${this.groupsUrl}/${id}`;
    return this.http.get<Group>(url);
  }
  getGroupIncludingPosts(id: number): Observable<Group> {
    const url = `${this.groupsUrl}/groupwithposts/${id}`;
    return this.http.get<Group>(url);
  }

  deleteMembership(userid: number, groupid: number): Observable<Membership> {
    const url = `${this.memberUrl}/delete/${userid}&&${groupid}`;
    return this.http.delete<Membership>(url, this.httpOptions);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      //this.log(`${operation} failed: ${error.message}`);
      console.log(operation); //create message service

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
