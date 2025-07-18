import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserLoginModel, UserModel } from '../../pages/account/user/user.model';
import { UserService } from '../../pages/account/user/user.service';
import { SessionService } from '../../services/session.service';
import { Router } from '@angular/router';
import { ToastService } from '../../services/toast.service';
import { ActivatedRoute } from '@angular/router';
import { SpinnerService } from '../../components/spinner/spinner.service';
import { ChangePasswordService } from './change-password.service';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss'
})
export class ChangePasswordComponent {
  @Output() closeModal = new EventEmitter<void>();
  public userModel: UserModel = new UserModel();
  public userLoginModel: UserLoginModel = new UserLoginModel();
  constructor(private userService: UserService,
    private sessionService: SessionService,
    private router: Router,
    private route: ActivatedRoute,
    private toaster: ToastService,
    private spinner: SpinnerService,
    private changePasswordService: ChangePasswordService,
  ) {
  }

  onSubmit() {
  }

  close() {
    this.closeModal.emit(); // Emit an event to close the modal
  }
  public Save(isValidForm: boolean): void {
    if (isValidForm && this.userModel.password == this.userModel.confirmPassword) {
      //this.userModel = this.sessionService.getUser(this.userModel);
      this.userLoginModel = this.sessionService.getUser();
      this.userModel.id = this.userLoginModel.id;
      //this.spinner.show();
      this.changePasswordService.ChangePassword(this.userModel).subscribe(data => {
        if (data) {
          this.toaster.success('Your password has been changed successfully.');
          //this.activeModal.close('');
          this.router.navigate(['auth/login'])
        } else {
          this.toaster.warning('The old password you have entered incorrect.');
        }
        //this.spinner.hide();
      });
    } else {
      this.toaster.warning('Please provide required input.');
    }
  }

  showPassword: { [key in 'current' | 'new' | 'confirm']: string } = {
    current: 'password',
    new: 'password',
    confirm: 'password'
  };

  togglePassword(field: 'current' | 'new' | 'confirm'): void {
    this.showPassword[field] = this.showPassword[field] === 'password' ? 'text' : 'password';
  }
}
