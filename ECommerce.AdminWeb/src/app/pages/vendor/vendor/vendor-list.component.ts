import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { AccessModel } from '../../../models/access.model';
import { ToastService } from 'src/app/services/toast.service';
import { VendorModel, VendorParameterModel, VendorGridModel, VendorListModel } from './vendor.model';
import { VendorService } from './vendor.service';

@Component({
    selector: 'app-vendor-list',
    templateUrl: './vendor-list.component.html',
})
export class VendorListComponent implements OnInit {
    public access: AccessModel = new AccessModel();
    public vendors: VendorModel[] = [];
    public vendorModel: VendorModel = new VendorModel();

    public vendorParameterModel: VendorParameterModel = new VendorParameterModel();
    public vendorGridModel: VendorGridModel = new VendorGridModel();

    public vendorList: VendorListModel = new VendorListModel();


    constructor(private vendorService: VendorService,
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

        this.vendorParameterModel.sortExpression = 'Id';

        this.fillDropdown();
    }

    public fillDropdown(): void {
        this.vendorService.getListMode(this.vendorParameterModel).subscribe(data => {
            this.vendorList = data;
            this.vendorGridModel.vendors = this.vendorList.vendors;
            this.vendorGridModel.totalRecords = this.vendorList.totalRecords;
        });
    }


    public add(): void {
        if (!this.access.canInsert) {
            this.toaster.warning('You do not have add access for this page.');
            return;
        }

        this.router.navigate(['app/vendor/vendor-list/add']);
    }

    public edit(id: number): void {
        if (!this.access.canUpdate) {
            this.toaster.warning('You do not have edit access for this page.');
            return;
        }

        this.router.navigate(['app/vendor/vendor-list/edit', id]);
    }

    public sort(sortExpression: string): void {
        if (sortExpression === this.vendorParameterModel.sortExpression) {
            this.vendorParameterModel.sortDirection = this.vendorParameterModel.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.vendorParameterModel.sortExpression = sortExpression;
            this.vendorParameterModel.sortDirection = 'asc';
        }
        this.search();
    }

    public reset(): void {
        this.vendorParameterModel = new VendorParameterModel();
        this.vendorParameterModel.sortExpression = 'Id';
        this.vendorParameterModel.sortDirection = 'asc';
        this.search();
    }

    public search(): void {
        if (!this.access.canView) {
            this.toaster.warning('You do not have view access for this page.');
            return;
        }

        this.vendorService.getForGrid(this.vendorParameterModel).subscribe(data => {
            this.vendorGridModel = data;
        });
    }

    public delete(id: number): void {
        if (!this.access.canDelete) {
            this.toaster.warning('You do not have delete access for this page.');
            return;
        }

        if (window.confirm('Are you sure you want to delete this vendor?')) {
            this.vendorService.delete(id).subscribe(data => {
                this.toaster.success('Vendor deleted successfully.');
                this.search();
            });
        }
    }

    public addProperties(vendorId: number, vendorName: string, categoryId: number): void {
        this.router.navigate(['/app/vendor/vendor-list/propertyGrid', vendorId, vendorName, categoryId]);
    }

    public setAccess(): void {
        this.access = this.sessionService.getAccess('vendor/vendor-list');
    }
}
