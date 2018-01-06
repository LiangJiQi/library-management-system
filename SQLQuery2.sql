use master
go
use MyDatabase
go
create table users
(
	账号 char(11)	primary key,
	密码 char(6)	not null,
	管理员 char(1)	check(管理员='0' or 管理员='1')
);
go
insert into users values('12345678901','666666','1');
insert into users values('00000000001','123456','0');
go