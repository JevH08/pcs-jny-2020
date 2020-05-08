DROP TABLE MH_KATEGORI CASCADE CONSTRAINTS;
DROP TABLE mh_produk CASCADE CONSTRAINTS;
DROP TABLE dtrans CASCADE CONSTRAINTS;
DROP TABLE history_emoney CASCADE CONSTRAINTS;
DROP TABLE htrans CASCADE CONSTRAINTS;
DROP TABLE MH_DISTRIBUTOR CASCADE CONSTRAINTS;
DROP TABLE mh_user CASCADE CONSTRAINTS;
DROP TABLE mh_chat CASCADE CONSTRAINTS;


CREATE TABLE MH_KATEGORI (
  Kode_Kategori varchar2(15) CONSTRAINTS PK_MH_KATEGORI  PRIMARY KEY,
  Nama_Kategori varchar2(100)  
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
   role  varchar2(1),
   status number(1)  
);


CREATE TABLE MH_DISTRIBUTOR (
	 kode_distributor  varchar2(20) CONSTRAINTS PK_MH_DISTRIBUTOR  PRIMARY KEY,
   nama_distributor  varchar2(200)  ,
   harga_per_kilo  number  ,
   batas_harga number,
   diskon  number  ,
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
   kondisi  number  ,
   tag  varchar2(200)  ,
   status  number  ,
  rating number  ,
  jumlah_pembeli number  
);





CREATE TABLE htrans(
	 kode_htrans  varchar2(20) CONSTRAINTS PK_htrans PRIMARY KEY,
   tgl_transaksi   date  ,
   subtotal  number  ,
   shipping  number  ,
   promo  number  ,
   grandtotal  number  ,
   fk_pelanggan  varchar2(20) references mh_user(kode_user)  ,
   fk_distributor  varchar2(20) references mh_distributor(kode_distributor) ,
   nama_tujuan  varchar2(256)  ,
   kota_tujuan  varchar2(256)  ,
   alamat_tujuan  varchar2(256)  
);


CREATE TABLE history_emoney(
	 kode_history  varchar2(20) CONSTRAINTS PK_history_emoney PRIMARY KEY,
   fk_user  varchar2(20) references mh_user(kode_user)  ,
   emoney  number  ,
   status  number  ,
   tgl_emoney   date  
);


CREATE TABLE dtrans(
	 kode_dtrans  varchar2(20) CONSTRAINTS PK_dtrans PRIMARY KEY,
   fk_htrans  varchar2(20)  references htrans(kode_htrans),
   fk_produk  varchar2(20)  references mh_produk(kode_produk),
   jumlah  number  ,
   harga  number  ,
   subtotal  number  ,
   status  number  ,
   rating  number  
);


CREATE TABLE mh_chat(
	 kode_chat varchar2(20) CONSTRAINTS PK_mh_chat PRIMARY KEY,
   fk_pembeli  varchar2(20)  references mh_user(kode_user) ,
   fk_penjual  varchar2(20)  references mh_user(kode_user) ,
   isi_chat  varchar2(256)  ,
	tgl_chat  date 
);

insert into mh_user values ('US20200426_001','BRIGITTA','yinyin','yinyin','yinyin@gmail.com','Jl.Kenanga No 24','SURABAYA','081234567890','P',to_date('10-01-2000','DD-MM-YYYY'),0,1,1);
insert into mh_user values ('US20200426_002','JEVON','jevon','jevon','jevon@gmail.com','Jln. Pelajar Pejuang 45 No. 877', 'SEMARANG','(+62)93168477753','L',to_date('13-04-2000','DD-MM-YYYY'),0,1,1);
insert into mh_user values ('US20200426_003','GEORGIA','nikita','nikita','nikita@gmail.com','Jln. Jaksa No. 461','JAKARTA','(+62)38362418235','P',to_date('21-08-2000','DD-MM-YYYY'),0,1,1);

purge recyclebin;