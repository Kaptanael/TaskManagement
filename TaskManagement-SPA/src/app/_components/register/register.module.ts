import { NgModule } from '@angular/core';
import { CommonSharedModule } from './../../common/common-shared.module';
import { PrimeNgSharedModule } from './../../common/primeng-shared.module';
import { RegisterComponent } from './register.component';
import { AuthService } from '../../_services/auth.service';

@NgModule({
  imports: [CommonSharedModule, PrimeNgSharedModule],
  exports: [RegisterComponent],
  declarations: [RegisterComponent],
  providers: [AuthService]
})
export class RegisterModule { }
