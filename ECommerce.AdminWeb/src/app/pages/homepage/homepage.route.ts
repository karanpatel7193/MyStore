import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "src/app/interceptors/auth.guard";
import { HomepageComponent } from "./homepage.component";

const routes: Routes = [
    {
        path: '',
        component: HomepageComponent, 
        children: [
            {
				path: 'block',
				loadChildren: () => import('../homepage/block/block.module').then(m => m.BlockModule),
				canActivate: [AuthGuard]
			},
        ]
        
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class HomepageRoute {
}
