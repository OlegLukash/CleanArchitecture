import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotfoundpageComponent } from './shared/notfoundpage/notfoundpage.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guards/auth.guard';

const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(mod => mod.AdminModule), canActivate: [AuthGuard]},
  { path: '',   redirectTo: '/admin', pathMatch: 'full', canActivate: [AuthGuard] },
  { path: '**', component: NotfoundpageComponent }
];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
