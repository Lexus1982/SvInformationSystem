select  A.TownName, A.StreetName, A.House + case when A.Corp != null then ' ' + A.Corp end as ComplexHouse, E.TypeName, isnull(E.EquipmentCount, 0) as EquipmentCount
   from    MEDIATE..NE_NETWORKS N join
          (
            select  A.ADDRESS_CODE,
                    T.TOWN_CODE as Id, rtrim(T.TOWN_PREFIX) + ' ' + rtrim(T.TOWN_NAME) as TownName,
                    S.STREET_CODE as StreetId, rtrim(S.STREET_PREFIX) + case when S.STREET_PREFIX != null then ' ' + rtrim(S.STREET_NAME) else rtrim(S.STREET_NAME) end as StreetName,
                    cast(A.HOUSE  as varchar(5)) + case when len(A.HOUSE_POSTFIX) > 0 and MEDIATE.dbo.IsDigitString(substring(A.HOUSE_POSTFIX, 1, 1)) != 1 then rtrim(A.HOUSE_POSTFIX) end as House,
                    case when len(A.HOUSE_POSTFIX) > 0 and MEDIATE.dbo.IsDigitString(substring(A.HOUSE_POSTFIX, 1, 1)) = 1 then 'корп. ' + rtrim(A.HOUSE_POSTFIX) end as Corp
            from    INTEGRAL..ADDRESS A join
                    INTEGRAL..STREETS S on A.STREET_CODE = S.STREET_CODE join
                    INTEGRAL..TOWNS T on S.TOWN_CODE = T.TOWN_CODE
          ) A on N.ADDRESS_CODE = A.ADDRESS_CODE left join
          (
            select  E.NETWORK_CODE, rtrim(T.TYPE_NAME) as TypeName, count(E.EQUIPMENT_CODE) as EquipmentCount
            from  MEDIATE..NE_NETWORK_EQUIPMENTS E join
                  MEDIATE..NE_EQUIPMENT_TYPES T on T.TYPE_CODE = E.EQUIPMENT_TYPE_CODE
            group by E.NETWORK_CODE, T.TYPE_NAME
          ) E on N.NETWORK_CODE = E.NETWORK_CODE
    order by 1,2,3,4,5


/*
select  N.ADDRESS_CODE as AddressId, rtrim(T.TYPE_NAME) as TypeName, count(*) as EquipmentCount
from    MEDIATE..NE_NETWORKS N join
        MEDIATE..NE_NETWORK_EQUIPMENTS E on E.NETWORK_CODE = N.NETWORK_CODE join
        MEDIATE..NE_EQUIPMENT_TYPES T on T.TYPE_CODE = E.EQUIPMENT_TYPE_CODE
group by N.ADDRESS_CODE, T.TYPE_NAME
*/