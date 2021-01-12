using System;
using System.Data;
using NetworkEquipments.Domain.Repositories.Reports;

namespace NetworkEquipments.Services.Interfaces
{
    public interface IReportService : IDisposable
    {
        DataTable GetReportData(ReportType type);
    }
}
