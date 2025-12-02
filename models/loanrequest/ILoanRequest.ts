import { ICustomer } from '../customer/ICustomer';
import { ILoanOfficer } from '../loanofficer/ILoanOfficer';

export interface ILoanRequest {
  loanRequestId: number;
  customerId: number;
  customer?: ICustomer;
  loanType: string;
  loanAmount: number;
  tenureMonths: number;
  purpose?: string;
  status: string;
  requestDate: Date;
  assignedOfficerId?: number;
  assignedOfficer?: ILoanOfficer;
  approvedDate?: Date;
  rejectedDate?: Date;
  rejectionReason?: string;
}

export interface IAddLoanRequest {
  customerId: number;
  loanType: string;
  loanAmount: number;
  tenureMonths: number;
  purpose?: string;
}

