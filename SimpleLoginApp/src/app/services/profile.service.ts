import { HttpClient, HttpEvent, HttpEventType, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { profileModel } from '../models/profile.model';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  headers = new HttpHeaders();
  
  constructor(private http: HttpClient) {
    this.headers.set("contentType", "application/json");
  }

  getBase64(empId: number) {
    let url = environment.rootApi+`/api/Profile/GetBase64ProfilePicture/${empId}`;
    return this.http.get(url, {responseType:"text" }).pipe(
      data =>{
        return data;
      })
   }
   getProfilePicture(empId: number) {
    let url = environment.rootApi+`/api/Profile/GetProfilePicture/${empId}`;
    return this.http.get(url, {responseType:"blob" }).pipe(
      data =>{
        return data;
      })
   }
  //  upload(formData: any, empId: number) {
  //   let url = environment.rootApi+`/api/Profile/upload/${empId}`;
  //   return this.http.post(url, formData, {
  //     reportProgress: true, 
  //     observe: 'events'}).pipe(formData => {
  //       return formData;
  //     });
  //  }

   upload(file: File, empId: number) {
    let url = environment.rootApi+`/api/Profile/Upload/${empId}`;

    var httpOptions = {
      headers: new HttpHeaders({"Accept": "application/json"})      
    }   
    httpOptions.headers.set("Content-Type", "multipart/form-data");

    var formData = new FormData();
    formData.append("profilePic", file);

    return this.http.post(url, formData, httpOptions)
    .pipe(data => {
      return data;
    });
   }

}
 