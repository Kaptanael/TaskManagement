import { NgModule } from '@angular/core';
import { CommonSharedModule } from '../common/common-shared.module';
import { PrimeNgSharedModule } from '../common/primeng-shared.module';
import { NavComponent } from './nav.component';
import { AuthService } from '../_services/auth.service';

@NgModule({
  imports: [CommonSharedModule, PrimeNgSharedModule],
  exports: [NavComponent],
  declarations: [NavComponent],
  providers: [AuthService]
})
export class NavModule { }
