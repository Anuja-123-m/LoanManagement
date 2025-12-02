import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoanOfficerService } from '../../../services/loanofficer-service';
import { IHelpReport } from '../../../models/helpreport/IHelpReport';

@Component({
  selector: 'app-officer-help-reports',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './officer-help-reports.html',
  styleUrl: './officer-help-reports.css',
})
export class OfficerHelpReports implements OnInit {
  helpReports: IHelpReport[] = [];
  isLoading = false;
  errorMessage = '';

  constructor(private loanOfficerService: LoanOfficerService) {}

  ngOnInit(): void {
    this.loadHelpReports();
  }

  loadHelpReports(): void {
    this.isLoading = true;
    this.errorMessage = '';
    
    this.loanOfficerService.getHelpReports().subscribe({
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
