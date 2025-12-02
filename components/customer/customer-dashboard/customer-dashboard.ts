import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-customer-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container mt-4">
      <h1>Customer Dashboard</h1>
      <p class="lead">Welcome to the Customer Dashboard. Manage your loan applications and track their status.</p>
      
      <div class="row mt-4 g-4">
        <div class="col-md-6 col-lg-3">
          <div class="card h-100 shadow-sm">
            <div class="card-body text-center">
              <div class="mb-3">
                <i class="bi bi-file-plus display-4 text-primary"></i>
              </div>
              <h5 class="card-title">Apply for Loan</h5>
              <p class="card-text">Submit a new loan application.</p>
              <a routerLink="/customer/apply-loan" class="btn btn-primary">Apply Now</a>
            </div>
          </div>
        </div>
        
        <div class="col-md-6 col-lg-3">
          <div class="card h-100 shadow-sm">
            <div class="card-body text-center">
              <div class="mb-3">
                <i class="bi bi-list-ul display-4 text-info"></i>
              </div>
              <h5 class="card-title">My Loan Requests</h5>
              <p class="card-text">View all your loan applications.</p>
              <a routerLink="/customer/loanrequests" class="btn btn-info">View</a>
            </div>
          </div>
        </div>

        <div class="col-md-6 col-lg-3">
          <div class="card h-100 shadow-sm">
            <div class="card-body text-center">
              <div class="mb-3">
                <i class="bi bi-question-circle display-4 text-warning"></i>
              </div>
              <h5 class="card-title">Help Section</h5>
              <p class="card-text">Get help and support.</p>
              <a routerLink="/customer/help" class="btn btn-warning">View Help</a>
            </div>
          </div>
        </div>

        <div class="col-md-6 col-lg-3">
          <div class="card h-100 shadow-sm">
            <div class="card-body text-center">
              <div class="mb-3">
                <i class="bi bi-chat-left-text display-4 text-success"></i>
              </div>
              <h5 class="card-title">Feedback</h5>
              <p class="card-text">Share your feedback with us.</p>
              <a routerLink="/customer/feedback" class="btn btn-success">Give Feedback</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class CustomerDashboard {}
