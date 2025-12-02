import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ILoanRequest } from '../models/loanrequest/ILoanRequest';
import { ILoanVerification, IUpdateLoanVerification } from '../models/loanverification/ILoanVerification';
import { IHelpReport } from '../models/helpreport/IHelpReport';
import { ILoanOfficer } from '../models/loanofficer/ILoanOfficer';

@Injectable({
  providedIn: 'root',
})
export class LoanOfficerService {
  private apiUrl = 'http://localhost:5125/api/LoanOfficer';
  private adminApiUrl = 'http://localhost:5125/api/Admin';

  constructor(private http: HttpClient) {}

  // Get loan officer by userId (using Admin endpoint since LoanOfficer needs officerId)
  // Note: This might need to be moved to a common endpoint or user profile endpoint
  getLoanOfficerByUserId(userId: number): Observable<ILoanOfficer | null> {
    // We'll need to get all officers and find the one with matching userId
    // For now, returning an observable that needs to be handled
    return new Observable((observer) => {
      this.getAllLoanOfficers().subscribe({
        next: (officers) => {
          const officer = officers.find(o => o.userId === userId);
          observer.next(officer || null);
          observer.complete();
        },
        error: (err) => {
          observer.error(err);
        }
      });
    });
  }

  // Helper method to get all loan officers (uses Admin endpoint)
  private getAllLoanOfficers(): Observable<ILoanOfficer[]> {
    return this.http.get<ILoanOfficer[]>(`${this.adminApiUrl}/loanofficers`);
  }

  // View Assigned Loan Requests
  getAssignedLoanRequests(officerId: number): Observable<ILoanRequest[]> {
    return this.http.get<ILoanRequest[]>(`${this.apiUrl}/loanrequests/${officerId}`);
  }

  // Update Loan Verification Details
  getAssignedLoanVerifications(officerId: number): Observable<ILoanVerification[]> {
    return this.http.get<ILoanVerification[]>(`${this.apiUrl}/loanverifications/${officerId}`);
  }

  getLoanVerificationById(officerId: number, loanVerificationId: number): Observable<ILoanVerification> {
    return this.http.get<ILoanVerification>(`${this.apiUrl}/loanverifications/${officerId}/${loanVerificationId}`);
  }

  updateLoanVerification(loanVerificationId: number, verification: IUpdateLoanVerification): Observable<any> {
    return this.http.put(`${this.apiUrl}/loanverifications/${loanVerificationId}`, verification, { responseType: 'text' as 'json' });
  }

  // View Help Reports
  getHelpReports(): Observable<IHelpReport[]> {
    return this.http.get<IHelpReport[]>(`${this.apiUrl}/helpreports`);
  }

  getHelpReportById(id: number): Observable<IHelpReport> {
    return this.http.get<IHelpReport>(`${this.apiUrl}/helpreports/${id}`);
  }
}
