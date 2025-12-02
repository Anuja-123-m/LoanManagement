import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { Notfound } from './components/notfound/notfound';
import { authGuard } from './guards/auth.guard';
import { roleGuard } from './guards/role.guard';

// Admin Components
import { AdminDashboard } from './components/admin/admin-dashboard/admin-dashboard';
import { CustomerManagement } from './components/admin/customer-management/customer-management';
import { LoanOfficerManagement } from './components/admin/loanofficer-management/loanofficer-management';
import { BackgroundVerificationManagement } from './components/admin/backgroundverification-management/backgroundverification-management';
import { LoanVerificationManagement } from './components/admin/loanverification-management/loanverification-management';
import { LoanRequestManagement } from './components/admin/loanrequest-management/loanrequest-management';
import { HelpReportManagement } from './components/admin/helpreport-management/helpreport-management';
import { FeedbackQuestionManagement } from './components/admin/feedbackquestion-management/feedbackquestion-management';
import { CustomerFeedbackView } from './components/admin/customerfeedback-view/customerfeedback-view';

// Customer Components
import { CustomerDashboard } from './components/customer/customer-dashboard/customer-dashboard';
import { ApplyLoan } from './components/customer/apply-loan/apply-loan';
import { ViewLoanRequests } from './components/customer/view-loan-requests/view-loan-requests';
import { CustomerHelpSection } from './components/customer/customer-help-section/customer-help-section';
import { CustomerFeedback } from './components/customer/customer-feedback/customer-feedback';

// Loan Officer Components
import { LoanOfficerDashboard } from './components/loanofficer/loanofficer-dashboard/loanofficer-dashboard';
import { AssignedLoanRequests } from './components/loanofficer/assigned-loan-requests/assigned-loan-requests';
import { LoanVerificationUpdate } from './components/loanofficer/loan-verification-update/loan-verification-update';
import { OfficerHelpReports } from './components/loanofficer/officer-help-reports/officer-help-reports';

export const routes: Routes = [
  // Public Routes
  { path: '', component: Home },
  { path: 'login', component: Login },
  { path: 'register', component: Register },

  // Admin Routes
  {
    path: 'admin',
    component: AdminDashboard,
    canActivate: [authGuard, roleGuard(['Admin'])],
  },
  {
    path: 'admin/customers',
    component: CustomerManagement,
    canActivate: [authGuard, roleGuard(['Admin'])],
  },
  {
    path: 'admin/loanofficers',
    component: LoanOfficerManagement,
    canActivate: [authGuard, roleGuard(['Admin'])],
  },
  {
    path: 'admin/backgroundverifications',
    component: BackgroundVerificationManagement,
    canActivate: [authGuard, roleGuard(['Admin'])],
  },
  {
    path: 'admin/loanverifications',
    component: LoanVerificationManagement,
    canActivate: [authGuard, roleGuard(['Admin'])],
  },
  {
    path: 'admin/loanrequests',
    component: LoanRequestManagement,
    canActivate: [authGuard, roleGuard(['Admin'])],
  },
  {
    path: 'admin/helpreports',
    component: HelpReportManagement,
    canActivate: [authGuard, roleGuard(['Admin'])],
  },
  {
    path: 'admin/feedbackquestions',
    component: FeedbackQuestionManagement,
    canActivate: [authGuard, roleGuard(['Admin'])],
  },
  {
    path: 'admin/customerfeedbacks',
    component: CustomerFeedbackView,
    canActivate: [authGuard, roleGuard(['Admin'])],
  },

  // Customer Routes
  {
    path: 'customer',
    component: CustomerDashboard,
    canActivate: [authGuard, roleGuard(['Customer'])],
  },
  {
    path: 'customer/apply-loan',
    component: ApplyLoan,
    canActivate: [authGuard, roleGuard(['Customer'])],
  },
  {
    path: 'customer/loanrequests',
    component: ViewLoanRequests,
    canActivate: [authGuard, roleGuard(['Customer'])],
  },
  {
    path: 'customer/help',
    component: CustomerHelpSection,
    canActivate: [authGuard, roleGuard(['Customer'])],
  },
  {
    path: 'customer/feedback',
    component: CustomerFeedback,
    canActivate: [authGuard, roleGuard(['Customer'])],
  },

  // Loan Officer Routes
  {
    path: 'loanofficer',
    component: LoanOfficerDashboard,
    canActivate: [authGuard, roleGuard(['LoanOfficer'])],
  },
  {
    path: 'loanofficer/loanrequests',
    component: AssignedLoanRequests,
    canActivate: [authGuard, roleGuard(['LoanOfficer'])],
  },
  {
    path: 'loanofficer/verifications',
    component: LoanVerificationUpdate,
    canActivate: [authGuard, roleGuard(['LoanOfficer'])],
  },
  {
    path: 'loanofficer/helpreports',
    component: OfficerHelpReports,
    canActivate: [authGuard, roleGuard(['LoanOfficer'])],
  },

  // Not Found
  { path: '**', component: Notfound },
];

