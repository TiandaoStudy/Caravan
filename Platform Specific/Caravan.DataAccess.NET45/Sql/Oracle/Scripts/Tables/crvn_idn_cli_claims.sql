-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_claims
(
     CCLM_ID        NUMBER(10)      NOT NULL
   , CCLI_ID        NUMBER(10)      NOT NULL
   , CCLM_TYPE      NVARCHAR2(250)  NOT NULL
   , CCLM_VALUE     NVARCHAR2(250)  NOT NULL

   , CONSTRAINT pk_crvn_idn_cli_claims PRIMARY KEY (CCLI_ID) ENABLE
   , CONSTRAINT fk_crvnidn_cliclaims_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_cli_claims NOCACHE;