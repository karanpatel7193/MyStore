import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { PropertyComponent } from './property.component';
import { PropertyRoute } from './property.route';
import { PropertyService } from './property.service';
import { PropertyFormComponent } from './property-form/property-form.component';
import { PropertyListComponent } from './property-list/property-list.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule,
        NgbPaginationModule,
        PropertyRoute
    ],
    declarations: [
        PropertyComponent,
        PropertyFormComponent,
        PropertyListComponent
    ],
    providers: [
        PropertyService,
    ],
})
export class PropertyModule { }
