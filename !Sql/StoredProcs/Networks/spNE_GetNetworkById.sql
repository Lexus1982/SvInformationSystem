use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetNetworkById')
		and type='P')
	drop procedure dbo.spNE_GetNetworkById
go

create proc spNE_GetNetworkById (
                                  @NETWORK_CODE     numeric(10,0)
)
as
begin
    select  N.NETWORK_CODE as Id, N.ADDRESS_CODE as AddressId, N.SEGMENT_NUM as SegmentNumber, rtrim(N.VLAN_MANAGE) as VlanManage,
            rtrim(N.VLAN_INET) as VlanInternet, rtrim(N.IP_INTERVAL) as IpInterval,isnull(E.EQUIPMENTS_COUNT, 0) as  EquipmentsCount,
            rtrim(N.COMMENTARY) as Commentary, N.USER_CODE as UserId, N.DATE_CHANGE as ChangeDate
    from    MEDIATE..NE_NETWORKS N left join
            (
              select    NETWORK_CODE, count(NETWORK_CODE) as EQUIPMENTS_COUNT
              from      MEDIATE..NE_NETWORK_EQUIPMENTS
              where     NETWORK_CODE = @NETWORK_CODE
              group by  NETWORK_CODE
            ) as E on E.NETWORK_CODE = N.NETWORK_CODE
    where   N.NETWORK_CODE = @NETWORK_CODE
end


/*

  exec MEDIATE..spNE_GetNetworkById 1

*/