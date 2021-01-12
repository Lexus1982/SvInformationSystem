namespace NetworkEquipments.Web.Models.EquipmentType
{
    public class EquipmentTypeFilterModel
    {
        public EquipmentTypeFilterModel(string value)
        {
            SearchValue = value;
        }

        public string SearchValue { get; set; }
    }
}