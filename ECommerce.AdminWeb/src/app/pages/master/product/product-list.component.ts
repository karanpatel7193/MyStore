import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { AccessModel } from '../../../models/access.model';
import { ToastService } from 'src/app/services/toast.service';
import {  ProductGridModel, ProductListModel, ProductMainModel, ProductMediaModel, ProductModel, ProductParameterModel } from './product.model';
import { ProductService } from './product.service';
import { CategoryMainModel } from '../category/category.model';
import { UserLoginModel } from '../../account/user/user.model';

@Component({
    selector: 'app-product-list',
    templateUrl: './product-list.component.html',
})
export class ProductListComponent implements OnInit {
    public access: AccessModel = new AccessModel();
    public products: ProductModel[] = [];
    public productModel: ProductModel = new ProductModel();
    public productParameterModel: ProductParameterModel = new ProductParameterModel();
    public productGridModel: ProductGridModel = new ProductGridModel();

    public productList: ProductListModel = new ProductListModel();
    public categories: CategoryMainModel[] = [];
    public productMedias: ProductMediaModel[] = [];
    

    constructor(private productService: ProductService,
        private sessionService: SessionService,
        private router: Router,
        private toaster: ToastService) {
        this.setAccess();
    }

    ngOnInit() {
        this.setPageListMode();
    }

    public setPageListMode(): void {

        if (!this.access.canView) {
            this.toaster.warning('You do not have view access of this page.');
            return;
        }

        this.productParameterModel.sortExpression = 'Id';

        this.fillDropdown();
    }

    public fillDropdown(): void {
        this.productService.getListMode(this.productParameterModel).subscribe(data => {
            this.productList = data;
            this.productGridModel.products = this.productList.products;
            this.productGridModel.totalRecords = this.productList.totalRecords;
            this.categories = this.productList.categories;
        });
    }


    public add(): void {
        if (!this.access.canInsert) {
            this.toaster.warning('You do not have add access for this page.');
            return;
        }

        this.router.navigate(['app/master/product/add']);
    }

    public edit(id: number): void {
        if (!this.access.canUpdate) {
            this.toaster.warning('You do not have edit access for this page.');
            return;
        }

        this.router.navigate(['app/master/product/edit', id]);
    }

    public sort(sortExpression: string): void {
        if (sortExpression === this.productParameterModel.sortExpression) {
            this.productParameterModel.sortDirection = this.productParameterModel.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.productParameterModel.sortExpression = sortExpression;
            this.productParameterModel.sortDirection = 'asc';
        }
        this.search();
    }

    public reset(productSelectorRef: any): void {
        this.productParameterModel = new ProductParameterModel();
        this.productParameterModel.sortExpression = 'Id';
        this.productParameterModel.sortDirection = 'asc';
        this.search();
        productSelectorRef.reset();

    }

    public search(): void {
        if (!this.access.canView) {
            this.toaster.warning('You do not have view access for this page.');
            return;
        }

        this.productService.getForGrid(this.productParameterModel).subscribe(data => {
            this.productGridModel = data;
        });
    }

    public delete(id: number): void {
        if (!this.access.canDelete) {
            this.toaster.warning('You do not have delete access for this page.');
            return;
        }

        if (window.confirm('Are you sure you want to delete this product?')) {
            this.productService.delete(id).subscribe(data => {
                this.toaster.success('Product deleted successfully.');
                this.search();
            });
        }
    }

    public sync(id: number): void {
        this.productParameterModel.id = id;
        this.productService.sync(this.productParameterModel).subscribe(data => {
            this.toaster.success('Product Sync successfully.');
            this.search();
        });
    }

    public syncAll(): void {
        this.productService.syncAll().subscribe(data => {
            this.toaster.success('All Product Sync successfully.');
            this.search();
        });
    }
    
    public onProductSelected(product: ProductMainModel): void {
        this.productParameterModel.id = product.id;
        this.productParameterModel.name = product.name;
    }

    public addProperties(productId: number, productName: string, categoryId: number): void {
        this.router.navigate(['/app/master/product/propertyGrid', productId, productName, categoryId]);
    }
    
    public setAccess(): void {
        this.access = this.sessionService.getAccess('master/product');
    }
}
