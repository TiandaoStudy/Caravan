-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_idn_sco_secrets
(
     CSSE_ID                NUMBER(10)      NOT NULL
   , CSCO_ID                NUMBER(10)      NOT NULL
   , CSSE_VALUE             NVARCHAR2(250)  NOT NULL
   , CSSE_TYPE              NVARCHAR2(250)  DEFAULT 'SharedSecret'
   , CSSE_DESCR             NVARCHAR2(2000)      
   , CSSE_EXPIRATION        DATE  

   , CONSTRAINT pk_crvn_idn_sco_secrets PRIMARY KEY (CSSE_ID) ENABLE
   , CONSTRAINT fk_crvnidnsco_secrets_scopes FOREIGN KEY (CSCO_ID) REFERENCES mydb.crvn_idn_scopes (CSCO_ID) ON DELETE CASCADE ENABLE
);

CREATE SEQUENCE mydb.sq_crvn_idn_sco_secrets NOCACHE;

COMMENT ON COLUMN mydb.crvn_idn_sco_secrets.CSSE_VALUE 
     IS 'Hash SHA256 o SHA512 codificato in Base64';

CREATE OR REPLACE TRIGGER mydb.ti_crvn_idn_sco_secrets
BEFORE INSERT ON mydb.crvn_idn_sco_secrets 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_idn_sco_secrets.nextval
    INTO :new.CSSE_ID
    FROM DUAL;
END;
/