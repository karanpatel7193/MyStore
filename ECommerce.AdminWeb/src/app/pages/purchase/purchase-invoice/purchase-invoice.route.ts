import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductPropertyListComponent } from '../../master/product-property/product-property-list.component';
import { PurchaseInvoiceComponent } from './purchase-invoice.component';
import { PurchaseInvoiceListComponent } from './purchase-invoice-list.component';
import { PurchaseInvoiceFormComponent } from './purchase-invoice-form.component';

const routes: Routes = [
	{
		path: '',
		component: PurchaseInvoiceComponent,
		children: [
			{
				path: '',
				redirectTo: 'list',
				pathMatch: 'full' 
			},
			{
				path: 'list',
				component: PurchaseInvoiceListComponent,
			},
			{
				path: 'add',
				component: PurchaseInvoiceFormComponent,
			},
			{
				path: 'edit/:id',
				component: PurchaseInvoiceFormComponent,
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
export class PurchaseInvoiceRoute {}
