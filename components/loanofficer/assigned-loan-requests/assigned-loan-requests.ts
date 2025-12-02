import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoanOfficerService } from '../../../services/loanofficer-service';
import { AuthService } from '../../../services/auth-service';
import { ILoanRequest } from '../../../models/loanrequest/ILoanRequest';

@Component({
  selector: 'app-assigned-loan-requests',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './assigned-loan-requests.html',
  styleUrl: './assigned-loan-requests.css',
})
export class AssignedLoanRequests implements OnInit {
  loanRequests: ILoanRequest[] = [];
  isLoading = false;
  errorMessage = '';
  officerId: number | null = null;

  constructor(
    private loanOfficerService: LoanOfficerService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.getOfficerIdAndLoadRequests();
  }

  getOfficerIdAndLoadRequests(): void {
    // Get userId from token or storage
    const userId = this.authService.getUserId();
    
    if (!userId) {
      // Try to extract from token or use a default
      // For now, we'll use localStorage to store officerId after login
      const storedOfficerId = localStorage.getItem('officerId');
      if (storedOfficerId) {
        this.officerId = parseInt(storedOfficerId, 10);
        this.loadAssignedLoanRequests();
      } else {
        this.errorMessage = 'Officer ID not found. Please login again.';
      }
      return;
    }
    
    // Try to get officerId by finding loan officer with matching userId
    // For now, using a workaround - you may need to create an API endpoint
    // that returns the current logged-in officer's ID
    this.loadAssignedLoanRequests();
  }

  loadAssignedLoanRequests(): void {
    if (!this.officerId) {
      // Try to get from localStorage or use placeholder
      const storedOfficerId = localStorage.getItem('officerId');
      this.officerId = storedOfficerId ? parseInt(storedOfficerId, 10) : 1; // Default to 1 for now
    }
    
    this.isLoading = true;
    this.errorMessage = '';
    
    const officerId = this.officerId!;
    
    this.loanOfficerService.getAssignedLoanRequests(officerId).subscribe({
      next: (data: ILoanRequest[]) => {
        this.loanRequests = data;
        this.isLoading = false;
      },
      error: (err: any) => {
        this.isLoading = false;
        this.errorMessage = err.error?.message || 'Error loading assigned loan requests. Please try again.';
        console.error('Error fetching assigned loan requests', err);
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
