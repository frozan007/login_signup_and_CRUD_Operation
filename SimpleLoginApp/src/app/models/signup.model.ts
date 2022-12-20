export class SignUpModel{
    id: number = 0;
    userId: number = 0;
    userName: string ="";
    firstName: string ="";
    lastName: string = "";
    dateOfBirth: Date = new Date();
    gender: GenderEnum  = GenderEnum.Male;
    email: string = ""; 
    password: string = ""; 
    confirmPassword: string = "";    
}
export enum GenderEnum{
    'Male' = 1,
    'Female' = 2
}
