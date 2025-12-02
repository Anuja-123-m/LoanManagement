import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-loanofficer-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container mt-4">
      <h1>Loan Officer Dashboard</h1>
      <p class="lead">Welcome to the Loan Officer Dashboard. Manage your assigned loan requests and verifications.</p>
      
      <div class="row mt-4 g-4">
        <div class="col-md-6">
          <div class="card h-100 shadow-sm">
            <div class="card-body text-center">
              <div class="mb-3">
                <i class="bi bi-file-text display-4 text-primary"></i>
              </div>
              <h5 class="card-title">Assigned Requests</h5>
              <p class="card-text">View all loan requests assigned to you.</p>
              <a routerLink="/loanofficer/loanrequests" class="btn btn-primary">View</a>
            </div>
          </div>
        </div>
        
        <div class="col-md-6">
          <div class="card h-100 shadow-sm">
            <div class="card-body text-center">
              <div class="mb-3">
                <i class="bi bi-check-circle display-4 text-success"></i>
              </div>
              <h5 class="card-title">Verifications</h5>
              <p class="card-text">Update loan verification details for assigned requests.</p>
              <a routerLink="/loanofficer/verifications" class="btn btn-success">Manage</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class LoanOfficerDashboard {}
