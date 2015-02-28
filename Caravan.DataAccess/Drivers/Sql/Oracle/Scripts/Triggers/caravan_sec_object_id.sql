-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.caravan_sec_object_id
BEFORE INSERT ON mydb.caravan_sec_object 
FOR EACH ROW
BEGIN
  SELECT COALESCE(max(cobj_id), -1) + 1
    INTO :new.cobj_id
    FROM mydb.caravan_sec_object
   WHERE capp_id = :new.capp_id
     AND cctx_id = :new.cctx_id;
END;