import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SignUpModel } from 'src/app/models/signup.model';
import { SignUpService } from 'src/app/services/signup.service';



@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  signUpModel: SignUpModel = new SignUpModel();  
  errorMessage: string = "";
  pwdStrength: string = "";

  

  constructor(private SignUpService: SignUpService, private route: Router ) { }

  ngOnInit(): void {
    
  }

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
  onConfirmPasswordChange(event: any){
    let value = event.target.value;
    if(value != this.signUpModel.password)
    {
      this.pwdStrength ="";
      this.errorMessage = "Password doesnt match.";
    }   
    else 
    {
      this.errorMessage = "";
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

  onSignupClick()
  {
    // this.SignUpService.getByUserId(this.SignUpService)
    this.SignUpService.addOrUpdate(this.signUpModel).subscribe(
      data => {
        console.log("data", data);
        alert("Signup Successful...");
        this.route.navigate(['login']);
      })
  }  
}
