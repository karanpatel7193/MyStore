import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ReviewModel, ReviewGridModel, ReviewParameterModel } from './review.model';
import { environment } from '../../../../environments/environment';
import { HttpService } from '../../../services/http.service';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {

  private apiUrl = environment.apiUrl;;

  constructor(private http: HttpService) { }

  public getForGrid(reviewParameter: ReviewParameterModel): Observable<ReviewGridModel> {
    return this.http.post(`${this.apiUrl}review/getGridData`, reviewParameter).pipe(
      map((response: ReviewGridModel) => response)
    );
  }

  public insert(review: ReviewModel): Observable<number> {
    return this.http.post(`${this.apiUrl}review/insert`, review).pipe(
      map((response) => response)
    );
  }

  public update(review: ReviewModel): Observable<number> {
    return this.http.post(`${this.apiUrl}review/update`, review).pipe(
      map((response: number) => response)
    );
  }

  public delete(id: number): Observable<void> {
    return this.http.post(`${this.apiUrl}review/delete/${id}`, {}).pipe(
      map((response: void) => response)
    );
  }
}