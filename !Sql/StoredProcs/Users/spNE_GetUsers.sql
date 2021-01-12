use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetUsers')
		and type='P')
	drop procedure dbo.spNE_GetUsers
go

create proc spNE_GetUsers
as
begin
   select USER_CODE as Id, rtrim(USER_NAME) as Login, rtrim(USER_PASSWORD) as Password,	rtrim(FULL_NAME) as Name,
		 			GROUP_CODE as GroupId, rtrim(USER_ACTIVE) as Active
	 from 	INTEGRAL..USERS
	 where 	GROUP_SIGN is null	-- Исключим все группы
end

/*
  exec MEDIATE..spNE_GetUsers
*/