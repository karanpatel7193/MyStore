import { Component, ViewChild, ElementRef } from '@angular/core';
import { CartService } from './cart.service';
import { CartGridModel, CartParameterModel, CartModel } from './cart.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ToastService } from '../../services/toast.service';
import { SessionService } from '../../services/session.service';
import { FormsModule } from '@angular/forms';
import { Toast } from 'bootstrap';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {
  cartGrid: CartGridModel = new CartGridModel();
  cartParameterModel: CartParameterModel = new CartParameterModel();
  cartItems: CartModel[] = [];
  toastMessage: string = '';
  @ViewChild('cartToast', { static: false }) toastRef!: ElementRef;

  private toastInstance: Toast | null = null;

  constructor(private cartService: CartService, private toasterService: ToastService, private sessionService: SessionService) { }

  ngOnInit() {
    this.loadCartData();
  }

  ngAfterViewInit(): void {
    if (this.toastRef) {
      this.toastInstance = new Toast(this.toastRef.nativeElement);
    }
  }
  

  calculateItemTotalPrice(item: CartModel): number {
    return (item.offerPrice || item.price) * item.quantity;
  }

  loadCartData(): void {
    const user = this.sessionService.getUser();
    const userId = user?.id || 0;
  
    const processCartItems = (items: CartModel[]): CartModel[] =>
      items.map((item) => ({
        ...item,
        totalPrice: this.calculateItemTotalPrice(item)
      }));
  
    if (userId > 0) {
      this.cartParameterModel.userId = userId;
      this.cartService.getCartData(this.cartParameterModel).subscribe({
        next: (response) => {
          this.cartGrid = response;
          this.cartItems = processCartItems(response.products || []);
          this.calculateTotals();
        },
        error: (error) => {
          console.error('Error fetching cart data from API:', error);
          this.showToast('Failed to load cart data.', 'error');
        }
      });
    } else {
      const localCart = localStorage.getItem('cart');
      const cartItems = localCart ? JSON.parse(localCart) as CartModel[] : [];
      this.cartItems = processCartItems(cartItems);
      this.calculateTotals();
    }
  }
  

  getProductImageUrl(product: CartModel): string {
    const baseUrl = 'https://localhost:7143';
    if (product?.mediaThumbUrl) {
      const imageUrl = product.mediaThumbUrl.startsWith('\\') || product.mediaThumbUrl.startsWith('./')
        ? `${baseUrl}${product.mediaThumbUrl.replace(/\\/g, '/')}`
        : product.mediaThumbUrl;
      return imageUrl;
    }
    return 'assets/images/no-image.png';
  }


  removeFromCart(productId: number): void {
    const user = this.sessionService.getUser();
    const userId = user?.id || 0;
  
    const handleRemovalSuccess = () => {
      this.loadCartData();
      this.showToast('Product removed from cart!');
    };
  
    const handleRemovalError = () => {
      this.showToast('Failed to remove product!');
    };
  
    if (userId > 0) {
      this.cartParameterModel.productId = productId;
      this.cartService.deleteCart(this.cartParameterModel).subscribe({
        next: handleRemovalSuccess,
        error: handleRemovalError
      });
    } else {
      let cart = JSON.parse(localStorage.getItem('cart') || '[]');
      cart = cart.filter((item: any) => item.productId !== productId);
      localStorage.setItem('cart', JSON.stringify(cart));
      handleRemovalSuccess();
    }
  }
  
  updateQuantity(item: CartModel, newQuantity: number): void {
    if (newQuantity < 1) return;
  
    const user = this.sessionService.getUser();
    const userId = user?.id || 0;
  
    const handleUpdateSuccess = () => {
      item.totalPrice = (item.offerPrice > 0 ? item.offerPrice : item.price) * newQuantity;
      this.calculateTotals();
      this.toasterService.success('Cart updated successfully!');
    };
  
    if (userId > 0) {
      const updatedCart: CartModel = { ...item, userId, quantity: newQuantity };
      this.cartService.updateCart(updatedCart).subscribe({
        next: () => {
          handleUpdateSuccess();
        }
        // 👇 No error handler – silent fail
      });
    } else {
      let cart: CartModel[] = JSON.parse(localStorage.getItem('cart') || '[]');
      const cartItem = cart.find(c => c.productId === item.productId);
      if (cartItem) {
        cartItem.quantity = newQuantity;
        cartItem.totalPrice = (cartItem.offerPrice > 0 ? cartItem.offerPrice : cartItem.price) * newQuantity;
        localStorage.setItem('cart', JSON.stringify(cart));
        handleUpdateSuccess();
      }
    }
  }
  
  
  
  
  
  calculateTotals(): void {
    let subtotal = 0;
    this.cartItems.forEach(item => {
      item.totalPrice = (item.offerPrice > 0 ? item.offerPrice : item.price) * item.quantity;
      subtotal += item.totalPrice;
    });

    this.cartGrid.totalRecords = this.cartItems.length;
    this.cartGrid.subTotal = subtotal;
    this.cartGrid.total = subtotal; // Modify if you add delivery charges
  }

  showToast(message: string, type: 'success' | 'error' = 'success') {
    this.toastMessage = message;
    const toastEl = this.toastRef.nativeElement;
    
    if (type === 'success') {
      toastEl.classList.remove('bg-danger');
      toastEl.classList.add('bg-success');
    } else {
      toastEl.classList.remove('bg-success');
      toastEl.classList.add('bg-danger');
    }
  
    const toastInstance = new Toast(toastEl);
    toastInstance.show();
  }
  
  placeOrder(): void {
    if (this.cartItems.length === 0) {
      this.showToast('Your cart is empty!', 'error');
      return;
    }
  
    // Simulating order placement logic
    this.showToast('Order placed successfully!', 'success');
  }
  
}