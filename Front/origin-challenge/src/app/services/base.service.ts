import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HeadersService } from './headers.service';
import { AppConfigService } from './app-config.service';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor(private http: HttpClient,
    private headersService: HeadersService,
    private configService: AppConfigService) { }


  private doNothing = () => { };

  async get(options: any) {

    const authHeader: any = options.token
      ? this.headersService.getAuthorizationHeader(options.token)
      : this.headersService.getCorsHeader();

    this.http.get<any>(this.configService.apiBaseUrl + options.url, { headers: authHeader }).subscribe({
      next: (data) => this.handleResponse(options, data),
      error: (e) => this.handleError(options, e)
    })
  }

  async post(options: any) {

    const authHeader: any = options.token
      ? this.headersService.getAuthorizationHeader(options.token)
      : this.headersService.getCorsHeader();

    this.http.post<any>(
      this.configService.apiBaseUrl + options.url,
      options.body,
      { headers: authHeader }).subscribe({
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
