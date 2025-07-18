import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccessModel } from '../models/access.model';
import { UserLoginModel } from '../pages/account/user/user.model';
import { WishlistModel } from '../pages/wishlist/wishlist.model';
import { CartService } from '../pages/cart/cart.service';
import { CartMainModel, CartModel } from '../pages/cart/cart.model';

@Injectable({ providedIn: 'root' })
export class SessionService {
    private isAuthenticatedSubject: BehaviorSubject<boolean>;
    public isAuthenticatedState: Observable<boolean>;
    private userSubject: BehaviorSubject<UserLoginModel>;
    private wishlistSubject: BehaviorSubject<WishlistModel[]>;
    private isMenuCollapsedSubject: BehaviorSubject<boolean>;
    public userState: Observable<UserLoginModel>;
    public menuCollapsedState: Observable<boolean>;

    constructor(private router: Router, private cartService: CartService) {
        let authenticated = this.getFromStorage('authenticated') === 'true' || false;
        let user = this.getFromStorage('user');
        let wishList = this.getFromStorage('wishlist');
        let isMenuCollapsed = this.getFromStorage('isMenuCollapsed');

        this.userSubject = new BehaviorSubject<UserLoginModel>(JSON.parse(user || '{}'));
        this.wishlistSubject = new BehaviorSubject<WishlistModel[]>(JSON.parse(wishList || '[]'));
        this.isMenuCollapsedSubject = new BehaviorSubject<boolean>(Boolean(isMenuCollapsed || false));
        this.userState = this.userSubject.asObservable();
        this.menuCollapsedState = this.isMenuCollapsedSubject.asObservable();
        this.isAuthenticatedSubject = new BehaviorSubject<boolean>(authenticated);
        this.isAuthenticatedState = this.isAuthenticatedSubject.asObservable();
    }

    public setIsAuthenticated(value: boolean) {
        this.setToStorage('authenticated', String(value));
        this.isAuthenticatedSubject.next(value);
    }

    public get isAuthenticated(): boolean {
        return this.isAuthenticatedSubject.value;
    }

    public get isMenuCollapsedValue(): boolean {
        return this.isMenuCollapsedSubject.value;
    }

    public setIsMenuCollapsed(value: boolean) {
        this.setToStorage('isMenuCollapsed', String(value));
        this.isMenuCollapsedSubject.next(value);
    }

    public getUser(): UserLoginModel {
        return this.userSubject.value;
    }

    public setUser(user: any): void {
        const validUser = user?.data ?? user;

        if (validUser?.id && validUser.id !== 0) {
            this.setToStorage("user", JSON.stringify(validUser));
            this.setToStorage("userId", validUser.id.toString());
            this.userSubject.next(validUser);

            const localCart = this.getFromStorage('cart');
            if (localCart) {
                this.migrateLocalCartToServer(validUser.id);
            }
        } else {
            console.error("Invalid user data received:", user);
        }
    }

    private migrateLocalCartToServer(userId: number): void {
        const cartItems: CartModel[] = JSON.parse(this.getFromStorage('cart') || '[]');

        if (cartItems.length > 0) {
            cartItems.forEach(item => item.userId = userId);

            const cartMainModel = new CartMainModel();
            cartMainModel.userId = userId;
            cartMainModel.cartItems = cartItems;

            this.cartService.insertBulk(cartMainModel).subscribe({
                next: () => {
                    console.log('Local cart migrated to server.');
                    this.setToStorage('cart', '');
                },
                error: (error) => {
                    console.error('Error migrating cart:', error);
                }
            });
        }
    }

    public getWishlist(): WishlistModel[] {
        return this.wishlistSubject.value;
    }

    public setWishlist(wishList: WishlistModel[]): void {
        this.setToStorage("wishlist", JSON.stringify(wishList));
        this.wishlistSubject.next(wishList);
    }

    public destroy(): void {
        if (typeof window !== 'undefined') {
            localStorage.clear();
        }
    }

    public getAccess(CurrentUrl: string): AccessModel {
        let Access = new AccessModel();
        Access.canView = true;
        Access.canInsert = true;
        Access.canUpdate = true;
        Access.canDelete = true;

        return Access;
    }

    public logout(): void {
        this.setToStorage('userId', '');
        this.setToStorage('user', '');
        this.setToStorage('authenticated', 'false');
        this.userSubject.next(null!);
        this.setIsAuthenticated(false);
        this.router.navigate(['/']);
		location.reload();
    }

    private getFromStorage(key: string): string | null {
        return typeof window !== 'undefined' ? localStorage.getItem(key) : null;
    }

    private setToStorage(key: string, value: string): void {
        if (typeof window !== 'undefined') {
            localStorage.setItem(key, value);
        }
    }
}
