-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_log_entries_id
BEFORE INSERT ON mydb.crvn_log_entries 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_log_entries_id.nextval
    INTO :new.clog_id
    FROM DUAL;
END;