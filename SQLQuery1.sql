use master;
go
use MyDatabase;
go
create table books
(
	ID char(2)			primary key,
	���� varchar(16)		not null,
	���� char(8)		not null,
	������ char(16)		not null,
	�������� char(12)	not null,
	��� char(8)		not null,
	);
	go
	insert into books values('01','�߼��������','̷��ǿ','���������','1999-10-10','��ѧ����');
	insert into books values('02','c�������','̷��ǿ','�廪������','2000-11-11','��ѧ����');
	insert into books values('03','c++���Գ������','�廪�̲�','�廪������','2001-07-05','��ѧ����');
	insert into books values('04','Visual C#.NET �������','������','�廪������','2002-4-1','��ѧ����');
	insert into books values('05','�������','jonBentley','���������','2005-7-17','���ն���');
	insert into books values('06','��ѧ֮��','���','���������','2006-9-19','���ն���');
	go