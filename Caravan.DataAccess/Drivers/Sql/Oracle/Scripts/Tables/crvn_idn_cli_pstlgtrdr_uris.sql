﻿-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_pstlgtrdr_uris
(
     CPLR_ID                NUMBER(10)      NOT NULL
   , CCLI_ID                NUMBER(10)      NOT NULL
   , CPLR_URI               NVARCHAR2(2000) NOT NULL

   , CONSTRAINT pk_crvn_idn_cli_pstlgtrdr_uris PRIMARY KEY (CPLR_ID) ENABLE
   , CONSTRAINT fk_crvnidncli_pstlgtrdu_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.crvn_idn_cli_pstlgtrdr_uris_id;
