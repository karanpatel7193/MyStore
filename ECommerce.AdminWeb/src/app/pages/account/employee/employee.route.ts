
import { NgModule } from '@angular/core';
import { ExtraOptions, RouterModule, Routes } from '@angular/router';
import { EmployeeComponent } from './employee.component';
import { EmployeeFormComponent } from './employee-form.component';
import { EmployeeListComponent } from './employee-list.component';

const routes: Routes = [{
    path: '',
    component: EmployeeComponent,
    children: [{
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
    },
    {
        path: 'list',
        component: EmployeeListComponent,
    },
    {
        path: 'add',
        component: EmployeeFormComponent,
    },
    {
        path: 'edit/:id',
        component: EmployeeFormComponent,
    },
	],
}];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class EmployeeRoute {
}
