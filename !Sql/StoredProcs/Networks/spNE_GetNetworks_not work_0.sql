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
  set forceplan on

  create table #t1
  (
    NETWORK_CODE numeric(10,0) not null,
    EQUIPMENTS_COUNT int not null,
    constraint pk_t1v primary key (NETWORK_CODE)
  )

  insert  #t1(NETWORK_CODE, EQUIPMENTS_COUNT)
  select  NETWORK_CODE, count(EQUIPMENT_CODE)
  from    MEDIATE..NE_NETWORK_EQUIPMENTS
  group by NETWORK_CODE


  select  N.NETWORK_CODE as Id, N.ADDRESS_CODE as AddressId, N.SEGMENT_NUM as SegmentNumber, rtrim(N.VLAN_MANAGE) as VlanManage,
          rtrim(N.VLAN_INET) as VlanInternet, rtrim(N.IP_INTERVAL) as IpInterval, isnull(E.EQUIPMENTS_COUNT, 0) as EquipmentsCount,
          rtrim(N.COMMENTARY) as Commentary, N.USER_CODE as UserId, N.DATE_CHANGE as ChangeDate
  from    MEDIATE..NE_NETWORKS N left join
          #t1 E on E.NETWORK_CODE = N.NETWORK_CODE
end


/*

  exec MEDIATE..spNE_GetNetworks

*/