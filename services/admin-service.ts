import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICustomer } from '../models/customer/ICustomer';
import { ILoanOfficer } from '../models/loanofficer/ILoanOfficer';
import { ILoanRequest } from '../models/loanrequest/ILoanRequest';
import { IBackgroundVerification, IUpdateBackgroundVerification } from '../models/backgroundverification/IBackgroundVerification';
import { ILoanVerification, IUpdateLoanVerification } from '../models/loanverification/ILoanVerification';
import { IHelpReport, IUpdateHelpReport } from '../models/helpreport/IHelpReport';
import { IFeedbackQuestion, IAddFeedbackQuestion, IUpdateFeedbackQuestion, ICustomerFeedback } from '../models/feedback/IFeedback';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  private apiUrl = 'http://localhost:5125/api/Admin';

  constructor(private http: HttpClient) {}

  // Customer Management
  getAllCustomers(): Observable<ICustomer[]> {
    return this.http.get<ICustomer[]>(`${this.apiUrl}/customers`);
  }

  getCustomerById(id: number): Observable<ICustomer> {
    return this.http.get<ICustomer>(`${this.apiUrl}/customers/${id}`);
  }

  addCustomer(customer: any): Observable<ICustomer> {
    return this.http.post<ICustomer>(`${this.apiUrl}/customers`, customer);
  }

  approveCustomer(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/customers/${id}/approve`, {}, { responseType: 'text' as 'json' });
  }

  rejectCustomer(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/customers/${id}/reject`, {}, { responseType: 'text' as 'json' });
  }

  // Loan Officer Management
  getAllLoanOfficers(): Observable<ILoanOfficer[]> {
    return this.http.get<ILoanOfficer[]>(`${this.apiUrl}/loanofficers`);
  }

  getLoanOfficerById(id: number): Observable<ILoanOfficer> {
    return this.http.get<ILoanOfficer>(`${this.apiUrl}/loanofficers/${id}`);
  }

  approveLoanOfficer(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/loanofficers/${id}/approve`, {}, { responseType: 'text' as 'json' });
  }

  rejectLoanOfficer(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/loanofficers/${id}/reject`, {}, { responseType: 'text' as 'json' });
  }

  // Background Verification Management
  getAllBackgroundVerifications(): Observable<IBackgroundVerification[]> {
    return this.http.get<IBackgroundVerification[]>(`${this.apiUrl}/backgroundverifications`);
  }

  getBackgroundVerificationById(id: number): Observable<IBackgroundVerification> {
    return this.http.get<IBackgroundVerification>(`${this.apiUrl}/backgroundverifications/${id}`);
  }

  assignOfficerForBackgroundVerification(verificationId: number, officerId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/backgroundverifications/${verificationId}/assign/${officerId}`, {}, { responseType: 'text' as 'json' });
  }

  updateBackgroundVerification(id: number, verification: IUpdateBackgroundVerification): Observable<any> {
    return this.http.put(`${this.apiUrl}/backgroundverifications/${id}`, verification, { responseType: 'text' as 'json' });
  }

  deleteBackgroundVerification(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/backgroundverifications/${id}`, { responseType: 'text' as 'json' });
  }

  // Loan Verification Management
  getAllLoanVerifications(): Observable<ILoanVerification[]> {
    return this.http.get<ILoanVerification[]>(`${this.apiUrl}/loanverifications`);
  }

  getLoanVerificationById(id: number): Observable<ILoanVerification> {
    return this.http.get<ILoanVerification>(`${this.apiUrl}/loanverifications/${id}`);
  }

  assignOfficerForLoanVerification(verificationId: number, officerId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/loanverifications/${verificationId}/assign/${officerId}`, {}, { responseType: 'text' as 'json' });
  }

  updateLoanVerification(id: number, verification: IUpdateLoanVerification): Observable<any> {
    return this.http.put(`${this.apiUrl}/loanverifications/${id}`, verification, { responseType: 'text' as 'json' });
  }

  deleteLoanVerification(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/loanverifications/${id}`, { responseType: 'text' as 'json' });
  }

  // Loan Request Management
  getAllLoanRequests(): Observable<ILoanRequest[]> {
    return this.http.get<ILoanRequest[]>(`${this.apiUrl}/loanrequests`);
  }

  // Help Report Management
  getAllHelpReports(): Observable<IHelpReport[]> {
    return this.http.get<IHelpReport[]>(`${this.apiUrl}/helpreports`);
  }

  getHelpReportById(id: number): Observable<IHelpReport> {
    return this.http.get<IHelpReport>(`${this.apiUrl}/helpreports/${id}`);
  }

  updateHelpReport(id: number, helpReport: IUpdateHelpReport): Observable<any> {
    return this.http.put(`${this.apiUrl}/helpreports/${id}`, helpReport, { responseType: 'text' as 'json' });
  }

  // Feedback Question Management
  getAllFeedbackQuestions(): Observable<IFeedbackQuestion[]> {
    return this.http.get<IFeedbackQuestion[]>(`${this.apiUrl}/feedbackquestions`);
  }

  getFeedbackQuestionById(id: number): Observable<IFeedbackQuestion> {
    return this.http.get<IFeedbackQuestion>(`${this.apiUrl}/feedbackquestions/${id}`);
  }

  addFeedbackQuestion(question: IAddFeedbackQuestion): Observable<any> {
    return this.http.post(`${this.apiUrl}/feedbackquestions`, question, { responseType: 'text' as 'json' });
  }

  updateFeedbackQuestion(id: number, question: IUpdateFeedbackQuestion): Observable<any> {
    return this.http.put(`${this.apiUrl}/feedbackquestions/${id}`, question, { responseType: 'text' as 'json' });
  }

  deleteFeedbackQuestion(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/feedbackquestions/${id}`, { responseType: 'text' as 'json' });
  }

  // Customer Feedback Management
  getAllCustomerFeedbacks(): Observable<ICustomerFeedback[]> {
    return this.http.get<ICustomerFeedback[]>(`${this.apiUrl}/customerfeedbacks`);
  }
}

