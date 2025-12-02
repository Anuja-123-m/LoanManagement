export interface IUser {
  userId: number;
  email: string;
  role: string;
  fullName?: string;
  phoneNumber?: string;
  createdDate: Date;
  isActive: boolean;
}

