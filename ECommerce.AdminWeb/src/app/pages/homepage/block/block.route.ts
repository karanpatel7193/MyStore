import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/interceptors/auth.guard';
import {  BlockFormComponent } from './block-form.component';
import { BlockComponent } from './block.component';
import { BlockListComponent } from './block-list.component';

const routes: Routes = [{
    path: '',
    component: BlockComponent,
    children: [
        {
            path:'',
            redirectTo: 'list',
            pathMatch: 'full' 
        },
        {
            path: 'list',
            component: BlockListComponent,
            canActivate : [AuthGuard]
        },
        {
            path: 'add',
            component: BlockFormComponent,
            canActivate : [AuthGuard]
        },
        {
            path: 'edit/:id',
            component: BlockFormComponent,
            canActivate : [AuthGuard]

        },
    ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BlockRoute { }
