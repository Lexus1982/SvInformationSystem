using System;
using System.Data;
using NetworkEquipments.Domain.ADO;
using NetworkEquipments.Domain.Repositories.Reports.NetworkEquipments;

namespace NetworkEquipments.Domain.Repositories.Reports
{
    public abstract class UserReports
    {
        public static UserReports Create(AdoContext context, ReportType type)
        {
            switch (type)
            {
                case ReportType.NE_EquipmentsForTypes:
                    return new EquipmentsForTypes(context);
                case ReportType.NE_EquipmentsForAddress:
                    return new EquipmentsForAddress(context);
                case ReportType.NE_EquipmentsForSegments:
                    return new EquipmentsForSegments(context);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public abstract DataTable GetReportData();

        protected DataTable ToTable(IDbCommand command)
        {
            command.CommandType = CommandType.Text;
            using (var reader = command.ExecuteReader())
            {
                var dataTable = new DataTable();
                dataTable.Load(reader);
                SetColumnCaptions(dataTable);
                return dataTable;
            }
        }

        protected abstract void SetColumnCaptions(DataTable table);
    }

    public enum ReportType
    {
        NE_EquipmentsForTypes = 4,
        NE_EquipmentsForAddress = 5,
        NE_EquipmentsForSegments = 6
    }
}
