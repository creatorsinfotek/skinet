import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderDetailedComponent } from './order-detailed/order-detailed.component';
import { OrderRoutingModule } from './orders-routing.module';



@NgModule({
  declarations: [OrderDetailedComponent],
  imports: [
    CommonModule,
    OrderRoutingModule
  ]
})
export class OrdersModule { }
