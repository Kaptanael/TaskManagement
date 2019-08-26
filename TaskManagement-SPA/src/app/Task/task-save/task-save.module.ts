import { NgModule } from '@angular/core';
import { CommonSharedModule } from './../../common/common-shared.module';
import { PrimeNgSharedModule } from './../../common/primeng-shared.module';
import { TaskSaveComponent } from './task-save.component';
import { TaskService } from '../../_services/task.service';
import { UserService } from 'src/app/_services/user.service';

@NgModule({
  imports: [CommonSharedModule, PrimeNgSharedModule],
  declarations: [TaskSaveComponent],
  providers: [UserService, TaskService]
})
export class TaskSaveModule { }
