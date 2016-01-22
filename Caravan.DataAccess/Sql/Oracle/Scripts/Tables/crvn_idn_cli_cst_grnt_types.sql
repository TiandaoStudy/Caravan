-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_cst_grnt_types
(
     CCGT_ID                NUMBER(10)      NOT NULL
   , CCLI_ID                NUMBER(10)      NOT NULL
   , CCGT_GRANT_TYPE        NVARCHAR2(250)  NOT NULL

   , CONSTRAINT pk_crvn_idn_cli_cst_grnt_types PRIMARY KEY (CCGT_ID) ENABLE
   , CONSTRAINT fk_crvnidncli_cstgrntt_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_cli_cst_grnt_types NOCACHE;
