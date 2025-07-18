
import { Injectable } from '@angular/core';
import { EmployeeModel, EmployeeMainModel, EmployeeParameterModel, EmployeeGridModel, EmployeeAddModel, EmployeeEditModel, EmployeeListModel } from './employee.model';
import { Observable, map } from 'rxjs';
import { HttpService } from 'src/app/services/http.service';

@Injectable()
export class EmployeeService {
    constructor(private http: HttpService) {
    }

    public getRecord(id: number): Observable<EmployeeModel> {
        return this.http.get('account/employee/employee/getRecord/' + id).pipe(
            map((response: EmployeeModel) => {
                return response;
            }),
        );
    }

    public getForLOV(employeeParameter: EmployeeParameterModel): Observable<EmployeeMainModel[]> {
        return this.http.post('account/employee/employee/getLovValue', employeeParameter).pipe(
            map((response: EmployeeMainModel[]) => {
                return response;
            }),
        );
    }

	
    public getAddMode(employeeParameter: EmployeeParameterModel): Observable<EmployeeAddModel> {
        return this.http.post('account/employee/employee/getAddMode', employeeParameter).pipe(
            map((response: EmployeeAddModel) => {
                return response;
            }),
        );
    }

    public getEditMode(employeeParameter: EmployeeParameterModel): Observable<EmployeeEditModel> {
        return this.http.post('account/employee/employee/getEditMode', employeeParameter).pipe(
            map((response: EmployeeEditModel) => {
                return response;
            }),
        );
    }
	

    public getForGrid(employeeParameter: EmployeeParameterModel): Observable<EmployeeGridModel> {
        return this.http.post('account/employee/employee/getGridData', employeeParameter).pipe(
            map((response: EmployeeGridModel) => {
                return response;
            }),
        );
    }

	
    public getListMode(employeeParameter: EmployeeParameterModel): Observable<EmployeeListModel> {
        return this.http.post('account/employee/employee/getListMode', employeeParameter).pipe(
            map((response: EmployeeListModel) => {
                return response;
            }),
        );
    }
	

    public save(employee: EmployeeModel): Observable<number> {
        if (employee.id === 0)
            return this.http.post('account/employee/employee/insert', employee).pipe(
                map((response: number) => {
                    return response;
                }),
            );
        else
            return this.http.post('account/employee/employee/update', employee).pipe(
                map((response: number) => {
                    return response;
                }),
            );
    }

    public delete(id: number): Observable<void> {
        return this.http.post('account/employee/employee/delete/' + id, null).pipe(
            map((response: void) => {
                return response;
            }),
        );
    }
}
