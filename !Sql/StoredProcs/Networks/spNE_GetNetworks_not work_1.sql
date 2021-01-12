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
            rtrim(N.VLAN_INET) as VlanInternet, rtrim(N.IP_INTERVAL) as IpInterval,isnull(E.EQUIPMENTS_COUNT, 0) as  EquipmentsCount,
            rtrim(N.COMMENTARY) as Commentary, N.USER_CODE as UserId, N.DATE_CHANGE as ChangeDate
    from    MEDIATE..NE_NETWORKS N left join
          (
            select    NETWORK_CODE, count(NETWORK_CODE) as EQUIPMENTS_COUNT
            from      MEDIATE..NE_NETWORK_EQUIPMENTS
            group by NETWORK_CODE
          ) as E on E.NETWORK_CODE = N.NETWORK_CODE
end


/*

  exec MEDIATE..spNE_GetNetworks

  insert into MEDIATE..NE_NETWORKS (ADDRESS_CODE, SEGMENT_NUM, VLAN_MANAGE, VLAN_INET, IP_INTERVAL, USER_CODE, DATE_CHANGE)
		VALUES (5052413, 1, '111','222','IP',3,getdate())

*/