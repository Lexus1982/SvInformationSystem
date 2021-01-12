use MEDIATE
go

if exists(select 1
		from sysobjects
		where id=object_id('dbo.spNE_BuildingSectionById')
		and type='P')
	drop procedure dbo.spNE_BuildingSectionById
go

create proc spNE_BuildingSectionById (
																	@SECTION_CODE     numeric(10,0),
																	@CUR_ORDER_SORT		varchar(250)
)
as
declare 	@PARENT_CODE     numeric(10,0),
					@ORDER_SORT			 varchar(250)
begin
		/*
		create table #Sections (
			SECTION_CODE    numeric(10,0)   not null,      					--
  		PARENT_CODE     numeric(10,0)   null,                   -- SECTION_CODE ��������
  		SECTION_NAME    varchar(255)    not null,               -- ������������ ������
  		TYPE_CODE       numeric(10,0)   not null,               -- ��� ���� ������ (1 - ������/���������, 2 - �����)
			ORDER_SORT			varchar(255)  	null										--
		)
		*/

		if @SECTION_CODE = null
				return 0

		select @ORDER_SORT = @CUR_ORDER_SORT + '.1'

		-- ��������� � �������� ������� ������� ������
		insert into #Sections (SECTION_CODE, PARENT_CODE, SECTION_NAME, /*GROUP_CODE,*/ TYPE_CODE, ORDER_SORT)
		select	S.SECTION_CODE, S.PARENT_CODE, S.SECTION_NAME, /*S.GROUP_CODE,*/ S.TYPE_CODE, @ORDER_SORT
		from 		MEDIATE..NE_SECTIONS S
		where		SECTION_CODE = @SECTION_CODE and
						TYPE_CODE = 1

		-- �������� ��� ������������ ������
		select 	@PARENT_CODE =	S.PARENT_CODE
		from 		MEDIATE..NE_SECTIONS S
		where		SECTION_CODE = @SECTION_CODE and
					TYPE_CODE = 1

		if @PARENT_CODE is null
				return 0

		-- ���� �������� � ������ ����, �� ��������� ����������� ��������� ������������ ������
		exec MEDIATE..spNE_BuildingSectionById @PARENT_CODE, @ORDER_SORT
end
/*

  exec MEDIATE..spNE_BuildingSectionById
*/