
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeeService } from './employee.service';
import { EmployeeParameterModel, EmployeeGridModel, EmployeeListModel, EmployeeModel } from './employee.model';
import { SessionService } from '../../../services/session.service';
import { AccessModel } from '../../../models/access.model';
import { CityMainModel } from '../../globalization/city/city.model';
import { StateMainModel } from '../../globalization/state/state.model';
import { ToastService } from 'src/app/services/toast.service';

@Component({
    selector: 'employee-list',
    templateUrl: './employee-list.component.html',
    styles: [`
    nb-card {
      transform: translate3d(0, 0, 0);
    }
  `],
})
export class EmployeeListComponent implements OnInit {
    public access: AccessModel = new AccessModel();
    public employeeParameter: EmployeeParameterModel = new EmployeeParameterModel();
    public employeeGrid: EmployeeGridModel = new EmployeeGridModel();
    public employeeList: EmployeeListModel = new EmployeeListModel();
    public states: StateMainModel[] = [];
    public citys: CityMainModel[] = [];


    constructor(private employeeService: EmployeeService,
        private sessionService: SessionService,
        private router: Router,
        private toastr: ToastService) {
        this.setAccess();
    }

    ngOnInit() {
        this.setPageListMode();
    }

    public reset(): void {
        this.employeeParameter = new EmployeeParameterModel();
        this.employeeParameter.sortExpression = 'Id';
        this.employeeParameter.sortDirection = 'asc';
        this.search();
    }

    public search(): void {
        if (!this.access.canView) {
            this.toastr.warning('You do not have view access of this page.');
            return;
        }

        this.employeeService.getForGrid(this.employeeParameter).subscribe(data => {
            this.employeeGrid = data;
        });
    }

    public sort(sortExpression: string): void {
        if (sortExpression === this.employeeParameter.sortExpression) {
            this.employeeParameter.sortDirection = this.employeeParameter.sortDirection === 'asc' ? 'desc' : 'asc';
        }
        else {
            this.employeeParameter.sortExpression = sortExpression;
            this.employeeParameter.sortDirection = 'asc';
        }
        this.search();
    }

    public add(): void {
        if (!this.access.canInsert) {
            this.toastr.warning('You do not have add access of this page.');
            return;
        }
        console.log('form button clicked!')
        this.router.navigate(['app/account/employee/add']);
    }

    public edit(id: number): void {
        if (!this.access.canUpdate) {
            this.toastr.warning('You do not have edit access of this page.');
            return;
        }

        this.router.navigate(['app/account/employee/edit', id]);
    }

    public delete(id: number): void {
        if (!this.access.canDelete) {
            this.toastr.warning('You do not have delete access of this page.');
            return;
        }

        if (window.confirm('Are you sure you want to delete?')) {
            this.employeeService.delete(id).subscribe(data => {
                this.toastr.success('Record deleted successfully.');
                this.search();
            });
        }
    }

    public setPageListMode(): void {
        if (!this.access.canView) {
            this.toastr.warning('You do not have view access of this page.');
            return;
        }

        this.employeeParameter.sortExpression = 'Id';
        this.setParameterByStateParam();

        this.employeeService.getListMode(this.employeeParameter).subscribe(data => {
            this.employeeList = data;
            this.employeeGrid.employees = this.employeeList.employees;
            this.employeeGrid.totalRecords = this.employeeList.totalRecords;
            this.states = this.employeeList.states;
            this.citys = this.employeeList.citys;

        });

    }

    public setParameterByStateParam(): void {

    }

    public setAccess(): void {
        const currentUrl = this.router.url.substring(0, this.router.url.indexOf('/list'));
        this.access = this.sessionService.getAccess(currentUrl);
    }
}
