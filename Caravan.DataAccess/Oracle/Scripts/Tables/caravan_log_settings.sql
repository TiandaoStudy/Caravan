-- REPLACE 'mydb' WITH DB NAME
-- REPLACE 'myapp' WITH APP NAME

CREATE TABLE mydb.caravan_log_settings
(
     clos_type        VARCHAR2(5 CHAR)    NOT NULL
   , clos_application VARCHAR2(30 CHAR)   NOT NULL
   , clos_enabled     NUMBER(1)           NOT NULL
   , clos_days        NUMBER(3)           NOT NULL
   , clos_max_entries NUMBER(5)           NOT NULL
   , CHECK (clos_type IN ('debug', 'info', 'warn', 'error', 'fatal')) ENABLE
   , CHECK (clos_enabled IN (0,1)) ENABLE
   , CHECK (clos_days > 0 AND clos_max_entries > 0) ENABLE
   , CONSTRAINT pk_caravan_log_settings PRIMARY KEY (clos_type, clos_application) USING INDEX TABLESPACE dati_base_index ENABLE
)
TABLESPACE dati_base;

COMMENT ON TABLE mydb.caravan_log_settings IS 'Tabelle delle impostazioni del sistema di logging delle applicazioni FINSA';
COMMENT ON COLUMN mydb.caravan_log_settings.clos_type IS 'Tipo di logging, può assumere i valori debug, info, warn, error, fatal';
COMMENT ON COLUMN mydb.caravan_log_settings.clos_application IS 'Applicazione relativa alla riga di impostazioni';
COMMENT ON COLUMN mydb.caravan_log_settings.clos_enabled IS 'Attivazione del logging, 0 spento, 1 acceso';
COMMENT ON COLUMN mydb.caravan_log_settings.clos_days IS 'Numeri di giorni di persistenza della riga di log';
COMMENT ON COLUMN mydb.caravan_log_settings.clos_max_entries IS 'Massimo numero di righe presenti nel log per il tipo e la applicazione';

INSERT INTO mydb.caravan_log_settings (clos_type, clos_application, clos_enabled, clos_days, clos_max_entries) VALUES ('debug', 'myapp', 1, 90, 1000);
INSERT INTO mydb.caravan_log_settings (clos_type, clos_application, clos_enabled, clos_days, clos_max_entries) VALUES ('info', 'myapp', 1, 90, 1000);
INSERT INTO mydb.caravan_log_settings (clos_type, clos_application, clos_enabled, clos_days, clos_max_entries) VALUES ('warn', 'myapp', 1, 90, 1000);
INSERT INTO mydb.caravan_log_settings (clos_type, clos_application, clos_enabled, clos_days, clos_max_entries) VALUES ('error', 'myapp', 1, 90, 1000);
INSERT INTO mydb.caravan_log_settings (clos_type, clos_application, clos_enabled, clos_days, clos_max_entries) VALUES ('fatal', 'myapp', 1, 90, 1000);
