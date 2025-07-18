import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserLoginModel, UserModel } from '../../pages/account/user/user.model';
import { Router } from '@angular/router';
import { UserService } from '../../pages/account/user/user.service';
import { SessionService } from '../../services/session.service';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  public currentUser: UserLoginModel = new UserLoginModel();
  public user: UserModel = new UserModel();

  constructor(private userService: UserService, private sessionService: SessionService,
    private router: Router,
    private route: ActivatedRoute,
    private toaster: ToastService) {
  }
  ngOnInit(): void {
    this.currentUser = this.sessionService.getUser();
  }
  public saveProfile(): void {
    if (this.currentUser) {
      this.userService.userUpdate(this.currentUser).subscribe(
        (response) => {
          this.toaster.success('Profile updated successfully!');
          this.router.navigate(['auth/login'])
        },
        (error) => {
          this.toaster.warning('Failed to update profile.');
        }
      );
    }
  }

  public cancel(): void {
    this.router.navigate(['/'])
  }
}
