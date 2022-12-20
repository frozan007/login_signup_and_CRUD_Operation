import { Component, OnInit, ViewChild } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { EmployeeModel, EmployeeModelInt } from 'src/app/models/employee.model';
import { DialogComponent } from '../dialog/dialog.component';
import { EmployeeService } from 'src/app/services/employee.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  employeeModel: EmployeeModel = new EmployeeModel();

  empModel: EmployeeModelInt[] = [];

  displayedColumns: string[] = ['id','profilePic','firstName', 'lastName', 'designation', 'status', 'action'];
  dataSource!: MatTableDataSource<EmployeeModelInt>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  constructor(private dialog : MatDialog, private EmployeeService: EmployeeService) { }

  ngOnInit(): void {
    this.getAll();
  }

  openDialog() {
    const dialogRef = this.dialog.open(DialogComponent,{
      data:{
        title: "Add Employee",
        action: "add"
      },
      width:"30vw" 
      });
    dialogRef.afterClosed().subscribe(result => {
     if(result){
      this.getAll();
     }
    });
  }

  getAll(){
    this.EmployeeService.getAll().subscribe((data) => {
    this.empModel = data;
    this.dataSource = new MatTableDataSource<EmployeeModelInt>(this.empModel);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    })
  }

  editEmployee(row: EmployeeModel){
    const dialogRef = this.dialog.open(DialogComponent,{
      data:{
        title: "Edit Employee",
        action: "edit",
        employeeInfo: row
      },
      width:"30vw" 
    });
    dialogRef.afterClosed().subscribe(result => {
     if(result){
      this.getAll();
     }
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  getStatus(value: number){
    let strResult = "";
    if(value == 1) {strResult = "Active"}
    else if(value == 2) {strResult = "Inactive"}
    else if(value == 3) {strResult = "Deleted"}
    return strResult;
  }

  // getBase64ProfilePicture(empId: number)
  // {
  //   this.EmployeeService.getBase64(empId).subscribe(data => {
  //     return data;
  //   });
  // }
  // getProfilePicture(empId: number)  
  // {
  //   this.EmployeeService.getProfilePicture(empId).subscribe(data => {
  //     if(data)
  //     {
  //       let reader = new FileReader();
  //       reader.addEventListener("load", ()=>{
  //         return reader.result?.toString();
  //       })
  //       reader.readAsDataURL(data);
  //     }
      
  //   });
  // }

  deleteEmployee(id: number){
    this.EmployeeService.delete(id).subscribe({next:(res)=> {
      console.log(res);
      alert("Employee deleted Successfully");
    },
    error:()=> {
      alert("Error while deleting Employee!!!")
  }})
  }


}
