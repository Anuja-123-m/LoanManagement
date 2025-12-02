import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ILoanRequest, IAddLoanRequest } from '../models/loanrequest/ILoanRequest';
import { IHelpReport, IAddHelpReport } from '../models/helpreport/IHelpReport';
import { IFeedbackQuestion, IAddFeedback } from '../models/feedback/IFeedback';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private apiUrl = 'http://localhost:5125/api/Customer';

  constructor(private http: HttpClient) {}

  // Loan Application
  applyForLoan(loanRequest: IAddLoanRequest): Observable<any> {
    return this.http.post(`${this.apiUrl}/apply-loan`, loanRequest, { responseType: 'text' as 'json' });
  }

  // View Loan Requests
  getLoanRequestsByCustomerId(customerId: number): Observable<ILoanRequest[]> {
    return this.http.get<ILoanRequest[]>(`${this.apiUrl}/loanrequests/${customerId}`);
  }

  getLoanRequestById(customerId: number, loanRequestId: number): Observable<ILoanRequest> {
    return this.http.get<ILoanRequest>(`${this.apiUrl}/loanrequests/${customerId}/${loanRequestId}`);
  }

  // Help Section
  getHelpReports(): Observable<IHelpReport[]> {
    return this.http.get<IHelpReport[]>(`${this.apiUrl}/helpreports`);
  }

  getHelpReportById(id: number): Observable<IHelpReport> {
    return this.http.get<IHelpReport>(`${this.apiUrl}/helpreports/${id}`);
  }

  addHelpReport(helpReport: IAddHelpReport): Observable<any> {
    return this.http.post(`${this.apiUrl}/helpreports`, helpReport, { responseType: 'text' as 'json' });
  }

  // Feedback
  getActiveFeedbackQuestions(): Observable<IFeedbackQuestion[]> {
    return this.http.get<IFeedbackQuestion[]>(`${this.apiUrl}/feedbackquestions`);
  }

  addFeedback(feedback: IAddFeedback): Observable<any> {
    return this.http.post(`${this.apiUrl}/feedback`, feedback, { responseType: 'text' as 'json' });
  }
}

