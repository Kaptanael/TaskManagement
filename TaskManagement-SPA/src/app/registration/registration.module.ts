import { NgModule } from '@angular/core';
import { RegistrationComponent } from './registration.component';
import { RegisterModule } from './../_components/register/register.module';
import { AuthService } from './../_services/auth.service';


@NgModule({
  imports: [
    RegisterModule
  ],
  declarations: [RegistrationComponent],
  providers: [AuthService]
})
export class RegistrationModule { }
