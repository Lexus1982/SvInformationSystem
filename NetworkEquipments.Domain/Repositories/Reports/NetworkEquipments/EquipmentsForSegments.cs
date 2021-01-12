using System.Data;
using NetworkEquipments.Domain.ADO;

namespace NetworkEquipments.Domain.Repositories.Reports.NetworkEquipments
{
    public class EquipmentsForSegments : UserReports
    {
        private readonly AdoContext _context;

        public EquipmentsForSegments(AdoContext context)
        {
            _context = context;
        }

        public override DataTable GetReportData()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"    select  N.SEGMENT_NUM as SegmentNumber, E.TypeName, sum(isnull(E.EquipmentCount, 0)) as EquipmentCount
                                            from    MEDIATE..NE_NETWORKS N left join
                                                    (
                                                        select  E.NETWORK_CODE, rtrim(T.TYPE_NAME) as TypeName, count(E.EQUIPMENT_CODE) as EquipmentCount
                                                        from    MEDIATE..NE_NETWORK_EQUIPMENTS E join
                                                                MEDIATE..NE_EQUIPMENT_TYPES T on T.TYPE_CODE = E.EQUIPMENT_TYPE_CODE
                                                        group by E.NETWORK_CODE, T.TYPE_NAME
                                                    ) E on N.NETWORK_CODE = E.NETWORK_CODE
                                            group by N.SEGMENT_NUM, E.TypeName
                                            order by 1,2";
                return ToTable(command);
            }
        }

        protected override void SetColumnCaptions(DataTable table)
        {
            table.Columns["SegmentNumber"].Caption = "№ сегмента сети";
            table.Columns["TypeName"].Caption = "Тип оборудования";
            table.Columns["EquipmentCount"].Caption = "Кол-во оборудования";
        }
    }
}
