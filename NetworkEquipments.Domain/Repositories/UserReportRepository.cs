using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Entities.Reports;
using NetworkEquipments.Domain.Extensions;
using NetworkEquipments.Domain.Repository;

namespace NetworkEquipments.Domain.Repositories
{
    public class UserReportRepository
    {
        private readonly AdoContext _context;

        public UserReportRepository(AdoContext context)
        {
            _context = context;
        }

        public DataTable CreateReport(ReportType type)
        {
            return UserReports.Create(_context, type).GetReportData();
        }
    }
}
