
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SearchGridModel } from '../search/search.model';
import { ProductDetailsGridModel, ProductDetailsModel, ProductDetailsParameterModel } from './product.model';
import { HttpService } from '../../services/http.service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpService) {}

  public getForCriteriea(categoryId: number): Observable<SearchGridModel> {
    return this.http.post(`${this.baseUrl}home/getForCriteriea?CategoryId=${categoryId}`, {}).pipe(
      map((response: SearchGridModel) => response || new SearchGridModel())
    );
  }

  public getProductDetails(productDetailsParameterModel: ProductDetailsParameterModel): Observable<ProductDetailsGridModel> {
		return this.http.post(`${this.baseUrl}product/getDetails`, productDetailsParameterModel).pipe(
			map((response: ProductDetailsGridModel) => {
				return response;
			}),
		);
	}
}




