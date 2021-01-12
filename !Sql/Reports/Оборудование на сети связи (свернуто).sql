-- вар. 1
select  rtrim(T.TYPE_NAME) as TypeName, isnull(EquipmentCount, 0) as EquipmentCount
from    MEDIATE..NE_EQUIPMENT_TYPES T left join
        (
          select EQUIPMENT_TYPE_CODE, count(*) as EquipmentCount
          from  MEDIATE..NE_NETWORK_EQUIPMENTS E
          group by EQUIPMENT_TYPE_CODE
        ) E on T.TYPE_CODE = E.EQUIPMENT_TYPE_CODE

-- вар. 2
select  rtrim(T.TYPE_NAME) as TypeName, count(*) as EquipmentCount
from    MEDIATE..NE_NETWORK_EQUIPMENTS E join
        MEDIATE..NE_EQUIPMENT_TYPES T on T.TYPE_CODE = E.EQUIPMENT_TYPE_CODE
group by T.TYPE_NAME