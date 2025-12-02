import { ILoanRequest } from '../loanrequest/ILoanRequest';
import { ICustomer } from '../customer/ICustomer';
import { ILoanOfficer } from '../loanofficer/ILoanOfficer';

export interface IBackgroundVerification {
  verificationId: number;
  loanRequestId: number;
  loanRequest?: ILoanRequest;
  customerId: number;
  customer?: ICustomer;
  assignedOfficerId?: number;
  assignedOfficer?: ILoanOfficer;
  status: string;
  verificationDetails?: string;
  remarks?: string;
  createdDate: Date;
  completedDate?: Date;
}

export interface IUpdateBackgroundVerification {
  status: string;
  verificationDetails?: string;
  remarks?: string;
}

