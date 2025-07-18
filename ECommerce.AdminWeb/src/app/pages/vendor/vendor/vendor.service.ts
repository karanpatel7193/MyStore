import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';
import { HttpService } from '../../../services/http.service';
import { VendorAddModel, VendorEditModel, VendorGridModel, VendorListModel, VendorModel, VendorParameterModel } from './vendor.model';

@Injectable()
export class VendorService {
	constructor(private http: HttpService) {
	}

	public getRecord(id: number): Observable<VendorModel> {
		return this.http.get('admin/vendor/getRecord/' + id).pipe(
			map((response: VendorModel) => {
				return response;
			})
		)
	}
	public getForGrid(vendorParameterModel: VendorParameterModel): Observable<VendorGridModel> {
		return this.http.post('admin/vendor/getForGrid', vendorParameterModel).pipe(
			map((response: VendorGridModel) => {
				return response;
			}),
		);
	}
	public save(VendorModel: VendorModel, mode: string): Observable<number> {
		if (mode == 'Add')
			return this.http.post('admin/vendor/insert', VendorModel).pipe(
				map((response: number) => response)
			);
		else
			return this.http.post('admin/vendor/update', VendorModel).pipe(
				map((response: number) => {
					return response;
				}),
			);
	}
	public delete(id: number): Observable<void> {
		return this.http.post('admin/vendor/delete/' + id, null).pipe(
			map((response: void) => {
				return response;
			}),
		);
	}
	public getForLOV(): Observable<VendorModel[]> {
		return this.http.post('admin/vendor/getLovValue').pipe(
			map((response: VendorModel[]) => {
				return response;
			}),
		);
	}

	public getAddMode(vendorParameterModel: VendorParameterModel): Observable<VendorAddModel> {
		return this.http.post('admin/vendor/getAddMode', vendorParameterModel).pipe(
			map((response: VendorAddModel) => {
				return response;
			}),
		);
	}

	public getEditMode(vendorParameterModel: VendorParameterModel): Observable<VendorEditModel> {
		return this.http.post('admin/vendor/getEditMode', vendorParameterModel).pipe(
			map((response: VendorEditModel) => {
				return response;
			})
		);
	}

	public getListMode(vendorParameterModel: VendorParameterModel): Observable<VendorListModel> {
		return this.http.post('admin/vendor/getListValue', vendorParameterModel).pipe(
			map((response: VendorListModel) => {
				return response;
			}),
		);
	}
	//other method
	
}
