-- REPLACE 'mydb' WITH DB NAME

CREATE TABLE mydb.crvn_log_settings
(
     capp_id          NUMBER(19)      NOT NULL
   , clos_type        NVARCHAR2(8)    NOT NULL
   , clos_enabled     NUMBER(1)       NOT NULL
   , clos_days        NUMBER(3)       NOT NULL
   , clos_max_entries NUMBER(7)       NOT NULL
   , CHECK (clos_type IN ('debug', 'trace', 'info', 'warn', 'error', 'fatal')) ENABLE
   , CHECK (clos_enabled IN (0, 1)) ENABLE
   , CHECK (clos_days > 0 AND clos_max_entries > 0) ENABLE
   , CONSTRAINT pk_crvn_log_settings PRIMARY KEY (capp_id, clos_type) ENABLE
   , CONSTRAINT fk_crvnlogsettings_crvnsecapps FOREIGN KEY (capp_id) REFERENCES mydb.crvn_sec_apps (capp_id) ON DELETE CASCADE ENABLE
);

COMMENT ON TABLE mydb.crvn_log_settings 
     IS 'Tabella delle impostazioni del sistema di log delle applicazioni FINSA';
COMMENT ON COLUMN mydb.crvn_log_entries.clos_type 
     IS 'Livello del messaggio di log, può assumere i valori debug, trace, info, warn, error, fatal';
COMMENT ON COLUMN mydb.crvn_log_settings.capp_id 
     IS 'Applicazione relativa alla riga di impostazioni';
COMMENT ON COLUMN mydb.crvn_log_settings.clos_enabled 
     IS 'Attivazione del log - 0 spento, 1 acceso';
COMMENT ON COLUMN mydb.crvn_log_settings.clos_days 
     IS 'Numeri di giorni di persistenza della riga di log';
COMMENT ON COLUMN mydb.crvn_log_settings.clos_max_entries 
     IS 'Massimo numero di righe presenti nel log per il tipo e la applicazione';