import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="container mt-4">
      <h1>Admin Dashboard</h1>
      <p>Welcome to the Admin Dashboard. This component needs to be implemented.</p>
      <div class="row mt-4">
        <div class="col-md-3">
          <div class="card">
            <div class="card-body">
              <h5 class="card-title">Customers</h5>
              <a routerLink="/admin/customers" class="btn btn-primary">Manage</a>
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="card">
            <div class="card-body">
              <h5 class="card-title">Loan Officers</h5>
              <a routerLink="/admin/loanofficers" class="btn btn-primary">Manage</a>
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="card">
            <div class="card-body">
              <h5 class="card-title">Loan Requests</h5>
              <a routerLink="/admin/loanrequests" class="btn btn-primary">View</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  `,
})
export class AdminDashboard {}

