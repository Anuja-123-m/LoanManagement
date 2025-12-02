import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerService } from '../../../services/customer-service';
import { AuthService } from '../../../services/auth-service';
import { IHelpReport, IAddHelpReport } from '../../../models/helpreport/IHelpReport';

@Component({
  selector: 'app-customer-help-section',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customer-help-section.html',
  styleUrl: './customer-help-section.css',
})
export class CustomerHelpSection implements OnInit {
  helpReports: IHelpReport[] = [];
  isLoading = false;
  errorMessage = '';
  successMessage = '';
  showAddForm = false;
  userId: number | null = null;

  newHelpReport: IAddHelpReport = {
    userId: 0,
    title: '',
    description: '',
    category: '',
  };

  categories = ['General', 'Technical', 'Loan', 'Payment', 'Account', 'Other'];

  constructor(
    private customerService: CustomerService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadHelpReports();
    const storedUserId = localStorage.getItem('userId');
    if (storedUserId) {
      this.userId = parseInt(storedUserId, 10);
      this.newHelpReport.userId = this.userId;
    } else {
      this.userId = 1; // Default for testing
      this.newHelpReport.userId = 1;
    }
  }

  loadHelpReports(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.customerService.getHelpReports().subscribe({
      next: (data: IHelpReport[]) => {
        this.helpReports = data;
        this.isLoading = false;
      },
      error: (err: any) => {
        this.isLoading = false;
        this.errorMessage = err.error?.message || 'Error loading help reports. Please try again.';
        console.error('Error fetching help reports', err);
      },
    });
  }

  showAddHelpForm(): void {
    this.showAddForm = true;
    this.errorMessage = '';
    this.successMessage = '';
  }

  cancelAddHelp(): void {
    this.showAddForm = false;
    this.newHelpReport = {
      userId: this.userId || 0,
      title: '',
      description: '',
      category: '',
    };
  }

  submitHelpReport(helpForm: any): void {
    if (helpForm.valid) {
      if (!this.userId) {
        this.errorMessage = 'User ID not found. Please login again.';
        return;
      }

      this.newHelpReport.userId = this.userId;
      this.isLoading = true;
      this.errorMessage = '';
      this.successMessage = '';

      this.customerService.addHelpReport(this.newHelpReport).subscribe({
        next: (response: any) => {
          this.isLoading = false;
          this.successMessage = 'Help report submitted successfully!';
          this.showAddForm = false;
          this.loadHelpReports();
          this.newHelpReport = {
            userId: this.userId || 0,
            title: '',
            description: '',
            category: '',
          };
          setTimeout(() => {
            this.successMessage = '';
          }, 3000);
        },
        error: (err: any) => {
          this.isLoading = false;
          this.errorMessage = err.error?.message || 'Error submitting help report. Please try again.';
          console.error('Error submitting help report', err);
        },
      });
    } else {
      this.errorMessage = 'Please fill in all required fields correctly.';
    }
  }

  getStatusBadgeClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'resolved':
        return 'badge bg-success';
      case 'closed':
        return 'badge bg-secondary';
      case 'inprogress':
        return 'badge bg-info';
      case 'open':
        return 'badge bg-warning';
      default:
        return 'badge bg-secondary';
    }
  }
}
