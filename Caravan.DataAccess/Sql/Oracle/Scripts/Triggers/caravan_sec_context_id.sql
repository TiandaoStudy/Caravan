-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.caravan_sec_context_id
BEFORE INSERT ON mydb.caravan_sec_context 
FOR EACH ROW
BEGIN
  SELECT COALESCE(max(cctx_id), -1) + 1
    INTO :new.cctx_id
    FROM mydb.caravan_sec_context
   WHERE capp_id = :new.capp_id;
END;