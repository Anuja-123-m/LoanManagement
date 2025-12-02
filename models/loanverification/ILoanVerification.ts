import { ILoanRequest } from '../loanrequest/ILoanRequest';
import { ICustomer } from '../customer/ICustomer';
import { ILoanOfficer } from '../loanofficer/ILoanOfficer';

export interface ILoanVerification {
  loanVerificationId: number;
  loanRequestId: number;
  loanRequest?: ILoanRequest;
  customerId: number;
  customer?: ICustomer;
  assignedOfficerId?: number;
  assignedOfficer?: ILoanOfficer;
  status: string;
  verificationDetails?: string;
  remarks?: string;
  verifiedAmount?: number;
  verifiedTenureMonths?: number;
  createdDate: Date;
  completedDate?: Date;
}

export interface IUpdateLoanVerification {
  status: string;
  verificationDetails?: string;
  remarks?: string;
  verifiedAmount?: number;
  verifiedTenureMonths?: number;
}

