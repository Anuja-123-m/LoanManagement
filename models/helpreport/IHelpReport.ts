import { IUser } from '../user/IUser';

export interface IHelpReport {
  helpReportId: number;
  userId: number;
  user?: IUser;
  title: string;
  description: string;
  category: string;
  status: string;
  createdDate: Date;
  updatedDate?: Date;
  resolution?: string;
}

export interface IAddHelpReport {
  userId: number;
  title: string;
  description: string;
  category: string;
}

export interface IUpdateHelpReport {
  title: string;
  description: string;
  status: string;
  resolution?: string;
}

