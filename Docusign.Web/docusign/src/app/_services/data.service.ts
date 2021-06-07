import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  public listTemplates(): Observable<any[]>{
    const url = environment.apiUrl + 'api/templates';
    return this.http.get<any[]>(url);
  }

}
