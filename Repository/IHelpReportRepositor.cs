using LoanManagementAPI.Models;

namespace LoanManagementAPI.Repository.HelpReportRepo
{
    public interface IHelpReportRepository
    {
        List<HelpReport> GetHelpReports();
        HelpReport? GetHelpReportById(int id);
        void AddHelpReport(HelpReport helpReport);
        bool UpdateHelpReport(int id, HelpReport helpReport);
        bool DeleteHelpReport(int id);
    }
}

