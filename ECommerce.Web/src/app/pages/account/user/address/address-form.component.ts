import { Component, inject, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AddressService } from './address.service';
import { AddressModel } from './address.model';
import { StateService } from '../../../../globalization/state/state.service';
import { CityService } from '../../../../globalization/city/city.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { SessionService } from '../../../../services/session.service';

@Component({
  selector: 'app-address-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './address-form.component.html',
})
export class AddressFormComponent implements OnInit {
  public address: AddressModel = new AddressModel();
  public states: any[] = [];
  public cities: any[] = [];

  private cityService = inject(CityService);
  private stateService = inject(StateService);
  private addressService = inject(AddressService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  constructor(private sessionService: SessionService) {}

  ngOnInit(): void {
    this.initializeAddressForm();
  }

  private initializeAddressForm(): void {
    const addressId = this.route.snapshot.queryParams['id'];
    if (addressId) {
      this.addressService.getAddressById(addressId).subscribe(address => {
        this.address = address;
        this.loadStates(true); // Edit mode
      });
    } else {
      this.loadStates(); // Add mode
    }
  }

  public loadStates(isEditMode: boolean = false): void {
    this.stateService.getForLOV({ countryId: 1 }).subscribe(res => {
      this.states = res; // Handle both wrapped and unwrapped responses
      if (isEditMode) {
        setTimeout(() => this.onStateChange(true), 0);
      }
    });
  }

  public onStateChange(isEditMode: boolean = false): void {
    if (!this.address.stateId) {
      this.cities = [];
      this.address.cityId = 0;
      return;
    }

    this.cityService.getForLOV({ stateId: this.address.stateId }).subscribe(res => {
      this.cities = res;

      if (isEditMode) {
        const city = this.cities.find(c => c.id === this.address.cityId);
        this.address.cityName = city ? city.name : '';
      } else {
        // Optionally, preselect the first city
        if (this.cities.length > 0) {
          this.address.cityId = this.cities[0].id;
          this.address.cityName = this.cities[0].name;
        }
      }
    });
  }

  public saveAddress(): void {
    const user = this.sessionService.getUser();
    const userId = user?.id || 0;
    if (userId === 0) {
      alert('Please log in to save your address');
      return;
    }

    this.address.userId = userId;
    const addressId = this.route.snapshot.queryParams['id'];

    if (addressId) {
      this.address.id = addressId;
      this.addressService.updateAddress(this.address).subscribe(response => {
        if (response) {
          alert('Address updated successfully');
          this.router.navigate(['/account/address']);
        } else {
          alert('Error updating address');
        }
      });
    } else {
      this.addressService.insertAddress(this.address).subscribe(response => {
        if (response > 0) {
          alert('Address saved successfully');
          this.router.navigate(['/account/address']);
        } else {
          alert('Error saving address');
        }
      });
    }
  }

  public cancel(): void {
    this.router.navigate(['/account/address']);
  }
}
