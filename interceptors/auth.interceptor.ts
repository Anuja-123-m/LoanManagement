import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth-service';

/**
 * Authentication interceptor that adds Bearer token to all outgoing HTTP requests
 * @param req - The outgoing HTTP request
 * @param next - The next handler in the interceptor chain
 * @returns Observable of the HTTP event
 */
export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getAuthToken();

  // Skip adding token for login and register requests
  if (req.url.includes('/login') || req.url.includes('/register')) {
    return next(req);
  }

  // If token exists, clone the request and add Authorization header
  if (token) {
    const authReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
    return next(authReq);
  }

  // If no token, proceed with original request
  return next(req);
};

