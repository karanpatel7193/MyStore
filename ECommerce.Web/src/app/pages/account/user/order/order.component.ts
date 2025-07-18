import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss'
})
export class OrderComponent implements OnInit {
  orders: any[] = [];
  orderItems: any[] = [];
  isLoading: boolean = true;
  ordersGrid:any
  constructor() {}

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    // this.orderService.getOrders().subscribe({
    //   next: (response: any) => {
    //     if (response && response.data) {
    //       this.orders = response.data;
    //     }
    //     this.isLoading = false;
    //   },
    //   error: (error) => {
    //     console.error('Error fetching orders:', error);
    //     this.isLoading = false;
    //   }
    // });
  }
  public getOrderImageUrl(ordr:any){

  }
}
