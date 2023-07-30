import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HeadersService } from './headers.service';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor(private http: HttpClient,
    private headersService: HeadersService) {
      this.server =  `https://localhost:7206`;
     }

  server: string = `https://localhost:7206`;

  private doNothing = () => { };

  async get(options: any) {
    this.http.get<any>( this.server + options.url, { headers: this.headersService.getCorsHeader() }).subscribe({
      next: (data) => this.handleResponse(options, data),
      error: (e) => this.handleError(options, e)
    })
  }

  async post(options: any) {
    this.http.post<any>(
      this.server + options.url,
      options.body,
      options.token ? this.headersService.getAuthorizationHeader(options.token) : this.headersService.getCorsHeader()).subscribe({
        next: data => this.handleResponse(options, data),
        error: e => this.handleError(options, e)
      })
  }

  handleError(options: any, statusText: string) {
    const fnError = options.error || this.doNothing;

    fnError(statusText);
  }

  handleResponse(options: any, response: any) {
    const fnOk = options.success || this.doNothing;
    fnOk(response);
  }
}
