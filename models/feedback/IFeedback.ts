import { ICustomer } from '../customer/ICustomer';

export interface IFeedbackQuestion {
  questionId: number;
  question: string;
  questionType: string;
  isActive: boolean;
  createdDate: Date;
  updatedDate?: Date;
}

export interface ICustomerFeedback {
  feedbackId: number;
  customerId: number;
  customer?: ICustomer;
  questionId?: number;
  feedbackQuestion?: IFeedbackQuestion;
  feedbackText?: string;
  rating?: number;
  createdDate: Date;
}

export interface IAddFeedback {
  customerId: number;
  questionId?: number;
  feedbackText?: string;
  rating?: number;
}

export interface IAddFeedbackQuestion {
  question: string;
  questionType: string;
  isActive: boolean;
}

export interface IUpdateFeedbackQuestion {
  question: string;
  questionType: string;
  isActive: boolean;
}

