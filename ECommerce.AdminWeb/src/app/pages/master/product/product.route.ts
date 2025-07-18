import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductFormComponent } from './product-form.component';
import { ProductListComponent } from './product-list.component';
import { ProductComponent } from './product.component';
import { ProductPropertyListComponent } from '../product-property/product-property-list.component';

const routes: Routes = [
	{
		path: '',
		component: ProductComponent,
		children: [
			{
				path: '',
				redirectTo: 'list',
				pathMatch: 'full' 
			},
			{
				path: 'list',
				component: ProductListComponent,
			},
			{
				path: 'add',
				component: ProductFormComponent,
			},
			{
				path: 'edit/:id',
				component: ProductFormComponent,
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
export class ProductRoute {}
