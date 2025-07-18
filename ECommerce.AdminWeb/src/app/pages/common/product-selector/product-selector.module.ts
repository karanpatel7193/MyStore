import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { ProductSelectorComponent } from './product-selector.component';
import { ProductService } from '../../master/product/product.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DatepickerModule } from 'src/app/components/datepicker/datepicker.module';
import { LayoutModule } from 'src/app/layouts/layout.module';
import { PageRoute } from '../../page.route';

@NgModule({
    declarations: [
        ProductSelectorComponent
    ],
    exports: [ProductSelectorComponent],
    imports: [
       CommonModule,
            LayoutModule,
            FormsModule,
            NgbModule,
            PageRoute,
            DatepickerModule

    ],
    providers: [
        ProductService
    ]
})
export class ProductSelectorModule { }
