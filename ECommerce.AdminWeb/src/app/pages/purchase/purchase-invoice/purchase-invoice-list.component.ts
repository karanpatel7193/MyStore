import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { AccessModel } from '../../../models/access.model';
import { ToastService } from 'src/app/services/toast.service';
import { PurchaseInvoiceGridModel, PurchaseInvoiceListModel, PurchaseInvoiceMainModel, PurchaseInvoiceModel, PurchaseInvoiceParameterModel } from './purchase-invoice.model';
import { PurchaseInvoiceService } from './purchase-invoice.service';
import { ProductMainModel } from '../../master/product/product.model';

@Component({
    selector: 'app-purchase-invoice-list',
    templateUrl: './purchase-invoice-list.component.html',
})
export class PurchaseInvoiceListComponent implements OnInit {
    public access: AccessModel = new AccessModel();
    public purcahseOrder: PurchaseInvoiceModel[] = [];
    public PurchaseInvoiceModeldel: PurchaseInvoiceModel = new PurchaseInvoiceModel();
    public PurchaseInvoiceParameterModel: PurchaseInvoiceParameterModel = new PurchaseInvoiceParameterModel();
    public PurchaseInvoiceGridModel: PurchaseInvoiceGridModel = new PurchaseInvoiceGridModel();
    public PurchaseInvoiceListModel: PurchaseInvoiceListModel = new PurchaseInvoiceListModel();
    public vendors: PurchaseInvoiceMainModel[] = [];
    public products: PurchaseInvoiceMainModel[] = [];
    

    constructor(private PurchaseInvoiceService: PurchaseInvoiceService,
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

        this.PurchaseInvoiceParameterModel.sortExpression = 'Id';

        this.fillDropdown();
    }

    public fillDropdown(): void {
        this.PurchaseInvoiceService.getListMode(this.PurchaseInvoiceParameterModel).subscribe(data => {
            this.PurchaseInvoiceListModel = data;
            this.PurchaseInvoiceGridModel.invoices = this.PurchaseInvoiceListModel.invoices;
            this.PurchaseInvoiceGridModel.totalRecords = this.PurchaseInvoiceListModel.totalRecords;
            this.vendors = this.PurchaseInvoiceListModel.vendors;
            this.products = this.PurchaseInvoiceListModel.products;
        });
    }


    public add(): void {
        if (!this.access.canInsert) {
            this.toaster.warning('You do not have add access for this page.');
            return;
        }

        this.router.navigate(['app/purchase/purchase-invoice/add']);
    }

    public edit(id: number): void {
        if (!this.access.canUpdate) {
            this.toaster.warning('You do not have edit access for this page.');
            return;
        }

        this.router.navigate(['app/purchase/purchase-invoice/edit', id]);
    }

    public sort(sortExpression: string): void {
        if (sortExpression === this.PurchaseInvoiceParameterModel.sortExpression) {
            this.PurchaseInvoiceParameterModel.sortDirection = this.PurchaseInvoiceParameterModel.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.PurchaseInvoiceParameterModel.sortExpression = sortExpression;
            this.PurchaseInvoiceParameterModel.sortDirection = 'asc';
        }
        this.search();
    }

    public reset(): void {
        this.PurchaseInvoiceParameterModel = new PurchaseInvoiceParameterModel();
        this.PurchaseInvoiceParameterModel.sortExpression = 'Id';
        this.PurchaseInvoiceParameterModel.sortDirection = 'asc';
        this.search();
    }

    public search(): void {
        if (!this.access.canView) {
            this.toaster.warning('You do not have view access for this page.');
            return;
        }

        this.PurchaseInvoiceService.getForGrid(this.PurchaseInvoiceParameterModel).subscribe(data => {
            this.PurchaseInvoiceGridModel = data;
        });
        
    }

    public onProductSelected(product: ProductMainModel): void {
        this.PurchaseInvoiceParameterModel.productId = product.id;
    }

    public delete(id: number): void {
        if (!this.access.canDelete) {
            this.toaster.warning('You do not have delete access for this page.');
            return;
        }

        if (window.confirm('Are you sure you want to delete this product?')) {
            this.PurchaseInvoiceService.delete(id).subscribe(data => {
                this.toaster.success('Product deleted successfully.');
                this.search();
            });
        }
    }

    public setAccess(): void {
        this.access = this.sessionService.getAccess('purchase/purchase-invoice');
    }
}
