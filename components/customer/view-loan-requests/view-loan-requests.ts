import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CustomerService } from '../../../services/customer-service';
import { AuthService } from '../../../services/auth-service';
import { ILoanRequest } from '../../../models/loanrequest/ILoanRequest';

@Component({
  selector: 'app-view-loan-requests',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './view-loan-requests.html',
  styleUrl: './view-loan-requests.css',
})
export class ViewLoanRequests implements OnInit {
  loanRequests: ILoanRequest[] = [];
  isLoading = false;
  errorMessage = '';
  customerId: number | null = null;

  constructor(
    private customerService: CustomerService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.getCustomerIdAndLoadRequests();
  }

  getCustomerIdAndLoadRequests(): void {
    const storedCustomerId = localStorage.getItem('customerId');
    if (storedCustomerId) {
      this.customerId = parseInt(storedCustomerId, 10);
      this.loadLoanRequests();
    } else {
      // For testing, use default 1
      this.customerId = 1;
      this.loadLoanRequests();
    }
  }

  loadLoanRequests(): void {
    if (!this.customerId || this.customerId === 0) {
      this.errorMessage = 'Customer ID not found. Please login again.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.customerService.getLoanRequestsByCustomerId(this.customerId).subscribe({
      next: (data: ILoanRequest[]) => {
        this.loanRequests = data;
        this.isLoading = false;
      },
      error: (err: any) => {
        this.isLoading = false;
        this.errorMessage = err.error?.message || 'Error loading loan requests. Please try again.';
        console.error('Error fetching loan requests', err);
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
