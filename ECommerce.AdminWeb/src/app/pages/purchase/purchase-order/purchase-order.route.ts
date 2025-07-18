import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductPropertyListComponent } from '../../master/product-property/product-property-list.component';
import { PurchaseOrderComponent } from './purchase-order.component';
import { PurchaseOrderListComponent } from './purchase-order-list.component';
import { PurchaseOrderFormComponent } from './purchase-order-form.component';

const routes: Routes = [
	{
		path: '',
		component: PurchaseOrderComponent,
		children: [
			{
				path: '',
				redirectTo: 'list',
				pathMatch: 'full' 
			},
			{
				path: 'list',
				component: PurchaseOrderListComponent,
			},
			{
				path: 'add',
				component: PurchaseOrderFormComponent,
			},
			{
				path: 'edit/:id',
				component: PurchaseOrderFormComponent,
			},
			{
				path: 'propertyGrid/:productId/:productName/:categoryId',
				component: ProductPropertyListComponent,
			}
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class PurchaseOrderRoute {}
