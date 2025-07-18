import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/internal/operators/map';
import { HttpService } from '../../../services/http.service';
import { PurchaseInvoiceAddModel, PurchaseInvoiceEditModel, PurchaseInvoiceGridModel, PurchaseInvoiceListModel, PurchaseInvoiceModel, PurchaseInvoiceParameterModel } from './purchase-invoice.model';

@Injectable()
export class PurchaseInvoiceService {
	constructor(private http: HttpService) {
	}

	public getRecord(id: number): Observable<PurchaseInvoiceModel> {
		return this.http.get('invoice/purchaseInvoice/getRecord/' + id).pipe(
			map((response: PurchaseInvoiceModel) => response)
		);
	}

	public getForGrid(purchaseInvoiceParameterModel: PurchaseInvoiceParameterModel): Observable<PurchaseInvoiceGridModel> {
		return this.http.post('invoice/purchaseInvoice/getForGrid', purchaseInvoiceParameterModel).pipe(
			map((response: PurchaseInvoiceGridModel) => response)
		);
	}

	public save(purchaseInvoiceModel: PurchaseInvoiceModel, mode: string): Observable<number> {
		if (mode === 'Add') {
			return this.http.post('invoice/purchaseInvoice/insert', purchaseInvoiceModel).pipe(
				map((response: number) => response)
			);
		} else {
			return this.http.post('invoice/purchaseInvoice/update', purchaseInvoiceModel).pipe(
				map((response: number) => response)
			);
		}
	}

	public delete(id: number): Observable<void> {
		return this.http.post('invoice/purchaseInvoice/delete/' + id, null).pipe(
			map((response: void) => response)
		);
	}

	public getForLOV(): Observable<PurchaseInvoiceModel[]> {
		return this.http.post('invoice/purchaseInvoice/getLovValue').pipe(
			map((response: PurchaseInvoiceModel[]) => response)
		);
	}

	public getAddMode(purchaseInvoiceParameterModel: PurchaseInvoiceParameterModel): Observable<PurchaseInvoiceAddModel> {
		return this.http.post('invoice/purchaseInvoice/getAddMode', purchaseInvoiceParameterModel).pipe(
			map((response: PurchaseInvoiceAddModel) => response)
		);
	}

	public getEditMode(purchaseInvoiceParameterModel: PurchaseInvoiceParameterModel): Observable<PurchaseInvoiceEditModel> {
		return this.http.post('invoice/purchaseInvoice/getEditMode', purchaseInvoiceParameterModel).pipe(
			map((response: PurchaseInvoiceEditModel) => response)
		);
	}

	public getListMode(purchaseInvoiceParameterModel: PurchaseInvoiceParameterModel): Observable<PurchaseInvoiceListModel> {
		return this.http.post('invoice/purchaseInvoice/getListValue', purchaseInvoiceParameterModel).pipe(
			map((response: PurchaseInvoiceListModel) => response)
		);
	}
}
