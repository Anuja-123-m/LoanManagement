import { IUser } from '../user/IUser';

export interface ICustomer {
  customerId: number;
  userId: number;
  user?: IUser;
  firstName: string;
  lastName: string;
  phoneNumber?: string;
  address?: string;
  city?: string;
  state?: string;
  zipCode?: string;
  dateOfBirth: Date;
  aadharNumber?: string;
  panNumber?: string;
  status: string;
  createdDate: Date;
  approvedDate?: Date;
}

export interface IAddCustomer {
  userId: number;
  firstName: string;
  lastName: string;
  phoneNumber?: string;
  address?: string;
  city?: string;
  state?: string;
  zipCode?: string;
  dateOfBirth: Date;
  aadharNumber?: string;
  panNumber?: string;
}

