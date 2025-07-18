import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryPropertyComponent } from './category-property.component';
import { CategoryPropertyListComponent } from './category-property-list.component';
import { CategoryPropertyFormComponent } from './category-property-form.component';

const routes: Routes = [
  {
    path: '',
    component: CategoryPropertyComponent,
    children: [
      {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full' 
      },
      {
        path: 'list/:categoryId/:categoryName',
        component: CategoryPropertyListComponent,
      },
      {
        path: 'add/:categoryId/:categoryName',
        component: CategoryPropertyFormComponent,
      },
      {
        path: 'edit/:categoryId/:categoryName/:id',
        component: CategoryPropertyFormComponent,
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryPropertyRoutingModule { }
