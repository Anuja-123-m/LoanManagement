import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { ILogin } from '../../models/auth/ILogin';
import { ILoginResponse } from '../../models/auth/ILoginResponse';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login implements OnInit {
  loginData: ILogin = {
    email: '',
    password: '',
  };

  isLoading = false;
  errorMessage = '';
  successMessage = '';
  returnUrl = '/';

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    if (this.authService.isLoggedIn()) {
      this.router.navigate([this.returnUrl]);
    }
  }

  onSubmit(loginForm: any) {
    if (loginForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';
      this.successMessage = '';

      this.authService.login(this.loginData).subscribe({
        next: (response: any) => {
          this.isLoading = false;
          this.successMessage = 'Login successful!';

          // ASP.NET Core Identity returns accessToken
          const token = response.accessToken || response.token;
          
          if (token) {
            this.authService.setAuthToken(token);
            
            // Extract role from JWT token first, then fallback to email
            const roleFromToken = this.extractRoleFromToken(token);
            const roleFromEmail = this.determineRoleFromEmail(this.loginData.email);
            const finalRole = roleFromToken || roleFromEmail || 'Customer';
            
            console.log('Role detection:', {
              email: this.loginData.email,
              roleFromToken,
              roleFromEmail,
              finalRole
            });
            
            // Set the role
            this.authService.setUserRole(finalRole);

            // Navigate based on role
            setTimeout(() => {
              const userRole = this.authService.getUserRole();
              console.log('Navigating with role:', userRole);
              
              if (userRole === 'Admin') {
                this.router.navigate(['/admin']);
              } else if (userRole === 'LoanOfficer') {
                this.router.navigate(['/loanofficer']);
              } else {
                // Default to Customer for all other cases
                this.router.navigate(['/customer']);
              }
            }, 500);
          } else {
            this.errorMessage = 'Token not received from server.';
          }
        },
        error: (error) => {
          this.isLoading = false;
          console.error('Login error:', error);
          if (error.status === 0) {
            this.errorMessage = 'Cannot connect to the server. Please ensure the API is running.';
          } else if (error.status === 400 || error.status === 401) {
            this.errorMessage = error.error?.message || 'Invalid email or password.';
          } else {
            this.errorMessage = error.error?.message || `Login failed (${error.status}). Please try again.`;
          }
        },
      });
    } else {
      this.errorMessage = 'Please fill in all required fields correctly.';
    }
  }

  onReset(loginForm: any) {
    loginForm.resetForm();
    this.loginData = { email: '', password: '' };
    this.errorMessage = '';
    this.successMessage = '';
  }

  /**
   * Extract role from JWT token
   */
  private extractRoleFromToken(token: string): string | null {
    try {
      // JWT token has 3 parts: header.payload.signature
      const parts = token.split('.');
      if (parts.length !== 3) return null;

      // Decode the payload (second part)
      const payload = JSON.parse(atob(parts[1]));
      
      // Check common role claim names
      return payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ||
             payload['role'] ||
             payload['Role'] ||
             payload['roles']?.[0] ||
             null;
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }

  /**
   * Determine role from email (for testing purposes)
   * Only checks the username part (before @) to avoid false matches from domain
   */
  private determineRoleFromEmail(email: string): string {
    const emailLower = email.toLowerCase().trim();
    
    // Extract username part (before @) only
    const username = emailLower.split('@')[0];
    
    // Check for specific role keywords in username only
    if (username.includes('admin')) {
      return 'Admin';
    } else if (username.includes('officer')) {
      return 'LoanOfficer';
    }
    
    // Default to Customer for all other emails
    return 'Customer';
  }
}

