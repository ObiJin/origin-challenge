import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {

  private appConfig: any;
  private errorMessage: string = 'No se pudo cargar el archivo de configuracion!';

  constructor(private http: HttpClient) { }

  loadAppConfig() {
    return this.http.get('/assets/config.json')
      .toPromise()
      .then(data => {
        this.appConfig = data;
      });
  }

  get apiBaseUrl() {

    if (!this.appConfig) {
      throw Error(this.errorMessage);
    }

    return this.appConfig.baseUrl;
  }
}
