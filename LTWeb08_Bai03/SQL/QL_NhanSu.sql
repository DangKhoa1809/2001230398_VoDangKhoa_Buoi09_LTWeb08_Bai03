CREATE DATABASE QL_NhanSu;
USE QL_NhanSu;

--TẠO BẢNG DEPARTMENT--
CREATE TABLE Deparment(
	DeptId INT IDENTITY(1, 1) PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL
);

--TẠO BẢNG Employee--
CREATE TABLE Employee(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	Gender NVARCHAR(5),
	City NVARCHAR(100),
	Image NVARCHAR(50),
	DeptId INT,
	CONSTRAINT FK_Employee_Deparment FOREIGN KEY (DeptId) REFERENCES Deparment (DeptId)
);

--NHẬP LIỆU BẢNG DEPARTMENT--
INSERT INTO Deparment (Name) VALUES
(N'Khoa CNTT'),
(N'Khoa Ngoại Ngữ'),
(N'Khoa Tài Chính'),
(N'Khoa Thực Phẩm'),
(N'Phòng Đào Tạo');

SELECT*FROM Deparment;

--NHẬP LIỆU BẢNG Employee--
INSERT INTO Employee (Name, Gender, City, Image, DeptId) VALUES
(N'Nguyễn Hải Yến', N'Nữ', N'Đà Lạt', N'nu.jpg', 1),
(N'Trương Mạnh Hùng', N'Nam', N'TP.HCM', N'nam.jpg', 1),
(N'Đinh Duy Minh', N'Nam', N'Thái Bình', N'nam.jpg', 2),
(N'Ngô Thị Nguyệt', N'Nữ', N'Long An', N'nu.jpg', 2),
(N'Đào Minh Châu', N'Nữ', N'Bạc Liêu', N'nu.jpg', 3),
(N'Phan Thị Ngọc Mai', N'Nữ', N'Bến Tre', N'nu.jpg', 3),
(N'Trương Nguyễn Quỳnh Anh', N'Nữ', N'TP.HCM', N'nu.jpg', 4),
(N'Lê Thanh Liêm', N'Nam', N'TP.HCM', N'nam.jpg', 4),
(N'Trần Thị Mơ', N'Nữ', N'TP.HCM', N'nu.jpg', 5);

SELECT*FROM Employee;


