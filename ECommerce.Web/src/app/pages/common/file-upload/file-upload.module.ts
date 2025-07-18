import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { FileUploadComponent } from './file-upload.component';

@NgModule({
    declarations: [
        FileUploadComponent
    ],
    exports: [FileUploadComponent],
    imports: [
        CommonModule,
        FormsModule,
    ],
    providers: [
    ]
})
export class FileUploadModule { }
