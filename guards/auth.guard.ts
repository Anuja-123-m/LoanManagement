import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth-service';

/**
 * Authentication guard that protects routes requiring user authentication
 * @param route - The activated route snapshot
 * @param state - The router state snapshot
 * @returns boolean indicating if route can be activated
 */
export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // Check if user is logged in
  if (authService.isLoggedIn()) {
    return true;
  }

  // If not logged in, redirect to login page with return URL
  router.navigate(['/login'], {
    queryParams: { returnUrl: state.url },
  });

  return false;
};

