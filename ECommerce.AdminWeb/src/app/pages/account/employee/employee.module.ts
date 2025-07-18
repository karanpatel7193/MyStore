
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { EmployeeRoute } from './employee.route';
import { EmployeeFormComponent } from './employee-form.component';
import { EmployeeListComponent } from './employee-list.component';
import { EmployeeComponent } from './employee.component';
import { EmployeeService } from './employee.service';
// import { ColumnResizerModule } from '../../common-component/column-resizer/column-resizer.module';


@NgModule({
    imports: [
        FormsModule,
        CommonModule,
		// ColumnResizerModule,
        EmployeeRoute
    ],
    declarations: [
        EmployeeComponent,
        EmployeeListComponent,
        EmployeeFormComponent
    ],
    providers: [
        EmployeeService
    ]
})
export class EmployeeModule { }
