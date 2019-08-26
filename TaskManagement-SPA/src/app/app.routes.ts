import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './_guards/auth.guard';
import { RegistrationComponent } from './registration/registration.component';
import { AdminRegistrationComponent } from './admin-registration/admin-registration.component';
import { TaskComponent } from './Task/task.component';
import { TaskSaveComponent } from './Task/task-save/task-save.component';
import { TaskDetailComponent } from './Task/task-detail/task-detail.component';


export const appRoutes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'task',
        component: TaskComponent
      },
      {
        path: 'task-add-edit',
        component: TaskSaveComponent
      },
      {
        path: 'task-add-edit/:id',
        component: TaskSaveComponent
      },
      {
        path: 'task-detail/:id',
        component: TaskDetailComponent
      },
      {
        path: 'admin-register',
        component: AdminRegistrationComponent
      }
    ]
  },
  {
    path: 'register',
    component: RegistrationComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '**',
    redirectTo: '',
    pathMatch: 'full'
  }
];
