import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../../services/admin-service';
import { ILoanRequest } from '../../../models/loanrequest/ILoanRequest';

@Component({
  selector: 'app-loanrequest-management',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loanrequest-management.html',
  styleUrl: './loanrequest-management.css',
})
export class LoanRequestManagement implements OnInit {
  loanRequests: ILoanRequest[] = [];
  isLoading = false;
  errorMessage = '';

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadLoanRequests();
  }

  loadLoanRequests(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.adminService.getAllLoanRequests().subscribe({
      next: (data: ILoanRequest[]) => {
        this.loanRequests = data;
        this.isLoading = false;
      },
      error: (err: any) => {
        this.isLoading = false;
        console.error('Error fetching loan requests', err);
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
            : err.error?.message || err.error?.title || err.message || 'Error loading loan requests. Please try again.';
          this.errorMessage = errorMsg;
        }
      },
    });
  }

  getStatusBadgeClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'approved':
        return 'badge bg-success';
      case 'rejected':
        return 'badge bg-danger';
      case 'pending':
        return 'badge bg-warning';
      case 'underverification':
        return 'badge bg-info';
      default:
        return 'badge bg-secondary';
    }
  }
}
