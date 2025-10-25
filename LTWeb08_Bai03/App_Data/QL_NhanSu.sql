Create Database QL_NhanSu
Use QL_NhanSu
Go

Create table tbl_Deparment
(
	DeptId int Primary key Not Null,
	Name nvarchar(100)
)

Create table tbl_Employee
(
	Id int Primary key Not Null,
	Name nvarchar(100),
	Gender nvarchar(100),
	City nvarchar(100),
	DeptId int Not Null,
	Foreign key (DeptId) References tbl_Deparment (DeptId)
)