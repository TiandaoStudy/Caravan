-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_sec_users
(
     cusr_id                        NUMBER(19)      NOT NULL
   , capp_id                        NUMBER(10)      NOT NULL 
   , cusr_login                     NVARCHAR2(32)   NOT NULL
   , cusr_hashed_pwd                NVARCHAR2(256)
   , cusr_active                    NUMBER(1)       DEFAULT 0 NOT NULL
   , cusr_first_name                NVARCHAR2(256)
   , cusr_last_name                 NVARCHAR2(256)
   , cusr_email                     NVARCHAR2(256)
   , cusr_email_confirmed           NUMBER(1)       DEFAULT 0 NOT NULL
   , cusr_security_stamp            NVARCHAR2(256)
   , cusr_lockout_enabled           NUMBER(1)       DEFAULT 1 NOT NULL
   , cusr_lockout_end_date          DATE            NOT NULL
   , cusr_access_failed_count       NUMBER(3)       DEFAULT 0 NOT NULL
   , cusr_two_factor_auth_enabled   NUMBER(1)       DEFAULT 0 NOT NULL

   -- INSERT tracking
   , TRCK_INSERT_DATE               DATE            NOT NULL
   , TRCK_INSERT_DB_USER            NVARCHAR2(32)   NOT NULL
   , TRCK_INSERT_APP_USER           NVARCHAR2(32) 
   
   -- UPDATE tracking
   , TRCK_UPDATE_DATE               DATE            
   , TRCK_UPDATE_DB_USER            NVARCHAR2(32)   
   , TRCK_UPDATE_APP_USER           NVARCHAR2(32) 

   , CHECK (cusr_login = lower(cusr_login)) ENABLE
   
   -- CHECKs for tracking
   , CHECK (TRCK_INSERT_DB_USER = LOWER(TRCK_INSERT_DB_USER)) ENABLE
   , CHECK (TRCK_INSERT_APP_USER = LOWER(TRCK_INSERT_APP_USER)) ENABLE
   , CHECK (TRCK_UPDATE_DB_USER = LOWER(TRCK_UPDATE_DB_USER)) ENABLE
   , CHECK (TRCK_UPDATE_APP_USER = LOWER(TRCK_UPDATE_APP_USER)) ENABLE
   , CHECK ((TRCK_UPDATE_DATE IS NULL AND TRCK_UPDATE_DB_USER IS NULL) OR (TRCK_UPDATE_DATE IS NOT NULL AND TRCK_UPDATE_DB_USER IS NOT NULL)) ENABLE
   , CHECK (TRCK_UPDATE_DATE IS NULL OR TRCK_UPDATE_DATE >= TRCK_INSERT_DATE) ENABLE

   , CONSTRAINT pk_crvn_sec_users PRIMARY KEY (cusr_id) ENABLE
   , CONSTRAINT uk_crvn_sec_users UNIQUE (capp_id, cusr_login) ENABLE
   , CONSTRAINT fk_crvnsecusers_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

COMMENT ON TABLE mydb.crvn_sec_users 
     IS 'Tabella che censisce gli utenti delle applicazioni FINSA';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_id 
     IS 'Identificativo riga, è una sequenza autoincrementale';
COMMENT ON COLUMN mydb.crvn_sec_users.capp_id 
     IS 'Identificativo della applicazione a cui un certo utente appartiene';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_login 
     IS 'La sigla usata per effettuare la login';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_hashed_pwd 
     IS 'Hash della password fissata per un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_active 
     IS 'Indica se un certo utente sia attivo o meno';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_first_name 
     IS 'Nome di un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_last_name 
     IS 'Cognome di un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_email 
     IS 'Indirizzo e-mail di un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_email_confirmed 
     IS 'Indica se il dato indirizzo e-mail sia stato confermato';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_phone 
     IS 'Numero di telefono di un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_phone_confirmed 
     IS 'Indica se il dato numero di telefono sia stato confermato';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_security_stamp 
     IS 'Rappresenta una sintesi delle informazioni di accesso per un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_lockout_enabled 
     IS 'Indica se ad un certo utente possa essere bloccata la login a fronte di un certo numero di accessi falliti';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_lockout_end_date 
     IS 'Indica la data di fine del blocco della login, valorizzare nel passato in fase di creazione utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_access_failed_count 
     IS 'Il numero di login fallite per un certo utente';
COMMENT ON COLUMN mydb.crvn_sec_users.cusr_two_factor_auth_enabled 
     IS 'Indica se un certo utente abbia abilitato la autenticazione a due fattori';

CREATE SEQUENCE mydb.crvn_sec_users_id NOCACHE;

CREATE OR REPLACE TRIGGER mydb.ti_crvn_sec_users
BEFORE INSERT ON mydb.crvn_sec_users 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_users_id.nextval, lower(:new.cusr_login), mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysdate_utc, mydb.pck_caravan_utils.f_get_sysuser, NULL, NULL
    INTO :new.cusr_id, :new.cusr_login, :new.cusr_lockout_end_date, :new.TRCK_INSERT_DATE, :new.TRCK_INSERT_DB_USER, :new.TRCK_UPDATE_DATE, :new.TRCK_UPDATE_DB_USER
    FROM DUAL;
END;
/

create or replace TRIGGER mydb.tu_crvn_sec_users
BEFORE UPDATE ON mydb.crvn_sec_users 
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