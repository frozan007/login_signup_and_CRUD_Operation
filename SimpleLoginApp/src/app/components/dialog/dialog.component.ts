import { HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeModel } from 'src/app/models/employee.model';
import { profileModel } from 'src/app/models/profile.model';
import { EmployeeService } from 'src/app/services/employee.service';
import { ProfileService } from 'src/app/services/profile.service';


@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit {
  employeeModel: EmployeeModel = new EmployeeModel();
  profileInfo: profileModel = new profileModel();  
  
  message: string = "";
  progress: number = 0;
  @Output() onUploadFinished = new EventEmitter();

  title: string = ""; 
  actionbtn: string ='Save';
  profilePicture: any;
  
  constructor(private ProfileService: ProfileService, private EmployeeService: EmployeeService, public dialogRef:MatDialogRef<DialogComponent>,
    @Inject(MAT_DIALOG_DATA) private dialogData: any
    ) {}

  ngOnInit(): void {
    if(this.dialogData.action == 'edit'){ this.actionbtn = "Update";}
    if(this.dialogData.title){ this.title = this.dialogData.title}
    if(this.dialogData.employeeInfo){this.employeeModel = this.dialogData.employeeInfo}
  }

  onSaveClick(){
    this.EmployeeService.addOrUpdate(this.employeeModel).subscribe(data =>{
      if(data){ 
        this.dialogRef.close(true);
        alert("Added Successfully...");     
        // if(this.profilePicture){
        //   this.ProfileService.upload(this.profilePicture, parseInt(data)).subscribe({next:(res)=> {
        //     console.log(res);
        //   },
        //   error:() => {
        //     alert("Error While Uploading Image!!!")
        //   }})
        // }
        // else {
        //   this.dialogRef.close(true);
        // }
    }else{
      alert("Error While Adding!!!");
    }    
        
    });
    
  }
  
  OnSelectedFile(event: any){
    if(event.target.files){
      this.profilePicture = event.target.files[0];
    }
  }

  
  // uploadFile(files){
  //   if(files.length === 0)
  //   return;

  //   let fileToUpload = <File>files[0];
  //   const formData = new FormData();
  //   formData.append('file', fileToUpload, fileToUpload.name);

  //   this.ProfileService.addOrUpdate(this.profileInfo).subscribe(event => {
  //     if(event.type === HttpEventType.UploadProgress){
  //       this.progress = Math.round(100 * event.loaded / event.total);
  //     }
  //     else if(event.type === HttpEventType.Response){
  //       this.message = 'Upload Success.';
  //       this.onUploadFinished.emit(event.body);
  //     }
  //   })
  // }

  onCancelClick(){
    this.dialogRef.close();
  }

}              



