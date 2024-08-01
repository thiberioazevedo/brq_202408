// import { StorangeEnum } from './../enums/storange.enum';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import * as saveAs from 'file-saver';
import { Observable, map } from 'rxjs';

export enum StorangeEnum {
    session,
    refreshTokenIam,
    tokenIam,
    c0,
    periodoFilter
}
export interface IExportService<T> {
  obterRegistrosExportarExcel(filtro: any, includes: string): Promise<T[]>;
}

export interface IHistoricoAnimalService {
  obterHistoricoAnimal(idEntrada: number, termo: string, pagina: number, registrosPorPagina: number, paramExtra?: string): Promise<any>;
}

export abstract class BaseService<T> {

  private restEndpoint = '';

  protected clientSecret = '';

  protected get UrlService() {
    return this.restEndpoint;
  }

  constructor(public httpClient: HttpClient) {
  }

   protected endpoint: string = '';

  protected ObterHeaderJson() {
    return new HttpHeaders();
    
    const data = (<any>JSON.parse( localStorage.getItem(StorangeEnum.session.toString()) || '')).data;
    let headers: any  = {'Content-Type': 'application/json'}
      headers = {
        ...headers,
        'Authorization': 'Bearer ' + data.accessToken,
        'x-r-t':data.refreshToken,

      }
    return new HttpHeaders(headers);
  }
  get Token(): string{
    let token = (localStorage.getItem(StorangeEnum.tokenIam.toString()));
    if(token === 'null')
      token = null;
    return <string>token;
  }

  protected get(endpoint: string) {
    return this.httpClient.get(`${environment.apiUrl}${endpoint}`, { headers: this.ObterHeaderJson() });
  }

  protected getAsArrayOfType(endpoint: string) {
    return this.httpClient.get<T[]>(`${environment.apiUrl}${endpoint}`, { headers: this.ObterHeaderJson() });
  }

  protected post(endpoint: string, body: T | T[]) {
    return this.httpClient.post(`${environment.apiUrl}${endpoint}`, body, { headers: this.ObterHeaderJson() });
  }

  protected genericPost<Ttype>(endpoint: string, body: Ttype) {
    return this.httpClient.post(`${environment.apiUrl}${endpoint}`, body, { headers: this.ObterHeaderJson() });
  }

  protected put(endpoint: string, body: T | T[]) {
    return this.httpClient.put(`${environment.apiUrl}${endpoint}`, body, { headers: this.ObterHeaderJson() });
  }

  protected delete(id: any) {
    let url = `${environment.apiUrl}/v1/${this.nomeClasse()}/${id}`;
    return this.httpClient.delete(url, { headers: this.ObterHeaderJson() });
  }

  nomeClasse(){
    let classe = this.constructor.name;
    classe = classe.substring(0, classe.length - 7);
    return classe;
  }

  protected postList(endpoint: string, body: T[]) {
    return this.httpClient.post(`${environment.apiUrl}${endpoint}`, body, { headers: this.ObterHeaderJson() });
  }

  protected postById(endpoint: string, id: number) {
    return this.httpClient.post(`${environment.apiUrl}${endpoint}?id=${id}`, id, { headers: this.ObterHeaderJson() });
  }
}
