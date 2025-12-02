import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { IRegister } from '../../models/auth/IRegister';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  registerData: IRegister = {
    email: '',
    password: '',
    confirmPassword: '',
  };

  isLoading = false;
  errorMessage = '';
  successMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit(registerForm: any) {
    if (registerForm.valid) {
      if (this.registerData.password !== this.registerData.confirmPassword) {
        this.errorMessage = 'Passwords do not match.';
        return;
      }

      this.isLoading = true;
      this.errorMessage = '';
      this.successMessage = '';

      this.authService.register(this.registerData).subscribe({
        next: (response) => {
          this.isLoading = false;
          this.successMessage = 'Registration successful! Please login.';
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },
        error: (error) => {
          this.isLoading = false;
          console.error('Registration error:', error);
          if (error.status === 0) {
            this.errorMessage = 'Cannot connect to the server. Please ensure the API is running.';
          } else if (error.status === 400) {
            this.errorMessage = error.error?.message || 'Registration failed. Please check your information.';
          } else {
            this.errorMessage = error.error?.message || `Registration failed (${error.status}). Please try again.`;
          }
        },
      });
    } else {
      this.errorMessage = 'Please fill in all required fields correctly.';
    }
  }

  onReset(registerForm: any) {
    registerForm.resetForm();
    this.registerData = { email: '', password: '', confirmPassword: '' };
    this.errorMessage = '';
    this.successMessage = '';
  }
}

