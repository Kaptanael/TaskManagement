import { NgModule } from '@angular/core';
import { AdminRegistrationComponent } from './admin-registration.component';
import { AuthService } from './../_services/auth.service';
import { RegisterModule } from '../_components/register/register.module';

@NgModule({
  imports: [
    RegisterModule
  ],
  declarations: [AdminRegistrationComponent],
  providers: [AuthService]
})
export class AdminRegistrationModule { }
