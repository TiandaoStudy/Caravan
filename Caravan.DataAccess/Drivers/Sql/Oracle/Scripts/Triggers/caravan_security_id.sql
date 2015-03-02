-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_entries_id
BEFORE INSERT ON mydb.crvn_sec_entries 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_entries_id.nextval
    INTO :new.csec_id
    FROM DUAL;
END;