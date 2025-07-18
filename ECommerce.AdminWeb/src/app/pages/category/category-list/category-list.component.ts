// import { Component, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { AccessModel } from 'src/app/models/access.model';
// import { CategoryGridModel, CategoryModel, CategoryParameterModel } from '../../master/category/category.model';
// import { CategoryService } from '../../master/category/category.service';
// import { SessionService } from 'src/app/services/session.service';
// import { ToastService } from 'src/app/services/toast.service';

// @Component({
//   selector: 'app-category-list',
//   templateUrl: './category-list.component.html',
//   styleUrls: ['./category-list.component.scss']
// })
// export class CategoryListComponent implements OnInit {
//     public access: AccessModel = new AccessModel();
//     public categoryParameter: CategoryParameterModel = new CategoryParameterModel();
//     public categoryGridModel: CategoryGridModel = new CategoryGridModel();
//     public parentCategories: CategoryModel[] = [];
//     public selectedCategory: string = '';

//     constructor(private categoryService: CategoryService,
//         private sessionService: SessionService,
//         private router: Router,
//         private toaster: ToastService) {
//         this.setAccess();
//     }

//     ngOnInit() {
//         this.setPageListMode();
//     }

//     public reset(): void {
//         this.categoryParameter = new CategoryParameterModel();
//         this.categoryParameter.sortExpression = 'id';
//         this.categoryParameter.sortDirection = 'asc';
//         this.search();
//     }

//     public search(): void {
//         if (!this.access.canView) {
//             this.toaster.warning('You do not have view access of this page.');
//             return;
//         }
//         this.categoryService.getForGrid(this.categoryParameter).subscribe(data => {
//             this.categoryGridModel = data;
//         });
//     }

//     public sort(sortExpression: string): void {
//         debugger
//         if (sortExpression === this.categoryParameter.sortExpression) {
//             this.categoryParameter.sortDirection = this.categoryParameter.sortDirection === 'asc' ? 'desc' : 'asc';
//         } else {
//             this.categoryParameter.sortExpression = sortExpression;
//             this.categoryParameter.sortDirection = 'asc';
//         }
//         this.search();
//     }

//     public add(): void {
//         if (!this.access.canInsert) {
//             this.toaster.warning('You do not have add access of this page.');
//             return;
//         }

//         this.router.navigate(['app/category/add']);
//     }

//     public edit(id: number): void {
//         if (!this.access.canUpdate) {
//             this.toaster.warning('You do not have edit access of this page.');
//             return;
//         }

//         this.router.navigate(['app/category/edit', id]);
//     }

//     public categoryAccess(item: any): void {
//         if (!this.access.canUpdate) {
//             this.toaster.warning('You do not have edit access of this page.');
//             return;
//         }

//         this.router.navigate(['app/categoryaccess',item.id,item.name]);
//     }

//     public delete(id: number): void {
//         if (!this.access.canDelete) {
//             this.toaster.warning('You do not have delete access of this page.');
//             return;
//         }

//         if (window.confirm('Are you sure you want to delete?')) {
//             this.categoryService.delete(id).subscribe(data => {
//                 this.toaster.success('Record deleted successfully.');
//                 this.search();
//             });
//         }
//     }

//     public setPageListMode(): void {

//         if (!this.access.canView) {
//             this.toaster.warning('You do not have view access of this page.');
//             return;
//         }
//         this.categoryParameter.sortExpression = 'Id';
//         this.categoryService.getParent().subscribe(data => {
//             this.parentCategories = data
//             },
//         );
//         this.search();
//     }

//     public addProperties(){
        
//     }

//     public setAccess(): void {
//         this.access = this.sessionService.getAccess('category');
//     }
// }
