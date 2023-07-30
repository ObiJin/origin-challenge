import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { LoginService } from './login.service';

@Injectable({
  providedIn: 'root'
})
export class CardsService {

  constructor(private baseService: BaseService, private loginService: LoginService) {
  }

  get(cardNumber: string, success: any = null, error: any = null) {

    const options = {
      url: `/cards/${cardNumber}`,
      body: "",
      success: success,
      error: error
    }

    return this.baseService.get(options);
  }

  block(body: any, success: any = null, error: any = null) {
    const options = {
      url: `/cards/block`,
      body: body,
      success: success,
      error: error
    }

    return this.baseService.post(options);
  }

  login(body: any, success: any = null, error: any = null) {
    const options = {
      url: `/login`,
      body: body,
      success: success,
      error: error
    }

    return this.baseService.post(options);
  }

  getBalance(number: string, success: any = null, error: any = null) {
    const options = {
      url: `/cards/balance/${number}`,
      body: "",
      success: success,
      error: error,
      token: this.loginService.getUserLogin().token
    }

    return this.baseService.get(options);
  }

  withdraw(body: any, success: any = null, error: any = null){
    const options = {
      url: `/cards/withdraw`,
      body: body,
      success: success,
      error: error,
      token: this.loginService.getUserLogin().token
    }

    return this.baseService.post(options);
  }

  getOperation(id: number, success: any = null, error: any = null){
    const options = {
      url: `/operation/${id}`,
      body: "",
      success: success,
      error: error,
      token: this.loginService.getUserLogin().token
    }

    return this.baseService.get(options);
  }
}
