import { Component, OnInit } from '@angular/core';
import { UserLoginModel } from '../user.model';
import { SessionService } from '../../../../services/session.service';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { ToastService } from '../../../../services/toast.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {

  public currentUser: UserLoginModel = new UserLoginModel();
  public originalUser: UserLoginModel = new UserLoginModel();
  public isEditMode: boolean = false;  

  constructor(
    private sessionService: SessionService, 
    private userService: UserService, 
    private router: Router, 
    private toaster: ToastService
  ) { }

  ngOnInit(): void {
    this.currentUser = this.sessionService.getUser();
    this.originalUser = { ...this.currentUser };  
  }

  public toggleEditMode(): void {
    if (this.isEditMode) {
      this.currentUser = { ...this.originalUser };
    }
    this.isEditMode = !this.isEditMode;  
  }

  public saveProfile(): void {
    if (this.currentUser) {
      this.userService.userUpdate(this.currentUser).subscribe(
        (response) => {
          this.toaster.success('Profile updated successfully!');
          this.isEditMode = false; // Exit edit mode after saving
          this.sessionService.setUser(this.currentUser); // Update session user
        },
        (error) => {
          this.toaster.warning('Failed to update profile.');
        }
      );
    }
  }
}
