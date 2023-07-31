import { Injectable } from '@angular/core';
import { StorageService } from './storage.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { Login } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private currentUserSubject: BehaviorSubject<Login> = new BehaviorSubject<Login>(new Login());

  constructor(private storageService: StorageService) {
    var currUsr = localStorage.getItem('currentUser');

    if (currUsr !== null) {
      this.currentUserSubject = new BehaviorSubject<Login>(JSON.parse(currUsr));
    }
  }

  loginListener(){
    return this.currentUserSubject.asObservable();
  }

  emitLoginStatus(state: Login){
    this.currentUserSubject.next(state);
  }

  saveInStorage(user: Login) {
    this.storageService.setItem('currentUser', user);
    this.emitLoginStatus(user);
  }

  getUserLogin(): Login {
    var log = JSON.parse(this.storageService.getItem('currentUser'));
    return log;
  }

  logOut() {
    this.storageService.removeItem('currentUser');
    this.emitLoginStatus(new Login());
  }

  removeCurrentUser() {
    this.storageService.removeItem('currentUser');
  }
}
