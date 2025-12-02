import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth-service';

/**
 * Role-based guard that protects routes requiring specific user roles
 * @param route - The activated route snapshot
 * @param state - The router state snapshot
 * @returns boolean indicating if route can be activated
 */
export const roleGuard = (allowedRoles: string[]): CanActivateFn => {
  return (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    // Check if user is logged in
    if (!authService.isLoggedIn()) {
      router.navigate(['/login'], {
        queryParams: { returnUrl: state.url },
      });
      return false;
    }

    // Get user role
    const userRole = authService.getUserRole();
    
    // Check if user role is in allowed roles
    if (userRole && allowedRoles.includes(userRole)) {
      return true;
    }

    // If role not allowed, redirect to home
    router.navigate(['/']);
    return false;
  };
};

