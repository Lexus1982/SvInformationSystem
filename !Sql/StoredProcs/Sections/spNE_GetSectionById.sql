use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_GetSectionById')
		and type='P')
	drop procedure dbo.spNE_GetSectionById
go

create proc spNE_GetSectionById (
                                  @SECTION_CODE     numeric(10,0)
)
as
begin
    select 	S.SECTION_CODE as Id, S.PARENT_CODE as ParentId, rtrim(S.SECTION_NAME) as Name, S.TYPE_CODE as TypeId, null as Position
		from 		MEDIATE..NE_SECTIONS S
		where		S.SECTION_CODE = @SECTION_CODE
end


/*

  exec MEDIATE..spNE_GetSectionById 4

*/