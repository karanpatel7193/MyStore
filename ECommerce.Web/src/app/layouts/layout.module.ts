import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

import { AuthLayoutComponent } from './auth-layout/auth-layout.component';
import { AppLayoutComponent } from './app-layout/app-layout.component';
import { FormsModule } from '@angular/forms';
import { HeaderComponent } from '../components/header/header.component';


const routes: Routes = [];

@NgModule({
  declarations: [
    AuthLayoutComponent,
    AppLayoutComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
],
  exports: []
})
export class LayoutModule { }
