import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminService } from '../../../services/admin-service';
import { ICustomer, IAddCustomer } from '../../../models/customer/ICustomer';

@Component({
  selector: 'app-customer-management',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customer-management.html',
  styleUrl: './customer-management.css',
})
export class CustomerManagement implements OnInit {
  customers: ICustomer[] = [];
  isLoading = false;
  errorMessage = '';
  successMessage = '';
  showAddForm = false;
  newCustomer: IAddCustomer = {
    userId: 0,
    firstName: '',
    lastName: '',
    phoneNumber: '',
    address: '',
    city: '',
    state: '',
    zipCode: '',
    dateOfBirth: new Date('2000-01-01'),
    aadharNumber: '',
    panNumber: ''
  };
  dateOfBirthString: string = '';

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.adminService.getAllCustomers().subscribe({
      next: (data: ICustomer[]) => {
        this.customers = data;
        this.isLoading = false;
      },
      error: (err: any) => {
        this.isLoading = false;
        console.error('Error fetching customers', err);
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
            : err.error?.message || err.error?.title || err.message || 'Error loading customers. Please try again.';
          this.errorMessage = errorMsg;
        }
      },
    });
  }

  approveCustomer(customer: ICustomer): void {
    if (confirm(`Do you want to approve customer: ${customer.firstName} ${customer.lastName}?`)) {
      this.adminService.approveCustomer(customer.customerId).subscribe({
        next: (response: any) => {
          this.successMessage = 'Customer approved successfully!';
          this.loadCustomers();
          setTimeout(() => {
            this.successMessage = '';
          }, 3000);
        },
        error: (err: any) => {
          this.errorMessage = err.error?.message || 'Error approving customer. Please try again.';
          console.error('Error approving customer', err);
        },
      });
    }
  }

  rejectCustomer(customer: ICustomer): void {
    if (confirm(`Do you want to reject customer: ${customer.firstName} ${customer.lastName}?`)) {
      this.adminService.rejectCustomer(customer.customerId).subscribe({
        next: (response: any) => {
          this.successMessage = 'Customer rejected successfully!';
          this.loadCustomers();
          setTimeout(() => {
            this.successMessage = '';
          }, 3000);
        },
        error: (err: any) => {
          this.errorMessage = err.error?.message || 'Error rejecting customer. Please try again.';
          console.error('Error rejecting customer', err);
        },
      });
    }
  }

  getStatusBadgeClass(status: string): string {
    switch (status?.toLowerCase()) {
      case 'approved':
        return 'badge bg-success';
      case 'rejected':
        return 'badge bg-danger';
      case 'pending':
        return 'badge bg-warning';
      default:
        return 'badge bg-secondary';
    }
  }

  openAddForm(): void {
    this.showAddForm = true;
    this.newCustomer = {
      userId: 0,
      firstName: '',
      lastName: '',
      phoneNumber: '',
      address: '',
      city: '',
      state: '',
      zipCode: '',
      dateOfBirth: new Date('2000-01-01'),
      aadharNumber: '',
      panNumber: ''
    };
    this.dateOfBirthString = '';
    this.errorMessage = '';
    this.successMessage = '';
  }

  closeAddForm(): void {
    this.showAddForm = false;
    this.errorMessage = '';
  }

  addCustomer(): void {
    if (!this.newCustomer.firstName || !this.newCustomer.lastName) {
      this.errorMessage = 'First Name and Last Name are required.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    // Convert date string to Date object if provided
    if (this.dateOfBirthString) {
      this.newCustomer.dateOfBirth = new Date(this.dateOfBirthString);
    }

    const customerData = {
      ...this.newCustomer,
      status: 'Pending'
    };

    this.adminService.addCustomer(customerData).subscribe({
      next: (customer: ICustomer) => {
        this.isLoading = false;
        this.successMessage = `Customer "${customer.firstName} ${customer.lastName}" added successfully!`;
        this.showAddForm = false;
        this.loadCustomers();
        setTimeout(() => {
          this.successMessage = '';
        }, 3000);
      },
      error: (err: any) => {
        this.isLoading = false;
        console.error('Error adding customer', err);
        const errorMsg = typeof err.error === 'string'
          ? err.error
          : err.error?.message || err.error?.title || err.message || 'Error adding customer. Please try again.';
        this.errorMessage = errorMsg;
      },
    });
  }
}
