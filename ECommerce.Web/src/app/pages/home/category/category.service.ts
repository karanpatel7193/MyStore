import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { HttpService } from '../../../services/http.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpService) { }

  public getCategoryList(): Observable<any> {
    return this.http.post(`${this.apiUrl}home/getCategoryList`, {}).pipe(
      map((response: any) => response)
    );
  }

  public getCategoryById(categoryId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}home/getCategory/${categoryId}`).pipe(
      map((response: any) => response)
    );
  }
}



