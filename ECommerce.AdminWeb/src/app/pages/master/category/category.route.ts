import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/interceptors/auth.guard';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryFormComponent } from './category-form/category-form.component'

const routes: Routes = [
    {
        path: '', // Parent path
        children: [
            {
                path: 'list', // Matches /category/list
                component: CategoryListComponent,
                canActivate: [AuthGuard],    
            },
            {
                path: 'add', // Matches /category/add
                component: CategoryFormComponent,
                canActivate: [AuthGuard],
            },
            {
                path: 'edit/:id', // Matches /category/edit/:id
                component: CategoryFormComponent,
                canActivate: [AuthGuard],
            },
            {
                path: '', // Redirect to /category/list by default
                redirectTo: 'list',
                pathMatch: 'full',
            },
        ],
    },
];


@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class categoryRoute { }
