using System.Data;
using NetworkEquipments.Domain.ADO;

namespace NetworkEquipments.Domain.Repositories.Reports.NetworkEquipments
{
    public class EquipmentsForTypes : UserReports
    {
        private readonly AdoContext _context;

        public EquipmentsForTypes(AdoContext context)
        {
            _context = context;
        }

        public override DataTable GetReportData()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"select  rtrim(T.TYPE_NAME) as TypeName, isnull(EquipmentCount, 0) as EquipmentCount
                                        from    MEDIATE..NE_EQUIPMENT_TYPES T left join
                                                (
                                                    select EQUIPMENT_TYPE_CODE, count(*) as EquipmentCount
                                                    from  MEDIATE..NE_NETWORK_EQUIPMENTS E
                                                    group by EQUIPMENT_TYPE_CODE
                                                ) E on T.TYPE_CODE = E.EQUIPMENT_TYPE_CODE";

                return ToTable(command);
            }
        }

        protected override void SetColumnCaptions(DataTable table)
        {
            table.Columns["TypeName"].Caption = "Тип оборудования";
            table.Columns["EquipmentCount"].Caption = "Кол-во оборудования";
        }
    }
}
