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
	kode in varchar2
)
is
nama varchar2(100);
cek number;
begin
	cek := 0;
	select nama_produk into nama from mh_produk where kode_produk = kode;
	dbms_output.put_line(nama);
	dbms_output.put_line(kode);
	for i in (
		select instr(nama, nama_embargo) from mh_embargo
	) loop
		cek := 1;
	end loop;
	dbms_output.put_line(cek);
	if (cek = 0) then
		update mh_produk set status = '0' where kode_produk = kode;
	else
		update mh_produk set status = '1' where kode_produk = kode;
	end if;
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
