create or replace function hitung
(
	peran in varchar2
)
return number
is
hitung number;
begin
	select count(*) into hitung from mh_user where role like '%'||peran||'%';
	return hitung;
end;
/


create or replace function cekHC
(
	kodeB in varchar2, kodeS in varchar2
)
return varchar2
is
cek varchar2(20);
begin
	cek := 'Tidak Ada';
	for i in (
		select kode_hchat from th_chat where fk_penjual like '%' || kodeS || '%' and fk_pembeli like '%' ||  kodeB || '%'
	) loop
		cek := i.kode_hchat;
	end loop;
	return cek;
end;
/


create or replace function autogenht
return varchar2
is
kode varchar2(13);
hitung number;
tgl varchar2(10);
begin
	select to_char(sysdate,'YYYYMMDD') into tgl from dual;
	select count(*) + 1 into hitung from htrans where KODE_htrans like '%' || tgl || '%';
	kode := 'HJ' || tgl || lpad(hitung,3,'0');
	return kode;
end;
/


create or replace function autogenht_c
return varchar2
is
kode varchar2(13);
hitung number;
tgl varchar2(10);
begin
	select to_char(sysdate,'YYYYMMDD') into tgl from dual;
	select count(*) + 1 into hitung from th_chat where kode_hchat like '%' || tgl || '%';
	kode := 'HC' || tgl || lpad(hitung,3,'0');
	return kode;
end;
/


create or replace function autogendt_c
return varchar2
is
kode varchar2(13);
hitung number;
tgl varchar2(10);
begin
	select to_char(sysdate,'YYYYMMDD') into tgl from dual;
	select count(*) + 1 into hitung from td_chat where kode_dchat like '%' || tgl || '%';
	kode := 'DC' || tgl || lpad(hitung,3,'0');
	return kode;
end;
/


create or replace procedure tambah
(
	kode in varchar2
)
is
jumlah number;
begin
	select status into jumlah from mh_user where lower(username_user) = lower(kode);
	jumlah := jumlah + 1;
	update mh_user set status = jumlah where lower(username_user) = lower(kode);
end;
/


create or replace procedure larangan
(
	kode in varchar2, nama in varchar2
)
is
cek number;
cek2 number;
begin
	cek := 0;
	select status into cek2 from mh_produk where kode_produk = kode;
	for i in (
		select nama_embargo from mh_embargo where lower(nama_embargo) = lower(nama)
	) loop
		cek := 1;
		dbms_output.put_line(i.nama_embargo);
	end loop;
	if ((cek = 0 and cek2 = 2) or (cek = 0 and cek2 = 0)) then
		update mh_produk set status = '0' where kode_produk = kode;
	else
		update mh_produk set status = '1' where kode_produk = kode;
	end if;
end;
/


create or replace procedure update_ship
(
	kodebaru in varchar2
)
is
ctr number;
ctr1 number;
ctr2 number;
begin
	select count (*) into ctr from dtrans where fk_htrans = kodebaru;
	select count (*) into ctr1 from dtrans where fk_htrans = kodebaru and status <> 0;
	select count (*) into ctr2 from dtrans where fk_htrans = kodebaru and status = 2;
	if (ctr = ctr2) then
		update htrans set status = 3 where kode_htrans = kodebaru;
	elsif ( ctr = ctr1 ) then
		update htrans set status = 1 where kode_htrans = kodebaru;
	end if;
end;
/


create or replace procedure transfer
(
	kodeUser in varchar2, uang in varchar2
)
is
history number;
begin
	select saldo into history from mh_user where kode_user = kodeUser;
	history := history + to_number(uang);
	update mh_user set saldo = history where kode_user = kodeUser;
end;
/


create or replace procedure beriRating
(
	kodeProduk in varchar2, rating in varchar2
)
is
totalRatingBaru number;
jumlahPembeliTemp number;
ratingBaru number;
ratingTemp number;
begin
	select rating, jumlah_pembeli into ratingTemp, jumlahPembeliTemp from mh_produk where kode_produk = kodeProduk;
	ratingBaru := to_number(ratingTemp) + to_number(rating);
	jumlahPembeliTemp := jumlahPembeliTemp + 1;
	totalRatingBaru := ratingBaru / jumlahPembeliTemp;
	update mh_produk set rating = ratingBaru, jumlah_pembeli = jumlahPembeliTemp, totalrating = to_char(totalRatingBaru);
end;
/


create or replace procedure cek_report( temp in varchar2)
is
hitung number;
begin
	hitung := 0;
	for i in (
		select kode_user from mh_user
	) loop
		select count(fk_dilapor) into hitung from mh_report where fk_dilapor = i.kode_user;
		if (hitung > 5) then
			dbms_output.put_line('Masuk');
			update mh_user set aktif = 1 where kode_user = i.kode_user;
		end if;
	end loop;
end;
/