import { Component, inject } from '@angular/core';
import { WishlistService } from './wishlist.service';
import { ProductModel } from '../product/product.model';
import { WishlistGridModel, WishlistParameterModel, WishlistProductModel } from './wishlist.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ToastService } from '../../services/toast.service';
import { SessionService } from '../../services/session.service';
import { LoginComponent } from '../../auth/login/login.component';
import { UserLoginModel } from '../account/user/user.model';

@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './wishlist.component.html',
  styleUrl: './wishlist.component.scss'
})
export class WishlistComponent {
  wishlist: WishlistGridModel = new WishlistGridModel();
  wishlistParameterModel: WishlistParameterModel = new WishlistParameterModel();
  public currentUser: UserLoginModel = new UserLoginModel();

  private toaster = inject(ToastService);

  constructor(private wishlistService: WishlistService, private toasterService: ToastService, private sessionService: SessionService) { }

  ngOnInit() {
    this.loadWishlist();
  }


  loadWishlist(): void {
    this.currentUser = this.sessionService.getUser();
    const userId = this.currentUser?.id;

    if (userId > 0) {
      this.wishlistParameterModel.userId = userId;
      this.wishlistService.getForGrid(this.wishlistParameterModel).subscribe(data => {
        this.wishlist = data;
        this.wishlist.totalRecords = this.wishlist.totalRecords;
        this.wishlistService.updateWishlistCount(this.wishlist.totalRecords);
      });
    } else {
      const storedWishlist = localStorage.getItem('wishlist');
      this.wishlist = storedWishlist ? JSON.parse(storedWishlist) : [];
      this.wishlistService.updateWishlistCount(this.wishlist.totalRecords);
    }
  }

  getProductImageUrl(product: any) {
    if (product?.mediaThumbUrl) {
      if (product.mediaThumbUrl.startsWith('\\') || product.mediaThumbUrl.startsWith('./')) {
        return `https://localhost:7143${product.mediaThumbUrl.replace(/\\/g, '/')}`;
      }
      return product.mediaThumbUrl;
    }
  }

  removeFromWishlist(productId: number): void {
    const isConfirmed = window.confirm('Are you sure you want to remove this item from your wishlist?');

    if (isConfirmed) {
      this.wishlistParameterModel.productId = productId;
      this.wishlistService.removeFromWishlist(this.wishlistParameterModel).subscribe({
        next: () => {
          this.loadWishlist();
          this.toaster.success(`Product removed from wishlist!`);
        },
        error: (error) => {
          this.toasterService.warning('Product removed from wishlist!');
        }
      });
    }
  }
}
