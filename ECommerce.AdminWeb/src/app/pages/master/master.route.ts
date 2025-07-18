import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "src/app/interceptors/auth.guard";
import { MasterComponent } from "./master.component";

const routes: Routes = [
    {
        path: '',
        component: MasterComponent, 
        children: [
            {
                path: 'script',
                loadChildren: () => import('./script/script.module').then(m => m.ScriptModule),
                canActivate: [AuthGuard]
			},
            {
                path: 'master-values',
                loadChildren: () => import('./master/master.module').then(m => m.MasterModule),
                canActivate: [AuthGuard]
			},
            {
                path: 'product',
                loadChildren: () => import('./product/product.module').then(m => m.ProductModule),
                canActivate: [AuthGuard]
			},
            {
				path: 'property',
				loadChildren: () => import('../master/property/property.module').then(m => m.PropertyModule),
				canActivate: [AuthGuard]
			},
            {
				path: 'category',
				loadChildren: () => import('../master/category/category.module').then(m => m.CategoryModule),
				canActivate: [AuthGuard]
			},
            {
				path: 'category-property',
				loadChildren: () => import('../master/category-property/category-property.module').then(m => m.CategoryPropertyModule),
				canActivate: [AuthGuard]
			},
        ]
        
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MasterRoute {
}
