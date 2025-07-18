import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryFormComponent } from './category-form/category-form.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { categoryRoute } from './category.route';
import { FormsModule } from '@angular/forms';
import { CategoryService } from './category.service';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { FileUploadModule } from '../../common/file-upload/file-upload.module';

@NgModule({
    declarations: [
        CategoryFormComponent,
        CategoryListComponent
    ],
    imports: [
        CommonModule,
        categoryRoute,
        FormsModule,
        NgbPaginationModule,
        FileUploadModule
    ],
    providers: [
        CategoryService,
    ]
})
export class CategoryModule { }
