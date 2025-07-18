import { Component, OnInit } from '@angular/core';
import { AddressService } from './address.service';
import { AddressModel, AddressParameterModel } from './address.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SessionService } from '../../../../services/session.service';

@Component({
  selector: 'app-address',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './address.component.html',
  styleUrls: ['./address.component.scss'],
})
export class AddressComponent implements OnInit {
  public addresses: AddressModel[] = [];
  public addressParameterModel: AddressParameterModel = new AddressParameterModel();

  constructor(
    private addressService: AddressService, 
    private router: Router,
    private sessionService:SessionService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadAddressData();
  }
  loadAddressData(): void {
    const user = this.sessionService.getUser();
    const userId = user?.id || 0;

    if (userId === 0) {
        return; // If the user is not logged in, do nothing
    }

    const params: AddressParameterModel = { id: 0, userId: userId }; // Fetch for the logged-in user

    this.addressService.getAddressData(params).subscribe({
        next: (response: any) => {
            if (response?.addresses) {
                this.addresses = response.addresses;
            } else if (response?.data?.addresses) {
                this.addresses = response.data.addresses;
            } else {
                this.addresses = []; // If no addresses found
            }
        },
        error: (err) => {
            console.error('Error fetching addresses:', err);
            this.addresses = []; // Handle error by setting addresses to an empty array
        }
    });
}


public deleteAddress(id: number): void {
  const userId = this.sessionService.getUser()?.id || 0;

  const handleDeletionSuccess = () => {
    this.addresses = this.addresses.filter(addr => addr.id !== id);
    alert('Address deleted successfully');
    this.router.navigate(['/account/address']);
  };

  const handleDeletionError = () => {
    alert('Failed to delete address');
  };

  if (userId > 0) {
    const deleteParams: AddressParameterModel = { id, userId };
    this.addressService.deleteAddress(deleteParams).subscribe({
      next: handleDeletionSuccess,
      error: handleDeletionError
    });
  } else {
    alert('User is not logged in');
    handleDeletionError();
  }
}
  public navigateToAddressForm(addressId?: number): void {
    // Navigate to address form with address ID if available
    if (addressId) {
      this.router.navigate(['/account/address-form'], { queryParams: { id: addressId } });
    } else {
      this.router.navigate(['/account/address-form']);
    }
  }
}
