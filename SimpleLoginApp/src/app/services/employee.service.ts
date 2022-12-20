import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmployeeModel, StatusEnum } from '../models/employee.model';
import { profileModel } from 'src/app/models/profile.model'

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  headers = new HttpHeaders();

  constructor(private http: HttpClient) { 
    this.headers.set("contentType", "application/json");
   }

   get(id: number) {
    let url = environment.rootApi+`/api/employee/Get/${id}`;
    return this.http.get<EmployeeModel>(url, {headers:this.headers}).pipe(
      data =>{
        return data;
      })
   }
   
   addOrUpdate(employeeInfo: EmployeeModel) {
    let url = environment.rootApi+"/api/employee/AddOrUpdate";
    return this.http.post<string>(url, employeeInfo,  {headers:this.headers}).pipe(
      data => {
        return data;
      }
    )
   }
   delete(id: number) {
    let url = environment.rootApi+`/api/employee/Delete/${id}`;
    return this.http.delete<EmployeeModel>(url,{headers:this.headers}).pipe(
      data => {
        return data;
      }
    )
   }
   getAll() {
    let url = environment.rootApi+"/api/Employee/GetAll";
    return this.http.get<any>(url, {headers:this.headers});
   }

   getBase64(empId: number) {
    let url = environment.rootApi+`/api/Profile/GetBase64ProfilePicture/${empId}`;
    return this.http.get(url, {responseType:"text"}).pipe(
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
}

