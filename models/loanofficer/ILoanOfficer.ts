import { IUser } from '../user/IUser';

export interface ILoanOfficer {
  officerId: number;
  userId: number;
  user?: IUser;
  firstName: string;
  lastName: string;
  phoneNumber?: string;
  address?: string;
  city?: string;
  state?: string;
  zipCode?: string;
  employeeId?: string;
  department?: string;
  status: string;
  createdDate: Date;
  approvedDate?: Date;
}

export interface IAddLoanOfficer {
  userId: number;
  firstName: string;
  lastName: string;
  phoneNumber?: string;
  address?: string;
  city?: string;
  state?: string;
  zipCode?: string;
  employeeId?: string;
  department?: string;
}

