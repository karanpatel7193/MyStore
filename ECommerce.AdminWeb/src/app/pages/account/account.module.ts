import { NgModule } from '@angular/core';
import { AccountRoute } from './account.route';
import { AccountComponent } from './account.component';
import { EmployeeComponent } from './employee/employee.component';

@NgModule({
    imports: [
        AccountRoute
    ],
    declarations: [
    AccountComponent,
  ],
    providers: [
    ],
})
export class AccountModule { }
