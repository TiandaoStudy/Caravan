-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.flex_log_settings
   (
      flos_type      VARCHAR2(10)   NOT NULL
    , flos_enabled   NUMBER(1)      NOT NULL
    , flos_days      NUMBER(3)      NOT NULL
    , CHECK (flos_type IN ('DEBUG', 'INFO', 'WARNING', 'ERROR', 'FATAL')) ENABLE
    , CHECK (flos_enabled IN (0,1)) ENABLE
    , CONSTRAINT pk_flex_log_settings PRIMARY KEY (flos_type) USING INDEX TABLESPACE dati_base_index ENABLE
   )
   TABLESPACE dati_base;

COMMENT ON TABLE mydb.flex_log_settings IS 'Tabelle delle impostazioni del sistema di logging delle applicazioni FINSA';
COMMENT ON COLUMN mydb.flex_log_settings.flos_type IS 'Tipo di logging, può assumere i valori DEBUG, INFO, WARNING, ERROR, FATAL';
COMMENT ON COLUMN mydb.flex_log_settings.flos_enabled IS 'Attivazione del logging, 0 spento, 1 acceso';
COMMENT ON COLUMN mydb.flex_log_settings.flos_days IS 'Numeri di giorni di persistenza della riga di log';


INSERT INTO mydb.flex_log_settings (flos_type,flos_enabled,flos_days) VALUES ('DEBUG','1','90');
INSERT INTO mydb.flex_log_settings (flos_type,flos_enabled,flos_days) VALUES ('INFO','1','90');
INSERT INTO mydb.flex_log_settings (flos_type,flos_enabled,flos_days) VALUES ('WARNING','1','90');
INSERT INTO mydb.flex_log_settings (flos_type,flos_enabled,flos_days) VALUES ('ERROR','1','90');
INSERT INTO mydb.flex_log_settings (flos_type,flos_enabled,flos_days) VALUES ('FATAL','1','90');
