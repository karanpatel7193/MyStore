import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { CartGridModel, CartMainModel, CartModel, CartParameterModel } from './cart.model';
import { environment } from '../../../environments/environment';
import { HttpService } from '../../services/http.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private apiUrl = environment.apiUrl;

  constructor(private http: HttpService) {}

  public getCartData(cartParameterModel: CartParameterModel): Observable<CartGridModel> {
    return this.http.post(`${this.apiUrl}cart/getForGrid`, cartParameterModel).pipe(
      map((response: any) => response || new CartGridModel())
    );
  }

  public insertCart(cartModel: CartModel): Observable<number> {
    return this.http.post(`${this.apiUrl}cart/insert`, cartModel).pipe(
      map((response: any) => response?.data || 0)
    );
  }

  public insertBulk(cartMainModel: CartMainModel): Observable<number> {
    return this.http.post(`${this.apiUrl}cart/insertBulk`, cartMainModel).pipe(
      map((response: any) => response?.data || 0)
    );
  }

  public updateCart(cartModel: CartModel): Observable<boolean> {
    return this.http.post(`${this.apiUrl}cart/update`, cartModel).pipe(
      map((response: any) => {
        return response?.data === true;
      })
    );
  }
  

  public deleteCart(cartParameterModel: CartParameterModel): Observable<boolean> {
    return this.http.post(`${this.apiUrl}cart/delete`, cartParameterModel).pipe(
      map((response: any) => response?.data || false)
    );
  }
}
