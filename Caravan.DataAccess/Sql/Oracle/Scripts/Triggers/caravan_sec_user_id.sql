-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.caravan_sec_user_id
BEFORE INSERT ON mydb.caravan_sec_user 
FOR EACH ROW
BEGIN
  SELECT COALESCE(max(cusr_id), -1) + 1
    INTO :new.cusr_id
    FROM mydb.caravan_sec_user
   WHERE capp_id = :new.capp_id;
END;