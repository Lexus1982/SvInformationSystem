using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkEquipments.Web.Models.Network
{
    public class NetworkViewModel
    {
        public IEnumerable<NetworkModel> Networks { get; set; }
        public NetworkSortModel Sort { get; set; }
        public NetworkFilterModel Filter { get; set; }
    }
}