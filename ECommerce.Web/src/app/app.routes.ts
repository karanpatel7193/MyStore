import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { AuthGuard } from './interceptors/auth.guard';
import { AccountComponent } from './pages/account/account.component';
import { ProfileComponent } from './pages/account/user/profile/profile.component';
import { AddressComponent } from './pages/account/user/address/address.component';
import { OrderComponent } from './pages/account/user/order/order.component';
import { AddressFormComponent } from './pages/account/user/address/address-form.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, },

  // {
  //   path: 'bootstrap',
  //   loadChildren: () => import(`./features/tutorials/example-bootstrap/tutorial.routes`)
  //   .then(routes => routes.routes)
  // },
  {
    path: 'block',
    loadComponent: () => import('./pages/home/block/block.component').then(mod => mod.BlockComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'product',
    loadComponent: () => import('./pages/product/product.component').then(mod => mod.ProductComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'product-details/:id',  // <-- Add this route
    loadComponent: () => import('./pages/product/product-details/product-details.component').then(mod => mod.ProductDetailsComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'header',
    loadComponent: () => import('./components/header/header.component').then(mod => mod.HeaderComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'footer',
    loadComponent: () => import('./components/footer/footer.component').then(mod => mod.FooterComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'slider',
    loadComponent: () => import('./pages/slider/slider.component').then(mod => mod.SliderComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'cart',
    loadComponent: () => import('./pages/cart/cart.component').then(mod => mod.CartComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'wishList',
    loadComponent: () => import('./pages/wishlist/wishlist.component').then(mod => mod.WishlistComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'home',
    loadComponent: () => import('./pages/home/home.component').then(mod => mod.HomeComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'banner',
    loadComponent: () => import('./pages/banner/banner.component').then(mod => mod.BannerComponent),
    canActivate: [AuthGuard]
  },
  {
    path: 'change-password',
    loadComponent: () => import('./auth/change-password/change-password.component').then(mod => mod.ChangePasswordComponent)
  },
  {
    path: 'forget-password',
    loadComponent: () => import('./auth/forget-password/forget-password.component').then(mod => mod.ForgetPasswordComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./auth/login/login.component').then(mod => mod.LoginComponent)
  },
  {
    path: 'profile',
    loadComponent: () => import('./auth/profile/profile.component').then(mod => mod.ProfileComponent),
    canActivate: [AuthGuard]
  },
  {
    path:'registration',
    loadComponent: () => import('./auth/registration/registration.component').then(m => m.RegistrationComponent)
  },
  {
    path:'forget-password',
    loadComponent: () => import('./auth/forget-password/forget-password.component').then(m => m.ForgetPasswordComponent)
  },

 {
  path:'registration',
  loadComponent: () => import('./auth/registration/registration.component').then(m => m.RegistrationComponent)
 },
 {
  path: 'account',
  loadComponent: () => import('./pages/account/account.component').then(mod => mod.AccountComponent)
},
{
  path: 'account',
  component: AccountComponent,
  children: [
    { path: 'profile', component: ProfileComponent },
    { path: 'address', component: AddressComponent },
    { path: 'address-form', component: AddressFormComponent },
    { path: 'orders', component: OrderComponent },
    { path: '', redirectTo: 'profile-info', pathMatch: 'full' }, // Default route inside account
  ],
},

];
