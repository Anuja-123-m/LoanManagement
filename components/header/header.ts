import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-header',
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  constructor(private authService: AuthService, private router: Router) {}

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  getUserRole(): string | null {
    return this.authService.getUserRole();
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }

  isAdmin(): boolean {
    return this.getUserRole() === 'Admin';
  }

  isCustomer(): boolean {
    return this.getUserRole() === 'Customer';
  }

  isLoanOfficer(): boolean {
    return this.getUserRole() === 'LoanOfficer';
  }
}

