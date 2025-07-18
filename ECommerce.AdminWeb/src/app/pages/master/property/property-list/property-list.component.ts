import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccessModel } from 'src/app/models/access.model';
import { SessionService } from 'src/app/services/session.service';
import { ToastService } from 'src/app/services/toast.service';
import { PropertyParameterModel, PropertyGridModel, PropertyModel } from '../property.model';
import { PropertyService } from '../property.service';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.scss']
})
export class PropertyListComponent implements OnInit {
    public access: AccessModel = new AccessModel();
    public propertyParameter: PropertyParameterModel = new PropertyParameterModel();
    public propertyGrid: PropertyGridModel = new PropertyGridModel();
    public propertys: PropertyModel[] = [];
    public selectedProperty: string = '';

    constructor(private propertyService: PropertyService,
        private sessionService: SessionService,
        private router: Router,
        private toaster: ToastService) {
        this.setAccess();
    }

    ngOnInit() {
        this.setPageListMode();
    }

    public reset(): void {
        this.propertyParameter = new PropertyParameterModel();
        this.propertyParameter.sortExpression = 'Id';
        this.propertyParameter.sortDirection = 'asc';
    }

    public search(): void {
        if (!this.access.canView) {
            this.toaster.warning('You do not have view access of this page.');
            return;
        }

        this.propertyService.getForGrid(this.propertyParameter).subscribe(data => {
            this.propertyGrid = data;
        });

    }

    public sort(sortExpression: string): void {
        if (sortExpression === this.propertyParameter.sortExpression) {
            this.propertyParameter.sortDirection = this.propertyParameter.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.propertyParameter.sortExpression = sortExpression;
            this.propertyParameter.sortDirection = 'asc';
        }
        this.search();
    }

    public add(): void {
        if (!this.access.canInsert) {
            this.toaster.warning('You do not have add access of this page.');
            return;
        }

        this.router.navigate(['app/master/property/add']);
    }

    public edit(id: number): void {
        if (!this.access.canUpdate) {
            this.toaster.warning('You do not have edit access of this page.');
            return;
        }

        this.router.navigate(['app/master/property/edit', id]);
    }

    public propertyAccess(item: any): void {
        if (!this.access.canUpdate) {
            this.toaster.warning('You do not have edit access of this page.');
            return;
        }

        this.router.navigate(['app/master/property/access',item.id,item.name]);
    }

    public delete(id: number): void {
        if (!this.access.canDelete) {
            this.toaster.warning('You do not have delete access of this page.');
            return;
        }

        if (window.confirm('Are you sure you want to delete?')) {
            this.propertyService.delete(id).subscribe(data => {
                this.toaster.success('Record deleted successfully.');
                this.search();
            });
        }
    }

    public setPageListMode(): void {

        if (!this.access.canView) {
            this.toaster.warning('You do not have view access of this page.');
            return;
        }

        this.propertyParameter.sortExpression = 'Id';
        this.search();
    }

    public setAccess(): void {
        this.access = this.sessionService.getAccess('master/property');
    }
}

