import { Injectable } from '@angular/core';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { HttpService } from 'src/app/services/http.service';
import { CustomerGridModel, CustomerMainModel, CustomerModel, CustomerParameterModel } from './customer.model';

@Injectable()
export class CustomerService {
    constructor(private http: HttpService) {}

    public getRecord(id: number): Observable<CustomerModel> {
        return this.http.get('admin/customer/getRecord/' + id).pipe(
            map((response: CustomerModel) => response)
        );
    }

    public getForLOV(customerParameterModel: CustomerParameterModel): Observable<CustomerMainModel[]> {
        return this.http.post('admin/customer/getForLOV', customerParameterModel).pipe(
            map((response: CustomerMainModel[]) => response)
        );
    }

    public getForGrid(customerParameterModel: CustomerParameterModel): Observable<CustomerGridModel> {
        return this.http.post('admin/customer/getForGrid', customerParameterModel).pipe(
            map((response: CustomerGridModel) => response)
        );
    }

    public save(customer: CustomerModel): Observable<number> {
        if (customer.id === 0) {
            return this.http.post('admin/customer/insert', customer).pipe(
                map((response: number) => response)
            );
        } else {
            return this.http.post('admin/customer/update', customer).pipe(
                map((response: number) => response)
            );
        }
    }

    public delete(id: number): Observable<void> {
        return this.http.post('admin/customer/delete/' + id, null).pipe(
            map(() => {})
        );
    }
}
