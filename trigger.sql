--user
create or replace trigger kode_user
before insert 
on mh_user
for each row
declare
ctr number;
kode varchar2(20);
tgl varchar2(10);
begin
	select to_char(sysdate,'YYYYMMDD') into tgl from dual;
	select count(*) + 1 into ctr from mh_user where kode_user like '%' || tgl || '%';

	:new.kode_user := 'US' || tgl || '_' || lpad(ctr,3,'0');
	:new.saldo := 0;
	:new.status := 0;
	:new.report := 0;
end;
/

--kategori
create or replace trigger kode_kategori
before insert 
on mh_kategori
for each row
declare
ctr number;
kode varchar2(20);
tgl varchar2(10);
begin
	select count(*) + 1 into ctr from mh_kategori;
	:new.kode_kategori := 'KA_' || lpad(ctr,3,'0');
	:new.status := 0;
end;
/

--embargo
create or replace trigger kode_embargo
before insert 
on mh_embargo
for each row
declare
ctr number;
kode varchar2(20);
tgl varchar2(10);
begin
	select count(*) + 1 into ctr from mh_embargo;
	:new.kode_embargo := 'KE_' || lpad(ctr,3,'0');
	:new.status := 0;
end;
/

--distributor
create or replace trigger kode_distributor
before insert 
on mh_distributor
for each row
declare
ctr number;
kode varchar2(20);
tgl varchar2(10);
begin
	select count(*) + 1 into ctr from mh_distributor;

	:new.kode_distributor := 'DI_' || lpad(ctr,3,'0');
	:new.status := 0;
end;
/

--produk
create or replace trigger kode_produk
before insert 
on mh_produk
for each row
declare
ctr number;
kode varchar2(20);
tgl varchar2(10);
begin
	select to_char(sysdate,'YYYYMMDD') into tgl from dual;
	select count(*) + 1 into ctr from mh_produk where kode_produk like '%' || tgl || '%';

	:new.kode_produk := 'PR_' || tgl || '_' || lpad(ctr,3,'0');
	:new.status := 2;
	:new.rating := 0;
end;
/

--history emoney
create or replace trigger KODE_HISTORY
before insert 
on history_emoney
for each row
declare
ctr number;
kode varchar2(20);
tgl varchar2(10);
begin
	select to_char(sysdate,'YYYYMMDD') into tgl from dual;
	select count(*) + 1 into ctr from history_emoney where KODE_HISTORY like '%' || tgl || '%';

	:new.KODE_HISTORY := 'HI_' || tgl || '_' || lpad(ctr,4,'0');
end;
/

--td_transaksi
create or replace trigger KODE_DTRANS
before insert
on dtrans
for each row
declare
ctr number;
begin
	select count(*) + 1 into ctr from dtrans where kode_dtrans like '%' || :new.fk_htrans ||'%';
	:new.kode_dtrans := :new.fk_htrans || '_' ||lpad(ctr,3,'0');
end;
/
