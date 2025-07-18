import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { ActivatedRoute, Router, UrlSegment } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { AccessModel } from '../../../models/access.model';
import { ToastService } from '../../../services/toast.service';
import { PurchaseInvoiceItemModel, PurchaseInvoiceMainModel, PurchaseInvoiceModel, PurchaseInvoiceParameterModel } from './purchase-invoice.model';
import { PurchaseInvoiceService } from './purchase-invoice.service';
import { ProductModel } from '../../master/product/product.model';
import { ProductService } from '../../master/product/product.service';
import { ProductMainModel } from '../../master/product/product.model';
import { debounceTime, distinctUntilChanged, filter, map, Observable } from 'rxjs';

@Component({
	selector: 'app-purchase-invoice-add',
	templateUrl: './purchase-invoice-form.component.html',
	styleUrls: ['./purchase-invoice-form.component.scss']
})
export class PurchaseInvoiceFormComponent implements OnInit, OnDestroy {
	public access: AccessModel = new AccessModel();
	public purchaseInvoice: PurchaseInvoiceModel = new PurchaseInvoiceModel();
	public PurchaseInvoiceItem: PurchaseInvoiceItemModel = new PurchaseInvoiceItemModel();
	public PurchaseInvoiceParameter: PurchaseInvoiceParameterModel = new PurchaseInvoiceParameterModel();
	public hasAccess: boolean = false;
	public mode: string = '';
	public id: number = 0;
	public PurchaseInvoiceId: number = 0;
	private sub: any;
	public vendors: PurchaseInvoiceMainModel[] = [];
	public products: PurchaseInvoiceMainModel[] = [];
	public isPercentage: boolean = true;
	public discountType: 'amount' | 'percentage' = 'amount';
	public discountLabel: string = 'Discount (₹)';
	public taxDiscountType: 'amount' | 'percentage' = 'amount';
	public taxDiscountLabel: string = 'Tax Discount (₹)';

	constructor(
		private PurchaseInvoiceService: PurchaseInvoiceService,
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

	onProductSelected(product: ProductMainModel, item: PurchaseInvoiceItemModel): void {
		this.productService.getRecord(product.id).subscribe({
			next: (product) => {
				item.productId = product.id;
				item.productName = product.name;
				item.price = product.finalSellPrice;
				item.isExpiry  = product.isExpiry; 
				if (!product.isExpiry) {
					item.expiryDate = null; 
				}
				this.calculateRowAmount(item);
			},
			error: (err) => {
				this.toaster.warning('You do not have add access to this page.');
			},
		});
	}



	public setAccess(): void {
		this.access = this.sessionService.getAccess('purchase/purchase-invoice');
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
		this.purchaseInvoice = new PurchaseInvoiceModel();
		this.purchaseInvoice.purchaseInvoiceItems.push(new PurchaseInvoiceItemModel());
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

		this.PurchaseInvoiceService.getAddMode(this.PurchaseInvoiceParameter).subscribe(data => {
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
		this.PurchaseInvoiceParameter.id = this.id;

		this.PurchaseInvoiceService.getEditMode(this.PurchaseInvoiceParameter).subscribe(data => {
			this.vendors = data.vendors;
			this.products = data.products;
			this.purchaseInvoice = data.purchaseInvoice;
			this.purchaseInvoice.purchaseInvoiceItems = data.purchaseInvoice.purchaseInvoiceItems
		});
	}

	save(isFormValid: boolean | null): void {
		if (!this.access.canInsert && !this.access.canUpdate) {
			this.toaster.warning('You do not have add or edit access to this page.');
			return;
		}
		if (isFormValid) {
			this.PurchaseInvoiceService.save(this.purchaseInvoice, this.mode).subscribe((data) => {
				if (data === 0) {
					this.toaster.warning('Record already exists.');
				} else if (data > 0) {
					this.toaster.success('Record submitted successfully.');
					this.router.navigate(['app/purchase/purchase-invoice/list']);
					this.cancel();
				}
			});
		} else {
			this.toaster.warning('Please provide valid input.');
		}
	}

	public cancel(): void {
		this.router.navigate(['app/purchase/purchase-invoice/list']);
	}


	public getTotalSums() {
		const totals = { totalAmount: 0, totalDiscount: 0, totalTax: 0, totalFinalAmount: 0, totalQuantity: 0 };
		this.purchaseInvoice.purchaseInvoiceItems.forEach((row) => {
			totals.totalQuantity += row.quantity;
			totals.totalAmount += row.amount;
			totals.totalDiscount += row.discountedAmount;
			totals.totalTax += row.tax;
			totals.totalFinalAmount += row.finalAmount;
		});

		this.purchaseInvoice.totalAmount = totals.totalAmount;
		this.purchaseInvoice.totalDiscount = totals.totalDiscount;
		this.purchaseInvoice.totalTax = totals.totalTax;
		this.purchaseInvoice.totalFinalAmount = totals.totalFinalAmount;
		this.purchaseInvoice.totalQuantity = totals.totalQuantity;
		return totals;
	}


	deleteRow(index: number): void {
		this.purchaseInvoice.purchaseInvoiceItems.splice(index, 1);
	}

	public calculateRowAmount(row: PurchaseInvoiceItemModel): void {
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

	public setDiscountType(row: PurchaseInvoiceItemModel, type: 'amount' | 'percentage'): void {
		row.discountType = type;
		this.calculateRowAmount(row);
	}

	public setTaxDiscountType(row: PurchaseInvoiceItemModel, type: 'amount' | 'percentage'): void {
		row.taxDiscountType = type;
		this.calculateRowAmount(row);
	}
	
	addRow(): void {
		this.purchaseInvoice.purchaseInvoiceItems.push(new PurchaseInvoiceItemModel());
	}

}






