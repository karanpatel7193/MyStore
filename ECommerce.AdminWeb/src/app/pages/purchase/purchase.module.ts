import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {  PurchaseRoute } from './purchase.route';
import {  PurchaseComponent } from './purchase.component';

@NgModule({
  declarations: [
    PurchaseComponent,
  ],
  imports: [
    CommonModule,
    PurchaseRoute
  ]
})
export class PurchaseModule { }
