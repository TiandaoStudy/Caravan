-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_claims
(
     ccla_id          NUMBER(19)      NOT NULL
   , cusr_id          NUMBER(19)      NOT NULL 
   , ccla_hash        NVARCHAR2(32)   NOT NULL
   , ccla_claim       NCLOB           NOT NULL

   -- INSERT tracking
   , TRCK_INSERT_DATE               DATE            NOT NULL
   , TRCK_INSERT_DB_USER            NVARCHAR2(32)   NOT NULL
   , TRCK_INSERT_APP_USER           NVARCHAR2(32) 
   
   -- UPDATE tracking
   , TRCK_UPDATE_DATE               DATE            
   , TRCK_UPDATE_DB_USER            NVARCHAR2(32)   
   , TRCK_UPDATE_APP_USER           NVARCHAR2(32) 
   
   -- CHECKs for tracking
   , CHECK (TRCK_INSERT_DB_USER = LOWER(TRCK_INSERT_DB_USER)) ENABLE
   , CHECK (TRCK_INSERT_APP_USER = LOWER(TRCK_INSERT_APP_USER)) ENABLE
   , CHECK (TRCK_UPDATE_DB_USER = LOWER(TRCK_UPDATE_DB_USER)) ENABLE
   , CHECK (TRCK_UPDATE_APP_USER = LOWER(TRCK_UPDATE_APP_USER)) ENABLE
   , CHECK ((TRCK_UPDATE_DATE IS NULL AND TRCK_UPDATE_DB_USER IS NULL) OR (TRCK_UPDATE_DATE IS NOT NULL AND TRCK_UPDATE_DB_USER IS NOT NULL)) ENABLE
   , CHECK (TRCK_UPDATE_DATE IS NULL OR TRCK_UPDATE_DATE >= TRCK_INSERT_DATE) ENABLE

   , CONSTRAINT pk_crvn_sec_claims PRIMARY KEY (ccla_id) ENABLE
   , CONSTRAINT uk_crvn_sec_claims UNIQUE (cusr_id, ccla_hash) ENABLE
   , CONSTRAINT fk_crvnsecclm_crvnsecusr FOREIGN KEY (cusr_id) REFERENCES mydb.crvn_sec_users (cusr_id) ON DELETE CASCADE ENABLE
);

COMMENT ON TABLE mydb.crvn_sec_claims 
     IS 'Tabella che censisce i claim degli utenti delle applicazioni FINSA';
COMMENT ON COLUMN mydb.crvn_sec_claims.ccla_id 
     IS 'Identificativo riga, è una sequenza autoincrementale';
COMMENT ON COLUMN mydb.crvn_sec_claims.cusr_id 
     IS 'Identificativo dello utente a cui un certo claim appartiene';
COMMENT ON COLUMN mydb.crvn_sec_claims.ccla_hash 
     IS 'Hash prodotto dopo la serializzazione del claim';
COMMENT ON COLUMN mydb.crvn_sec_claims.ccla_claim 
     IS 'Il claim serializzato';

CREATE SEQUENCE mydb.sq_crvn_sec_claims NOCACHE;

CREATE OR REPLACE TRIGGER mydb.ti_crvn_sec_claims
BEFORE INSERT ON mydb.crvn_sec_claims 
FOR EACH ROW
BEGIN
  SELECT mydb.sq_crvn_sec_claims.nextval, mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser, NULL, NULL
    INTO :new.cusr_id, :new.TRCK_INSERT_DATE, :new.TRCK_INSERT_DB_USER, :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/

create or replace TRIGGER mydb.tu_crvn_sec_claims
BEFORE UPDATE ON mydb.crvn_sec_claims 
FOR EACH ROW
BEGIN
  IF UPDATING('TRCK_INSERT_DATE') 
  OR UPDATING('TRCK_INSERT_DB_USER') 
  OR UPDATING('TRCK_UPDATE_DATE') 
  OR UPDATING('TRCK_UPDATE_DB_USER') 
  THEN
    mydb.pck_caravan_utils.sp_err_when_updating_trck_cols;
  END IF;

  SELECT mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser
    INTO :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/