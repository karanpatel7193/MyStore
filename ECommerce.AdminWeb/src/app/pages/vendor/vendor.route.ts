import { ExtraOptions, RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { AuthGuard } from 'src/app/interceptors/auth.guard';
import { VendorComponent } from './vendor.component';

const routes: Routes = [
    {
        path: '',
        component:VendorComponent,
        children: [
            {
                path: 'vendor-list',
                loadChildren: () => import('./vendor/vendor.module').then(m => m.VendorModule),
                canActivate: [AuthGuard]
			},
        ],
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class VendorRoute {
}
