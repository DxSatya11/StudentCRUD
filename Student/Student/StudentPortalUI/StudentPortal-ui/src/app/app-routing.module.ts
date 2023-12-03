import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentsComponent } from './students/students.component';
import { ViewstudentComponent } from './students/viewstudent/viewstudent.component';

const routes: Routes = [
  { path: '',
    component:StudentsComponent
  },

  { path: 'Students',
    component:StudentsComponent
  },
  { path: 'Students/:id',
    component:ViewstudentComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
