/*==============================================================*/
/* DBMS name:      ORACLE Version 11g                           */
/* Created on:     11/5/2016 9:39:03 PM                         */
/*==============================================================*/


alter table H_TRANS
   drop constraint FK_H_TRANS_REFERENCE_WP_MEMIL;

alter table H_TRANS
   drop constraint FK_H_TRANS_REFERENCE_WH_MEMIL;

alter table H_TRANS
   drop constraint FK_H_TRANS_REFERENCE_WK_MEMIL;

alter table WH_MEMILIKI
   drop constraint FK_WH_MEMIL_WH_MEMILI_WARGA;

alter table WH_MEMILIKI
   drop constraint FK_WH_MEMIL_WH_MEMILI_HARTA;

alter table WK_MEMILIKI
   drop constraint FK_WK_MEMIL_WK_MEMILI_WARGA;

alter table WK_MEMILIKI
   drop constraint FK_WK_MEMIL_WK_MEMILI_KENDARAA;

alter table WP_MEMILIKI
   drop constraint FK_WP_MEMIL_WP_MEMILI_WARGA;

alter table WP_MEMILIKI
   drop constraint FK_WP_MEMIL_WP_MEMILI_PBB;

drop table HARTA cascade constraints purge;

drop table H_TRANS cascade constraints purge;

drop table KENDARAAN cascade constraints purge;

drop table PBB cascade constraints;

drop table SYSTEMUSER cascade constraints purge;

drop table WARGA cascade constraints purge;

drop index WH_MEMILIKI2_FK;

drop index WH_MEMILIKI_FK;

drop table WH_MEMILIKI cascade constraints purge;

drop index WK_MEMILIKI2_FK;

drop index WK_MEMILIKI_FK;

drop table WK_MEMILIKI cascade constraints purge;

drop index WP_MEMILIKI2_FK;

drop index WP_MEMILIKI_FK;

drop table WP_MEMILIKI cascade constraints purge;

/*==============================================================*/
/* Table: HARTA                                                 */
/*==============================================================*/
create table HARTA 
(
   H_ID                 INTEGER              not null,
   H_STATUS             VARCHAR2(30)         not null,
   H_BIAYA              INTEGER              not null,
   H_KETERANGAN         VARCHAR2(255),
   constraint PK_HARTA primary key (H_ID)
);

/*==============================================================*/
/* Table: H_TRANS                                               */
/*==============================================================*/
create table H_TRANS 
(
   HT_ID                VARCHAR2(10)         not null,
   ID_WK                INTEGER,
   ID_WP                INTEGER,
   ID_WH                INTEGER,
   HT_TANGGAL           DATE                 not null,
   HT_TOTAL             INTEGER              not null,
   HT_KETERANGAN        VARCHAR2(255),
   constraint PK_H_TRANS primary key (HT_ID)
);

/*==============================================================*/
/* Table: KENDARAAN                                             */
/*==============================================================*/
create table KENDARAAN 
(
   K_ID                 INTEGER              not null,
   K_KATEGORI           VARCHAR2(255)        not null,
   K_BIAYA              INTEGER              not null,
   K_KETERANGAN         VARCHAR2(255),
   constraint PK_KENDARAAN primary key (K_ID)
);

/*==============================================================*/
/* Table: PBB                                                   */
/*==============================================================*/
create table PBB 
(
   PBB_ID               INTEGER              not null,
   PBB_TIPE             VARCHAR2(255)        not null,
   PBB_BIAYA            INTEGER              not null,
   PBB_KETERANGA        VARCHAR2(255),
   constraint PK_PBB primary key (PBB_ID)
);

/*==============================================================*/
/* Table: SYSTEMUSER                                            */
/*==============================================================*/
create table SYSTEMUSER 
(
   USERNAME             VARCHAR2(20)         not null,
   PASSWORD             VARCHAR2(20)         not null,
   ROLE                 VARCHAR2(20),
   constraint PK_SYSTEMUSER primary key (USERNAME)
);

/*==============================================================*/
/* Table: WARGA                                                 */
/*==============================================================*/
create table WARGA 
(
   NIK                  VARCHAR2(16)         not null,
   NAMA                 VARCHAR2(255)        not null,
   ALAMAT               VARCHAR2(255)        not null,
   TGL_LAHIR            DATE                 not null,
   JK                   VARCHAR(1)           not null,
   TELP                 VARCHAR2(14)         not null,
   K_KETERANGAN         VARCHAR2(255),
   constraint PK_WARGA primary key (NIK)
);

/*==============================================================*/
/* Table: WH_MEMILIKI                                           */
/*==============================================================*/
create table WH_MEMILIKI 
(
   ID_WH                INTEGER              not null,
   NIK                  VARCHAR2(16)         not null,
   H_ID                 INTEGER              not null,
   constraint PK_WH_MEMILIKI primary key (ID_WH)
);

/*==============================================================*/
/* Index: WH_MEMILIKI_FK                                        */
/*==============================================================*/
create index WH_MEMILIKI_FK on WH_MEMILIKI (
   NIK ASC
);

/*==============================================================*/
/* Index: WH_MEMILIKI2_FK                                       */
/*==============================================================*/
create index WH_MEMILIKI2_FK on WH_MEMILIKI (
   H_ID ASC
);

/*==============================================================*/
/* Table: WK_MEMILIKI                                           */
/*==============================================================*/
create table WK_MEMILIKI 
(
   ID_WK                INTEGER              not null,
   NIK                  VARCHAR2(16)         not null,
   K_ID                 INTEGER              not null,
   constraint PK_WK_MEMILIKI primary key (ID_WK)
);

/*==============================================================*/
/* Index: WK_MEMILIKI_FK                                        */
/*==============================================================*/
create index WK_MEMILIKI_FK on WK_MEMILIKI (
   NIK ASC
);

/*==============================================================*/
/* Index: WK_MEMILIKI2_FK                                       */
/*==============================================================*/
create index WK_MEMILIKI2_FK on WK_MEMILIKI (
   K_ID ASC
);

/*==============================================================*/
/* Table: WP_MEMILIKI                                           */
/*==============================================================*/
create table WP_MEMILIKI 
(
   ID_WP                INTEGER              not null,
   NIK                  VARCHAR2(16)         not null,
   PBB_ID               INTEGER              not null,
   constraint PK_WP_MEMILIKI primary key (ID_WP)
);

/*==============================================================*/
/* Index: WP_MEMILIKI_FK                                        */
/*==============================================================*/
create index WP_MEMILIKI_FK on WP_MEMILIKI (
   NIK ASC
);

/*==============================================================*/
/* Index: WP_MEMILIKI2_FK                                       */
/*==============================================================*/
create index WP_MEMILIKI2_FK on WP_MEMILIKI (
   PBB_ID ASC
);

alter table H_TRANS
   add constraint FK_H_TRANS_REFERENCE_WP_MEMIL foreign key (ID_WP)
      references WP_MEMILIKI (ID_WP) on delete cascade;

alter table H_TRANS
   add constraint FK_H_TRANS_REFERENCE_WH_MEMIL foreign key (ID_WH)
      references WH_MEMILIKI (ID_WH) on delete cascade;

alter table H_TRANS
   add constraint FK_H_TRANS_REFERENCE_WK_MEMIL foreign key (ID_WK)
      references WK_MEMILIKI (ID_WK) on delete cascade;

alter table WH_MEMILIKI
   add constraint FK_WH_MEMIL_WH_MEMILI_WARGA foreign key (NIK)
      references WARGA (NIK) on delete cascade;

alter table WH_MEMILIKI
   add constraint FK_WH_MEMIL_WH_MEMILI_HARTA foreign key (H_ID)
      references HARTA (H_ID) on delete cascade;

alter table WK_MEMILIKI
   add constraint FK_WK_MEMIL_WK_MEMILI_WARGA foreign key (NIK)
      references WARGA (NIK) on delete cascade;

alter table WK_MEMILIKI
   add constraint FK_WK_MEMIL_WK_MEMILI_KENDARAA foreign key (K_ID)
      references KENDARAAN (K_ID) on delete cascade;

alter table WP_MEMILIKI
   add constraint FK_WP_MEMIL_WP_MEMILI_WARGA foreign key (NIK)
      references WARGA (NIK) on delete cascade;

alter table WP_MEMILIKI
   add constraint FK_WP_MEMIL_WP_MEMILI_PBB foreign key (PBB_ID)
      references PBB (PBB_ID) on delete cascade;

INSERT INTO systemuser VALUES ('admin', 'admin', 'admin');
INSERT INTO systemuser VALUES ('administrasi', 'administrasi', 'administrasi');

INSERT INTO HARTA VALUES (0, 'TIDAK BAYAR', 0, 'BEBAS PAJAK');
INSERT INTO HARTA VALUES (1, 'GOLONGAN BAWAH', 100000, 'BAYAR PAJAK');
INSERT INTO HARTA VALUES (2, 'GOLONGAN MENENGAH', 300000, 'BAYAR PAJAK');
INSERT INTO HARTA VALUES (3, 'GOLONGAN ATAS', 500000, 'BAYAR PAJAK');

INSERT INTO KENDARAAN VALUES (0, 'TIDAK BAYAR', 0, 'BEBAS PAJAK');
INSERT INTO KENDARAAN VALUES (1, '< 2000', 50000, 'BAYAR PAJAK');
INSERT INTO KENDARAAN VALUES (2, '< 2010', 150000, 'BAYAR PAJAK');
INSERT INTO KENDARAAN VALUES (3, '< 2020', 250000, 'BAYAR PAJAK');

INSERT INTO PBB VALUES (0, 'TIDAK BAYAR', 0, 'BEBAS PAJAK');
INSERT INTO PBB VALUES (1, '< 10 M2', 100000, 'BAYAR PAJAK');
INSERT INTO PBB VALUES (2, '< 30 M2', 300000, 'BAYAR PAJAK');
INSERT INTO PBB VALUES (3, '< 50 M2', 500000, 'BAYAR PAJAK');
INSERT INTO PBB VALUES (4, '> 51 M2', 1000000, 'BAYAR PAJAK');

INSERT INTO WARGA VALUES ('0031000000000004', 'Abbie', '-', to_date('29-9-1951', 'DD-MM-YYYY'), 1, 4983822, NULL);
INSERT INTO WARGA VALUES ('0031000000000007', 'Abby', '-', to_date('18-3-1966', 'DD-MM-YYYY'), 0, 4650268, NULL);
INSERT INTO WARGA VALUES ('0031000000000013', 'Abel', '-', to_date('8-8-1971', 'DD-MM-YYYY'), 0, 6637788, NULL);
INSERT INTO WARGA VALUES ('0031000000000020', 'Adam', '-', to_date('19-1-1961', 'DD-MM-YYYY'), 1, 4914395, NULL);
INSERT INTO WARGA VALUES ('0031000000000022', 'Addie', '-', to_date('21-5-1964', 'DD-MM-YYYY'), 1, 4917896, NULL);
INSERT INTO WARGA VALUES ('0031000000000024', 'Aditya', '-', to_date('16-9-1986', 'DD-MM-YYYY'), 1, 4019763, NULL);
INSERT INTO WARGA VALUES ('0031000000000025', 'Adlai', '-', to_date('23-4-1953', 'DD-MM-YYYY'), 0, 6752721, NULL);
INSERT INTO WARGA VALUES ('0031000000000026', 'Adnan', '-', to_date('6-10-1984', 'DD-MM-YYYY'), 0, 8371946, NULL);
INSERT INTO WARGA VALUES ('0031000000000030', 'Adolphe', '-', to_date('15-6-1976', 'DD-MM-YYYY'), 0, 1987136, NULL);
INSERT INTO WARGA VALUES ('0031000000000035', 'Adrien', '-', to_date('5-6-1952', 'DD-MM-YYYY'), 0, 2990487, NULL);
INSERT INTO WARGA VALUES ('0031000000000041', 'Ahmad', '-', to_date('28-11-1967', 'DD-MM-YYYY'), 0, 6476195, NULL);
INSERT INTO WARGA VALUES ('0031000000000046', 'Al', '-', to_date('11-12-1964', 'DD-MM-YYYY'), 0, 7266705, NULL);
INSERT INTO WARGA VALUES ('0031000000000049', 'Alan', '-', to_date('4-4-1964', 'DD-MM-YYYY'), 1, 5243635, NULL);

INSERT INTO WP_MEMILIKI VALUES (4, '0031000000000004', 0);
INSERT INTO WP_MEMILIKI VALUES (7, '0031000000000007', 1);
INSERT INTO WP_MEMILIKI VALUES (13, '0031000000000013', 0);
INSERT INTO WP_MEMILIKI VALUES (20, '0031000000000020', 2);
INSERT INTO WP_MEMILIKI VALUES (22, '0031000000000022', 3);
INSERT INTO WP_MEMILIKI VALUES (24, '0031000000000024', 0);
INSERT INTO WP_MEMILIKI VALUES (25, '0031000000000025', 3);
INSERT INTO WP_MEMILIKI VALUES (26, '0031000000000026', 0);
INSERT INTO WP_MEMILIKI VALUES (30, '0031000000000030', 0);
INSERT INTO WP_MEMILIKI VALUES (35, '0031000000000035', 2);
INSERT INTO WP_MEMILIKI VALUES (41, '0031000000000041', 0);
INSERT INTO WP_MEMILIKI VALUES (46, '0031000000000046', 0);
INSERT INTO WP_MEMILIKI VALUES (49, '0031000000000049', 0);

INSERT INTO WH_MEMILIKI VALUES (4, '0031000000000004', 2);
INSERT INTO WH_MEMILIKI VALUES (7, '0031000000000007', 3);
INSERT INTO WH_MEMILIKI VALUES (13, '0031000000000013', 3);
INSERT INTO WH_MEMILIKI VALUES (20, '0031000000000020', 3);
INSERT INTO WH_MEMILIKI VALUES (22, '0031000000000022', 0);
INSERT INTO WH_MEMILIKI VALUES (24, '0031000000000024', 0);
INSERT INTO WH_MEMILIKI VALUES (25, '0031000000000025', 3);
INSERT INTO WH_MEMILIKI VALUES (26, '0031000000000026', 0);
INSERT INTO WH_MEMILIKI VALUES (30, '0031000000000030', 0);
INSERT INTO WH_MEMILIKI VALUES (35, '0031000000000035', 0);
INSERT INTO WH_MEMILIKI VALUES (46, '0031000000000046', 0);
INSERT INTO WH_MEMILIKI VALUES (49, '0031000000000049', 0);

INSERT INTO WK_MEMILIKI VALUES (4, '0031000000000004', 0);
INSERT INTO WK_MEMILIKI VALUES (7, '0031000000000007', 0);
INSERT INTO WK_MEMILIKI VALUES (13, '0031000000000013', 2);
INSERT INTO WK_MEMILIKI VALUES (20, '0031000000000020', 0);
INSERT INTO WK_MEMILIKI VALUES (22, '0031000000000022', 0);
INSERT INTO WK_MEMILIKI VALUES (24, '0031000000000024', 0);
INSERT INTO WK_MEMILIKI VALUES (25, '0031000000000025', 0);
INSERT INTO WK_MEMILIKI VALUES (26, '0031000000000026', 0);
INSERT INTO WK_MEMILIKI VALUES (30, '0031000000000030', 3);
INSERT INTO WK_MEMILIKI VALUES (35, '0031000000000035', 0);
INSERT INTO WK_MEMILIKI VALUES (46, '0031000000000046', 1);
INSERT INTO WK_MEMILIKI VALUES (49, '0031000000000049', 0);

commit;