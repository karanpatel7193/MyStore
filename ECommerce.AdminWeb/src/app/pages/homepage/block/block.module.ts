import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlockRoute,} from './block.route';
import { FormsModule } from '@angular/forms';
import { NgbModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { BlockService } from './block.service';
import {  BlockFormComponent } from './block-form.component';
import { BlockComponent } from './block.component';
import { BlockListComponent } from './block-list.component';
import { ProductSelectorModule } from "../../common/product-selector/product-selector.module";
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { LayoutModule } from 'src/app/layouts/layout.module';
import { PageRoute } from '../../page.route';

@NgModule({
    declarations: [
        BlockFormComponent,
        BlockComponent,
        BlockListComponent
    ],
    imports: [

        LayoutModule,
        FormsModule,
        NgbModule,
        CommonModule,
        BlockRoute,
        FormsModule,
        NgbPaginationModule,
        ProductSelectorModule,
        HttpClientModule,

    ],
    providers: [
        BlockService,
    ],

    schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class BlockModule { }
