import { NgModule } from '@angular/core';
import { CommonSharedModule } from '../common/common-shared.module';
import { LoadingImageModule } from '../_components/loding-image/loding-image.module';
import { PrimeNgSharedModule } from '../common/primeng-shared.module';
import { TaskComponent } from './task.component';
import { taskRoutes } from './task.route';
import { TaskService } from '../_services/task.service';
import { UserService } from '../_services/user.service';
import { AuthService } from './../_services/auth.service';

@NgModule({
  imports: [taskRoutes,LoadingImageModule, CommonSharedModule, PrimeNgSharedModule],
  declarations: [TaskComponent],
  providers: [TaskService, UserService, AuthService]
})
export class TaskModule { }
