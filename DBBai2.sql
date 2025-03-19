create database BaiTapLinQBai2
go

use BaiTapTuan2
go

set dateformat dmy
go

create table KhoHang
(
	msKho nvarchar(15) not null,
	DiaChiKho nvarchar(150),
	DienThoaiKho nvarchar(16),
	TenKhuKho nvarchar(50),
	primary key(msKho)
)
go

create table PhanBo
(
	lanPB nvarchar(15) not null,
	msKho nvarchar(15),
	ngayPB smalldatetime,
	primary key(lanPB)
)
go

create table ChiTietPB
(
	lanPB nvarchar(15) not null,
	msHang nvarchar(15) not null,
	soLuong float,
	GhiChu text,
	primary key(lanPB, msHang)
)
go

create table HangHoa
(
	msHang nvarchar(15) not null,
	TenHang nvarchar(100),
	dvTinh nvarchar(50) null,
	primary key(msHang) 
)
go

----Create Constraint foreign key
--Table PhanBo
alter table PhanBo
add constraint fk_KhoHang_PhanBo foreign key(msKho) references KhoHang(msKho)
go

--Table ChiTietPB
alter table ChiTietPB
add constraint fk_PhanBo_ChiTietPB foreign key(lanPB) references PhanBo(lanPB),
	constraint fk_HangHoa_ChiTietPB foreign key(msHang) references HangHoa(msHang)
go



