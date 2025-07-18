import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';
import { HttpService } from '../../../services/http.service';
import { ProductAddModel, ProductEditModel, ProductGridModel, ProductListModel, ProductModel, ProductParameterModel } from './product.model';
import { ProductPropertyParameterModel, ProductPropertyGridModel } from '../product-property/product-property.model';

@Injectable()
export class ProductService {
	constructor(private http: HttpService) {
	}

	public getRecord(id: number): Observable<ProductModel> {
		return this.http.get('admin/product/getRecord/' + id).pipe(
			map((response: ProductModel) => {
				return response;
			})
		)
	}
	
	public getForGrid(productParameterModel: ProductParameterModel): Observable<ProductGridModel> {
		return this.http.post('admin/product/getForGrid', productParameterModel).pipe(
			map((response: ProductGridModel) => {
				return response;
			}),
		);
	}

	public getForPropertyGrid(productPropertyParameterModel: ProductPropertyParameterModel): Observable<ProductPropertyGridModel>{
		return this.http.post('admin/product/getForPropertyGrid', productPropertyParameterModel).pipe(
			map((response: ProductPropertyGridModel) => {
				return response;
			}),
		);
	}

	public save(ProductModel: ProductModel, mode: string): Observable<number> {
		if (mode == 'Add')
			return this.http.post('admin/product/insert', ProductModel).pipe(
				map((response: number) => response)
			);
		else
			return this.http.post('admin/product/update', ProductModel).pipe(
				map((response: number) => {
					return response;
				}),
			);
	}
	public getAddMode(productParameterModel: ProductParameterModel): Observable<ProductAddModel> {
		return this.http.post('admin/product/getAddMode', productParameterModel).pipe(
			map((response: ProductAddModel) => {
				return response;
			}),
		);
	}

	public getEditMode(productParameterModel: ProductParameterModel): Observable<ProductEditModel> {
		return this.http.post('admin/product/getEditMode', productParameterModel).pipe(
			map((response: ProductEditModel) => {
				return response;
			})
		);
	}
	public delete(id: number): Observable<void> {
		return this.http.post('admin/product/delete/' + id, null).pipe(
			map((response: void) => {
				return response;
			}),
		);
	}
	public getForLOV(productParameterModel: ProductParameterModel): Observable<ProductModel[]> {
		return this.http.post('admin/product/getLovValue', productParameterModel).pipe(
			map((response: ProductModel[]) => {
				return response;
			}),
		);
	}

	public getListMode(productParameterModel: ProductParameterModel): Observable<ProductListModel> {
		return this.http.post('admin/product/getListValue', productParameterModel).pipe(
			map((response: ProductListModel) => {
				return response;
			}),
		);
	}

    public insertProperty(productPropertyParameterModel: ProductPropertyParameterModel): Observable<void> {
		return this.http.post('admin/product/insertProperty', productPropertyParameterModel).pipe(
			map((response: void) => {
				return response;
			}),
		);
	}

    public syncAll(): Observable<void> {
		return this.http.post('admin/product/syncAll', null).pipe(
			map((response: void) => {
				return response;
			}),
		);
	}

    public sync(productParameterModel: ProductParameterModel): Observable<void> {
		return this.http.post('admin/product/sync', productParameterModel).pipe(
			map((response: void) => {
				return response;
			}),
		);
	}
	//code for the varient
	saveProduct(body: any): Observable<any> {
		return this.http.post('admin/product/insert', body);
	  }
	  
	
}
