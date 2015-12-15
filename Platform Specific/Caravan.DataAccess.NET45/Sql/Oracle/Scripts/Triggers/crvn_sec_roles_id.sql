-- REPLACE 'mydb' WITH DB NAME

CREATE OR REPLACE TRIGGER mydb.crvn_sec_roles_id
BEFORE INSERT ON mydb.crvn_sec_roles 
FOR EACH ROW
BEGIN
  SELECT mydb.crvn_sec_roles_id.nextval
    INTO :new.crol_id
    FROM DUAL;
END;
/