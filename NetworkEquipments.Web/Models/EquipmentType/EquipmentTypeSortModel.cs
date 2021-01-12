namespace NetworkEquipments.Web.Models.EquipmentType
{
    public class EquipmentTypeSortModel
    {
        public EquipmentTypeSortState NameSort { get; private set; }
        public EquipmentTypeSortState PositionSort { get; private set; }
        public EquipmentTypeSortState Current { get; private set; }

        public EquipmentTypeSortModel(EquipmentTypeSortState sortOrder)
        {
            NameSort = sortOrder == EquipmentTypeSortState.NameAsc ? EquipmentTypeSortState.NameDesc : EquipmentTypeSortState.NameAsc;
            PositionSort = sortOrder == EquipmentTypeSortState.PositionAsc ? EquipmentTypeSortState.PositionDesc : EquipmentTypeSortState.PositionAsc;
            Current = sortOrder;
        }
    }

    public enum EquipmentTypeSortState
    {
        NameAsc,
        NameDesc,
        PositionAsc,
        PositionDesc
    }
}
