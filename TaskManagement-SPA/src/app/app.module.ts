import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';

import { LoadingImageModule } from './_components/loding-image/loding-image.module';
import { PrimeNgSharedModule } from './common/primeng-shared.module';
import { appRoutes } from './app.routes';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './_components/register/register.component';
import { RegistrationComponent } from './registration/registration.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AdminRegistrationComponent } from './admin-registration/admin-registration.component';
import { TaskComponent } from './Task/task.component';
import { TaskSaveComponent } from './Task/task-save/task-save.component';
import { TaskDetailComponent } from './Task/task-detail/task-detail.component';

import { AuthGuard } from './_guards/auth.guard';
import { AuthService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_interceptors/error.interceptor';

export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      LoginComponent,
      RegisterComponent,
      RegistrationComponent,
      DashboardComponent,
      AdminRegistrationComponent,
      TaskComponent,
      TaskSaveComponent,
      TaskDetailComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      LoadingImageModule,
      PrimeNgSharedModule,
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: {
            // tslint:disable-next-line: object-literal-shorthand
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],
   providers: [
      AuthService,
      AuthGuard,
      ErrorInterceptorProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
