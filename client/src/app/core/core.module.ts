import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { TestErrorComponent } from './test-error/test-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrrorComponent } from './server-errror/server-errror.component';
import { ToastrModule } from 'ngx-toastr';


@NgModule({
  declarations: [NavBarComponent, TestErrorComponent, NotFoundComponent, ServerErrrorComponent],
  imports: [
    CommonModule,
    RouterModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    })
  ],
  exports: [
    NavBarComponent
  ]

})
export class CoreModule { }
