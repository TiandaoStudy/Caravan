-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.caravan_log_id
BEFORE INSERT ON mydb.caravan_log 
FOR EACH ROW
BEGIN
  SELECT COALESCE(max(clog_id), -1) + 1
    INTO :new.clog_id
    FROM mydb.caravan_log
   WHERE capp_id = :new.capp_id;
END;