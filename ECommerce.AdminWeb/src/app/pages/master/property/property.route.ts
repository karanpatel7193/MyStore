import { ExtraOptions, RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from 'src/app/interceptors/auth.guard';
import { PropertyComponent } from './property.component';
import { PropertyListComponent } from './property-list/property-list.component';
import { PropertyFormComponent } from './property-form/property-form.component';

const routes: Routes = [ {
    path: '',
    component: PropertyComponent,
    children: [
        {
			path:'',
			redirectTo: 'list',
			pathMatch: 'full' 
		},
        {
            path: 'list',
            component: PropertyListComponent,
            canActivate : [AuthGuard]
        },
        {
            path: 'add',
            component: PropertyFormComponent,
            canActivate : [AuthGuard]
        },
        {
            path: 'edit/:id',
            component: PropertyFormComponent,
            canActivate : [AuthGuard]
        }
    ]
}];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class PropertyRoute {
}
