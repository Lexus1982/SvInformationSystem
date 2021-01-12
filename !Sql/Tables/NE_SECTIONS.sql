use MEDIATE
GO

-- drop table MEDIATE..NE_SECTIONS
create table  NE_SECTIONS (
  SECTION_CODE    numeric(10,0)   identity not null,      --
  PARENT_CODE     numeric(10,0)   null,                   -- SECTION_CODE ��������
  SECTION_NAME    varchar(255)    not null,               -- ������������ ������
  GROUP_CODE      numeric(10,0)   not null,               -- ��� ������ ������ (1 - �����������������, 2 - ����������)
  TYPE_CODE       numeric(10,0)   not null,               -- ��� ���� ������ (1 - ������/���������, 2 - �����)
	USER_CODE       numeric(5,0)    not null,               -- ��� ������������ (fk INTEGRAL..USERS)
  DATE_CHANGE     datetime        not null,               -- ���� ���������� ���������

	CONSTRAINT PK_SECTION_CODE PRIMARY KEY CLUSTERED ( SECTION_CODE )  on 'default'
)

--CREATE UNIQUE INDEX UI_NE_SECTIONS_SECTION_NAME ON NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE)
--go

CREATE INDEX FI_NE_SECTIONS_GROUP_CODE ON NE_SECTIONS (GROUP_CODE)
go

ALTER TABLE MEDIATE.dbo.NE_SECTIONS
ADD CONSTRAINT FK_NE_SECTIONS_USER_CODE
FOREIGN KEY ( USER_CODE )
REFERENCES INTEGRAL.dbo.USERS (USER_CODE)
go

-- ���������
-- select * from MEDIATE..NE_SECTIONS
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (null, '������������ �� ���� �����', 2, 1, 3, getdate())
  insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
  values (1, '������������ �� ���� ����� (��������)', 2, 2, 3, getdate())
  insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
  values (1, '������������ � ������� ���������� �������', 2, 2, 3, getdate())
  insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
  values (1, '������������ � ������� ��������� ����', 2, 2, 3, getdate())

insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (null, '���������� ����', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (null, '���������� ����', 2, 1, 3, getdate())


insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (null, '������ 1', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (7, '������ 1-1', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (9, '���������� 1-1', 2, 2, 3, getdate())

insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (null, '������ 2', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (8, '������ 2-1', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (10, '������ 2-1-1', 2, 1, 3, getdate())
insert into MEDIATE..NE_SECTIONS (PARENT_CODE, SECTION_NAME, GROUP_CODE, TYPE_CODE, USER_CODE, DATE_CHANGE)
values (11, '���������� 2-1-1', 2, 2, 3, getdate())
/*
    select * from MEDIATE..NE_SECTIONS order by isnull(PARENT_CODE, SECTION_CODE), SECTION_CODE

*/


