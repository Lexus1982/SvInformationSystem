using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkEquipments.Web.Models.Network
{
    public class NetworkSortModel
    {
        public NetworkSortState TownNameSort { get; private set; }
        public NetworkSortState StreetNameSort { get; private set; }
        public NetworkSortState ComplexHouseSort { get; private set; }
        public NetworkSortState SegmentNumberSort { get; private set; }
        public NetworkSortState VlanManageSort { get; private set; }
        public NetworkSortState VlanInternetSort { get; private set; }
        public NetworkSortState IpIntervalSort { get; private set; }
        public NetworkSortState EquipmentsCountSort { get; private set; }
        public NetworkSortState CommentarySort { get; private set; }
        
        public NetworkSortState Current { get; private set; }

        public NetworkSortModel(NetworkSortState sortOrder)
        {

            TownNameSort = sortOrder == NetworkSortState.TownNameAsc ? NetworkSortState.TownNameDesc : NetworkSortState.TownNameAsc;
            StreetNameSort = sortOrder == NetworkSortState.StreetNameAsc ? NetworkSortState.StreetNameDesc : NetworkSortState.StreetNameAsc;
            ComplexHouseSort = sortOrder == NetworkSortState.ComplexHouseAsc ? NetworkSortState.ComplexHouseDesc : NetworkSortState.ComplexHouseAsc;
            SegmentNumberSort = sortOrder == NetworkSortState.SegmentNumberAsc ? NetworkSortState.SegmentNumberDesc : NetworkSortState.SegmentNumberAsc;
            VlanManageSort = sortOrder == NetworkSortState.VlanManageAsc ? NetworkSortState.VlanManageDesc : NetworkSortState.VlanManageAsc;
            VlanInternetSort = sortOrder == NetworkSortState.VlanInternetAsc ? NetworkSortState.VlanInternetDesc : NetworkSortState.VlanInternetAsc;
            IpIntervalSort = sortOrder == NetworkSortState.IpIntervalAsc ? NetworkSortState.IpIntervalDesc : NetworkSortState.IpIntervalAsc;
            EquipmentsCountSort = sortOrder == NetworkSortState.EquipmentsCountAsc ? NetworkSortState.EquipmentsCountDesc : NetworkSortState.EquipmentsCountAsc;
            CommentarySort = sortOrder == NetworkSortState.CommentaryAsc ? NetworkSortState.CommentaryDesc : NetworkSortState.CommentaryAsc;

            Current = sortOrder;
        }
    }

    public enum NetworkSortState
    {
        TownNameAsc,
        TownNameDesc,
        StreetNameAsc,
        StreetNameDesc,
        ComplexHouseAsc,
        ComplexHouseDesc,
        SegmentNumberAsc,
        SegmentNumberDesc,
        VlanManageAsc,
        VlanManageDesc,
        VlanInternetAsc,
        VlanInternetDesc,
        IpIntervalAsc,
        IpIntervalDesc,
        EquipmentsCountAsc,
        EquipmentsCountDesc,
        CommentaryAsc,
        CommentaryDesc
    }
}