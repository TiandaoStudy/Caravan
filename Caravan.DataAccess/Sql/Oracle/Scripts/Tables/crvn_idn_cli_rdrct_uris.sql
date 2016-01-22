-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_rdrct_uris
(
     CCRU_ID                NUMBER(10)      NOT NULL
   , CCLI_ID                NUMBER(10)      NOT NULL
   , CCRU_URI               NVARCHAR2(2000) NOT NULL

   , CONSTRAINT pk_crvn_idn_cli_rdrct_uris PRIMARY KEY (CCRU_ID) ENABLE
   , CONSTRAINT fk_crvnidncli_rdrctu_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_cli_rdrct_uris NOCACHE;
