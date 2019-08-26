import { NgModule } from '@angular/core';
import { CommonSharedModule } from './../../common/common-shared.module';
import { PrimeNgSharedModule } from './../../common/primeng-shared.module';
import { LoadingImageModule } from './../../_components/loding-image/loding-image.module';
import { TaskDetailComponent } from './task-detail.component';
import { TaskService } from '../../_services/task.service';


@NgModule({
  imports: [LoadingImageModule, CommonSharedModule, PrimeNgSharedModule],
  declarations: [TaskDetailComponent],
  providers: [TaskService]
})
export class TaskDetailModule { }