import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {  GroupProductPropertyRoutingModule } from './group-product-property-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GroupProductFormComponent } from '../group-product/group-product-form.component';
import { GroupProductListComponent } from '../group-product/group-product-list.component';
import { GroupProductPropertyService } from './group-product-property.service';
import { GroupProductPropertyFormComponent } from './group-product-property-form.component';
import { GroupProductPropertyListComponent } from './group-product-property-list.component';
import { GroupProductPropertyComponent } from './group-product-property.component';


@NgModule({
  declarations: [
    GroupProductPropertyFormComponent,
    GroupProductPropertyListComponent,
    GroupProductPropertyComponent,
  ],
  imports: [
    CommonModule,
    GroupProductPropertyRoutingModule,
    NgbModule,
    FormsModule
  ],
  providers: [
    GroupProductPropertyService
]
})
export class GroupProductPropertyModule { }
