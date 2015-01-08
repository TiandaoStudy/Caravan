-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.caravan_log_id
BEFORE INSERT ON mydb.caravan_log 
FOR EACH ROW
BEGIN
  SELECT mydb.caravan_log_seq.nextval
    INTO :new.clog_id
    FROM dual;
END;