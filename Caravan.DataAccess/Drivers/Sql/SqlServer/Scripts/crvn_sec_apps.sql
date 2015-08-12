CREATE TABLE [crvn_sec_apps] (
  [capp_id] int NOT NULL IDENTITY(0, 1)
, [capp_name] nvarchar(32) NOT NULL CHECK (capp_name = lower(capp_name))
, [capp_descr] nvarchar(256) NOT NULL
);
GO
ALTER TABLE [crvn_sec_apps] ADD CONSTRAINT [pk_crvn_sec_apps] PRIMARY KEY ([capp_id]);
GO
CREATE UNIQUE INDEX [uk_crvn_sec_apps] ON [crvn_sec_apps] ([capp_name] ASC);