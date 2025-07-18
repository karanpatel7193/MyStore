import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpService } from '../../../services/http.service';
import { ProductVariantGridModel, ProductVariantModel, ProductVariantParameterModel } from './productVariant.model';

@Injectable()
export class ProductVariantService {
	constructor(private http: HttpService) { }

	public getRecord(id: number): Observable<ProductVariantModel> {
		return this.http.get('admin/productvariant/getRecord/' + id).pipe(
			map((response: ProductVariantModel) => {
				return response;
			})
		);
	}

	public getForGrid(productVariantParameterModel: ProductVariantParameterModel): Observable<ProductVariantGridModel> {
		return this.http.post('admin/productvariant/getForGrid', productVariantParameterModel).pipe(
			map((response: ProductVariantGridModel) => {
				return response;
			})
		);
	}

	public save(model: ProductVariantModel, mode: string): Observable<number> {
		if (mode === 'Add') {
			return this.http.post('admin/productvariant/insert', model).pipe(
				map((response: number) => response)
			);
		} else {
			return this.http.post('admin/productvariant/update', model).pipe(
				map((response: number) => response)
			);
		}
	}
	public update(productVariantModel: ProductVariantModel): Observable<number> {
		return this.http.post('admin/productvariant/update', productVariantModel).pipe(
			map((response: number) => response)
		);
	}

	public delete(id: number): Observable<void> {
		return this.http.post('admin/productvariant/delete/' + id, null).pipe(
			map((response: void) => response)
		);
	}
}
