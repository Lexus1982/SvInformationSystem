use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetNetworks')
		and type='P')
	drop procedure dbo.spNE_GetNetworks
go

create proc spNE_GetNetworks
as
begin
    select  N.NETWORK_CODE as Id, N.ADDRESS_CODE as AddressId, N.SEGMENT_NUM as SegmentNumber, rtrim(N.VLAN_MANAGE) as VlanManage,
            rtrim(N.VLAN_INET) as VlanInternet, rtrim(N.IP_INTERVAL) as IpInterval,
            (
              select    count(NETWORK_CODE)
              from      MEDIATE..NE_NETWORK_EQUIPMENTS
              where     NETWORK_CODE = N.NETWORK_CODE
            ) as  EquipmentsCount,
            rtrim(N.COMMENTARY) as Commentary, N.USER_CODE as UserId, N.DATE_CHANGE as ChangeDate
    from    MEDIATE..NE_NETWORKS N
end


/*

  exec MEDIATE..spNE_GetNetworks

*/