-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_objects_id
BEFORE INSERT ON mydb.crvn_sec_objects 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_objects_id.nextval
    INTO :new.cobj_id
    FROM DUAL;
END;
/