import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { retry } from 'rxjs';
import { LoginResponseModel, UserLoginModel } from 'src/app/models/user-login.model';
import { UserModel } from 'src/app/models/user.model';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  errorMessage: string = "";
  pwdStrength: string = "";

  userModel: UserLoginModel = new UserLoginModel();

  hide: boolean = true;
  error= "";

  loginForm!: FormGroup;
  constructor(
    private loginService: LoginService,
    private route: Router
    ) { }

  ngOnInit(): void {}

  hideShowPass(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }
  
  onPasswordChange(event: any){
    let value = this.checkStrength(event.target.value);
      if(value == "Good")
      {
        this.pwdStrength = value;
        this.errorMessage ="";
      }
      else if(value == "Strong")
      {
        this.pwdStrength = value;
        this.errorMessage ="";
      }
      else if(value == "Weak")
      {
        this.pwdStrength ="";
        this.errorMessage ="Password is Weak.";
      }
      else if(value == "Too short")
      {
        this.pwdStrength ="";
        this.errorMessage ="Password should be 8 or more characters.";
      }
    
    
  }

  checkStrength(password: string) {  
    var strength = 0  
    if (password.length < 8) {
        return 'Too short'  
    }  
    if (password.length > 7) strength += 1;
    if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) strength += 1; 
    if (password.match(/([a-zA-Z])/) && password.match(/([0-9])/)) strength += 1; 
    if (password.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1;
    if (password.match(/(.*[!,%,&,@,#,$,^,*,?,_,~].*[!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1; 
  
    if (strength < 2) {
        return 'Weak'  
    } else if (strength == 2) {
        return 'Good'  
    } else { 
        return 'Strong'  
    }   
  }

  onSubmit() {
    this.error = "";
    this.loginService.login(this.userModel).subscribe(
      (data: LoginResponseModel) => { 
        if(data.isSuccess == true){
          this.route.navigate(['../dashboard'])
        }
        else {
          this.error = data.response;
        }
        
      } 
    )
  }

  // onSubmit()
  // {
  //  this.loginService.addOrUpdate(this.userModel).subscribe(
  //   data => {
  //     console.log("data", data);
  //     alert("Signup Successful...");
  //     this.loginForm.reset();
  //   });
  // }

}
