import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { timestamp } from 'rxjs/operators';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  title = 'SkiNet';

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    const basketId = localStorage.getItem('basket_id');
    if(basketId) {
      this.basketService.getBasket(basketId).subscribe( () =>{
        console.log('basket initialised');
      }, error => {
        console.log(error);
      });
    }
  }
}
