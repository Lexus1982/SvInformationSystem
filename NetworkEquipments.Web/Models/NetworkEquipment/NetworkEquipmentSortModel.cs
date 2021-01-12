using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkEquipments.Web.Models.NetworkEquipment
{
    public class NetworkEquipmentSortModel
    {
        public NetworkEquipmentSortState TownNameSort { get; private set; }
        public NetworkEquipmentSortState StreetNameSort { get; private set; }
        public NetworkEquipmentSortState ComplexHouseSort { get; private set; }
        public NetworkEquipmentSortState EntranceSort { get; private set; }
        public NetworkEquipmentSortState EquipmentTypeNameSort { get; private set; }
        public NetworkEquipmentSortState IpSort { get; private set; }
        public NetworkEquipmentSortState CommentarySort { get; private set; }
        public NetworkEquipmentSortState Current { get; private set; }

        public NetworkEquipmentSortModel(NetworkEquipmentSortState sortOrder)
        {

            TownNameSort = sortOrder == NetworkEquipmentSortState.TownNameAsc ? NetworkEquipmentSortState.TownNameDesc : NetworkEquipmentSortState.TownNameAsc;
            StreetNameSort = sortOrder == NetworkEquipmentSortState.StreetNameAsc ? NetworkEquipmentSortState.StreetNameDesc : NetworkEquipmentSortState.StreetNameAsc;
            ComplexHouseSort = sortOrder == NetworkEquipmentSortState.ComplexHouseAsc ? NetworkEquipmentSortState.ComplexHouseDesc : NetworkEquipmentSortState.ComplexHouseAsc;
            EntranceSort = sortOrder == NetworkEquipmentSortState.EntranceAsc ? NetworkEquipmentSortState.EntranceDesc : NetworkEquipmentSortState.EntranceAsc;
            EquipmentTypeNameSort = sortOrder == NetworkEquipmentSortState.EquipmentTypeNameAsc ? NetworkEquipmentSortState.EquipmentTypeNameDesc : NetworkEquipmentSortState.EquipmentTypeNameAsc;
            IpSort = sortOrder == NetworkEquipmentSortState.IpAsc ? NetworkEquipmentSortState.IpDesc : NetworkEquipmentSortState.IpAsc;
            CommentarySort = sortOrder == NetworkEquipmentSortState.CommentaryAsc ? NetworkEquipmentSortState.CommentaryDesc : NetworkEquipmentSortState.CommentaryAsc;

            Current = sortOrder;
        }
    }

    public enum NetworkEquipmentSortState
    {
        TownNameAsc,
        TownNameDesc,
        StreetNameAsc,
        StreetNameDesc,
        ComplexHouseAsc,
        ComplexHouseDesc,
        EntranceAsc,
        EntranceDesc,
        EquipmentTypeNameAsc,
        EquipmentTypeNameDesc,
        IpAsc,
        IpDesc,
        CommentaryAsc,
        CommentaryDesc
    }
}