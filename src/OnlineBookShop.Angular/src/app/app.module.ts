import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { MatInputModule } from '@angular/material';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NotfoundpageComponent } from './shared/notfoundpage/notfoundpage.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guards/auth.guard';
import { AuthInterceptor } from './_interceptors/auth.interceptor';

@NgModule({
   declarations: [
      AppComponent,
      NotfoundpageComponent,
      LoginComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      HttpClientModule,
      AppRoutingModule,
      ReactiveFormsModule,
      MatInputModule
   ],
   providers: [
      AuthGuard, 
      { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
