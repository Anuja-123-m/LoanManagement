import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerService } from '../../../services/customer-service';
import { AuthService } from '../../../services/auth-service';
import { IAddLoanRequest } from '../../../models/loanrequest/ILoanRequest';

@Component({
  selector: 'app-apply-loan',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './apply-loan.html',
  styleUrl: './apply-loan.css',
})
export class ApplyLoan implements OnInit {
  loanRequest: IAddLoanRequest = {
    customerId: 0,
    loanType: '',
    loanAmount: 0,
    tenureMonths: 0,
    purpose: '',
  };

  isLoading = false;
  errorMessage = '';
  successMessage = '';
  customerId: number | null = null;

  loanTypes = [
    'Home Loan',
    'Car Loan',
    'Education Loan',
    'Personal Loan',
    'Business Loan',
    'Gold Loan',
  ];

  constructor(
    private customerService: CustomerService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Get customer ID from localStorage or auth service
    const storedCustomerId = localStorage.getItem('customerId');
    if (storedCustomerId) {
      this.customerId = parseInt(storedCustomerId, 10);
      this.loanRequest.customerId = this.customerId;
    } else {
      // For testing, use default 1
      this.customerId = 1;
      this.loanRequest.customerId = 1;
    }
  }

  onSubmit(loanForm: any): void {
    if (loanForm.valid) {
      if (!this.customerId || this.customerId === 0) {
        this.errorMessage = 'Customer ID not found. Please login again.';
        return;
      }

      this.loanRequest.customerId = this.customerId;
      this.isLoading = true;
      this.errorMessage = '';
      this.successMessage = '';

      // Log the request payload for debugging
      console.log('Submitting loan request:', this.loanRequest);

      this.customerService.applyForLoan(this.loanRequest).subscribe({
        next: (response: any) => {
          this.isLoading = false;
          this.successMessage = 'Loan application submitted successfully!';
          console.log('Loan application successful:', response);
          setTimeout(() => {
            this.router.navigate(['/customer/loanrequests']);
          }, 2000);
        },
        error: (err: any) => {
          this.isLoading = false;
          console.error('Error applying for loan - Full error:', err);
          console.error('Error status:', err.status);
          console.error('Error message:', err.message);
          console.error('Error error object:', err.error);

          // Better error message handling
          if (err.status === 0) {
            this.errorMessage = 'Cannot connect to the server. Please ensure the API is running on http://localhost:5125';
          } else if (err.status === 401 || err.status === 403) {
            this.errorMessage = 'Authentication failed. Please login again.';
          } else if (err.status === 400) {
            // Try to extract detailed error message
            const errorMsg = err.error?.message || err.error?.title || err.error || 'Bad request. Please check your input.';
            this.errorMessage = `Error: ${errorMsg}. Customer ID: ${this.customerId}. Please ensure the customer exists in the database.`;
          } else if (err.status === 404) {
            this.errorMessage = 'API endpoint not found. Please check the API URL.';
          } else if (err.status === 500) {
            const errorMsg = err.error?.message || err.error || 'Internal server error. Please contact support.';
            this.errorMessage = `Server error: ${errorMsg}`;
          } else {
            const errorMsg = err.error?.message || err.error || err.message || 'Unknown error occurred.';
            this.errorMessage = `Error (${err.status}): ${errorMsg}`;
          }
        },
      });
    } else {
      this.errorMessage = 'Please fill in all required fields correctly.';
    }
  }

  onReset(loanForm: any): void {
    loanForm.resetForm();
    this.loanRequest = {
      customerId: this.customerId || 0,
      loanType: '',
      loanAmount: 0,
      tenureMonths: 0,
      purpose: '',
    };
    this.errorMessage = '';
    this.successMessage = '';
  }
}
