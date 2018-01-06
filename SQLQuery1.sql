use master;
go
use MyDatabase;
go
create table books
(
	ID char(2)			primary key,
	书名 varchar(16)		not null,
	作者 char(8)		not null,
	出版社 char(16)		not null,
	出版日期 char(12)	not null,
	类别 char(8)		not null,
	);
	go
	insert into books values('01','高级程序设计','谭浩强','人民出版社','1999-10-10','科学技术');
	insert into books values('02','c程序设计','谭浩强','清华出版社','2000-11-11','科学技术');
	insert into books values('03','c++语言程序设计','清华教材','清华出版社','2001-07-05','科学技术');
	insert into books values('04','Visual C#.NET 程序设计','刘秋香','清华出版社','2002-4-1','科学技术');
	insert into books values('05','编程珠玑','jonBentley','人民出版社','2005-7-17','科普读物');
	insert into books values('06','数学之美','吴军','人民出版社','2006-9-19','科普读物');
	go