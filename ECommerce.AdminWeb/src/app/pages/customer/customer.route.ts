import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { AuthGuard } from 'src/app/interceptors/auth.guard';
import { CustomerListComponent } from './customer-list.componet';

const routes: Routes = [
    {
        path: '',
        component: CustomerListComponent,
        canActivate: [AuthGuard],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class CustomerRoute {
}
