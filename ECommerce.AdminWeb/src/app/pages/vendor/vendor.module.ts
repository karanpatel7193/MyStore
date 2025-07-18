import { NgModule } from '@angular/core';
import { VendorComponent } from './vendor.component';
import { VendorRoute } from './vendor.route';

@NgModule({
    imports: [
        VendorRoute
    ],
    declarations: [
    VendorComponent
  ],
    providers: [
    ],
})
export class VendorModule { }
