using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkEquipments.Web.Models.NetworkEquipment
{
    public class NetworkEquipmentFilterModel
    {
        public NetworkEquipmentFilterModel(string value)
        {
            SearchValue = value;
        }

        public string SearchValue { get; set; }
    }
}