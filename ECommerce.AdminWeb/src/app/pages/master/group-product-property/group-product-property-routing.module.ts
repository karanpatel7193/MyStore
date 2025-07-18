import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GroupProductPropertyComponent } from './group-product-property.component';
import { GroupProductPropertyListComponent } from './group-product-property-list.component';
import { GroupProductPropertyFormComponent } from './group-product-property-form.component';

const routes: Routes = [
  {
    path: '',
    component: GroupProductPropertyComponent,
    children: [
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full' 
      },
      {
        path: 'list/:groupProductId',
        component: GroupProductPropertyListComponent,
      },
      {
        path: 'add/:groupProductId',
        component: GroupProductPropertyFormComponent,
      },
      {
        path: 'edit/:groupProductId/:id',
        component: GroupProductPropertyFormComponent,
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GroupProductPropertyRoutingModule { }
