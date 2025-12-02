import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../../services/admin-service';
import { ILoanOfficer } from '../../../models/loanofficer/ILoanOfficer';

@Component({
  selector: 'app-loanofficer-management',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loanofficer-management.html',
  styleUrl: './loanofficer-management.css',
})
export class LoanOfficerManagement implements OnInit {
  loanOfficers: ILoanOfficer[] = [];
  isLoading = false;
  errorMessage = '';
  successMessage = '';

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadLoanOfficers();
  }

  loadLoanOfficers(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.adminService.getAllLoanOfficers().subscribe({
      next: (data: ILoanOfficer[]) => {
        this.loanOfficers = data;
        this.isLoading = false;
      },
      error: (err: any) => {
        this.isLoading = false;
        console.error('Error fetching loan officers', err);
        console.error('Error details:', {
          status: err.status,
          statusText: err.statusText,
          error: err.error,
          message: err.message
        });

        if (err.status === 0 || !err.status) {
          this.errorMessage = 'Cannot connect to the server. Please ensure the API is running on http://localhost:5125';
        } else if (err.status === 401 || err.status === 403) {
          this.errorMessage = 'Unauthorized. Please login again with Admin credentials.';
        } else if (err.status === 404) {
          this.errorMessage = 'API endpoint not found. Please check the API configuration.';
        } else if (err.status === 500) {
          this.errorMessage = 'Server error. Please check the API logs.';
        } else {
          const errorMsg = typeof err.error === 'string'
            ? err.error
            : err.error?.message || err.error?.title || err.message || 'Error loading loan officers. Please try again.';
          this.errorMessage = errorMsg;
        }
      },
    });
  }

  approveLoanOfficer(officer: ILoanOfficer): void {
    if (confirm(`Do you want to approve loan officer: ${officer.firstName} ${officer.lastName}?`)) {
      this.adminService.approveLoanOfficer(officer.officerId).subscribe({
        next: (response: any) => {
          this.successMessage = 'Loan officer approved successfully!';
          this.loadLoanOfficers();
          setTimeout(() => {
            this.successMessage = '';
          }, 3000);
        },
        error: (err: any) => {
          this.errorMessage = err.error?.message || 'Error approving loan officer. Please try again.';
          console.error('Error approving loan officer', err);
        },
      });
    }
  }

  rejectLoanOfficer(officer: ILoanOfficer): void {
    if (confirm(`Do you want to reject loan officer: ${officer.firstName} ${officer.lastName}?`)) {
      this.adminService.rejectLoanOfficer(officer.officerId).subscribe({
        next: (response: any) => {
          this.successMessage = 'Loan officer rejected successfully!';
          this.loadLoanOfficers();
          setTimeout(() => {
            this.successMessage = '';
          }, 3000);
        },
        error: (err: any) => {
          this.errorMessage = err.error?.message || 'Error rejecting loan officer. Please try again.';
          console.error('Error rejecting loan officer', err);
        },
      });
    }
  }

  getStatusBadgeClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'approved':
        return 'badge bg-success';
      case 'rejected':
        return 'badge bg-danger';
      case 'pending':
        return 'badge bg-warning';
      default:
        return 'badge bg-secondary';
    }
  }
}
