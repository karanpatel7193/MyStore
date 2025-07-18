import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AppLayoutComponent } from '../layouts/app-layout/app-layout.component';
import { AuthGuard } from '../interceptors/auth.guard';

const routes: Routes = [
    {
        path: '',
        component: AppLayoutComponent, 
        children: [
            {
                path: 'dashboard',
                loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
                canActivate: [AuthGuard]
			},
			{
				path: 'master',
				loadChildren: () => import('./master/master.module').then(m => m.MasterModule),
				canActivate: [AuthGuard]
			},
            {
				path: 'account',
				loadChildren: () => import('./account/account.module').then(m => m.AccountModule),
				canActivate: [AuthGuard]
			},
            {
                path: 'customer',
                loadChildren: () => import('./customer/customer.module').then(m => m.CustomerModule),
                canActivate: [AuthGuard]
			},
            {
				path: 'vendor',
				loadChildren: () => import('./vendor/vendor.module').then(m => m.VendorModule),
				canActivate: [AuthGuard]
			},
            {
				path: 'homepage',
				loadChildren: () => import('./homepage/homepage.module').then(m => m.HomepageModule),
				canActivate: [AuthGuard]
			},
            {
				path: 'purchase',
				loadChildren: () => import('./purchase/purchase.module').then(m => m.PurchaseModule),
				canActivate: [AuthGuard]
			},
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class PageRoute {
}
