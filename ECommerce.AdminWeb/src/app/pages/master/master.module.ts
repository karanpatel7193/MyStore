import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MasterRoute } from './master.route';
import { MasterComponent } from './master.component';
import { FileUploadComponent } from '../common/file-upload/file-upload.component';



@NgModule({
  declarations: [
    MasterComponent,
  ],
  imports: [
    CommonModule,
    MasterRoute
  ]
})
export class MasterModule { }
