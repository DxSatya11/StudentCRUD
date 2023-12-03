import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { student } from '../Infrastructure/student.interface';
import { Observable } from 'rxjs';
import { Gender } from '../Infrastructure/gender.interface';
import { StudentViewModel } from '../Infrastructure/studentview-model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private baseApiUri = "https://studentdev.azurewebsites.net";
  constructor(private httpClient : HttpClient) { }
  getAllStudents() :Observable<student[]>
  {
    return this.httpClient.get<student[]>(this.baseApiUri + '/api/Student/GetAllStudent')
  }

  getAllGender() :Observable<Gender[]>
  {
    return this.httpClient.get<Gender[]>(this.baseApiUri + '/api/Gender/GetAllGender')
  }

  getStudents(Id: string): Observable<student>
   {
    return this.httpClient.get<student>(this.baseApiUri + '/api/Student/GetStudent/' + Id);
   }

   updateStudent(ID:number,studentViewModel : student ):Observable<student>
   {
    debugger;
     const vm : StudentViewModel =
     {
      
      name:studentViewModel.name,
      age:studentViewModel.age,
      dob:studentViewModel.dob,
      email:studentViewModel.email,
      profilimage:studentViewModel.profilImage, 

      genderid:studentViewModel.genderid,
      presentAddress:studentViewModel.address.presentAddress,

     }
     debugger;
     return this.httpClient.put<student>(this.baseApiUri + '/api/Student/UpdateStudent/' + ID,vm);
   }

   deleteStudents(Id:number): Observable<student>
   {
    return this.httpClient.delete<student>(this.baseApiUri + '/api/Student/DeleteStudent/' + Id);
   }


   addStudent(studentViewModel : student ):Observable<student>
   {
     const vm : StudentViewModel =
     {
      
      name:studentViewModel.name,
      age:studentViewModel.age,
      dob:studentViewModel.dob,
      email:studentViewModel.email,
      profilimage:studentViewModel.profilImage,
      genderid:studentViewModel.genderid,
      presentAddress:studentViewModel.address.presentAddress,

     }
    
     return this.httpClient.post<student>(this.baseApiUri + '/api/Student/AddStudent/' , vm);
   }

   uploadImage(id:number,file:File):Observable<any>
   {
    debugger;
    const formData = new FormData();
    formData.append("profil",file);
    debugger;
   return  this.httpClient.post(this.baseApiUri + '/api/Student/ImageUpload/' + id,formData,{responseType:'text'});
   }

//    getRelativePath(relativePath:string) {
//     return `${this.baseApiUri}/${relativePath}`;
//  }


getRelativePath(relativePath: string) {
  debugger;
  // Check if relativePath is already a complete URL
  if (relativePath.startsWith('http://') || relativePath.startsWith('https://')) {
    return relativePath; // Return the complete URL as is
  }

  // Remove any leading slash from relativePath
  relativePath = relativePath.replace(/^\//, '');

  // Combine with the base API URI
  return `${this.baseApiUri}/${relativePath}`;
}

 

}
