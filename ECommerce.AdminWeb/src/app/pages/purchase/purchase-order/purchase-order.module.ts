import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {  PurchaseOrderRoute } from './purchase-order.route';
import { PurchaseOrderService } from './purchase-order.service';
import { PurchaseOrderComponent } from './purchase-order.component';
import { PurchaseOrderFormComponent } from './purchase-order-form.component';
import { PurchaseOrderListComponent } from './purchase-order-list.component';
import { ProductSelectorModule } from '../../common/product-selector/product-selector.module';

@NgModule({
	imports: [
    FormsModule,
    CommonModule,
    NgbModule,
    PurchaseOrderRoute,
	ProductSelectorModule
],
	declarations: [
		PurchaseOrderComponent,
		PurchaseOrderFormComponent,
		PurchaseOrderListComponent
	],
	providers: [
		PurchaseOrderService
	]
})
export class PurchaseOrderModule { }
