import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class CardsService {

  constructor(private baseService: BaseService) { 
  }

  get(cardNumber: string, success: any = null, error: any = null){

    const options = {
      url: `/cards/${cardNumber}`,
      body: "",
      success: success,
      error: error
    }

    return this.baseService.get(options);
  }

  block(body: any, success: any = null, error: any = null){
    const options = {
      url: `/cards/block`,
      body: body,
      success: success,
      error: error
    }

    return this.baseService.post(options);
  }

  login(body: any, success: any = null, error: any = null){
    const options = {
      url: `/login`,
      body: body,
      success: success,
      error: error
    }

    return this.baseService.post(options);
  }


}
