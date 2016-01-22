CREATE TABLE [crvn_log_settings] (
  [clos_type] nvarchar(8) NOT NULL CHECK (clos_type IN ('debug', 'trace', 'info', 'warn', 'error', 'fatal'))
, [capp_id] int NOT NULL
, [clos_enabled] bit NOT NULL CHECK (clos_enabled IN (0, 1))
, [clos_days] smallint NOT NULL
, [clos_max_entries] int NOT NULL
,CONSTRAINT chk_clos_days_clos_max_entries CHECK (clos_days > 0 AND clos_max_entries > 0)
);
GO

ALTER TABLE [crvn_log_settings] ADD CONSTRAINT [pk_crvn_log_settings] PRIMARY KEY ([capp_id],[clos_type]);
GO

ALTER TABLE [crvn_log_settings] ADD CONSTRAINT [fk_crvnlogsettings_crvnsecapps] FOREIGN KEY ([capp_id]) REFERENCES [crvn_sec_apps]([capp_id]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- REPLACE '<capp_id>' WITH CAPP_ID
INSERT INTO [crvn_log_settings] ([clos_type],[capp_id],[clos_enabled],[clos_days],[clos_max_entries])
     VALUES ('trace', <capp_id>, 1, 30, 10000)
GO
INSERT INTO [crvn_log_settings] ([clos_type],[capp_id],[clos_enabled],[clos_days],[clos_max_entries])
     VALUES ('debug', <capp_id>, 1, 30, 1000)
GO
INSERT INTO [crvn_log_settings] ([clos_type],[capp_id],[clos_enabled],[clos_days],[clos_max_entries])
     VALUES ('info', <capp_id>, 1, 30, 1000)
GO
INSERT INTO [crvn_log_settings] ([clos_type],[capp_id],[clos_enabled],[clos_days],[clos_max_entries])
     VALUES ('warn', <capp_id>, 1, 30, 1000)
GO
INSERT INTO [crvn_log_settings] ([clos_type],[capp_id],[clos_enabled],[clos_days],[clos_max_entries])
     VALUES ('error', <capp_id>, 1, 30, 1000)
GO
INSERT INTO [crvn_log_settings] ([clos_type],[capp_id],[clos_enabled],[clos_days],[clos_max_entries])
     VALUES ('fatal', <capp_id>, 1, 30, 1000)
GO
