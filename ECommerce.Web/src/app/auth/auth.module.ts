import { NgModule } from '@angular/core';
import { AuthRoute } from './auth.route';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { UserService } from '../pages/account/user/user.service';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { RegistrationSuccessComponent } from './registration/registration-success.component';
import { NgbActiveModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegistrationComponent } from './registration/registration.component';
import { RegistrationActiveComponent } from './registration/registration-active.component';
import { RoleService } from '../pages/account/role/role.service';
import { LoginComponent } from './login/login.component';

@NgModule({
    imports: [
        AuthRoute,
        FormsModule,
        CommonModule,
    ],
    
    declarations: [
    ],
    // providers: [
    //     RoleService,UserService,NgbActiveModal
    // ],
})
export class AuthModule { }
