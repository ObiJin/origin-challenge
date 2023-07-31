import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  private storageSub= new Subject<string>();

  watchStorage(): Observable<any> {
    return this.storageSub.asObservable();
  }

  getItem(key: string): string {
    return localStorage.getItem(key) ?? "{}";
  }

  setItem(key: string, data:any) {
    localStorage.setItem(key, JSON.stringify(data));
    this.storageSub.next('changed');
  }

  removeItem(key: string) {
    localStorage.removeItem(key);
    this.storageSub.next('changed');
  }

  updateItem(key: string, data:any){
    localStorage.removeItem(key);
    localStorage.setItem(key, JSON.stringify(data));
    this.storageSub.next('changed');
  }
}
