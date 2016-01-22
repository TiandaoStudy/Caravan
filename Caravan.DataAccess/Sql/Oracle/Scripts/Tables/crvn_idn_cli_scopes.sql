-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_scopes
(
     CCSC_ID                NUMBER(10)      NOT NULL
   , CCLI_ID                NUMBER(10)      NOT NULL
   , CSCO_SCOPE_NAME        NVARCHAR2(200)  NOT NULL

   , CONSTRAINT pk_crvn_idn_cli_scopes PRIMARY KEY (CCSC_ID) ENABLE
   , CONSTRAINT fk_crvnidncli_scopes_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_cli_scopes NOCACHE;
