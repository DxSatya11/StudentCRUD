import { Component,OnInit, ViewChild } from '@angular/core';
import { StudentService } from './student.service';
import { student } from '../Infrastructure/student.interface';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
//import {MatSort } from '@amgular/material/sort';
//import { ViewChild } from '@angular/core';



@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit{
  student:student[] = [];
  displayedColumns: string[] = ['Name','Email','DOB','Age','Gender','Edit'];
  dataSource:MatTableDataSource<student> = new MatTableDataSource<student>();
  @ViewChild(MatPaginator) matpaginator! : MatPaginator;
  @ViewChild(MatSort) matsort! : MatSort;
  FilterText:string= "";
  constructor(private studentService: StudentService){}

  ngOnInit(): void {
    this.studentService.getAllStudents().subscribe(
      (studentData) =>{
        debugger;
      this.student = studentData;
      this.dataSource = new MatTableDataSource<student>(this.student);

      if(this.matpaginator){
        this.dataSource.paginator = this.matpaginator;
      }

      if(this.matsort){
        this.dataSource.sort = this.matsort;
      }
      }
    )
  }

  FilterStudent()
  {
    this.dataSource.filter = this.FilterText;
  }

}
