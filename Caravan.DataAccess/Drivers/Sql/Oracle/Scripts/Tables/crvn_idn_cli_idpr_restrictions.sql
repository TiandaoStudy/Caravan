-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_idpr_restrictions
(
     CCPR_ID                NUMBER(10)      NOT NULL
   , CCLI_ID                NUMBER(10)      NOT NULL
   , CCPR_PROVIDER          NVARCHAR2(200)  NOT NULL

   , CONSTRAINT pk_crvn_idn_cli_idpr_restrictions PRIMARY KEY (CCPR_ID) ENABLE
   , CONSTRAINT fk_crvnidncli_idprrestr_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_idn_cli_idpr_restrictions_id;
