// import { Component, OnInit } from '@angular/core';

// @Component({
//   selector: 'app-employee',
//   templateUrl: './employee.component.html',
//   styleUrls: ['./employee.component.scss']
// })
// export class EmployeeComponent implements OnInit {

//   constructor() { }

//   ngOnInit(): void {
//   }

// }

import { Component } from '@angular/core';

@Component({
  selector: 'employee',
  template: `
    <router-outlet></router-outlet>
  `,
})
export class EmployeeComponent {
}
