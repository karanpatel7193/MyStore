import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CustomerRoute } from './customer.route';
import { CustomerService } from './customer.service';
import { CustomerListComponent } from './customer-list.componet';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    CustomerListComponent
    
  ],
  imports: [
    CommonModule,
    FormsModule,
    CustomerRoute,
    NgbPaginationModule

],
  providers:[
    CustomerService
  ]
})
export class CustomerModule { }
