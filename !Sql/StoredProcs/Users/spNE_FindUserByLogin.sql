use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_FindUserByLogin')
		and type='P')
	drop procedure dbo.spNE_FindUserByLogin
go

create proc spNE_FindUserByLogin (
												@USER_NAME	char(50)		-- Логин пользователя
)
as
begin
   select USER_CODE as Id, rtrim(USER_NAME) as Login, rtrim(USER_PASSWORD) as Password,	rtrim(FULL_NAME) as Name,
		 			GROUP_CODE as GroupId, rtrim(USER_ACTIVE) as Active
	 from 	INTEGRAL..USERS
	 where 	GROUP_SIGN is null and	-- Исключим все группы
					rtrim(USER_NAME) = rtrim(@USER_NAME)
end

/*
  exec MEDIATE..spNE_FindUserByLogin 'admin'
*/
