using System.Data;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Interfaces;
using NetworkEquipments.Domain.Repositories.Reports;
using NetworkEquipments.Services.Interfaces;

namespace NetworkEquipments.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly AdoContext _context;

        public ReportService(IAdoContext context)
        {
            _context = context as AdoContext;
        }

        public DataTable GetReportData(ReportType type)
        {
            return UserReports.Create(_context, type).GetReportData();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
