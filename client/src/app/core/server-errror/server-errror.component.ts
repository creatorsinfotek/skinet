import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-errror',
  templateUrl: './server-errror.component.html',
  styleUrls: ['./server-errror.component.scss']
})
export class ServerErrrorComponent implements OnInit {
  error: any;
  constructor(private router: Router) { 
    const navigation = this.router.getCurrentNavigation();
    this.error = navigation && navigation.extras && navigation.extras.state &&
        navigation.extras.state.error;
  }

  ngOnInit(): void {
  }

}
