import { Component, OnInit } from '@angular/core';
import { Order } from '../shared/models/order';
import { OrdersService } from '../orders/orders.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent {
  order?: Order ;

  constructor(private orderService: OrdersService,  private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.getOrder();
  }
  
  getOrder() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return
    this.orderService.getOrderDetailed(+id).subscribe({
      next: order => this.order = order
    })
  }

}
