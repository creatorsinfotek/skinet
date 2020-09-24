import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  getOrderForUser() {
    return this.httpClient.get(this.baseUrl + 'orders');
  }

  getOrderDetailed(id: number) {
    return this.httpClient.get(this.baseUrl + 'orders/' + id);
  }
}
