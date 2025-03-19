create database BaiTapTuan2
go

set dateformat dmy
go

use BaiTapTuan2
go

create table SINHVIEN
(
	MaSV nvarchar(15) not null,
	HoTen nvarchar(150),
	DiaChi nvarchar(250),
	Email nvarchar(250),
	LopHoc nvarchar(15),
	primary key(MaSV)
)
go

create table DANGKY
(
	MaSV nvarchar(15) not null,
	MaMH nvarchar(15) not null,
	DiemGK float,
	DiemCK float,
	DiemDT float,
	GhiChu nvarchar(400),
	primary key (MaSV, MaMH)
)
go
create table MONHOC
(
	MaMH nvarchar(15) not null,
	TenHM nvarchar(250),
	SoTC int,
	HocKy int,
	primary key (MaMH)
)
go

alter table DANGKY
add constraint fk_DK_SV foreign key (MaSV) references SINHVIEN(MaSV);
go

alter table DANGKY
add constraint fk_DK_MH foreign key (MaMH) references MONHOC(MaMH);
go

INSERT INTO SINHVIEN (MaSV, HoTen, DiaChi, Email, LopHoc) VALUES 
(N'SV001', N'Nguyễn Văn A', N'123 Đường ABC, Quận 1, Hồ Chí Minh', N'nguyenvana@example.com', N'L01'),
(N'SV002', N'Trần Thị B', N'456 Đường DEF, Quận 2, Hồ Chí Minh', N'tranthib@example.com', N'L02'),
(N'SV003', N'Lê Văn C', N'789 Đường GHI, Quận 3, Hồ Chí Minh', N'levanc@example.com', N'L03'),
(N'SV004', N'Phạm Thị D', N'101 Đường JKL, Quận 4, Hồ Chí Minh', N'phamthid@example.com', N'L01'),
(N'SV005', N'Hoàng Văn E', N'202 Đường MNO, Quận 5, Hồ Chí Minh', N'hoangvane@example.com', N'L02'),
(N'SV006', N'Võ Thị F', N'303 Đường PQR, Quận 6, Hồ Chí Minh', N'vothif@example.com', N'L03'),
(N'SV007', N'Bùi Văn G', N'404 Đường STU, Quận 7, Hồ Chí Minh', N'buivang@example.com', N'L01'),
(N'SV008', N'Đỗ Thị H', N'505 Đường VWX, Quận 8, Hồ Chí Minh', N'dothih@example.com', N'L02'),
(N'SV009', N'Ngô Văn I', N'606 Đường YZ, Quận 9, Hồ Chí Minh', N'ngovani@example.com', N'L03'),
(N'SV010', N'Huỳnh Thị J', N'707 Đường ABC, Quận 10, Hồ Chí Minh', N'huynhthij@example.com', N'L01');
go

INSERT INTO MONHOC (MaMH, TenHM, SoTC, HocKy) VALUES
(N'MH001', N'Toán Cao Cấp', 3, 1),
(N'MH002', N'Vật Lý Đại Cương', 4, 1),
(N'MH003', N'Thực Hành Máy Tính', 2, 1),
(N'MH004', N'Lập Trình Cơ Bản', 3, 2),
(N'MH005', N'Nguyên Lý Mạch Điện', 4, 2);
go

INSERT INTO DANGKY (MaSV, MaMH, DiemGK, DiemCK, DiemDT, GhiChu) VALUES 
(N'SV001', N'MH001', 8.5, 9.0, 8.75, N'Hoàn thành tốt các bài tập'),
(N'SV002', N'MH001', 7.5, 8.0, 7.75, N'Chưa tham gia đầy đủ các buổi học'),
(N'SV003', N'MH002', 6.5, 7.5, 7.0, N'Đạt yêu cầu bài kiểm tra giữa kỳ'),
(N'SV004', N'MH002', 9.0, 9.5, 9.25, N'Rất nỗ lực và cải thiện điểm'),
(N'SV005', N'MH003', 7.0, 8.0, 7.5, N'Cần chú ý hơn trong bài tập thực hành'),
(N'SV006', N'MH003', 6.0, 7.5, 6.75, N'Cần cải thiện kỹ năng thực hành'),
(N'SV007', N'MH004', 8.0, 9.0, 8.5, N'Hoàn thành tốt lập trình cơ bản'),
(N'SV008', N'MH004', 7.0, 7.5, 7.25, N'Chưa hoàn thành bài tập đúng hạn'),
(N'SV009', N'MH005', 8.5, 8.0, 8.25, N'Hiểu bài nhưng cần luyện tập thêm'),
(N'SV010', N'MH005', 9.0, 9.0, 9.0, N'Xuất sắc trong các bài kiểm tra và bài tập'),
(N'SV001', N'MH002', 8.0, 8.5, 8.25, N'Thực hiện tốt bài kiểm tra giữa kỳ'),
(N'SV002', N'MH003', 6.5, 7.0, 6.75, N'Cần cải thiện điểm số'),
(N'SV003', N'MH004', 7.5, 8.0, 7.75, N'Hoàn thành tốt bài tập lập trình'),
(N'SV004', N'MH003', 6.0, 6.5, 6.25, N'Cần tham gia đầy đủ các buổi thực hành'),
(N'SV005', N'MH004', 9.0, 8.5, 8.75, N'Hoàn thành tốt và có tiến bộ rõ rệt'),
(N'SV006', N'MH002', 7.0, 7.5, 7.25, N'Cần nỗ lực hơn trong học tập'),
(N'SV007', N'MH005', 8.0, 7.5, 7.75, N'Hiểu bài nhưng còn thiếu kinh nghiệm thực tế'),
(N'SV008', N'MH001', 7.5, 8.0, 7.75, N'Điểm giữa kỳ khá, cần cải thiện điểm cuối kỳ'),
(N'SV009', N'MH003', 6.0, 7.0, 6.5, N'Cần cải thiện trong thực hành máy tính'),
(N'SV010', N'MH004', 8.5, 9.0, 8.75, N'Có khả năng lập trình rất tốt');
go

