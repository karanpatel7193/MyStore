import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { AccessModel } from '../../../models/access.model';
import { ToastService } from 'src/app/services/toast.service';
import { PurchaseOrderGridModel, PurchaseOrderListModel, PurchaseOrderMainModel, PurchaseOrderModel, PurchaseOrderParameterModel } from './purchase-order.model';
import { PurchaseOrderService } from './purchase-order.service';
import { ProductMainModel } from '../../master/product/product.model';

@Component({
    selector: 'app-purchase-order-list',
    templateUrl: './purchase-order-list.component.html',
})
export class PurchaseOrderListComponent implements OnInit {
    public access: AccessModel = new AccessModel();
    public purcahseOrder: PurchaseOrderModel[] = [];
    public purchaseOrderModeldel: PurchaseOrderModel = new PurchaseOrderModel();
    public purchaseOrderParameterModel: PurchaseOrderParameterModel = new PurchaseOrderParameterModel();
    public purchaseOrderGridModel: PurchaseOrderGridModel = new PurchaseOrderGridModel();
    public purchaseOrderListModel: PurchaseOrderListModel = new PurchaseOrderListModel();
    public vendors: PurchaseOrderMainModel[] = [];
    public products: PurchaseOrderMainModel[] = [];
    

    constructor(private purchaseOrderService: PurchaseOrderService,
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

        this.purchaseOrderParameterModel.sortExpression = 'Id';

        this.fillDropdown();
    }

    public fillDropdown(): void {
        this.purchaseOrderService.getListMode(this.purchaseOrderParameterModel).subscribe(data => {
            this.purchaseOrderListModel = data;
            this.purchaseOrderGridModel.orderInvoices = this.purchaseOrderListModel.orderInvoices;
            this.purchaseOrderGridModel.totalRecords = this.purchaseOrderListModel.totalRecords;
            this.vendors = this.purchaseOrderListModel.vendors;
            this.products = this.purchaseOrderListModel.products;
        });
    }


    public add(): void {
        if (!this.access.canInsert) {
            this.toaster.warning('You do not have add access for this page.');
            return;
        }

        this.router.navigate(['app/purchase/purchase-order/add']);
    }

    public edit(id: number): void {
        if (!this.access.canUpdate) {
            this.toaster.warning('You do not have edit access for this page.');
            return;
        }

        this.router.navigate(['app/purchase/purchase-order/edit', id]);
    }

    public sort(sortExpression: string): void {
        if (sortExpression === this.purchaseOrderParameterModel.sortExpression) {
            this.purchaseOrderParameterModel.sortDirection = this.purchaseOrderParameterModel.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.purchaseOrderParameterModel.sortExpression = sortExpression;
            this.purchaseOrderParameterModel.sortDirection = 'asc';
        }
        this.search();
    }

    public reset(): void {
        this.purchaseOrderParameterModel = new PurchaseOrderParameterModel();
        this.purchaseOrderParameterModel.sortExpression = 'Id';
        this.purchaseOrderParameterModel.sortDirection = 'asc';
        this.search();
    }

    public search(): void {
        if (!this.access.canView) {
            this.toaster.warning('You do not have view access for this page.');
            return;
        }

        this.purchaseOrderService.getForGrid(this.purchaseOrderParameterModel).subscribe(data => {
            this.purchaseOrderGridModel = data;
        });
    }

    public onProductSelected(product: ProductMainModel): void {
        this.purchaseOrderParameterModel.productId = product.id;
    }

    public delete(id: number): void {
        if (!this.access.canDelete) {
            this.toaster.warning('You do not have delete access for this page.');
            return;
        }

        if (window.confirm('Are you sure you want to delete this product?')) {
            this.purchaseOrderService.delete(id).subscribe(data => {
                this.toaster.success('Product deleted successfully.');
                this.search();
            });
        }
    }

    public setAccess(): void {
        this.access = this.sessionService.getAccess('purchase/purchase-order');
    }
}
