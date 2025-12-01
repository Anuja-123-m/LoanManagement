using Microsoft.EntityFrameworkCore;
using LoanManagementAPI.Database;
using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.HelpReportRepo
{
    public class HelpReportRepository : IHelpReportRepository
    {
        LoanDbContext loanDbContext;
        public HelpReportRepository(LoanDbContext loanDbContext)
        {
            this.loanDbContext = loanDbContext;
        }

        public void AddHelpReport(HelpReport helpReport)
        {
            try
            {
                loanDbContext.HelpReports.Add(helpReport);
                loanDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error adding help report to database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool DeleteHelpReport(int id)
        {
            try
            {
                var helpReport = loanDbContext.HelpReports.FirstOrDefault(h => h.HelpReportId == id);
                if (helpReport != null)
                {
                    loanDbContext.HelpReports.Remove(helpReport);
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error deleting help report from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public List<HelpReport> GetHelpReports()
        {
            try
            {
                return loanDbContext.HelpReports.Include(h => h.CreatedByUser).ToList();
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving help reports from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public HelpReport? GetHelpReportById(int id)
        {
            try
            {
                return loanDbContext.HelpReports.Include(h => h.CreatedByUser).FirstOrDefault(h => h.HelpReportId == id);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error retrieving help report from database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }

        public bool UpdateHelpReport(int id, HelpReport helpReport)
        {
            try
            {
                var existingHelpReport = loanDbContext.HelpReports.FirstOrDefault(h => h.HelpReportId == id);
                if (existingHelpReport != null)
                {
                    existingHelpReport.Title = helpReport.Title;
                    existingHelpReport.Content = helpReport.Content;
                    existingHelpReport.Category = helpReport.Category;
                    existingHelpReport.UpdatedDate = DateTime.Now;
                    loanDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error updating help report in database: " + ex.Message;
                throw new Exception(errorMessage);
            }
        }
    }
}

