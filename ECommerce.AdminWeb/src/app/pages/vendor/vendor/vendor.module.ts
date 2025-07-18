import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { VendorComponent } from './vendor.component';
import { VendorRoute } from './vendor.route';
import { VendorService } from './vendor.service';
import { FileUploadModule } from "../../common/file-upload/file-upload.module";
import { VendorFormComponent } from './vendor-form.component';
import { VendorListComponent } from './vendor-list.component';
import { StateService } from '../../globalization/state/state.service';

@NgModule({
	imports: [
    FormsModule,
    CommonModule,
    NgbModule,
    VendorRoute,
    FileUploadModule
],
	declarations: [
		VendorComponent,
		VendorFormComponent,
		VendorListComponent,
	],
	providers: [
		VendorService,
		StateService
	]
})
export class VendorModule { }
