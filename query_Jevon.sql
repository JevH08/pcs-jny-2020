INSERT INTO MH_PRODUK(nama_produk, desc_barang, fk_kategori, fk_penjual, stok, harga, berat, kondisi, tag, status) 
values('Kopi Kapal Api', 'Kopi murah tidak bikin Ngantuk', 'KA_006', US20200426_004, 100, 6000, 100, 0, '#kopi', 2);

--panggil htrans
exec autogenht

INSERT INTO htrans(kode_htrans, tgl_transaksi, berat, subtotal, fk_pelanggan, status) values()

INSERT INTO dtrans(fk_htrans, fk_produk, jumlah, harga, subtotal, status) values()