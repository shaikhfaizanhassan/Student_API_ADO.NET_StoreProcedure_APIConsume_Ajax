create proc GetAllStudent
as
begin
select * from Student_table;
end

create proc GetAllStudentByID
(
	@stdid int
)
as
begin
select *  from Student_table where Sid=@stdid;
end

create proc CreateStudent
(
	@sname nvarchar(50),
	@semail nvarchar(50),
	@spassword nvarchar(50),
	@scontact nvarchar(50)
)
as
begin
insert into Student_table (Sname,Semail,Spassword,Scontact)
values (@sname,@semail,@spassword,@scontact);
end

create proc DeleteStudent
(
	@stdid int
)
as
begin
delete from Student_table where Sid=@stdid;
end

create proc UpdateStudent
(
	@sid int,
	@sname nvarchar(50),
	@semail nvarchar(50),
	@spassword nvarchar(50),
	@scontact nvarchar(50)
)
as
begin
update Student_table set Sname=@sname, Semail=@semail,Spassword=@spassword,Scontact=@scontact
where Sid=@sid;
end






