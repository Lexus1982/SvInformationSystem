using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkEquipments.Web.Models.Network
{
    public class NetworkFilterModel
    {
        public NetworkFilterModel(string value)
        {
            SearchValue = value;
        }

        public string SearchValue { get; set; }
    }
}