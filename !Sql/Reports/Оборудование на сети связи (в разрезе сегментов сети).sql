select  N.SEGMENT_NUM as SegmentNumber, E.TypeName, sum(isnull(E.EquipmentCount, 0)) as EquipmentCount
   from    MEDIATE..NE_NETWORKS N left join
          (
            select  E.NETWORK_CODE, rtrim(T.TYPE_NAME) as TypeName, count(E.EQUIPMENT_CODE) as EquipmentCount
            from  MEDIATE..NE_NETWORK_EQUIPMENTS E join
                  MEDIATE..NE_EQUIPMENT_TYPES T on T.TYPE_CODE = E.EQUIPMENT_TYPE_CODE
            group by E.NETWORK_CODE, T.TYPE_NAME
          ) E on N.NETWORK_CODE = E.NETWORK_CODE
    group by N.SEGMENT_NUM, E.TypeName
    order by 1,2


/*
select  N.SEGMENT_NUM as SegmentNumber, rtrim(T.TYPE_NAME) as TypeName, count(*) as EquipmentCount
from    MEDIATE..NE_NETWORKS N join
        MEDIATE..NE_NETWORK_EQUIPMENTS E on E.NETWORK_CODE = N.NETWORK_CODE join
        MEDIATE..NE_EQUIPMENT_TYPES T on T.TYPE_CODE = E.EQUIPMENT_TYPE_CODE
group by N.SEGMENT_NUM, T.TYPE_NAME
*/