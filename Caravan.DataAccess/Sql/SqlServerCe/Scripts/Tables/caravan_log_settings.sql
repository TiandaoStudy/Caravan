-- REPLACE 'dbo' WITH SCHEMA NAME

CREATE TABLE caravan_log_settings
(
     clos_type        NVARCHAR(5)     NOT NULL
   , capp_id          BIGINT          NOT NULL
   , clos_enabled     NUMERIC(1)      NOT NULL
   , clos_days        NUMERIC(3)      NOT NULL
   , clos_max_entries NUMERIC(7)      NOT NULL   
   , CONSTRAINT pk_caravan_log_settings PRIMARY KEY (clos_type, capp_id)
   , CONSTRAINT fk_crvnlogsettings_crvnsecapp FOREIGN KEY (capp_id) REFERENCES caravan_sec_app (capp_id) ON DELETE CASCADE ON UPDATE CASCADE
);

, CHECK (clos_type IN ('debug', 'info', 'warn', 'error', 'fatal')) ENABLE
, CHECK (clos_enabled IN (0,1)) ENABLE
, CHECK (clos_days > 0 AND clos_max_entries > 0) ENABLE