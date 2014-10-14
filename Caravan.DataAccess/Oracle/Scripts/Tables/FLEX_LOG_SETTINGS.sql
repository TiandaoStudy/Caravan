-- REPLACE 'mydb' WITH DB NAME
-- REPLACE 'myapp' WITH APP NAME

CREATE TABLE mydb.flex_log_settings
   (
      flos_type        CHAR(5 CHAR)        NOT NULL
    , flos_application VARCHAR2(30 CHAR)   NOT NULL
    , flos_enabled     NUMBER(1)           NOT NULL
    , flos_days        NUMBER(3)           NOT NULL
    , flos_max_entries NUMBER(5)           NOT NULL
    , CHECK (flos_type IN ('debug', 'info', 'warn', 'error', 'fatal')) ENABLE
    , CHECK (flos_enabled IN (0,1)) ENABLE
    , CHECK (flos_days > 0 AND flos_max_entries > 0) ENABLE
    , CONSTRAINT pk_flex_log_settings PRIMARY KEY (flos_type, flos_application) USING INDEX TABLESPACE dati_base_index ENABLE
   )
   TABLESPACE dati_base;

COMMENT ON TABLE mydb.flex_log_settings IS 'Tabelle delle impostazioni del sistema di logging delle applicazioni FINSA';
COMMENT ON COLUMN mydb.flex_log_settings.flos_type IS 'Tipo di logging, può assumere i valori DEBUG, INFO, WARN, ERROR, FATAL';
COMMENT ON COLUMN mydb.flex_log_settings.flos_application IS 'Applicazione relativa alla riga di impostazioni';
COMMENT ON COLUMN mydb.flex_log_settings.flos_enabled IS 'Attivazione del logging, 0 spento, 1 acceso';
COMMENT ON COLUMN mydb.flex_log_settings.flos_days IS 'Numeri di giorni di persistenza della riga di log';
COMMENT ON COLUMN mydb.flex_log_settings.flos_max_entries IS 'Massimo numero di righe presenti nel log per il tipo e la applicazione';

INSERT INTO mydb.flex_log_settings (flos_type, flos_application, flos_enabled, flos_days, flos_max_entries) VALUES ('debug', 'myapp', 1, 90, 1000);
INSERT INTO mydb.flex_log_settings (flos_type, flos_application, flos_enabled, flos_days, flos_max_entries) VALUES ('info', 'myapp', 1, 90, 1000);
INSERT INTO mydb.flex_log_settings (flos_type, flos_application, flos_enabled, flos_days, flos_max_entries) VALUES ('warn', 'myapp', 1, 90, 1000);
INSERT INTO mydb.flex_log_settings (flos_type, flos_application, flos_enabled, flos_days, flos_max_entries) VALUES ('error', 'myapp', 1, 90, 1000);
INSERT INTO mydb.flex_log_settings (flos_type, flos_application, flos_enabled, flos_days, flos_max_entries) VALUES ('fatal', 'myapp', 1, 90, 1000);
