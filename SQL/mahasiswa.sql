
-- Database: `TestAssesment`
--
CREATE DATABASE TestAssesment
-- --------------------------------------------------------

--
-- Struktur dari tabel `mahasiswa`
--
--drop TABLE mahasiswa
CREATE TABLE mahasiswa (
  id int identity NOT NULL,
  name varchar(100) NOT NULL,
  nrp varchar(100) NOT NULL,
  email varchar(100) NOT NULL,
  jurusan varchar(100) NOT NULL,
  dosen_wali_id int
    PRIMARY KEY (id),
    CONSTRAINT FK_dosen_wali FOREIGN KEY (dosen_wali_id)
	REFERENCES dosen_wali(id)
) 

--
-- Data untuk tabel `mahasiswa`
--

INSERT INTO mahasiswa (name, nrp, email, jurusan, dosen_wali_id) VALUES
('Hery', '109696', 'heryanakasih@gmail.com', 'Teknik Informatika', 1),
('Setiawanto', '109699', 'user001@gmail.com', 'Teknik Pangan', 2),
('Heryama', '22222', 'user002@gmail.com', 'Teknik Industri', 2),
('Heryana Kasih Setiawan', '22222', 'user003@gmail.com', 'Teknik Informatika', 3)

select * from mahasiswa


--drop TABLE dosen_wali
CREATE TABLE dosen_wali (
  id int identity NOT NULL,
  nama_dosen varchar(100) NOT NULL,
  nidn varchar(100) NOT NULL
  PRIMARY KEY (id),
) 



INSERT INTO dosen_wali (nama_dosen, nidn) VALUES
('Hery S.Kom', '999999'),
('Wanto ST', '8888888'), 
('Hera ST', '222222')

select * from dosen_wali
