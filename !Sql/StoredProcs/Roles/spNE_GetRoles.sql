use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetRoles')
		and type='P')
	drop procedure dbo.spNE_GetRoles
go

create proc spNE_GetRoles
as
begin
    select  PROP_CODE as Id, rtrim(PROP_NAME) as Name
    from    INTEGRAL..PROPERTIES
    where   PROP_CODE in (1050, 1051)
      /*
        1050	Network Equipments. Администратор
        1051	Network Equipments. Пользователь
      */
end

/*
  exec MEDIATE..spNE_GetRoles
*/