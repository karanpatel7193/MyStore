import { Component, OnInit } from '@angular/core';
import { CustomerService } from './customer.service';
import { CustomerParameterModel, CustomerGridModel } from './customer.model';
import { ToastService } from 'src/app/services/toast.service';
import { AccessModel } from 'src/app/models/access.model';
import { UserLoginModel } from '../account/user/user.model';
import { SessionService } from 'src/app/services/session.service';

@Component({
    selector: 'customer-list',
    templateUrl: './customer-list.componet.html',
    styles: [``],
})
export class CustomerListComponent implements OnInit {
    public customerParameter: CustomerParameterModel = new CustomerParameterModel();
    public customerGrid: CustomerGridModel = new CustomerGridModel();
    public access: AccessModel = new AccessModel();
    public currentUser: UserLoginModel = new UserLoginModel();

    

    constructor(
        private customerService: CustomerService,
        private toaster: ToastService,
        private sessionService: SessionService
    ) {}

    ngOnInit() {
        this.currentUser = this.sessionService.getUser();
        this.setPageListMode();
    }

    public reset(): void {
        this.customerParameter = new CustomerParameterModel();
        this.customerParameter.sortExpression = 'Id';
        this.customerParameter.sortDirection = 'asc';
    }

    public search(): void {
        if (!this.access.canView) {
            this.toaster.warning('You do not have view access for this page.');
            return;
        }
        this.customerParameter.userId = this.currentUser.id;   
        this.customerService.getForGrid(this.customerParameter).subscribe(data => {
            this.customerGrid = data;
        });
    }

    public sort(sortExpression: string): void {
        if (sortExpression === this.customerParameter.sortExpression) {
            this.customerParameter.sortDirection = this.customerParameter.sortDirection === 'asc' ? 'desc' : 'asc';
        } else {
            this.customerParameter.sortExpression = sortExpression;
            this.customerParameter.sortDirection = 'asc';
        }
        this.search();
    }

    public setPageListMode(): void {
        this.customerParameter.sortExpression = 'Id';
        this.search();
    }
}
