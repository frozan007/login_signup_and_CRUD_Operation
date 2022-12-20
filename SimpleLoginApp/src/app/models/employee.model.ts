export class EmployeeModel {
    id: number = 0;
    firstName:  string = "";
    lastName:   string = "";
    designation: string = "";
    status: StatusEnum = StatusEnum.Active;
}

export enum StatusEnum {
    'Active' = 1,
    'Inactive' = 2,
    'Deleted' = 3
}

export interface EmployeeModelInt {
    id: number;
    firstName:  string;
    lastName:   string;
    designation: string;
    status: StatusEnum;
}