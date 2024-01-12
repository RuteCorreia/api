import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../../models/cliente/cliente.model';
import { AlvoBiologico } from '../../models/alvo-biologico/alvo-biologico.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const baseUrl = 'https://flytec.keltecnologia.com.br/api/v1/alvoBiologico/';

@Injectable({
  providedIn: 'root'
})
export class AlvoBiologicoService {


  constructor(private http: HttpClient) { }

  getAll(): Observable<AlvoBiologico[]> {
    return this.http.get<AlvoBiologico[]>(baseUrl);
  }

  get(id: any): Observable<AlvoBiologico> {
    debugger;
    return this.http.get<AlvoBiologico>(baseUrl + id);
  }

  create(data: AlvoBiologico): Observable<any> {
    debugger;
    return this.http.post(baseUrl, data,
    httpOptions);
  }

  update(id: any, data: AlvoBiologico): Observable<any> {
    debugger;
    return this.http.put(baseUrl + id, data,
    httpOptions
    );
  }

  delete(id: any): Observable<any> {
    debugger;
    return this.http.delete(baseUrl + id);
  }

  deleteAll(): Observable<any> {
    return this.http.delete(baseUrl);
  }

  findByTitle(title: any): Observable<AlvoBiologico[]> {
    return this.http.get<AlvoBiologico[]>(`${baseUrl}+"FindUserByName?name="+${title}`);
  }

  getDropdownData(): Observable<any[]> {
    return this.http.get<any[]>(baseUrl);
  }
}
