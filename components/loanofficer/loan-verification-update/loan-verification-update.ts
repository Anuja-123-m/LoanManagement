import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LoanOfficerService } from '../../../services/loanofficer-service';
import { AuthService } from '../../../services/auth-service';
import { ILoanVerification, IUpdateLoanVerification } from '../../../models/loanverification/ILoanVerification';

@Component({
  selector: 'app-loan-verification-update',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './loan-verification-update.html',
  styleUrl: './loan-verification-update.css',
})
export class LoanVerificationUpdate implements OnInit {
  verifications: ILoanVerification[] = [];
  isLoading = false;
  errorMessage = '';
  successMessage = '';
  officerId: number | null = null;

  // Form data for updating verification
  selectedVerification: ILoanVerification | null = null;
  updateForm: IUpdateLoanVerification = {
    status: '',
    verificationDetails: '',
    remarks: '',
    verifiedAmount: undefined,
    verifiedTenureMonths: undefined,
  };
  isUpdating = false;
  showUpdateForm = false;

  constructor(
    private loanOfficerService: LoanOfficerService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.getOfficerIdAndLoadVerifications();
  }

  getOfficerIdAndLoadVerifications(): void {
    const storedOfficerId = localStorage.getItem('officerId');
    if (storedOfficerId) {
      this.officerId = parseInt(storedOfficerId, 10);
      this.loadAssignedVerifications();
    } else {
      // For now, use default 1 - in production, this should come from user profile
      this.officerId = 1;
      this.loadAssignedVerifications();
    }
  }

  loadAssignedVerifications(): void {
    if (!this.officerId) {
      const storedOfficerId = localStorage.getItem('officerId');
      this.officerId = storedOfficerId ? parseInt(storedOfficerId, 10) : 1;
    }
    
    this.isLoading = true;
    this.errorMessage = '';
    
    const officerId = this.officerId!;
    
    this.loanOfficerService.getAssignedLoanVerifications(officerId).subscribe({
      next: (data: ILoanVerification[]) => {
        this.verifications = data;
        this.isLoading = false;
      },
      error: (err: any) => {
        this.isLoading = false;
        this.errorMessage = err.error?.message || 'Error loading loan verifications. Please try again.';
        console.error('Error fetching loan verifications', err);
      },
    });
  }

  editVerification(verification: ILoanVerification): void {
    this.selectedVerification = verification;
    this.updateForm = {
      status: verification.status,
      verificationDetails: verification.verificationDetails || '',
      remarks: verification.remarks || '',
      verifiedAmount: verification.verifiedAmount,
      verifiedTenureMonths: verification.verifiedTenureMonths,
    };
    this.showUpdateForm = true;
    this.errorMessage = '';
    this.successMessage = '';
  }

  cancelUpdate(): void {
    this.showUpdateForm = false;
    this.selectedVerification = null;
    this.updateForm = {
      status: '',
      verificationDetails: '',
      remarks: '',
      verifiedAmount: undefined,
      verifiedTenureMonths: undefined,
    };
  }

  updateVerification(): void {
    if (!this.selectedVerification) return;

    if (!this.updateForm.status) {
      this.errorMessage = 'Please select a status.';
      return;
    }

    this.isUpdating = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.loanOfficerService
      .updateLoanVerification(this.selectedVerification.loanVerificationId, this.updateForm)
      .subscribe({
        next: (response: any) => {
          this.isUpdating = false;
          this.successMessage = 'Loan verification updated successfully!';
          this.showUpdateForm = false;
          this.selectedVerification = null;
          this.loadAssignedVerifications();
          setTimeout(() => {
            this.successMessage = '';
          }, 3000);
        },
        error: (err: any) => {
          this.isUpdating = false;
          this.errorMessage = err.error?.message || 'Error updating loan verification. Please try again.';
          console.error('Error updating loan verification', err);
        },
      });
  }

  getStatusBadgeClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'completed':
        return 'badge bg-success';
      case 'failed':
        return 'badge bg-danger';
      case 'inprogress':
        return 'badge bg-info';
      case 'pending':
        return 'badge bg-warning';
      default:
        return 'badge bg-secondary';
    }
  }
}
