
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, UrlSegment } from '@angular/router';
import { EmployeeService } from './employee.service';
import { EmployeeModel, EmployeeAddModel, EmployeeEditModel, EmployeeParameterModel } from './employee.model';
import { SessionService } from '../../../services/session.service';
import { AccessModel } from '../../../models/access.model';
import { CityMainModel } from '../../globalization/city/city.model';
import { StateMainModel } from '../../globalization/state/state.model';
import { ToastService } from 'src/app/services/toast.service';
import { CountryMainModel } from 'src/app/models/country.model';

@Component({
    selector: 'employee-form',
    templateUrl: './employee-form.component.html',
    styles: [`
    nb-card {
      transform: translate3d(0, 0, 0);
    }
  `],
})
export class EmployeeFormComponent implements OnInit, OnDestroy {
    public access: AccessModel = new AccessModel();
    public employee: EmployeeModel = new EmployeeModel();
    public employees: EmployeeModel[] = [];
    public employeeAdd: EmployeeAddModel = new EmployeeAddModel();
    public employeeEdit: EmployeeEditModel = new EmployeeEditModel();
    public employeeParameter: EmployeeParameterModel = new EmployeeParameterModel();
    public countrys: CountryMainModel[] = [];
    public states: StateMainModel[] = [];
    public citys: CityMainModel[] = [];


    public hasAccess: boolean = false;
    public mode: string = '';
    public id: number = 0;
    private sub: any;

    constructor(private employeeService: EmployeeService,
        private sessionService: SessionService,
        private router: Router,
        private route: ActivatedRoute,
        private toastr: ToastService) {

    }

    ngOnInit() {
        this.getRouteData();
    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }

    public getRouteData(): void {
        this.sub = this.route.params.subscribe(params => {
            const segments: UrlSegment[] = this.route.snapshot.url;
            if (segments.toString().toLowerCase() === 'add')
                this.id = 0;
            else
                this.id = +params['id']; // (+) converts string 'id' to a number
            this.setPageMode();
        });
    }

    public clearModels(): void {
        this.employee = new EmployeeModel();
    }

    public setPageMode(): void {
        if (this.id === undefined || this.id === 0)
            this.setPageAddMode();
        else
            this.setPageEditMode();

        if (this.hasAccess) {
        }
    }

    public setPageAddMode(): void {
        if (!this.access.canInsert) {
            this.toastr.warning('You do not have add access of this page.');
            return;
        }
        this.hasAccess = true;
        this.mode = 'Add';


        this.employeeService.getAddMode(this.employeeParameter).subscribe(data => {
            this.employeeAdd = data;
            this.states = this.employeeAdd.states;
            this.citys = this.employeeAdd.citys;
            this.countrys = this.employeeAdd.countrys;
        });

        this.clearModels();
    }

    public setPageEditMode(): void {
        if (!this.access.canUpdate) {
            this.toastr.warning('You do not have update access of this page.');
            return;
        }
        this.hasAccess = true;
        this.mode = 'Edit';


        this.employeeParameter.id = this.id;
        this.employeeService.getEditMode(this.employeeParameter).subscribe(data => {
            this.employeeEdit = data;
            this.employee = this.employeeEdit.employee;
            this.states = this.employeeEdit.states;
            this.citys = this.employeeEdit.citys;
            this.countrys = this.employeeEdit.countrys;
        });

    }

    public save(isFormValid: boolean): void {
        if (isFormValid) {
            if (!this.access.canInsert && !this.access.canUpdate) {
                this.toastr.warning('You do not have add or edit access of this page.');
                return;
            }

            this.employeeService.save(this.employee).subscribe(data => {
                if (data === 0)
                    this.toastr.warning('Record is already exist.');
                else if (data > 0) {
                    this.toastr.success('Record submitted successfully.');
                    this.cancel();
                }
            });
        } else {
            this.toastr.warning('Please provide valid input.');
        }
    }

    public cancel(): void {
        this.router.navigate(['/account/employee/employee/list']);
    }
}