use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_SearchNetworks')
		and type='P')
	drop procedure dbo.spNE_SearchNetworks
go

create proc spNE_SearchNetworks (
																				@SEARCH_VALUE     varchar(250)
)
as
begin
		/*select  T.TOWN_PREFIX,T.TOWN_NAME,S.STREET_PREFIX,S.STREET_NAME,A.HOUSE,A.HOUSE_POSTFIX
            from    INTEGRAL..ADDRESS A join
                    INTEGRAL..STREETS S on S.STREET_CODE = A.STREET_CODE join
                    INTEGRAL..TOWNS T on T.TOWN_CODE = S.TOWN_CODE
            where   T.TOWN_PREFIX + ' ' + T.TOWN_NAME + ' ' + S.STREET_PREFIX + ' ' + S.STREET_NAME + ' ' +
                    cast(A.HOUSE as varchar(50)) + ' ' + A.HOUSE_POSTFIX like '%' + rtrim(@SEARCH_VALUE) + '%'
*/

    select  N.NETWORK_CODE as Id, N.ADDRESS_CODE as AddressId, N.SEGMENT_NUM as SegmentNumber, rtrim(N.VLAN_MANAGE) as VlanManage,
            rtrim(N.VLAN_INET) as VlanInternet, rtrim(N.IP_INTERVAL) as IpInterval, isnull(E.EQUIPMENTS_COUNT, 0) as  EquipmentsCount,
            rtrim(N.COMMENTARY) as Commentary, N.USER_CODE as UserId, N.DATE_CHANGE as ChangeDate
    from    MEDIATE..NE_NETWORKS N left join
          (
            select    NETWORK_CODE, count(EQUIPMENT_CODE) as EQUIPMENTS_COUNT
            from      MEDIATE..NE_NETWORK_EQUIPMENTS
            group by NETWORK_CODE
        ) E on E.NETWORK_CODE = N.NETWORK_CODE
		where 	N.ADDRESS_CODE in (
            select  ADDRESS_CODE
            from    INTEGRAL..ADDRESS A join
                    INTEGRAL..STREETS S on S.STREET_CODE = A.STREET_CODE join
                    INTEGRAL..TOWNS T on T.TOWN_CODE = S.TOWN_CODE
            where   upper(T.TOWN_PREFIX + ' ' + T.TOWN_NAME + ' ' + S.STREET_PREFIX + ' ' + S.STREET_NAME + ' ' +
                    cast(A.HOUSE as varchar(50)) + ' ' + A.HOUSE_POSTFIX) like '%' + upper(rtrim(@SEARCH_VALUE)) + '%'
          ) or  upper(cast(N.SEGMENT_NUM as varchar(50)) + ' ' +  N.VLAN_MANAGE + ' ' + N.VLAN_INET + ' ' + N.IP_INTERVAL) 
                like '%' + upper(rtrim(@SEARCH_VALUE)) + '%'


end


/*

  exec MEDIATE..spNE_SearchNetworks '0'

*/