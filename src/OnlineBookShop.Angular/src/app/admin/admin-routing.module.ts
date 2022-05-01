import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminComponent } from './admin/admin.component';
import { BookListComponent } from './books/book-list/book-list.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EditBookComponent } from './books/editBook/editBook.component';



const adminRoutes: Routes = [
  {
    path: '',
    component: AdminComponent,
    children: [
      {
        path: '',
        children: [
          { path: 'dashboard', component: DashboardComponent },
          { path: 'books', component: BookListComponent },
          { path: 'editBook/:id', component: EditBookComponent }
        ]
      }
    ]
  }
];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(adminRoutes)
  ],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
