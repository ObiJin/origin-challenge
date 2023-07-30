import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HeadersService {

  constructor() { }
  
  getAuthorizationHeader(token: string) : object{
    return { Authorization: `Bearer ${token}`,  "Access-Control-Allow-Origin": "*" };
  }

  getCorsHeader(){
    return { "Access-Control-Allow-Origin": "*" }
  }
}