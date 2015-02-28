-- REPLACE 'src' WITH MASTER DB NAME
-- REPLACE 'dst' WITH USER DB NAME

-- Security
grant select on src.caravan_sec_app to dst;

-- Log
grant select on src.caravan_log_settings to dst;
grant select, insert on src.caravan_log to dst;