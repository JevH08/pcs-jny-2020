DROP TABLE MH_KATEGORI CASCADE CONSTRAINTS;
DROP TABLE MH_EMBARGO CASCADE CONSTRAINTS;
DROP TABLE mh_user CASCADE CONSTRAINTS;
DROP TABLE MH_REPORT CASCADE CONSTRAINTS;
DROP TABLE MH_DISTRIBUTOR CASCADE CONSTRAINTS;
DROP TABLE mh_produk CASCADE CONSTRAINTS;
DROP TABLE htrans CASCADE CONSTRAINTS;
DROP TABLE history_emoney CASCADE CONSTRAINTS;
DROP TABLE dtrans CASCADE CONSTRAINTS;
DROP TABLE th_chat CASCADE CONSTRAINTS;
DROP TABLE td_chat CASCADE CONSTRAINTS;


CREATE TABLE MH_KATEGORI (
  Kode_Kategori varchar2(15) CONSTRAINTS PK_MH_KATEGORI  PRIMARY KEY,
  Nama_Kategori varchar2(100)  ,
	status number(1)
);


CREATE TABLE MH_EMBARGO (
  Kode_EMBARGO varchar2(15) CONSTRAINTS PK_MH_EMBARGO  PRIMARY KEY,
  Nama_EMBARGO varchar2(100)  ,
	status number(1)
);


CREATE TABLE mh_user(
	kode_user  varchar2(20) CONSTRAINTS PK_mh_user PRIMARY KEY,
   nama_user  varchar2(100)  ,
   username_user  varchar2(70)  ,
   password_user  varchar2(70)  ,
   email_user  varchar2(100)  ,
   alamat_user  varchar2(150)  ,
   kota_user  varchar2(100)  ,
   telepon_user  varchar2(20)  ,
   jenis_kelamin  varchar2(1)  ,
   tgl_lahir date,
   saldo  number  ,
   norek varchar2(20),
   role  varchar2(1),
   status number(1), 
aktif number
);


CREATE TABLE MH_REPORT (
  Kode_REPORT varchar2(15) CONSTRAINTS PK_MH_REPORT  PRIMARY KEY,
fk_pelapor  varchar2(20)  references mh_user(kode_user),
fk_dilapor  varchar2(20)  references mh_user(kode_user),
  alasan varchar2(200)  
);


CREATE TABLE MH_DISTRIBUTOR (
	 kode_distributor  varchar2(20) CONSTRAINTS PK_MH_DISTRIBUTOR  PRIMARY KEY,
   nama_distributor  varchar2(200)  ,
   harga_per_kilo  number,
   batas_harga number,
   diskon  number,
   status  number  
);



CREATE TABLE MH_PRODUK (
   kode_produk  varchar2(20) CONSTRAINTS PK_MH_PRODUK PRIMARY KEY,
   nama_produk  varchar2(200)  ,
   desc_barang  varchar2(256)  ,
   fk_kategori  varchar2(15)  references mh_kategori(kode_kategori),
   fk_penjual  varchar2(15) references mh_user(kode_user) ,
   stok  number  ,
   harga  number  ,
   berat  number  ,
   kondisi  number,
   tag  varchar2(200)  ,
   status  number,
  rating number  ,
  jumlah_pembeli number,
 totalrating varchar2(5)  
);

CREATE TABLE htrans(
	 kode_htrans  varchar2(20) CONSTRAINTS PK_htrans PRIMARY KEY,
   tgl_transaksi   date  ,
   berat number,
   subtotal  number  ,
   shipping  number  ,
   promo  number  ,
   grandtotal  number  ,
   fk_pelanggan  varchar2(20) references mh_user(kode_user)  ,
   fk_distributor  varchar2(20) references mh_distributor(kode_distributor) ,
   nama_tujuan  varchar2(256)  ,
   kota_tujuan  varchar2(256)  ,
	telepon_tujuan  varchar2(20)  ,
   alamat_tujuan  varchar2(256)  ,
	status varchar2(1)
);

CREATE TABLE history_emoney(
	 kode_history  varchar2(20) CONSTRAINTS PK_history_emoney PRIMARY KEY,
   fk_user  varchar2(20) references mh_user(kode_user)  ,
   emoney  number  ,
   status  number  ,
   tgl_emoney   date,
   ket varchar2 (255)  
);

CREATE TABLE dtrans(
	 kode_dtrans  varchar2(20) CONSTRAINTS PK_dtrans PRIMARY KEY,
   fk_htrans  varchar2(20)  references htrans(kode_htrans),
   fk_produk  varchar2(20)  references mh_produk(kode_produk),
   jumlah  number  ,
   harga  number  ,
   subtotal  number  ,
   status  number  ,
reportB number,
reportS number,
rating number
);


CREATE TABLE th_chat(
   kode_hchat varchar2(20) CONSTRAINTS PK_th_chat PRIMARY KEY,
   fk_pembeli  varchar2(20)  references mh_user(kode_user) ,
   fk_penjual  varchar2(20)  references mh_user(kode_user)
);


CREATE TABLE td_chat(
   kode_dchat varchar2(20) CONSTRAINTS PK_td_chat PRIMARY KEY,
   fk_hchat varchar2(20) references th_chat(kode_hchat),
   pengirim number,
   isi_chat varchar(256),
   tgl_chat date,
statusB number,
	statusS number
);

insert into mh_user values ('US20200426_001','BRIGITTA','yinyin','yinyin','yinyin@gmail.com','Jl.Kenanga No 24','Surabaya','081234567890','P',to_date('10-01-2000','DD-MM-YYYY'),0,'0',1,1,0);
insert into mh_user values ('US20200426_002','JEVON','jevon','jevon','jevon@gmail.com','Jln. Pelajar Pejuang 45 No. 877', 'Semarang','(+62)93168477753','L',to_date('13-04-2000','DD-MM-YYYY'),0,'0',1,1,0);
insert into mh_user values ('US20200426_003','GEORGIA','nikita','nikita','nikita@gmail.com','Jln. Jaksa No. 461','Jakarta','(+62)38362418235','P',to_date('21-08-2000','DD-MM-YYYY'),0,'0',1,1,0);
insert into mh_user values ('US20200426_004','PENJUAL','penjual','penjual','penjual@gmail.com','Jln. Kembang Kuning 45 No. 7', 'Jakarta','(+62)93165137753','L',to_date('13-12-1995','DD-MM-YYYY'),0,'122516135',2,1,0);
insert into mh_user values ('US20200426_005','PEMBELI','pembeli','pembeli','pembeli@gmail.com','Jln. Tugu Pahlawan No. 1','Bali','(+62)38362421545','P',to_date('12-01-1990','DD-MM-YYYY'),3000000,'124345454',3,1,0);
insert into mh_user values ('US20200426_006','NIKKI','nikki','nikki','nikki@gmail.com','Jln. Melati No.1 ','Semarang','(+62)3836222545','P',to_date('10-10-1997','DD-MM-YYYY'),2800000,'128365454',3,1,0);
insert into mh_user values ('US20200426_007','PENJUAL1','penjual1','penjual1','penjual1@gmail.com','Jln. Perak No. 2', 'Surabaya','(+62)9316521753','P',to_date('3-2-1995','DD-MM-YYYY'),0,'122315135',2,1,0);
insert into mh_user values ('US20200426_008','PENJUAL2','penjual2','penjual2','penjual2@gmail.com','Jln. Jendral Sudirman No. 5', 'Semarang','(+62)4806521753','P',to_date('23-5-1998','DD-MM-YYYY'),0,'152518135',2,1,0);
insert into mh_user values ('US20200426_009','PEMBELI1','pembeli1','pembeli1','pembeli1@gmail.com','Jln. Tugu Pahlawan No. 12','Yogyakarta','(+62)7836232155','L',to_date('12-11-1990','DD-MM-YYYY'),400000,'124515434',3,1,0);

insert into mh_kategori values('KA_001', 'Art', 0);
insert into mh_kategori values('KA_002', 'Glass', 0);
insert into mh_kategori values('KA_003', 'Electronic', 0);
insert into mh_kategori values('KA_004', 'Games', 0);
insert into mh_kategori values('KA_005', 'Fahion', 0);
insert into mh_kategori values('KA_006', 'Food', 0);
insert into mh_kategori values('KA_007', 'Health', 0);
insert into mh_kategori values('KA_008', 'Fashion Accessories', 0);

insert into mh_embargo values('EM_001', 'Narkotika', 0);
insert into mh_embargo values('EM_002', 'Nikotin', 0);
insert into mh_embargo values('EM_003', 'Sabu', 0);
insert into mh_embargo values('EM_004', 'Ganja', 0);
insert into mh_embargo values('EM_005', 'Rokok', 0);
insert into mh_embargo values('EM_006', 'Psikotropika', 0);
insert into mh_embargo values('EM_007', 'Morfin', 0);
insert into mh_embargo values('EM_008', 'Heroin', 0);

insert into mh_distributor values ('DI_001','JNE', 600, 500,30,0);
insert into mh_distributor values ('DI_002','JNE EXPRESS', 800, 400,45,0);
insert into mh_distributor values ('DI_003','GRAB', 300, 400,10,0);
insert into mh_distributor values ('DI_004','GOJEK', 450, 450,20,0);
insert into mh_distributor values ('DI_005','Si Cepat', 600, 300,25,0);
insert into mh_distributor values ('DI_006','ABC', 200, 800,5,0);
insert into mh_distributor values ('DI_007','PACKET GO', 350, 550,10,0);
insert into mh_distributor values ('DI_008','SEND', 450, 400,5,0);

insert into mh_produk values ('PR_20200604_001', 'Iphone X', 'Iphone X merk Apple hitam', 'KA_003','US20200426_004',3,5000000,650,0,'#iphone #electronic #apple',0,10,2,'5');
insert into mh_produk values ('PR_20200604_002', 'Iphone5', 'Iphone5 merk Apple putih / hitam', 'KA_003','US20200426_004',4,2500000,650,0,'#iphone #electronic #apple',0,9,2,'4.5');
insert into mh_produk values ('PR_20200604_003', 'piring cantik', 'piring cantik dari Italia', 'KA_002','US20200426_007',2,500000,650,0,'#cantik #Italia #piring',1,8,2,'4');
insert into mh_produk values ('PR_20200604_004', 'jam tangan anak', 'jam tangan anak - anak lucu banyak model', 'KA_008','US20200426_007',3,250000,650,0,'#jam #kid #lucu',1,15,3,'5');
insert into mh_produk values ('PR_20200604_005', 'Tas Billabong', 'Tas untuk jalan ke mal kece', 'KA_008','US20200426_007',5,3000000,650,0,'#tas #kece #billabong',0,7,2,'3.5');
insert into mh_produk values ('PR_20200604_006', 'Topi', 'Topi merk billabong warna putih', 'KA_008','US20200426_004',3,350000,650,0,'#cap #billabong #putih',0,20,5,'4');
insert into mh_produk values ('PR_20200604_007', 'UNO STACK', 'UNO STACKO', 'KA_004','US20200426_008',2,50000,650,0,'#games #uno #stacko',1,32,8,'4');
insert into mh_produk values ('PR_20200604_008', 'Baju sabrina', 'baju wanita model sabrina tersedia 3 warna', 'KA_005','US20200426_008',25,175000,650,0,'#girl #sabrina #baju',1,24,6,'4');

insert into history_emoney values ('HI_20200607_0001', 'US20200426_004',2000000,0);
insert into history_emoney values ('HI_20200607_0002', 'US20200426_005',2500000,2);
insert into history_emoney values ('HI_20200608_0001', 'US20200426_005',500000,2);
insert into history_emoney values ('HI_20200608_0002', 'US20200426_004',500000,0);
insert into history_emoney values ('HI_20200608_0003', 'US20200426_006',1000000,2);
insert into history_emoney values ('HI_20200606_0001', 'US20200426_006',1000000,2);
insert into history_emoney values ('HI_20200609_0001', 'US20200426_006',8000000,2);
insert into history_emoney values ('HI_20200610_0001', 'US20200426_007',3000000,0);

insert into mh_report values ('RE_001','US20200426_004','US20200426_009','berbicara kasar');
insert into mh_report values ('RE_002','US20200426_008','US20200426_009','berbicara kasar');
insert into mh_report values ('RE_003','US20200426_007','US20200426_009','berbicara kasar');
insert into mh_report values ('RE_004','US20200426_004','US20200426_005','pesanan terlalu lama di accept padahal sudah sampai');
insert into mh_report values ('RE_005','US20200426_006','US20200426_007','barang rusak');
insert into mh_report values ('RE_006','US20200426_005','US20200426_007','penipuan tidak sesuai deskripsi');
insert into mh_report values ('RE_007','US20200426_004','US20200426_005','melakukan pemesanan tapi tidak di acc');
insert into mh_report values ('RE_008','US20200426_006','US20200426_004','respon lambat');
purge recyclebin;