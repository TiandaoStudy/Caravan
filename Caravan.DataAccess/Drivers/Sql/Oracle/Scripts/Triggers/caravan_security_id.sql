-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.caravan_security_id
BEFORE INSERT ON mydb.caravan_security 
FOR EACH ROW
BEGIN
  SELECT COALESCE(max(csec_id), -1) + 1
    INTO :new.csec_id
    FROM mydb.caravan_security
   WHERE capp_id = :new.capp_id;
END;