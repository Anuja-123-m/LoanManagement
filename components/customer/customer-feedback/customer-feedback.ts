import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomerService } from '../../../services/customer-service';
import { AuthService } from '../../../services/auth-service';
import { IFeedbackQuestion, IAddFeedback } from '../../../models/feedback/IFeedback';

@Component({
  selector: 'app-customer-feedback',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './customer-feedback.html',
  styleUrl: './customer-feedback.css',
})
export class CustomerFeedback implements OnInit {
  feedbackQuestions: IFeedbackQuestion[] = [];
  isLoading = false;
  errorMessage = '';
  successMessage = '';
  customerId: number | null = null;

  feedback: IAddFeedback = {
    customerId: 0,
    questionId: undefined,
    feedbackText: '',
    rating: undefined,
  };

  constructor(
    private customerService: CustomerService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadFeedbackQuestions();
    const storedCustomerId = localStorage.getItem('customerId');
    if (storedCustomerId) {
      this.customerId = parseInt(storedCustomerId, 10);
      this.feedback.customerId = this.customerId;
    } else {
      this.customerId = 1; // Default for testing
      this.feedback.customerId = 1;
    }
  }

  loadFeedbackQuestions(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.customerService.getActiveFeedbackQuestions().subscribe({
      next: (data: IFeedbackQuestion[]) => {
        this.feedbackQuestions = data;
        this.isLoading = false;
      },
      error: (err: any) => {
        this.isLoading = false;
        this.errorMessage = err.error?.message || 'Error loading feedback questions. Please try again.';
        console.error('Error fetching feedback questions', err);
      },
    });
  }

  submitFeedback(feedbackForm: any): void {
    if (feedbackForm.valid) {
      if (!this.customerId || this.customerId === 0) {
        this.errorMessage = 'Customer ID not found. Please login again.';
        return;
      }

      this.feedback.customerId = this.customerId;
      this.isLoading = true;
      this.errorMessage = '';
      this.successMessage = '';

      this.customerService.addFeedback(this.feedback).subscribe({
        next: (response: any) => {
          this.isLoading = false;
          this.successMessage = 'Feedback submitted successfully! Thank you!';
          this.feedback = {
            customerId: this.customerId || 0,
            questionId: undefined,
            feedbackText: '',
            rating: undefined,
          };
          feedbackForm.resetForm();
          setTimeout(() => {
            this.successMessage = '';
          }, 3000);
        },
        error: (err: any) => {
          this.isLoading = false;
          this.errorMessage = err.error?.message || 'Error submitting feedback. Please try again.';
          console.error('Error submitting feedback', err);
        },
      });
    } else {
      this.errorMessage = 'Please fill in the required fields correctly.';
    }
  }

  onReset(feedbackForm: any): void {
    feedbackForm.resetForm();
    this.feedback = {
      customerId: this.customerId || 0,
      questionId: undefined,
      feedbackText: '',
      rating: undefined,
    };
    this.errorMessage = '';
    this.successMessage = '';
  }
}
