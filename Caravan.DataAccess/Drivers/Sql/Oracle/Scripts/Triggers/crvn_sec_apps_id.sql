-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_apps_id
BEFORE INSERT ON mydb.crvn_sec_apps 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_apps_id.nextval
    INTO :new.capp_id
    FROM DUAL;
END;