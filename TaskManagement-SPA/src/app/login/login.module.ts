import { NgModule } from '@angular/core';
import { CommonSharedModule } from '../common/common-shared.module';
import { PrimeNgSharedModule } from '../common/primeng-shared.module';
import { LoginComponent } from './login.component';
import { AuthService } from '../_services/auth.service';

@NgModule({
  imports: [CommonSharedModule, PrimeNgSharedModule],
  exports: [LoginComponent],
  declarations: [LoginComponent],
  providers: [AuthService]
})
export class LoginModule { }
