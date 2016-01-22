CREATE TABLE [crvn_log_entries] (
  [clog_id] bigint NOT NULL IDENTITY(0, 1)
, [clog_date] datetime DEFAULT GETDATE() NOT NULL
, [clos_type] nvarchar(8) NOT NULL
, [capp_id] int NOT NULL
, [cusr_login] nvarchar(32) NULL CHECK (cusr_login is null or cusr_login = lower(cusr_login))
, [clog_code_unit] nvarchar(256) NULL CHECK (clog_code_unit = lower(clog_code_unit))
, [clog_function] nvarchar(256) NULL CHECK (clog_function = lower(clog_function))
, [clog_short_msg] nvarchar(1024) NOT NULL
, [clog_long_msg] NVARCHAR(max) NULL
, [clog_context] nvarchar(256) NULL
, [clog_key_0] nvarchar(32) NULL CHECK (clog_key_0 is null or clog_key_0 = lower(clog_key_0))
, [clog_value_0] nvarchar(1024) NULL 
, [clog_key_1] nvarchar(32) NULL CHECK (clog_key_1 is null or clog_key_1 = lower(clog_key_1))
, [clog_value_1] nvarchar(1024) NULL
, [clog_key_2] nvarchar(32) NULL CHECK (clog_key_2 is null or clog_key_2 = lower(clog_key_2))
, [clog_value_2] nvarchar(1024) NULL
, [clog_key_3] nvarchar(32) NULL CHECK (clog_key_3 is null or clog_key_3 = lower(clog_key_3))
, [clog_value_3] nvarchar(1024) NULL
, [clog_key_4] nvarchar(32) NULL CHECK (clog_key_4 is null or clog_key_4 = lower(clog_key_4))
, [clog_value_4] nvarchar(1024) NULL
, [clog_key_5] nvarchar(32) NULL CHECK (clog_key_5 is null or clog_key_5 = lower(clog_key_5))
, [clog_value_5] nvarchar(1024) NULL
, [clog_key_6] nvarchar(32) NULL CHECK (clog_key_6 is null or clog_key_6 = lower(clog_key_6))
, [clog_value_6] nvarchar(1024) NULL
, [clog_key_7] nvarchar(32) NULL CHECK (clog_key_7 is null or clog_key_7 = lower(clog_key_7))
, [clog_value_7] nvarchar(1024) NULL
, [clog_key_8] nvarchar(32) NULL CHECK (clog_key_8 is null or clog_key_8 = lower(clog_key_8))
, [clog_value_8] nvarchar(1024) NULL
, [clog_key_9] nvarchar(32) NULL CHECK (clog_key_9 is null or clog_key_9 = lower(clog_key_9))
, [clog_value_9] nvarchar(1024) NULL
);
GO
ALTER TABLE [crvn_log_entries] ADD CONSTRAINT [pk_crvn_log_entries] PRIMARY KEY ([clog_id]);
GO
CREATE INDEX [ix_crvn_log_date] ON [crvn_log_entries] ([clog_date] DESC);
GO
CREATE INDEX [ix_crvn_log_settings] ON [crvn_log_entries] ([capp_id] ASC, [clos_type] ASC);
GO
ALTER TABLE [crvn_log_entries] ADD CONSTRAINT [fk_crvnlog_crvnlogsettings] FOREIGN KEY ( [capp_id],[clos_type]) REFERENCES [crvn_log_settings]([capp_id],[clos_type]) ON DELETE CASCADE ON UPDATE NO ACTION;