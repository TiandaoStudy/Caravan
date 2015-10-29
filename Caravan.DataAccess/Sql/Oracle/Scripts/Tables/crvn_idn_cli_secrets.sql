-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_cli_secrets
(
     CCSE_ID                NUMBER(10)      NOT NULL
   , CCLI_ID                NUMBER(10)      NOT NULL
   , CCSE_VALUE             NVARCHAR2(250)  NOT NULL
   , CCSE_TYPE              NVARCHAR2(250)  
   , CCSE_DESCR             NVARCHAR2(2000)      
   , CCSE_EXPIRATION        NVARCHAR2(200)  

   , CONSTRAINT pk_crvn_idn_cli_secrets PRIMARY KEY (CCSE_ID) ENABLE
   , CONSTRAINT fk_crvnidncli_secrets_clients FOREIGN KEY (CCLI_ID) REFERENCES mydb.crvn_idn_clients (CCLI_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_cli_secrets NOCACHE;
