import { Address } from "./address.interface";
import { Gender } from "./gender.interface";

export interface student
{
    id:number,
    name:string,
    age:number,
    dob:Date,
    email:string,
    profilImage:string,
    genderid:number,
    address:Address,
    gender:Gender
   
}