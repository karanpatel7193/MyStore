import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PurchaseInvoiceRoute } from './purchase-invoice.route';
import { PurchaseInvoiceService } from './purchase-invoice.service';
import { PurchaseInvoiceComponent } from './purchase-invoice.component';
import { PurchaseInvoiceFormComponent } from './purchase-invoice-form.component';
import { PurchaseInvoiceListComponent } from './purchase-invoice-list.component';
import { ProductSelectorModule } from '../../common/product-selector/product-selector.module';

@NgModule({
	imports: [
		FormsModule,
		CommonModule,
		NgbModule,
		PurchaseInvoiceRoute,
		ProductSelectorModule,
		ReactiveFormsModule
	],
	declarations: [
		PurchaseInvoiceComponent,
		PurchaseInvoiceFormComponent,
		PurchaseInvoiceListComponent
	],
	providers: [
		PurchaseInvoiceService
	]
})
export class PurchaseInvoiceModule { }
