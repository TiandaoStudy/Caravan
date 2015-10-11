-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_idpr_restrs
(
     CCPR_ID                NUMBER(10)      NOT NULL
   , CCLI_ID                NUMBER(10)      NOT NULL
   , CCPR_PROVIDER          NVARCHAR2(200)  NOT NULL

   , CONSTRAINT pk_crvn_idn_cli_idpr_restrs PRIMARY KEY (CCPR_ID) ENABLE
   , CONSTRAINT fk_crvnidncli_idprrst_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_cli_idpr_restrs;
