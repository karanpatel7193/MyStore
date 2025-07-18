import { Component } from '@angular/core';
import { AddressComponent } from './user/address/address.component';
import { OrderComponent } from './user/order/order.component';
import { ProfileComponent } from './user/profile/profile.component';
import { ProfileSidebarComponent } from './profile-sidebar/profile-sidebar.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [AddressComponent,OrderComponent,ProfileComponent,ProfileSidebarComponent,FormsModule,CommonModule,RouterModule],
  templateUrl: './account.component.html',
  styleUrl: './account.component.scss'
})
export class AccountComponent {

}
