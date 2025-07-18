import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductDetailsGridModel, ProductDetailsModel, ProductDetailsParameterModel, ProductModel, ProductParameterModel, ProductVarientModel } from '../product.model';
import { ProductService } from '../product.service';
import { WishlistService } from '../../wishlist/wishlist.service';
import { WishlistModel, WishlistProductModel } from '../../wishlist/wishlist.model';
import { CartModel } from '../../cart/cart.model';
import { CartService } from '../../cart/cart.service';
import { SessionService } from '../../../services/session.service';
import { ReviewGridModel, ReviewMediaModel, ReviewModel, ReviewParameterModel } from '../review/review.model';
import { ReviewService } from '../review/review.service';
import { ToastService } from '../../../services/toast.service';
import { UserLoginModel } from '../../account/user/user.model';
import { AccessModel } from '../../../models/access.model';
import { ImageUrlPipe } from "../../../globalization/pipe/image-url-pipe";

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [FormsModule, CommonModule, ImageUrlPipe],
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {

  product: ProductDetailsModel | any;
  quantity: number = 1;
  id: number = 0;
  isLoading: boolean = true; // Loading state to show loading spinner
  public productParameterModel: ProductParameterModel = new ProductParameterModel();
  public productDetailsParameterModel: ProductDetailsParameterModel = new ProductDetailsParameterModel();
  public productDetailsGridModel: ProductDetailsGridModel = new ProductDetailsGridModel();
  public reviewGridModel: ReviewGridModel = new ReviewGridModel();
  public wishlist: WishlistModel = new WishlistModel();
  public reviewsMedia: ReviewMediaModel[] = [];
  public review: ReviewModel = new ReviewModel();
  public reviewParameterModel: ReviewParameterModel = new ReviewParameterModel();
  public selectedFile: File | null = null;
  public reviews: ReviewModel[] = [];
  public totalReviews: number = 0;
  public toastMessage: string = '';
  public averageRating: number = 0;
  public products: ProductDetailsModel[] = [];
  public currentUser: UserLoginModel = new UserLoginModel();
  public access: AccessModel = new AccessModel();
  public colorOptions: string[] = [];
  public ramOptions: string[] = [];
  public storageOptions: string[] = [];
  public variantPropertyValue: string | number = '';
  public productId: number = 0;

  private productService = inject(ProductService);
  private sessionService = inject(SessionService);
  private route = inject(ActivatedRoute);
  private wishlistService = inject(WishlistService);
  private cartService = inject(CartService);
  private reviewService = inject(ReviewService);
  private toaster = inject(ToastService);
  private router = inject(Router);
  private toastService = inject(ToastService);
  constructor() { }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    this.id = idParam ? +idParam : 0;
    this.loadProductDetails();
    this.getReviews();
  }


  variantGroups: any[] = [];
  selectedVariants: { [key: number]: string } = {};
  

  loadProductDetails(): void {
    this.productDetailsParameterModel.id = this.id;

    this.productService.getProductDetails(this.productDetailsParameterModel)
      .subscribe((data: ProductDetailsGridModel) => {
        this.product = data.productDetails;
        this.productDetailsGridModel.productDetailsSpecification = data.productDetailsSpecification || [];
        this.productDetailsGridModel.productVarient = data.productVarient || [];
        this.productDetailsGridModel.productAllVarient = data.productAllVarient || [];
        this.groupVariants(this.productDetailsGridModel.productAllVarient);
        this.setPreSelectedVariants(this.productDetailsGridModel.productAllVarient, this.id);
      });
  }

  groupVariants(variants: any[]): void {
    const grouped: { [key: number]: any } = {};
    for (const v of variants) {
      if (!grouped[v.variantPropertyId]) {
        grouped[v.variantPropertyId] = {
          variantPropertyId: v.variantPropertyId,
          propertyName: v.variantPropertyName,
          options: []
        };
      }
      grouped[v.variantPropertyId].options.push(v);
    }
    this.variantGroups = Object.values(grouped);
  }

  setPreSelectedVariants(allVariants: any[], currentProductId: number): void {
    const match = allVariants.find(v => v.id === currentProductId);
    if (!match) return;

    const variantIds = match.productVariantIds.split(',').map((x: string) => +x);
    const matchedVariants = this.productDetailsGridModel.productVarient.filter(v => variantIds.includes(v.id));

    for (const v of matchedVariants) {
      this.selectedVariants[v.variantPropertyId] = v.variantPropertyValue;
    }
  }
  onVariantSelected(variant: any): void {
    const selectedIds = Object.keys(this.selectedVariants).map((key) => {
      const group = this.variantGroups.find(g => g.variantPropertyId === +key);
      const selectedValue = this.selectedVariants[+key];
      const matched = group?.options.find((opt: any) => opt.variantPropertyValue === selectedValue);
      return matched?.id;
    }).filter(id => id !== undefined);

    const sortedSelectedIds = selectedIds.sort((a, b) => a - b).join(',');

    const matchingProduct = this.productDetailsGridModel.productAllVarient.find(p =>
      p.productVariantIds.split(',').map(Number).sort((a, b) => a - b).join(',') === sortedSelectedIds
    );

    if (matchingProduct) {
      // Replace product with matched variant
      const matchedDetails = this.product.find((p: any) => p.id === matchingProduct.id);
      if (matchedDetails) {
        this.product = [matchedDetails]; // Assuming single product display
        this.id = matchedDetails.id; // update ID if needed for any actions
      }
    }
  }


  // onVariantSelected(variant: any): void {
  //   const selectedIds = Object.keys(this.selectedVariants).map((key) => {
  //     const group = this.variantGroups.find(g => g.variantPropertyId === +key);
  //     const selectedValue = this.selectedVariants[+key];
  //     const matched = group?.options.find((opt: ProductVarientModel) => opt.variantPropertyValue === selectedValue);
  //     return matched?.id;
  //   }).filter(id => id !== undefined);

  //   const matchingProduct = this.productDetailsGridModel.productAllVarient.find(p =>
  //     p.productVariantIds.split(',').map(id => +id).sort().join(',') === selectedIds.sort().join(',')
  //   );

  //   if (matchingProduct) {
  //     this.router.navigate(['/product-details', matchingProduct.id]);
  //   }
  // }
  // onVariantSelected(variant: any): void {
  //   const selectedIds = Object.keys(this.selectedVariants).map((key) => {
  //     console.log('Selected Variant IDs:', selectedIds);
  //     const group = this.variantGroups.find(g => g.variantPropertyId === +key);
  //     const selectedValue = this.selectedVariants[+key];
  //     const matched = group?.options.find((opt: ProductVarientModel) => opt.variantPropertyValue === selectedValue);
  //     console.log('Matched Variant:', matched);
  //     return matched?.id;
  //   });


  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  getReviews(): void {
    this.reviewParameterModel.productId = this.id;

    this.reviewService.getForGrid(this.reviewParameterModel).subscribe({
      next: (response: any) => {
        if (response && response.reviews) {
          const mediaList: ReviewMediaModel[] = response.mediaList || [];

          this.reviews = response.reviews.map((review: ReviewModel) => ({
            id: review.id,
            userName: review.userName || 'Anonymous',
            rating: review.rating,
            comments: review.comments,
            date: review.date ? new Date(review.date) : null,
            mediaList: mediaList.filter((media: ReviewMediaModel) => media.reviewId === review.id)
          }));

          this.totalReviews = response.totalRecords;
          this.averageRating = this.calculateAverageRating(response.reviews);
        } else {
          this.reviews = [];
          this.totalReviews = 0;
          this.averageRating = 0;
        }
      },
      error: (err) => {
        console.error('Error fetching reviews:', err);
      }
    });
  }

  getUserInitials(userId: number): string {
    return `U${userId}`;
  }

  handleFileInput(event: Event) {
    const target = event.target as HTMLInputElement;
    if (target.files) {
      this.review.mediaList = [];
      Array.from(target.files).forEach((file) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
          const media = new ReviewMediaModel();
          media.mediaType = file.type;
          media.mediaURL = reader.result as string;
          this.review.mediaList.push(media);
          this.toaster.success('File uploaded successfully.');
        };
      });
    }
  }

  increaseQuantity(): void {
    this.quantity++;
  }

  decreaseQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  selectedImage: string | null = null;

  changeImage(imageUrl: string) {
    this.selectedImage = imageUrl;
  }

  addToWishlist(id: number): void {
    if (!this.product) return alert('Product details not available!');

    const userId = this.sessionService.getUser()?.id || 0;
    const wishlistItem: WishlistProductModel = {
      id: 0,
      userId,
      productId: this.id,
      mediaThumbUrl: this.product.thumbUrl,
      name: this.product.name,
      finalSellPrice: this.product.finalSellPrice,
      isExpiry: this.product.isExpiry,
    };

    if (userId === 0) {
      const wishlist = JSON.parse(localStorage.getItem('wishlist') || '[]');
      const exists = wishlist.some((item: WishlistProductModel) => item.productId === wishlistItem.productId);
      if (!exists) {
        wishlist.push(wishlistItem);
        localStorage.setItem('wishlist', JSON.stringify(wishlist));
        alert('Product added to wishlist locally.');
      } else {
        alert('Product is already in the wishlist.');
      }
    } else {
      this.wishlistService.addToWishlist(wishlistItem).subscribe({
        next: () => {
          this.toaster.success(` Product added to wishlist.`);
          this.router.navigate(['/wishList']);
        },
        error: () => this.toaster.warning(`Product is already in the wishlist.`),
      });
    }
  }


  private saveToLocalWishlist(wishlistItem: WishlistModel): void {
    const existingWishlist = localStorage.getItem('wishlist');
    let wishlist: WishlistModel[] = existingWishlist ? JSON.parse(existingWishlist) : [];

    // Check if the product is already in the wishlist
    const existingProduct = wishlist.find(item => item.productId === wishlistItem.productId);
    if (!existingProduct) {
      wishlist.push(wishlistItem);
      localStorage.setItem('wishlist', JSON.stringify(wishlist));
      this.toaster.success(`"${this.product!.name}" wishlist submitted successfully.`);
    } else {
      this.toaster.warning(`"${this.product!.name}" Product is already in the wishlist!.`);
    }
  }
  public delete(id: number): void {
    if (!this.access.canDelete) {
      this.toaster.warning('You do not have delete access of this page.');
      return;
    }

    if (window.confirm('Are you sure you want to delete?')) {
      this.reviewService.delete(id).subscribe(data => {
        this.toaster.success('Record deleted successfully.');
        this.getReviews();
      });
    }
  }

  addToCart(product: ProductModel): void {
    const userId = this.sessionService.getUser()?.id || 0;

    const cartItem: CartModel = {
      userId: userId,
      addedDate: new Date(),
      discountAmount: 0,
      id: 0,
      isActive: true,
      offerPrice: product.finalSellPrice,
      price: product.sellPrice,
      productId: product.id,
      productName: product.name,
      quantity: this.quantity || 1,
      sku: product.sku,
      totalPrice: product.finalSellPrice * (this.quantity || 1),
      mediaThumbUrl: product.thumbUrl,
    };

    if (userId === 0) {
      this.saveToLocalCart(cartItem);
    } else {
      this.cartService.insertCart(cartItem).subscribe({
        next: () => this.toaster.success(`Product added to cart.`),
        error: () => alert('Error adding product to cart.')
      });
    }
  }




  /* Save cart data to localStorage */
  private saveToLocalCart(cartItem: CartModel): void {
    let cart: CartModel[] = JSON.parse(localStorage.getItem('cart') || '[]');

    // Check if product already exists in cart
    const existingItemIndex = cart.findIndex(item => item.productId === cartItem.productId);

    if (existingItemIndex > -1) {
      // If product exists, update the quantity
      cart[existingItemIndex].quantity += cartItem.quantity;
    } else {
      // Add new item to cart
      cart.push(cartItem);
    }

    localStorage.setItem('cart', JSON.stringify(cart));
    this.toaster.success(`"${cartItem.productName}" added to cart successfully.`);
  }

  submitReview(id: number) {
    if (!this.product) return alert('Product details not available!');

    this.currentUser = this.sessionService.getUser();

    const userId = this.currentUser?.id;
    this.review.userId = userId;
    this.review.productId = this.id;
    this.reviewService.insert(this.review).subscribe(

      (data) => {
        if (data == 0) {
          this.toaster.warning('Record already exists.');
        } else if (data > 0) {
          this.toaster.success('Review submitted successfully.');
          this.getReviews();
        }
      }
    );
  }

  calculateAverageRating(reviews: any[]): any {
    if (!reviews.length) return 0;
    const total = reviews.reduce((sum, review) => sum + review.rating, 0);
    return (total / reviews.length).toFixed(1);
  }

  getRatingPercentage(rating: number): number {
    const count = this.reviews.filter(r => r.rating === rating).length;
    return this.totalReviews ? (count / this.totalReviews) * 100 : 0;
  }
  
}
