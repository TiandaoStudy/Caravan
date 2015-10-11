-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_cors_origins
(
     CCCO_ID                NUMBER(10)      NOT NULL
   , CCLI_ID                NUMBER(10)      NOT NULL
   , CCCO_ORIGIN            NVARCHAR2(150)  NOT NULL

   , CONSTRAINT pk_crvn_idn_cli_cors_origins PRIMARY KEY (CCLI_ID) ENABLE
   , CONSTRAINT fk_crvnidn_clicorsorig_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_cli_cors_origins;
