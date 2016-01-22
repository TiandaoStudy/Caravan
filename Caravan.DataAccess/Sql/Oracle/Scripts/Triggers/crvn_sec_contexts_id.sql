-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_contexts_id
BEFORE INSERT ON mydb.crvn_sec_contexts 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_contexts_id.nextval
    INTO :new.cctx_id
    FROM DUAL;
END;
/