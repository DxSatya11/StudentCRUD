import { Component, OnInit } from '@angular/core';
import { StudentService } from '../student.service';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { student } from 'src/app/Infrastructure/student.interface';
import { Gender } from 'src/app/Infrastructure/gender.interface';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-viewstudent',
  templateUrl: './viewstudent.component.html',
  styleUrls: ['./viewstudent.component.css']
})
export class ViewstudentComponent implements OnInit{
  Id:string|null|undefined;
  GenderList: Gender[] = [];
  isNew = false;
  haderLabel = '';
  ProfilDp='';
  studentsData:student = {
    id: 0,
    name: '',
    email: '',
    dob: new Date,
    age: 0,
    profilImage: '',
    genderid: 0,
    address: {
      id: 0,
      presentAddress: '',
      studentId: 0
    },
    gender: {
      id: 0,
      title: ''
    },
   
  }

  constructor(private studentService: StudentService, private route:ActivatedRoute,
    private snackBar:MatSnackBar,
    private router:Router){}

  ngOnInit(): void{
    this.studentService.getAllGender().subscribe((GenderData) =>
    {
      this.GenderList = GenderData;
    })

    
  
    this.route.paramMap.subscribe((params)=>{this.Id = params.get('id')});

    if(this.Id)
    {
      if(this.Id)
      {
        if(this.Id.toLocaleLowerCase() == "AddStudent".toLocaleLowerCase())
        {
          this.isNew = true;
          this.haderLabel = "Add New Student";
          this.DefaultImage();
        }
        else{
          this.isNew = false;
          this.haderLabel = "Update Student";
          this.studentService.getStudents(this.Id).subscribe((Data: student) =>{
        //DisplayData
        console.log(Data);
        this.studentsData = Data;
        this.DefaultImage();

        debugger;
      });


        }
      }
      // this.studentService.getStudents(this.Id).subscribe((Data: student) =>{
      //   //DisplayData
      //   console.log(Data);
      //   this.studentsData = Data;
      // });
    }   
    
  }

  private DefaultImage():void{
    debugger;
    if(this.studentsData.profilImage)
    {
      debugger;
      this.ProfilDp = this.studentService.getRelativePath(this.studentsData.profilImage);

    }
    else{
      this.ProfilDp = '/assets/User.png';

    }

  }

  ImageUpload(event :any):void
  {
    debugger;
    if(this.Id)
    {
      debugger;
      console.log(this.ProfilDp);
      const file:File = event.target.files[0];
      this.studentService.uploadImage(this.studentsData.id,file).subscribe((successResponse)=>{
        debugger;
        console.log(successResponse);
        this.studentsData.profilImage = successResponse;
        this.DefaultImage();

        this.snackBar.open("Profil Updated",undefined,{duration:3000});

      },
      (errorResponse) =>{});

    }
   
  }

  UpdateStudent() : void
  {
    debugger;
    this.studentService.updateStudent(this.studentsData.id,this.studentsData).subscribe((successResponse) =>{
      this.snackBar.open("Student Updated",undefined,{duration:3000});
    },
    (errorResponse)=> {
      console.log("Error");
    }
    )
//p
  }

  DeleteStudent(): void{

    this.studentService.deleteStudents(this.studentsData.id).subscribe((successResponse) =>{
      this.snackBar.open("Student Deleted",undefined,{duration:3000});

      setTimeout(() => {
        this.router.navigateByUrl('Students');
        
      },2000);

    });
  }

  AddStudent():void{

    this.studentService.addStudent(this.studentsData).subscribe((successResponse) =>{
      this.snackBar.open("Student Added",undefined,{duration:3000});

      setTimeout(() => {
        this.router.navigateByUrl('Students');
        
      },2000);

    },
    (errorRespone) => {console.log(errorRespone)}
    
    );
  }

}