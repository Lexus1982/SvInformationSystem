use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetNetworksByIpInterval')
		and type='P')
	drop procedure dbo.spNE_GetNetworksByIpInterval
go

create proc spNE_GetNetworksByIpInterval (
																				@IP_INTERVAL     varchar(250)
)
as
begin
		--if @IP_INTERVAL = null return -1

    select  N.NETWORK_CODE as Id, N.ADDRESS_CODE as AddressId, N.SEGMENT_NUM as SegmentNumber, rtrim(N.VLAN_MANAGE) as VlanManage,
            rtrim(N.VLAN_INET) as VlanInternet, rtrim(N.IP_INTERVAL) as IpInterval, isnull(E.EQUIPMENTS_COUNT, 0) as  EquipmentsCount,
            rtrim(N.COMMENTARY) as Commentary, N.USER_CODE as UserId, N.DATE_CHANGE as ChangeDate
    from    MEDIATE..NE_NETWORKS N left join
          (
            select    NETWORK_CODE, count(NETWORK_CODE) as EQUIPMENTS_COUNT
            from      MEDIATE..NE_NETWORK_EQUIPMENTS
            group by NETWORK_CODE
        ) E on E.NETWORK_CODE = N.NETWORK_CODE
		where		rtrim(IP_INTERVAL) = rtrim(@IP_INTERVAL)
end


/*

  exec MEDIATE..spNE_GetNetworksByIpInterval '192.168.33.0'

  insert into MEDIATE..NE_NETWORKS (ADDRESS_CODE, SEGMENT_NUM, VLAN_MANAGE, VLAN_INET, IP_INTERVAL, USER_CODE, DATE_CHANGE)
		VALUES (5052413, 1, '111','222','IP',3,getdate())

*/