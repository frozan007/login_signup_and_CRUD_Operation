import { UserModel } from "./user.model";

export class UserLoginModel{
    userName: string = "";
    password: string = "";
}
export class LoginResponseModel{
    user: UserModel = new UserModel();
    isSuccess: boolean = false;
    response: string = "";
}