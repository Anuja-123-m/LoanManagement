import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ILogin } from '../models/auth/ILogin';
import { IRegister } from '../models/auth/IRegister';
import { ILoginResponse } from '../models/auth/ILoginResponse';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5125';

  constructor(private http: HttpClient) {}

  /**
   * Register a new user
   * @param registerData - IRegister object containing email, password, and confirmPassword
   * @returns Observable with registration response
   */
  register(registerData: IRegister): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, registerData);
  }

  /**
   * Authenticate user with email and password
   * @param loginData - ILogin object containing email and password
   * @returns Observable with authentication response
   */
  login(loginData: ILogin): Observable<ILoginResponse> {
    return this.http.post<ILoginResponse>(`${this.apiUrl}/login`, loginData);
  }

  /**
   * Check if user is currently logged in
   * @returns boolean indicating login status
   */
  isLoggedIn(): boolean {
    return localStorage.getItem('authToken') !== null;
  }

  /**
   * Get user role from token or storage
   * @returns user role or null
   */
  getUserRole(): string | null {
    return localStorage.getItem('userRole');
  }

  /**
   * Store authentication token in localStorage
   * @param token - JWT or authentication token
   */
  setAuthToken(token: string): void {
    localStorage.setItem('authToken', token);
  }

  /**
   * Get stored authentication token
   * @returns stored token or null if not found
   */
  getAuthToken(): string | null {
    return localStorage.getItem('authToken');
  }

  /**
   * Store user role in localStorage
   * @param role - User role (Admin, Customer, LoanOfficer)
   */
  setUserRole(role: string): void {
    localStorage.setItem('userRole', role);
  }

  /**
   * Store user ID in localStorage
   * @param userId - User ID
   */
  setUserId(userId: string): void {
    localStorage.setItem('userId', userId);
  }

  /**
   * Get user ID from storage
   * @returns user ID or null
   */
  getUserId(): string | null {
    return localStorage.getItem('userId');
  }

  /**
   * Logout user by removing all stored data
   */
  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userRole');
    localStorage.removeItem('userId');
  }
}

