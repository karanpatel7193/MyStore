import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';
import { HttpService } from '../../../services/http.service';
import { ProductPropertyParameterModel, ProductPropertyGridModel } from '../../master/product-property/product-property.model';
import { PurchaseOrderAddModel, PurchaseOrderEditModel, PurchaseOrderGridModel, PurchaseOrderListModel, PurchaseOrderModel, PurchaseOrderParameterModel } from './purchase-order.model';

@Injectable()
export class PurchaseOrderService {
	constructor(private http: HttpService) {
	}

	public getRecord(id: number): Observable<PurchaseOrderModel> {
		return this.http.get('invoice/purchaseOrder/getRecord/' + id).pipe(
			map((response: PurchaseOrderModel) => {
				return response;
			})
		)
	}
	public getForGrid(purchaseOrderParameterModel: PurchaseOrderParameterModel): Observable<PurchaseOrderGridModel> {
		return this.http.post('invoice/purchaseOrder/getForGrid', purchaseOrderParameterModel).pipe(
			map((response: PurchaseOrderGridModel) => {
				return response;
			}),
		);
	}


	public save(ProductModel: PurchaseOrderModel, mode: string): Observable<number> {
		if (mode == 'Add')
			return this.http.post('invoice/purchaseOrder/insert', ProductModel).pipe(
				map((response: number) => response)
			);
		else
			return this.http.post('invoice/purchaseOrder/update', ProductModel).pipe(
				map((response: number) => {
					return response;
				}),
			);
	}

	public delete(id: number): Observable<void> {
		return this.http.post('invoice/purchaseOrder/delete/' + id, null).pipe(
			map((response: void) => {
				return response;
			}),
		);
	}
	
	public getForLOV(): Observable<PurchaseOrderModel[]> {
		return this.http.post('invoice/purchaseOrder/getLovValue').pipe(
			map((response: PurchaseOrderModel[]) => {
				return response;
			}),
		);
	}

	// public getForPrice(purchaseOrderParameterModel: PurchaseOrderParameterModel): Observable<PurchaseOrderPriceModel[]> {
	// 	return this.http.post('invoice/purchaseOrder/getPriceValue', purchaseOrderParameterModel).pipe(
	// 		map((response: PurchaseOrderPriceModel[]) => {
	// 			return response;
	// 		}),
	// 	);
	// }

	public getAddMode(purchaseOrderParameterModel: PurchaseOrderParameterModel): Observable<PurchaseOrderAddModel> {
		return this.http.post('invoice/purchaseOrder/getAddMode', purchaseOrderParameterModel).pipe(
			map((response: PurchaseOrderAddModel) => {
				return response;
			}),
		);
	}

	public getEditMode(purchaseOrderParameterModel: PurchaseOrderParameterModel): Observable<PurchaseOrderEditModel> {
		return this.http.post('invoice/purchaseOrder/getEditMode', purchaseOrderParameterModel).pipe(
			map((response: PurchaseOrderEditModel) => {
				return response;
			})
		);
	}

	public getListMode(purchaseOrderParameterModel: PurchaseOrderParameterModel): Observable<PurchaseOrderListModel> {
		return this.http.post('invoice/purchaseOrder/getListValue', purchaseOrderParameterModel).pipe(
			map((response: PurchaseOrderListModel) => {
				return response;
			}),
		);
	}


}
