use master
go
use MyDatabase
go
create table users
(
	�˺� char(11)	primary key,
	���� char(6)	not null,
	����Ա char(1)	check(����Ա='0' or ����Ա='1')
);
go
insert into users values('12345678901','666666','1');
insert into users values('00000000001','123456','0');
go