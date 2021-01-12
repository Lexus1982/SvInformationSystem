use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_SearchEquipmentTypes')
		and type='P')
	drop procedure dbo.spNE_SearchEquipmentTypes
go

create proc spNE_SearchEquipmentTypes (
																				@SEARCH_VALUE     varchar(250)
)
as
begin
			select 	TYPE_CODE as Id, rtrim(TYPE_NAME) as Name, POSITION as Position, USER_CODE as UserId, DATE_CHANGE as ChangeDate
			from		MEDIATE..NE_EQUIPMENT_TYPES
      where   upper(TYPE_NAME) like '%' + upper(@SEARCH_VALUE) + '%'
end

/*

  exec MEDIATE..spNE_SearchEquipmentTypes '4'

*/