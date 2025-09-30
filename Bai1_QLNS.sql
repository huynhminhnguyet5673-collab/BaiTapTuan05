CREATE DATABASE QLNV

GO
USE QLNS;
GO

CREATE TABLE tblDeparment
(
	Deptld int not null,
	Name nvarchar(50),
	Constraint tblDepament primary key (Deptld) 
)

CREATE TABLE tblEmloyee
(
	Id int not null,
	Name nvarchar(50),
	Gender nvarchar(30),
	City nvarchar(100),
	Deptld int,
	primary key(Id),
	Constraint tblDeparment_tblEmloyee foreign key(Deptld) references tblDeparment(Deptld)
)

INSERT tblDeparment (Deptld, Name) Values
(1,N'Khoa CNTT'),
(2,N'Khoa Ngoại Ngữ'),
(3,N'Khoa tài chính'),
(4,N'Khoa thực phẩm'),
(5,N'Phòng đào tạo');

select *from tblDeparment

INSERT tblEmloyee (Id,Name,Gender,City,Deptld) Values
(1,N'Nguyễn Hải',N'Nam',N'Đà Lạt',1),
(2,N'Trương Mạnh Hùng',N'Nam',N'TP.HCM',1),
(3,N'Đinh Duy Minh',N'Nữ',N'Thái Bình',2),
(4,N'Ngô Thị Nguyệt',N'Nữ',N'Long An',2),
(5,N'Đào Minh Châu',N'Nữ',N'Bạc Liêu',3),
(14,N'Phan Thị Ngọc Mai',N'Nữ',N'Bến Tre',3),
(15,N'Trương Nguyễn Quỳnh Anh',N'Nữ',N'TP.HCM',4),
(16,N'Lê Thanh Liêm',N'Nam',N'TP.HCM',4),
(17,N'bbb',N'Nữ',N'TP.HCM',5);

select *from tblEmloyee