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