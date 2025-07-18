import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { AddressGridModel, AddressModel, AddressParameterModel } from './address.model';
import { environment } from '../../../../../environments/environment';
import { HttpService } from '../../../../services/http.service';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  private apiUrl = environment.apiImageUrl;

  constructor(private http: HttpService) { }

  public getAddressData(addressParameter: AddressParameterModel): Observable<AddressGridModel> {
    return this.http.post(`${this.apiUrl}/api/address/getForGrid`, addressParameter).pipe(
      map((response: AddressGridModel) => response || new AddressGridModel())
    );
  }
  public getAddressById(id: number): Observable<AddressModel> {
    return this.http.get(`${this.apiUrl}/api/address/getRecord/${id}`).pipe(
      map((response: AddressModel) => {
        return response;
      }),
    );
  }
  public insertAddress(addressModel: AddressModel): Observable<number> {
    return this.http.post(`${this.apiUrl}/api/address/insert`, addressModel).pipe(
      map((response: number) => response || 0)
    );
  }

  public updateAddress(addressModel: AddressModel): Observable<boolean> {
    return this.http.post(`${this.apiUrl}/api/address/update`, addressModel).pipe(
      map((response: boolean) => response || false)
    );
  }

  public deleteAddress(addressParameterModel: AddressParameterModel): Observable<boolean> {
    return this.http.post(`${this.apiUrl}/api/address/delete`, addressParameterModel).pipe(
      map((response: boolean) => response || false)
    );
  }
}
