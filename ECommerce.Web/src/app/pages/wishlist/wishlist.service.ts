import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { WishlistModel, WishlistParameterModel, WishlistGridModel, WishlistProductModel } from './wishlist.model';
import { HttpService } from '../../services/http.service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private apiUrl = environment.apiUrl;
  private wishlistCountSubject = new BehaviorSubject<number>(0); // Store wishlist count
  wishlistCount$ = this.wishlistCountSubject.asObservable();

  constructor(private http: HttpService) { }

  public addToWishlist(wishlistModel: WishlistProductModel): Observable<any> {
    return this.http.post(`${this.apiUrl}wishlist/insert`, wishlistModel);
  }

  public removeFromWishlist(wishlistParameterModel: WishlistParameterModel): Observable<any> {
    return this.http.post(`${this.apiUrl}wishlist/deleteWishlist`, wishlistParameterModel);
  }

  public getForGrid(wishlistParameterModel: WishlistParameterModel): Observable<WishlistGridModel> {
    return this.http.post(`${this.apiUrl}wishlist/getForGrid`, wishlistParameterModel).pipe(
      map((response: WishlistGridModel) => response)
    );
  }

  public checkIfProductInWishlist(wishlistParameterModel: WishlistParameterModel): Observable<any> {
    return this.http.post(`${this.apiUrl}wishlist/CheckIfProductInWishlist`, wishlistParameterModel);
  }
  updateWishlistCount(count: number): void {
    this.wishlistCountSubject.next(count);
  }
}

