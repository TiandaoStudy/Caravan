CREATE TABLE [caravan_sec_app] (
  [capp_id] bigint NOT NULL IDENTITY(0, 1)
, [capp_name] nvarchar(300) NOT NULL
, [capp_description] nvarchar(2000) NOT NULL
);
GO
CREATE TABLE [caravan_security] (
  [csec_id] bigint NOT NULL IDENTITY(0, 1)
, [capp_id] bigint NOT NULL
, [cusr_id] bigint NULL
, [cgrp_id] bigint NULL
, [cctx_id] bigint NOT NULL
, [cobj_id] bigint NOT NULL
);
GO
CREATE TABLE [caravan_sec_user] (
  [cusr_id] bigint NOT NULL IDENTITY(0, 1)
, [capp_id] bigint NOT NULL
, [cusr_active] int NOT NULL
, [cusr_login] nvarchar(300) NOT NULL
, [cusr_hashed_pwd] nvarchar(2000) NULL
, [cusr_first_name] nvarchar(2000) NULL
, [cusr_last_name] nvarchar(2000) NULL
, [cusr_email] nvarchar(2000) NULL
);
GO
CREATE TABLE [caravan_sec_user_group] (
  [cusr_id] bigint NOT NULL
, [cusr_app_id] bigint NOT NULL
, [cgrp_id] bigint NOT NULL
, [cgrp_app_id] bigint NOT NULL
);
GO
CREATE TABLE [caravan_sec_group] (
  [cgrp_id] bigint NOT NULL IDENTITY(0, 1)
, [capp_id] bigint NOT NULL
, [cgrp_name] nvarchar(300) NOT NULL
, [cgrp_description] nvarchar(2000) NOT NULL
, [cgrp_admin] int NOT NULL
, [cgrp_notes] nvarchar(2000) NOT NULL
);
GO
CREATE TABLE [caravan_sec_context] (
  [cctx_id] bigint NOT NULL IDENTITY(0, 1)
, [capp_id] bigint NOT NULL
, [cctx_name] nvarchar(300) NOT NULL
, [cctx_description] nvarchar(2000) NOT NULL
);
GO
CREATE TABLE [caravan_sec_object] (
  [cobj_id] bigint NOT NULL IDENTITY(0, 1)
, [cctx_id] bigint NOT NULL
, [capp_id] bigint NOT NULL
, [cobj_name] nvarchar(300) NOT NULL
, [cobj_description] nvarchar(2000) NOT NULL
, [cobj_type] nvarchar(2000) NOT NULL
);
GO
CREATE TABLE [caravan_log_settings] (
  [clos_type] nvarchar(5) NOT NULL
, [capp_id] bigint NOT NULL
, [clos_enabled] int NOT NULL
, [clos_days] int NOT NULL
, [clos_max_entries] int NOT NULL
);
GO
CREATE TABLE [caravan_log] (
  [clog_id] bigint NOT NULL IDENTITY(0, 1)
, [clog_date] datetime DEFAULT GETDATE() NOT NULL
, [clos_type] nvarchar(5) NOT NULL
, [capp_id] bigint NOT NULL
, [clog_user] nvarchar(2000) NULL
, [clog_code_unit] nvarchar(2000) NOT NULL
, [clog_function] nvarchar(2000) NOT NULL
, [clog_short_msg] nvarchar(2000) NOT NULL
, [clog_context] nvarchar(2000) NULL
, [clog_key_0] nvarchar(2000) NULL
, [clog_value_0] nvarchar(2000) NULL
, [clog_key_1] nvarchar(2000) NULL
, [clog_value_1] nvarchar(2000) NULL
, [clog_key_2] nvarchar(2000) NULL
, [clog_value_2] nvarchar(2000) NULL
, [clog_key_3] nvarchar(2000) NULL
, [clog_value_3] nvarchar(2000) NULL
, [clog_key_4] nvarchar(2000) NULL
, [clog_value_4] nvarchar(2000) NULL
, [clog_key_5] nvarchar(2000) NULL
, [clog_value_5] nvarchar(2000) NULL
, [clog_key_6] nvarchar(2000) NULL
, [clog_value_6] nvarchar(2000) NULL
, [clog_key_7] nvarchar(2000) NULL
, [clog_value_7] nvarchar(2000) NULL
, [clog_key_8] nvarchar(2000) NULL
, [clog_value_8] nvarchar(2000) NULL
, [clog_key_9] nvarchar(2000) NULL
, [clog_value_9] nvarchar(2000) NULL
, [clog_long_msg] NVARCHAR(max) NULL
);
GO
ALTER TABLE [caravan_sec_app] ADD CONSTRAINT [pk_caravan_sec_app] PRIMARY KEY ([capp_id]);
GO
ALTER TABLE [caravan_security] ADD CONSTRAINT [pk_caravan_security] PRIMARY KEY ([csec_id],[capp_id]);
GO
ALTER TABLE [caravan_sec_user] ADD CONSTRAINT [pk_caravan_sec_user] PRIMARY KEY ([cusr_id],[capp_id]);
GO
ALTER TABLE [caravan_sec_user_group] ADD CONSTRAINT [pk_caravan_sec_usrgrp] PRIMARY KEY ([cusr_id],[cusr_app_id],[cgrp_id],[cgrp_app_id]);
GO
ALTER TABLE [caravan_sec_group] ADD CONSTRAINT [pk_caravan_sec_group] PRIMARY KEY ([cgrp_id],[capp_id]);
GO
ALTER TABLE [caravan_sec_context] ADD CONSTRAINT [pk_caravan_sec_context] PRIMARY KEY ([cctx_id],[capp_id]);
GO
ALTER TABLE [caravan_sec_object] ADD CONSTRAINT [pk_caravan_sec_object] PRIMARY KEY ([cobj_id],[cctx_id],[capp_id]);
GO
ALTER TABLE [caravan_log_settings] ADD CONSTRAINT [pk_caravan_log_settings] PRIMARY KEY ([clos_type],[capp_id]);
GO
ALTER TABLE [caravan_log] ADD CONSTRAINT [pk_caravan_log] PRIMARY KEY ([clog_id],[capp_id]);
GO
CREATE UNIQUE INDEX [uk_caravan_sec_app] ON [caravan_sec_app] ([capp_name] ASC);
GO
CREATE UNIQUE INDEX [uk_caravan_sec_user] ON [caravan_sec_user] ([cusr_login] ASC,[capp_id] ASC);
GO
CREATE UNIQUE INDEX [uk_caravan_sec_group] ON [caravan_sec_group] ([cgrp_name] ASC,[capp_id] ASC);
GO
CREATE UNIQUE INDEX [uk_caravan_sec_context] ON [caravan_sec_context] ([cctx_name] ASC,[capp_id] ASC);
GO
CREATE UNIQUE INDEX [uk_caravan_sec_object] ON [caravan_sec_object] ([cobj_name] ASC,[cctx_id] ASC);
GO
CREATE INDEX [idx_caravan_log_date] ON [caravan_log] ([clog_date] DESC);
GO
CREATE INDEX [idx_caravan_log_settings] ON [caravan_log] ([clos_type] ASC,[capp_id] ASC);
GO
ALTER TABLE [caravan_security] ADD CONSTRAINT [fk_crvnsecurity_crvnsecapp] FOREIGN KEY ([capp_id]) REFERENCES [caravan_sec_app]([capp_id]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
ALTER TABLE [caravan_sec_user] ADD CONSTRAINT [fk_crvnsecuser_crvnsecapp] FOREIGN KEY ([capp_id]) REFERENCES [caravan_sec_app]([capp_id]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
ALTER TABLE [caravan_sec_user_group] ADD CONSTRAINT [fk_crvnsecusrgrp_crvnsecusr] FOREIGN KEY ([cusr_id], [cusr_app_id]) REFERENCES [caravan_sec_user]([cusr_id], [capp_id]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
ALTER TABLE [caravan_sec_group] ADD CONSTRAINT [fk_crvnsecgroup_crvnsecapp] FOREIGN KEY ([capp_id]) REFERENCES [caravan_sec_app]([capp_id]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
ALTER TABLE [caravan_sec_context] ADD CONSTRAINT [fk_crvnsecctx_crvnsecapp] FOREIGN KEY ([capp_id]) REFERENCES [caravan_sec_app]([capp_id]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
ALTER TABLE [caravan_sec_object] ADD CONSTRAINT [fk_crvnsecobj_crvnsecctx] FOREIGN KEY ([cctx_id], [capp_id]) REFERENCES [caravan_sec_context]([cctx_id], [capp_id]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
ALTER TABLE [caravan_log_settings] ADD CONSTRAINT [fk_crvnlogsettings_crvnsecapp] FOREIGN KEY ([capp_id]) REFERENCES [caravan_sec_app]([capp_id]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO
ALTER TABLE [caravan_log] ADD CONSTRAINT [fk_crvnlog_crvnlogsettings] FOREIGN KEY ([clos_type], [capp_id]) REFERENCES [caravan_log_settings]([clos_type], [capp_id]) ON DELETE CASCADE ON UPDATE NO ACTION;
GO