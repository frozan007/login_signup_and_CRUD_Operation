import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { sample } from 'rxjs';
import { environment } from 'src/environments/environment';

import { SignUpModel } from '../models/signup.model';

@Injectable({
  providedIn: 'root'
})
export class SignUpService {  
  headers = new HttpHeaders();
  
  constructor(private http: HttpClient) { 
    this.headers.set("contentType", "application/json");
  }  

  getById(id:number){
    let url = environment.rootApi+`/api/Signup/GetById/${id}`;    
    return this.http.post<SignUpModel>(url, {headers:this.headers}).pipe(
      data => {
        return data;
      }  
    )
  }
  getByUserId(userId: number){
    let url = environment.rootApi+`/api/Signup/GetByUserId/${userId}`;    
    return this.http.get<SignUpModel>(url, {headers:this.headers}).pipe(
      data => {
        return data;
      }  
    )
  }
  addOrUpdate(signUpInfo: SignUpModel){
    let url = environment.rootApi+"/api/Signup/AddOrUpdate";    
    return this.http.post<SignUpModel>(url, signUpInfo, {headers:this.headers}).pipe(
      data => {
        return data;
      }  
    )
  }
  delete(id: string){
    let url = environment.rootApi+"/api/Signup/Delete" + id;    
    return this.http.delete<SignUpModel>(url,{headers:this.headers}).pipe(
      data => {
        return data;
      }  
    )
  }
}
