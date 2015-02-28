-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.caravan_sec_app_id
BEFORE INSERT ON mydb.caravan_sec_app 
FOR EACH ROW
BEGIN
  SELECT COALESCE(max(capp_id), -1) + 1
    INTO :new.capp_id
    FROM mydb.caravan_sec_app;
END;