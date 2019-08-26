import { RouterModule } from '@angular/router'; 
import { TaskComponent } from './task.component';
import { TaskSaveComponent } from './task-save/task-save.component';
import { TaskDetailComponent } from './task-detail/task-detail.component';

export const taskRoutes = RouterModule.forChild([
    {
        path: '',
        children: [
            {
                path: '',
                component: TaskComponent
            },
            {
                path: 'task-add-edit',
                component: TaskSaveComponent
            },
            {
                path: 'task-detail/:id',
                component: TaskDetailComponent
            }
        ]
    }
]);
