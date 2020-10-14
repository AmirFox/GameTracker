import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import {catchError } from 'rxjs/operators'
import { Router } from '@angular/router';
import { Game } from '../models/game';

@Injectable()
export class GameService {
  myAppUrl: string = "";

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.myAppUrl = baseUrl;
  }

  getGames(): Observable<any> {
    return this._http.get(this.myAppUrl + 'api/games/list')
      .pipe(catchError(this.errorHandler));
  }

  getGameById(id): Observable<any>{
    return this._http.get(this.myAppUrl + 'api/games/game/' + id)
      .pipe(catchError(this.errorHandler));
  }

  addGame(game): Observable<any> {
    return this._http.post(this.myAppUrl + 'api/games/add', JSON.stringify(game), this.httpOptions)
      .pipe(catchError(this.errorHandler));
  }

  updateGame(game): Observable<any> {
    return this._http.put(this.myAppUrl + 'api/games/update', JSON.stringify(game), this.httpOptions)
      .pipe(catchError (this.errorHandler));
  }

  deleteGame(id): Observable<any> {
    return this._http.delete(this.myAppUrl + 'api/games/delete/' + id)
      .pipe(catchError(this.errorHandler));
  }

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}