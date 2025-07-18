import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryPropertyRoutingModule } from './category-property-routing.module';
import { CategoryPropertyListComponent } from './category-property-list.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CategoryPropertyService } from './category-property.service';
import { CategoryPropertyComponent } from './category-property.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CategoryPropertyFormComponent } from './category-property-form.component';


@NgModule({
  declarations: [
    CategoryPropertyListComponent,
    CategoryPropertyComponent,
    CategoryPropertyFormComponent
  ],
  imports: [
    CommonModule,
    CategoryPropertyRoutingModule,
    NgbModule,
    FormsModule
  ],
  providers: [
    CategoryPropertyService
]
})
export class CategoryPropertyModule { }
