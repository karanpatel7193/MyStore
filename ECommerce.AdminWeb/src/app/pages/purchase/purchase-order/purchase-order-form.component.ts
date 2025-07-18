import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { AccessModel } from '../../../models/access.model';
import { ToastService } from '../../../services/toast.service';
import { PurchaseOrderItemModel, PurchaseOrderMainModel, PurchaseOrderModel, PurchaseOrderParameterModel } from './purchase-order.model';
import { PurchaseOrderService } from './purchase-order.service';
import { ProductModel } from '../../master/product/product.model';
import { ProductService } from '../../master/product/product.service';
import { ProductMainModel } from '../../master/product/product.model';
import { debounceTime, distinctUntilChanged, filter, map, Observable } from 'rxjs';

@Component({
	selector: 'app-purchase-order-add',
	templateUrl: './purchase-order-form.component.html',
	styleUrls: ['./purchase-order-form.component.scss']
})
export class PurchaseOrderFormComponent implements OnInit, OnDestroy {
	public access: AccessModel = new AccessModel();
	public purchaseOrder: PurchaseOrderModel = new PurchaseOrderModel();
	public purchaseOrderItem: PurchaseOrderItemModel = new PurchaseOrderItemModel();
	public purchaseOrderParameter: PurchaseOrderParameterModel = new PurchaseOrderParameterModel();
	public hasAccess: boolean = false;
	public mode: string = '';
	public id: number = 0;
	public purchaseOrderId: number = 0;
	private sub: any;
	public vendors: PurchaseOrderMainModel[] = [];
	public products: PurchaseOrderMainModel[] = [];
	public isPercentage: boolean = true;
	public discountType: 'amount' | 'percentage' = 'amount';
	public discountLabel: string = 'Discount (₹)';
	public taxDiscountType: 'amount' | 'percentage' = 'amount';
	public taxDiscountLabel: string = 'Tax Discount (₹)';

	constructor(
		private purchaseOrderService: PurchaseOrderService,
		private productService: ProductService,
		private sessionService: SessionService,
		private router: Router,
		private route: ActivatedRoute,
		private toaster: ToastService,
	) {
		this.setAccess();
	}

	ngOnInit() {
		this.getRouteData();
	}

	ngOnDestroy() {
		this.sub.unsubscribe();
	}

	onProductSelected(product: ProductMainModel, item: PurchaseOrderItemModel): void {
		this.productService.getRecord(product.id).subscribe({
			next: (product) => {
				item.productId = product.id;
				item.productName = product.name;
				item.price = product.finalSellPrice;
				this.calculateRowAmount(item);
			},
			error: (err) => {
				this.toaster.warning('You do not have add access to this page.');
			},
		});
	}


	public setAccess(): void {
		this.access = this.sessionService.getAccess('purchase/purchase-order');
	}

	public getRouteData(): void {
		this.sub = this.route.params.subscribe(params => {
			const segments: UrlSegment[] = this.route.snapshot.url;
			if (segments.toString().toLowerCase() === 'add')
				this.id = 0;
			else
				this.id = +params['id']; // (+) 
			this.setPageMode();
		});
	}

	public clearModels(): void {
		this.purchaseOrder = new PurchaseOrderModel();
		this.purchaseOrder.purchaseOrderItem.push(new PurchaseOrderItemModel());
	}
	public setPageMode(): void {
		if (this.id === undefined || this.id === 0)
			this.setPageAddMode();

		else
			this.setPageEditMode();

		if (this.hasAccess) {
		}
	}

	public setPageAddMode(): void {
		if (!this.access.canInsert) {
			this.toaster.warning('You do not have add access of this page.');
			return;
		}
		this.hasAccess = true;
		this.mode = 'Add';

		this.purchaseOrderService.getAddMode(this.purchaseOrderParameter).subscribe(data => {
			this.vendors = data.vendors;
			this.products = data.products;
		});
		this.clearModels();
	}

	public setPageEditMode(): void {
		if (!this.access.canUpdate) {
			this.toaster.warning('You do not have update access of this page.');
			return;
		}
		this.hasAccess = true;
		this.mode = 'Edit';
		this.purchaseOrderParameter.id = this.id;

		this.purchaseOrderService.getEditMode(this.purchaseOrderParameter).subscribe(data => {
			this.vendors = data.vendors;
			this.products = data.products;
			this.purchaseOrder = data.purchaseOrder;
			this.purchaseOrder.purchaseOrderItem = data.purchaseOrder.purchaseOrderItem
		});
	}

	save(isFormValid: boolean | null): void {
		if (!this.access.canInsert && !this.access.canUpdate) {
			this.toaster.warning('You do not have add or edit access to this page.');
			return;
		}
		if (isFormValid) {
			this.purchaseOrderService.save(this.purchaseOrder, this.mode).subscribe((data) => {
				if (data === 0) {
					this.toaster.warning('Record already exists.');
				} else if (data > 0) {
					this.toaster.success('Record submitted successfully.');
					this.router.navigate(['app/purchase/purchase-order/list']);
					this.cancel();
				}
			});
		} else {
			this.toaster.warning('Please provide valid input.');
		}
	}

	public cancel(): void {
		this.router.navigate(['app/purchase/purchase-order/list']);
	}


	public getTotalSums() {
		const totals = { totalAmount: 0, totalDiscount: 0, totalTax: 0, totalFinalAmount: 0, totalQuantity: 0 };
		this.purchaseOrder.purchaseOrderItem.forEach((row) => {
			totals.totalQuantity += row.quantity;
			totals.totalAmount += row.amount;
			totals.totalDiscount += row.discountedAmount;
			totals.totalTax += row.tax;
			totals.totalFinalAmount += row.finalAmount;
		});

		this.purchaseOrder.totalAmount = totals.totalAmount;
		this.purchaseOrder.totalDiscount = totals.totalDiscount;
		this.purchaseOrder.totalTax = totals.totalTax;
		this.purchaseOrder.totalFinalAmount = totals.totalFinalAmount;
		this.purchaseOrder.totalQuantity = totals.totalQuantity;
		return totals;
	}


	deleteRow(index: number): void {
		this.purchaseOrder.purchaseOrderItem.splice(index, 1);
	}

	public calculateRowAmount(row: PurchaseOrderItemModel): void {
		const amount = row.quantity * row.price;

		let discount = 0;
		if (row.discountType === 'amount') {
			discount = row.discountedAmount;
		} else if (row.discountType === 'percentage') {
			discount = (amount * row.discountedAmount) / 100;
		}

		let taxDiscount = 0;
		if (row.taxDiscountType === 'amount') {
			taxDiscount = row.tax;
		} else if (row.taxDiscountType === 'percentage') {
			taxDiscount = (amount * row.tax) / 100;
		}

		row.amount = amount;
		row.finalAmount = amount - discount + taxDiscount;
	}

	public setDiscountType(row: PurchaseOrderItemModel, type: 'amount' | 'percentage'): void {
		row.discountType = type;
		this.calculateRowAmount(row);
	}

	public setTaxDiscountType(row: PurchaseOrderItemModel, type: 'amount' | 'percentage'): void {
		row.taxDiscountType = type;
		this.calculateRowAmount(row);
	}



	addRow(): void {
		this.purchaseOrder.purchaseOrderItem.push(new PurchaseOrderItemModel());
	}


	// public onProductSelected(product: ProductMainModel, item: PurchaseOrderItemModel): void {
	// 	if (product) {
	// 		item.productId = product.id;
	// 		item.productName = product.name;

	// 	}

}






