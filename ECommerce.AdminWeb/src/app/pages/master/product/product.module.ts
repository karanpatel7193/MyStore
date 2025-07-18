import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProductFormComponent } from './product-form.component';
import { ProductListComponent } from './product-list.component';
import { ProductComponent } from './product.component';
import { ProductRoute } from './product.route';
import { ProductService } from './product.service';
import { FileUploadModule } from "../../common/file-upload/file-upload.module";
import { ProductPropertyListComponent } from '../product-property/product-property-list.component';
import { ProductSelectorModule } from '../../common/product-selector/product-selector.module';
import { CategoryPropertyService } from '../category-property/category-property.service';
import { ProductVariantService } from '../productVariant/productVariant.service';
import { TagInputModule } from 'ngx-chips';

@NgModule({
	imports: [
    FormsModule,
    CommonModule,
    NgbModule,
    ProductRoute,
    FileUploadModule,
    ProductSelectorModule,
	TagInputModule
],
	declarations: [
		ProductComponent,
		ProductFormComponent,
		ProductListComponent,
        ProductPropertyListComponent
	],
	providers: [
		ProductService,
		CategoryPropertyService,
		ProductVariantService
	]
})
export class ProductModule { }
