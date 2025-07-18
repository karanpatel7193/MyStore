import { NgModule } from '@angular/core';
import { MenuListComponent } from './menu-list.component';
import { MenuFormComponent } from './menu-form.component';
import { MenuRoute } from './menu.route';
import { MenuComponent } from './menu.component';
import { MenuService } from './menu.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { RoleGuardService } from 'src/app/services/role-guard.service';

@NgModule({
    imports: [
        MenuRoute,
        CommonModule,
        FormsModule,
        NgbPaginationModule
    ],
    declarations: [
        MenuComponent,
        MenuListComponent,
        MenuFormComponent,
    ],
    providers: [
        MenuService,
        RoleGuardService
    ],
})
export class MenuModule { }
