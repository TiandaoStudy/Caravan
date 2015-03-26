-- REPLACE 'src' WITH MASTER DB NAME
-- REPLACE 'dst' WITH USER DB NAME

-- Security
grant select, references on src.crvn_sec_apps to dst;
grant select, references on src.crvn_sec_groups to dst;
grant select, references on src.crvn_sec_users to dst;

-- Log
grant select on src.crvn_log_settings to dst;
grant select, insert on src.crvn_log_entries to dst;