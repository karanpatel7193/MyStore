import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VendorFormComponent } from './vendor-form.component';
import { VendorListComponent } from './vendor-list.component';
import { VendorComponent } from './vendor.component';

const routes: Routes = [
	{
		path: '',
		component: VendorComponent,
		children: [
			{
				path: '',
				redirectTo: 'list',
				pathMatch: 'full' 
			},
			{
				path: 'list',
				component: VendorListComponent,
			},
			{
				path: 'add',
				component: VendorFormComponent,
			},
			{
				path: 'edit/:id',
				component: VendorFormComponent,
			},
			
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class VendorRoute {}
