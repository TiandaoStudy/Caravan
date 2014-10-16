-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.caravan_log_settings
(
     clos_type        NVARCHAR2(5)    NOT NULL
   , capp_id          NUMBER          NOT NULL
   , clos_enabled     NUMBER(1)       NOT NULL
   , clos_days        NUMBER(3)       NOT NULL
   , clos_max_entries NUMBER(7)       NOT NULL
   , CHECK (clos_type IN ('debug', 'info', 'warn', 'error', 'fatal')) ENABLE
   , CHECK (clos_enabled IN (0,1)) ENABLE
   , CHECK (clos_days > 0 AND clos_max_entries > 0) ENABLE
   , CONSTRAINT pk_caravan_log_settings PRIMARY KEY (clos_type, capp_id) USING INDEX TABLESPACE dati_base_index ENABLE
   , CONSTRAINT fk_crvnlogsettings_crvnsecapp FOREIGN KEY (capp_id) REFERENCES mydb.caravan_sec_app (capp_id) ON DELETE CASCADE ENABLE
)
TABLESPACE dati_base;

COMMENT ON TABLE mydb.caravan_log_settings IS 'Tabelle delle impostazioni del sistema di logging delle applicazioni FINSA';
COMMENT ON COLUMN mydb.caravan_log_settings.clos_type IS 'Tipo di logging, può assumere i valori debug, info, warn, error, fatal';
COMMENT ON COLUMN mydb.caravan_log_settings.capp_id IS 'Applicazione relativa alla riga di impostazioni';
COMMENT ON COLUMN mydb.caravan_log_settings.clos_enabled IS 'Attivazione del logging, 0 spento, 1 acceso';
COMMENT ON COLUMN mydb.caravan_log_settings.clos_days IS 'Numeri di giorni di persistenza della riga di log';
COMMENT ON COLUMN mydb.caravan_log_settings.clos_max_entries IS 'Massimo numero di righe presenti nel log per il tipo e la applicazione';

INSERT INTO mydb.caravan_log_settings (clos_type, capp_id, clos_enabled, clos_days, clos_max_entries) VALUES ('debug', 0, 1, 90, 1000);
INSERT INTO mydb.caravan_log_settings (clos_type, capp_id, clos_enabled, clos_days, clos_max_entries) VALUES ('info', 0, 1, 90, 1000);
INSERT INTO mydb.caravan_log_settings (clos_type, capp_id, clos_enabled, clos_days, clos_max_entries) VALUES ('warn', 0, 1, 90, 1000);
INSERT INTO mydb.caravan_log_settings (clos_type, capp_id, clos_enabled, clos_days, clos_max_entries) VALUES ('error', 0, 1, 90, 1000);
INSERT INTO mydb.caravan_log_settings (clos_type, capp_id, clos_enabled, clos_days, clos_max_entries) VALUES ('fatal', 0, 1, 90, 1000);
