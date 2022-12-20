import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginResponseModel, UserLoginModel } from '../models/user-login.model';
import { environment } from 'src/environments/environment';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  headers = new HttpHeaders();

  constructor( private http: HttpClient ){
    this.headers.set("contentType", "application/json");
  }  



  addOrUpdate(User: UserModel){
    let url = environment.rootApi+"/api/User/AddOrUpdate";
    return this.http.post<UserModel>(url, User, {headers:this.headers}).pipe(
      data => {
        return data;
      }  
    )
  }

  login(User: UserLoginModel) {
    let url = environment.rootApi+"/api/User/Login";
    return this.http.post<LoginResponseModel>(url, User, {headers:this.headers}).pipe(
      data => {
        return data;
      }  
    )
  }

  // login(login: UserLoginModel){
  //   let url = environment.rootApi+"/api/User/Login";
  //   return this.http.post<LoginResponseModel>(url, login, {headers:headers}).pipe(
  //     data => {
  //       return data;
  //     }  
  //   )
  // }
}
