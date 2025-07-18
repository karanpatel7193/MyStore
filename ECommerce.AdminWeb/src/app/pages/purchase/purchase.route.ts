import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "src/app/interceptors/auth.guard";
import {  PurchaseComponent } from "./purchase.component";

const routes: Routes = [
    {
        path: '',
        component: PurchaseComponent, 
        children: [
            {
                path: 'purchase-order',
                loadChildren: () => import('./purchase-order/purchase-order.module').then(m => m.PurchaseOrderModule),
                canActivate: [AuthGuard]
			},
            {
                path: 'purchase-invoice',
                loadChildren: () => import('./purchase-invoice/purchase-invoice.module').then(m => m.PurchaseInvoiceModule),
                canActivate: [AuthGuard]
			},
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class PurchaseRoute {
}
